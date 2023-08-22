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

namespace Test_OP_Web.Controllers
{

    
    public class OptionController : Controller
    {

        private readonly ILogger<OptionController> _logger;
        private readonly OptionContext _context;
        private readonly UserManager<UserAxe> _userManager;
        //UserAxe UserAxe { get; set; }

        public OptionController(ILogger<OptionController> logger, OptionContext context, UserManager<UserAxe> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            var options = await _context.Options.OrderBy(x => x.NumVar).ToListAsync();

            if (options == null)
                return View();

            return View(options);
        }


        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Create()
        {
            Option option = new Option { Questions = new() { } };
            for (int i = 0; i < 20; i++)
            {

                var question = new Question() { Anwsers = new() };
                for (int j = 0; j < 6; j++)
                    question.Anwsers.Add(new Anwser());


                option.Questions.Add(question);
            }

            return View(option);

        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Create(Option option)
        {
            for (int i = 0; i < option.Questions.Count; i++)
            {
                Question question = option.Questions[i];
                if (question.QuestionString == null || question.QuestionString == "")
                    ModelState.AddModelError($"Questions[{i}].QuestionString", "Question must be not empty");



                if (question.Anwsers.Where(x => x.Text != null).ToList().Count == 0)
                    ModelState.AddModelError($"Questions[{i}].QuestionString", "Anwsers must be more then 0 count");

                if (ModelState.IsValid)
                    question.Anwsers = question.Anwsers.Where(x => x.Text != null).ToList();

                question.NumVar = option.NumVar;
                question.NumQ = i + 1;


                if (question.Anwsers.Count == 1)
                    question.NoVariant = true;
            }

            if (ModelState.IsValid)
            {

                _context.Add(option);
                _context.SaveChanges();

                RedirectToAction(nameof(ThanksReport));
            }
            return View(option);
        }

        public IActionResult ThanksReport()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Option(int id)
        {

            var option = await _context.Options.Include(x => x.Questions).ThenInclude(x => x.Anwsers).FirstOrDefaultAsync(x => x.Id == id);

            if (option == null)
                return View("NotFound");

            return View(option);

        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {

            var option = await _context.Options.Include(x => x.Questions).ThenInclude(x => x.Anwsers).FirstOrDefaultAsync(x => x.Id == id);

            if (option == null)
                return View("NotFound");

            _context.Remove(option);
            var res = _context.SaveChanges();



            return RedirectToAction(nameof(Index));

        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> EditQuestion(int questionId)
        {
            var question = await _context.Questions.Include(x => x.Anwsers).FirstOrDefaultAsync(x => x.Id == questionId);

            if (question == null)
                return View("NotFound");


            ViewData["jsneed"] = "true";
            return View(question);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditQuestion(Question question)
        {

            var q = await _context.Questions.Include(x => x.Anwsers).FirstOrDefaultAsync(x => x.Id == question.Id);

            if (q == null)
                return View("Error","ошибка");

            q.QuestionString = question.QuestionString;

            for (int i = 0; i < q.Anwsers.Count; i++)
            {
                var enterQ = question.Anwsers.FirstOrDefault(x => x.Id == q.Anwsers[i].Id);
                q.Anwsers[i].Right = enterQ.Right;
                q.Anwsers[i].Text = enterQ.Text;
            }


            await _context.SaveChangesAsync();



            return RedirectToAction("Index", "Reports");
        }

    }
}
