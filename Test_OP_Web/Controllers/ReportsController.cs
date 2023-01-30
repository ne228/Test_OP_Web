using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Test_OP_Web.Data;
using Test_OP_Web.Data.Options;

namespace Test_OP_Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ILogger<ReportsController> _logger;
        private readonly OptionContext _context;
        private readonly UserManager<UserAxe> _userManager;
        //UserAxe UserAxe { get; set; }

        public ReportsController(ILogger<ReportsController> logger, OptionContext context, UserManager<UserAxe> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        // GET: Reports
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {

            var reports = await _context.Reports
                .Where(x => x.Confirm == false).Include(x => x.Question)
                .ToListAsync();
            return View(reports);
        }

        // GET: Reports/Details/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Reports/Create //https://localhost:5001/reports/create?questionId=1
        public IActionResult Create(int questionId)
        {
            UserAxe UserAxe = _userManager.GetUserAsync(HttpContext.User).Result;

            var question = _context.Questions.Include(x => x.Anwsers).FirstOrDefault(x => x.Id == questionId);

            if (question == null)
                return NotFound();


            var report = new Report()
            {
                Question = question
            };

            return View(report);
        }

        // POST: Reports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public async Task<IActionResult> Create(Report model)
        {
            if (ModelState.IsValid)
            {

                var report = new Report();
                //.Options.Include(x => x.Questions)
                UserAxe UserAxe = _userManager.GetUserAsync(HttpContext.User).Result;

                var question = _context.Questions
                    .Include(x => x.Anwsers)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == model.Question.Id);

                if (question == null)
                    return NotFound();


                report.QuestionId = question.Id;
                report.UserAxe = UserAxe;
                report.DateTime = DateTime.Now;
                report.Message = model.Message;


                _context.Reports.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ThanksReport));
            }
            return View(model);
        }

        public async Task<IActionResult> Confirm(int id)
        {

            var report = await _context.Reports.FirstOrDefaultAsync(x => x.Id == id);
            if (report == null)
                return NotFound();

            report.Confirm = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ThanksReport()
        {

            return View();
        }



        [Authorize(Roles = "admin")]
        // GET: Reports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Message,DateTime")] Report report)
        {
            if (id != report.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(report);
        }


        [Authorize(Roles = "admin")]
        // GET: Reports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Reports/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool ReportExists(int id)
        {
            return _context.Reports.Any(e => e.Id == id);
        }
    }
}
