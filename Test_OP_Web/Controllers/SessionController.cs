using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_OP_Web.Data;
using Test_OP_Web.Data.Options;
using Test_OP_Web.Models;
using Test_OP_Web.Services;

namespace Test_OP_Web.Controllers
{
    [Authorize]
    public class SessionController : Controller
    {
        private readonly ILogger<SessionController> _logger;
        private readonly OptionContext _context;
        private readonly UserManager<UserAxe> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly StatisticsService _statisticsService;
        //UserAxe UserAxe { get; set; }



        public SessionController(ILogger<SessionController> logger,
            OptionContext context,
            UserManager<UserAxe> userManager,
            RoleManager<IdentityRole> roleManager,
            StatisticsService statisticsService)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _statisticsService = statisticsService;

        }

        public async Task<IActionResult> Index()
        {

            var UserAxe = _userManager.GetUserAsync(HttpContext.User).Result;
            var ses = await _context.Sessions.Where(x => x.UserAxe == UserAxe)
                .Include(x => x.SessionQuestions)
                    .ThenInclude(x => x.Question)
                        .ThenInclude(x => x.Anwsers)
                .ToListAsync();


            return View(ses);
        }

        public IActionResult Session(Session session)
        {
            UserAxe UserAxe = _userManager.GetUserAsync(HttpContext.User).Result;

            Session ses;
            if (_userManager.GetRolesAsync(UserAxe).Result.Contains("admin"))
                ses = _context.GetSessionById(session.Id);
            else
                ses = _context.GetSessionById(session.Id, UserAxe);


            if (ses == null)
                return View("NoSession");

            if (ses.Сompleted)
                return View("Details", ses);


            return View(ses);
        }


        public IActionResult Details(Session session)
        {

            UserAxe UserAxe = _userManager.GetUserAsync(HttpContext.User).Result;

            Session ses;
            ses = _userManager.GetRolesAsync(UserAxe).Result.Contains("admin") ? _context.GetSessionById(session.Id) : _context.GetSessionById(session.Id, UserAxe);

            if (ses == null)
                return View("NoSession");


            if (!ses.Сompleted)
                return View("NoSession");


            return View(ses);
        }


        [HttpGet]
        public async Task<IActionResult> CreateSession()
        {
            var UserAxe = _userManager.GetUserAsync(HttpContext.User).Result;
            var sessions = await _context.Sessions.Where(x => x.UserAxe == UserAxe)
                .Include(x => x.SessionQuestions)
                    .ThenInclude(x => x.Question)
                        .ThenInclude(x => x.Anwsers)
                .ToListAsync();

            var options = _context.Options.OrderBy(x => x.NumVar).ToList();
            var numVars = new List<int>();
            foreach (var option in options)
                numVars.Add(option.NumVar);

            ViewBag.NumVars = numVars;
            ViewBag.Sessions = sessions;
            return View();
        }

        [HttpPost]
        public IActionResult CreateSession(CreateSessionModel createSessionModel)
        {

            UserAxe UserAxe = _userManager.GetUserAsync(HttpContext.User).Result;
            if (!ModelState.IsValid)
                return View(createSessionModel);

            Option option = _context.Options.Include(x => x.Questions)
                .ThenInclude(q => q.Anwsers)
                .FirstOrDefault(x => x.NumVar == createSessionModel.NumVar);


            if (option == null)
                return View("NoOption");

            Session session = new Session()
            {
                TimeStart = DateTime.Now,
                Name = createSessionModel.Name,
                NumVar = option.NumVar,
                Сompleted = false,
                UserAxe = UserAxe
            };


            foreach (var question in option.Questions.OrderBy(x => x.NumQ))
            {
                SessionQuestion sessionQuestion = new()
                {
                    Question = question.ToCopyQuestion(),
                };
                session.SessionQuestions.Add(sessionQuestion);
            }

            _context.Sessions.Add(session);
            _context.SaveChanges();

            _logger.LogInformation($"{UserAxe.Email} {session.Name} created session {session.NumVar}");

            return RedirectToAction("Session", "Session", new { Id = session.Id });
        }

