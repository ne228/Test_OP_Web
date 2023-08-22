using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Test_OP_Web.Models;
using Test_OP_Web.Services;

namespace Test_OP_Web.Data.Options
{
    public class OptionContext : IdentityDbContext<UserAxe>
    {
        public DbSet<Option> Options { get; set; }
        public DbSet<Question> Questions { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<SessionQuestion> SessionQuestions { get; set; }
        public DbSet<PersonStat> PersonStat { get; set; }
        public DbSet<Report> Reports { get; set; }

        public OptionContext(DbContextOptions<OptionContext> options) : base(options)
        {


        }



        public Session GetSessionById(int Id, UserAxe userAxe)
        {

            var ses = Sessions
                .Where(x => x.UserAxe == userAxe)
                .Include(x => x.SessionQuestions).ThenInclude(x => x.Enter)
                .Include(x => x.SessionQuestions).ThenInclude(x => x.Question.Anwsers)
                //.AsSplitQuery()
                .FirstOrDefault(x => x.Id == Id);

            if (ses == null)
                return null;

            return ses;
        }
        public Session GetSessionById(int Id)
        {
            var ses = Sessions
                .Include(x => x.SessionQuestions).ThenInclude(x => x.Enter)
                .Include(x => x.SessionQuestions).ThenInclude(x => x.Question.Anwsers)
                //.AsSplitQuery()
                .FirstOrDefault(x => x.Id == Id);


            if (ses == null)
                return null;

            return ses;
        }


        public async Task<List<Session>> GetSessionsByNumVar(int NumVar)
        {


            var ses = await Sessions
                .Where(x => x.NumVar == NumVar)
                .Include(x => x.SessionQuestions).ThenInclude(x => x.Question.Anwsers)
                .Include(x => x.SessionQuestions).ThenInclude(x => x.Enter)

                .ToListAsync();


            if (ses == null)
                return null;

            return ses;
        }
        public async Task<List<Session>> GetSessions()
        {


            var ses = await Sessions
                .Include(x => x.SessionQuestions).ThenInclude(x => x.Question.Anwsers)
                .Include(x => x.SessionQuestions).ThenInclude(x => x.Enter)

                .ToListAsync();


            if (ses == null)
                return null;

            return ses;
        }

        public int GetNextQuestionId(int Id, int SessionId)
        {
            var ses = Sessions.Include(x => x.SessionQuestions).ThenInclude(x => x.Question).FirstOrDefault(x => x.Id == SessionId);

            if (ses == null)
                return 0;
            var q = ses.SessionQuestions.FirstOrDefault(x => x.Id == Id);

            if (q == null)
                return 0;

            var res = ses.SessionQuestions.FirstOrDefault(x => x.Question.NumQ == q.Question.NumQ + 1);

            if (res == null)
                return 0;

            return res.Id;

        }

        public List<Session> GetSessionsByFilter(FilterModel filterModel)
        {

            if (filterModel.Name == null)
                filterModel.Name = "";


            var sessions =
                Sessions.Include(x => x.SessionQuestions).ThenInclude(x => x.Question.Anwsers)
                .Include(x => x.SessionQuestions).ThenInclude(x => x.Enter)

                .Where(
                x => x.Name.Contains(filterModel.Name) &&
                filterModel.DateTimeStart <= x.TimeStart && x.TimeStart <= filterModel.DateTimeFinish &&
                filterModel.NumVStart <= x.NumVar && x.NumVar <= filterModel.NumVFinish
                );

            return sessions.ToList();
        }

    }
}
