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

namespace Test_OP_Web.Controllers
{

    [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Option(int id)
        {

            var options = await _context.Options.Include(x => x.Questions).ThenInclude(x => x.Anwsers).FirstOrDefaultAsync(x => x.Id == id);

            if (options == null)
                return View("NotFound");

            return View(options);

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
        public async Task<string> EditQuestion(Question question)
        {

            var q = await _context.Questions.Include(x => x.Anwsers).FirstOrDefaultAsync(x => x.Id == question.Id);

            if (q == null)
                return "error";

            q.QuestionString = question.QuestionString;

            for (int i = 0; i < q.Anwsers.Count; i++)
            {
                var enterQ = question.Anwsers.FirstOrDefault(x => x.Id == q.Anwsers[i].Id);
                q.Anwsers[i].Right = enterQ.Right;
                q.Anwsers[i].Text = enterQ.Text;
            }


            await _context.SaveChangesAsync();

            return "ok";
        }


    

    }
}
