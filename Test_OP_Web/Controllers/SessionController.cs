﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Test_OP_Web.Data;
using Test_OP_Web.Data.Options;
using Test_OP_Web.Migrations;
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
                .Include(x => x.SessionQuestions).ThenInclude(x => x.Enter)

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
        public IActionResult CreateSession()
        {


            var options = _context.Options.OrderBy(x => x.NumVar).ToList();
            var numVars = new List<int>();
            foreach (var option in options)
                numVars.Add(option.NumVar);

            ViewBag.NumVars = numVars;
            return View();
        }

        [HttpPost]
        public IActionResult CreateSession(CreateSessionModel createSessionModel)
        {

            UserAxe UserAxe = _userManager.GetUserAsync(HttpContext.User).Result;
            if (!ModelState.IsValid)
                return View(createSessionModel);

            Option option = _context.Options.Include(x => x.Questions)
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



            foreach (var item in option.Questions.OrderBy(x => x.NumQ))
            {
                SessionQuestion sessionQuestion = new()
                {
                    Question = item
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
                //await Task.Run(() =>
                //{
                {
                    Stopwatch stopwatch = new Stopwatch();
                    Stopwatch stopwatch2 = new Stopwatch();
                    stopwatch.Start();
                    stopwatch2.Start();
                    //UserAxe UserAxe = _userManager.GetUserAsync(HttpContext.User).Result;
                    //Session ses;




                    //if (_userManager.GetRolesAsync(UserAxe).Result.Contains("admin"))
                    //    ses = _context.GetSessionById(SessionId);
                    //else
                    //    ses = _context.GetSessionById(SessionId, UserAxe);

                    //if (ses == null)
                    //    throw new Exception("NoSession");


                    //if (ses.Сompleted)
                    //    throw new Exception($"Session allready comleted\nSessionId {SessionId}, NumQ = {NumQ}, Text = {Text}");


                    //var question = ses.SessionQuestions.FirstOrDefault(x => x.Question.NumQ == NumQ);
                    //if (question == null)
                    //    throw new Exception("NoQuestion");

                    //Проверка на вопрос без вариантов ответа

                    var question = await _context.SessionQuestions.Include(x => x.Question.Anwsers)
                        .Where(x => x.Question.NumQ == NumQ && x.SessionId == SessionId)
                         .Select(x => new
                         {
                             Question = new
                             {
                                 // Выберите только нужные поля из связанной сущности Question
                                 x.Question.Id,                                 
                                 x.Question.NoVariant,
                                 

                                 // ... добавьте остальные нужные поля Question
                                 Anwsers = x.Question.Anwsers.Select(a => new
                                 {
                                     // Выберите только нужные поля из связанных сущностей Anwsers
                                     a.Id,
                                     a.Text,
                                     // ... добавьте остальные нужные поля Anwsers
                                 }).ToList(),
                                 
                                 Enter = x.Enter.Select(a => new
                                 {
                                     // Выберите только нужные поля из связанных сущностей Anwsers
                                     a.Id,
                                     a.Text,
                                     // ... добавьте остальные нужные поля Anwsers
                                 }).ToList()
                             }
                         })
                         .FirstOrDefaultAsync();

                    //.FirstOrDefaultAsync(x =>
                    //     x.Question.NumQ == NumQ &&
                    //     x.SessionId == SessionId);

                    stopwatch2.Stop();
                    if (question == null)
                        throw new Exception("Question are not exist");

                    
                    if (question.Question.NoVariant)
                    {
                        question.Question.Enter.Clear();
                        question.Question.Enter.Add(new { id = 12,Text = Text });
                    }
                    else
                    {
                        int anwserId = int.TryParse(AnswerId, out int parsedResult) ? parsedResult : throw new Exception("Ошибка приведения.");

                        var anwser = question.Question.Anwsers.FirstOrDefault(x => x.Id == anwserId)
                            ?? throw new Exception("NoAnwser");


                        if (question.Question.Enter.Any(x => x.Id == anwser.Id))
                            question.Question.Enter.Remove(anwser);
                        else
                            question.Question.Enter.Add(anwser);

                        stopwatch.Stop();
                        _logger.LogInformation($"DB SELECT: {stopwatch2.ElapsedMilliseconds} ms\n" +
                                               $"\tALLTIME: {stopwatch.ElapsedMilliseconds} ms");

                        //_logger.LogInformation($"{UserAxe.Email} send questionID {question.Id}");

                    }

                }
                //});
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return exc.Message;
            }

            await _context.SaveChangesAsync();

            return "ok";
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
