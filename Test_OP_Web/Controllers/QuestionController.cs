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
using Test_OP_Web.Data.OptionTemplate;
using Test_OP_Web.Models;

namespace Test_OP_Web.Controllers
{

    public class QuestionController : Controller
    {
        private readonly OptionContext _optionContext;
        private readonly ILogger<QuestionController> _logger;

        public QuestionController(OptionContext optionContext, ILogger<QuestionController> logger)
        {
            _optionContext = optionContext;
            _logger = logger;
        }

        // GET: Question/Index
        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            var questions = _optionContext.QuestionTemplates
                             .Include(x => x.Anwsers)
                             .ToList();
            return View(questions);
        }


        // GET: Question/Create
        public IActionResult Create()
        {
            return View(new CreateQuestionModel());
        }

        // POST: Question/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateQuestionModel model)
        {
            if (ModelState.IsValid)
            {
                var question = model.toQuestion();

                _logger.LogInformation(question.ToString());
                await _optionContext.QuestionCreate(question);
                return Redirect("/Question/Index");
            }

            return View(model);
        }

        [HttpGet("Question/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using (var transaction = _optionContext.Database.BeginTransaction())
            {
                try
                {
                    // Удаляем зависимые записи
                    var answers = _optionContext.AnwserTemplates.Where(a => a.question.Id == id).ToList();
                    _optionContext.AnwserTemplates.RemoveRange(answers);

                    // Удаляем запись из QuestionTemplates
                    var question = _optionContext.QuestionTemplates.Find(id);
                    if (question == null) return NotFound();

                    _optionContext.QuestionTemplates.Remove(question);

                    // Сохраняем изменения
                    _optionContext.SaveChanges();
                    transaction.Commit();
                    TempData["Message"] = "Question and its related answers deleted successfully.";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    TempData["Error"] = "An error occurred: " + ex.Message;
                }
            }

            return RedirectToAction("Index");
        }

        // GET: Question/Edit/{id}
        [HttpGet("Question/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var question = _optionContext.QuestionTemplates
                .Include(q => q.Anwsers)
                .Where(q => q.Id == id)
                .FirstOrDefault();

            if (question == null)
                return NotFound();

            return View(question);
        }

        // GET: Question/Edit/{id}
        [HttpPost]
        public IActionResult Edit(QuestionTemplate questionTemplate)
        {
            var question = _optionContext.QuestionTemplates
                .Include(q => q.Anwsers)
                .Where(q => q.Id == questionTemplate.Id)
                .FirstOrDefault();

            if (question == null)
                return NotFound();

            question.QuestionString = questionTemplate.QuestionString;
            for (int i = 0; i < questionTemplate.Anwsers.Count; i++)
            {
                AnwserTemplate anws = questionTemplate.Anwsers[i];
                question.Anwsers[i].Text = anws.Text;
                question.Anwsers[i].Right = anws.Right;
            }

            _optionContext.SaveChanges();   

            return View(question);
        }

    }
}

