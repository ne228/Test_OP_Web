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
using Test_OP_Web.Models;

namespace Test_OP_Web.Controllers
{
    [Authorize]
    public class SessionController : Controller
    {
        private readonly ILogger<SessionController> _logger;
        private readonly OptionContext _context;
        private UserManager<UserAxe> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        //UserAxe UserAxe { get; set; }



        public SessionController(ILogger<SessionController> logger,
            OptionContext context,
            UserManager<UserAxe> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;

        }



        public IActionResult Index()
        {

            UserAxe UserAxe = _userManager.GetUserAsync(HttpContext.User).Result;
            var ses = _context.Sessions.Where(x => x.UserAxe == UserAxe).Include(x => x.SessionQuestions).ThenInclude(x => x.Question).ToList();

            return View(ses);
        }

        [Authorize]
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
            if (_userManager.GetRolesAsync(UserAxe).Result.Contains("admin"))
                ses = _context.GetSessionById(session.Id);
            else
                ses = _context.GetSessionById(session.Id, UserAxe);

            if (ses == null)
                return View("NoSession");


            if (!ses.Сompleted)
                return View("NoSession");


            return View(ses);
        }


        [HttpGet]
        public IActionResult CreateSession()
        {
            return View();
        }



        [HttpPost]
        public IActionResult CreateSession(CreateSessionModel createSessionModel)
        {

            UserAxe UserAxe = _userManager.GetUserAsync(HttpContext.User).Result;
            if (!ModelState.IsValid)
                return View(createSessionModel);

            Option option = _context.Options.Include(x => x.Questions).FirstOrDefault(x => x.NumVar == createSessionModel.NumVar);




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



            foreach (var item in option.Questions.OrderBy(x => x.NumQ))
            {
                SessionQuestion sessionQuestion = new()
                {
                    Question = item,
                    Enter = 0,
                    NumQ = item.NumQ
                };

                session.SessionQuestions.Add(sessionQuestion);
            }



            _context.Sessions.Add(session);
            _context.SaveChanges();



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



            var question = ses.SessionQuestions.FirstOrDefault(x => x.NumQ == qusetionModel.NumQ);
            if (question == null)
                return View("NoQuestion");





            qusetionModel.Question = question;

            return View("Question", question);

        }
        [HttpPost]
        public string Question(Question enterQuestion, int SessionId, int NumQ)
        {

            UserAxe UserAxe = _userManager.GetUserAsync(HttpContext.User).Result;
            Session ses;
            if (_userManager.GetRolesAsync(UserAxe).Result.Contains("admin"))
                ses = _context.GetSessionById(SessionId);
            else
                ses = _context.GetSessionById(SessionId, UserAxe);

            if (ses == null)
                return "NoSession";


            if (ses.Сompleted)
                return "Session allready comleted";


            var question = ses.SessionQuestions.FirstOrDefault(x => x.NumQ == NumQ);
            if (question == null)
                return "NoQuestion";


            question.Enter = enterQuestion.Enter;

            _context.SaveChanges();

            question = ses.SessionQuestions.FirstOrDefault(x => x.NumQ == NumQ);



            return "ok";
            //return RedirectToAction("Question", "Session", new { SessionId = SessionId, NumQ = NumQ });
            //return View(question);

        }



        public IActionResult Complete(int SessionId)
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

            _context.SaveChanges();

            return RedirectToAction("Details", "Session", new { Id = SessionId });

        }
    }


}