        [HttpGet]
        public IActionResult Question(GetQusetionModel qusetionModel)
        {

            UserAxe UserAxe = _userManager.GetUserAsync(HttpContext.User).Result;
            Session ses;
            if (_userManager.GetRolesAsync(UserAxe).Result.Contains("admin"))
                ses = _context.GetSessionById(qusetionModel.SessionId);
            else
                ses = _context.GetSessionById(qusetionModel.SessionId, UserAxe);




            if (ses == null)
                return View("NoSession");

            if (ses.Сompleted)
                return RedirectToAction("Details", "Session", new { Id = qusetionModel.SessionId });



            var question = ses.SessionQuestions.FirstOrDefault(x => x.Question.NumQ == qusetionModel.NumQ);
            if (question == null)
                return View("NoQuestion");

            qusetionModel.Question = question;


            ViewData["CountQuestions"] = ses.SessionQuestions.Count;

            return View("Question", question);

        }
        [HttpPost]
        [Authorize]
        public async Task<string> Question(string AnswerId, int SessionId, int NumQ, string Text)
        {
            try
            {
                await Task.Run(async () =>
                {
                    {
                        //Проверка на вопрос без вариантов ответа

                        var question = await _context.SessionQuestions.Include(x => x.Question.Anwsers)
                                       .Include(e => e.Question)
                                    .FirstOrDefaultAsync(x =>
                             x.Question.NumQ == NumQ &&
                             x.SessionId == SessionId);


                        if (question == null)
                            throw new Exception("Question are not exist");

                        if (question.Blocked)
                            throw new Exception("blocked");


                        if (question.Question.NoVariant)
                        {
                            // 1 элемент списка - это верный ответ
                            // если кол-во элементов равно 1 значит user ответ не вводил
                            // введенный ответ - это последний ответ пользователя
                            if (question.Question.Anwsers.Count > 1)                            {
                                // Удалаяем второй элемент // тоесть последний
                                question.Question.Anwsers.RemoveRange(1, 1);
                            }
                            
                            question.Question.Anwsers.Add(new SessionAnwser() { Text = Text });
                        }
                        else
                        {
                            int anwserId = int.TryParse(AnswerId, out int parsedResult) ? parsedResult : throw new Exception("Ошибка приведения.");

                            var anwser = question.Question.Anwsers.FirstOrDefault(x => x.Id == anwserId)
                                ?? throw new Exception("NoAnwser");

                            // Меняем на противополоэное
                            anwser.Enter = !anwser.Enter;
                            //if (question.Enter.Any(x => x.Text == anwser.Text))
                            //    question.Enter.Remove(anwser.ToSessionAnwser());
                            //else
                            //    question.Enter.Add(anwser.ToSessionAnwser());


                        }

                    }
                });
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return exc.Message;
            }

            await _context.SaveChangesAsync();

            return "ok";
        }

        public async Task<IActionResult> ShowAnwser(int SessionId, int NumQ)
        {
            try
            {
                var question = await _context.SessionQuestions
                                    .Include(x => x.Question)
                                    .ThenInclude(x => x.Anwsers)
                                    .FirstOrDefaultAsync(x =>
                                    x.Question.NumQ == NumQ &&
                                    x.SessionId == SessionId) ?? throw new Exception("NoQuestion");

                question.Blocked = true;
                await _context.SaveChangesAsync();


                return View(question);
            }
            catch (Exception exc)
            {
                _logger.LogWarning(exc.Message);
                return NotFound();
            }

        }


        public IActionResult Complete(int SessionId)
        {
            try
            {
                UserAxe UserAxe = _userManager.GetUserAsync(HttpContext.User).Result;

                Session ses;
                if (_userManager.GetRolesAsync(UserAxe).Result.Contains("admin"))
                    ses = _context.GetSessionById(SessionId);
                else
                    ses = _context.GetSessionById(SessionId, UserAxe);

                if (ses == null)
                    return View("NoSession");

                ses.Сompleted = true;
                ses.TimeFinsih = DateTime.Now;
                //ses.GetRight = ses.getRight();


                _context.SaveChanges();

                _logger.LogInformation($"{UserAxe.Email} complete sessiod {SessionId}");
                return RedirectToAction("Details", "Session", new { Id = SessionId });
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                throw exc;
            }



        }



        [HttpGet]
        public async Task<IActionResult> Stat()
        {
            UserAxe UserAxe = _userManager.GetUserAsync(HttpContext.User).Result;
            var stat = await _statisticsService.PersonStat(UserAxe.Email);


            var stats = await _statisticsService.PersonsStat();
            return View(stats);
        }
    }

}
