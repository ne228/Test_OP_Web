using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_OP_Web.Data;
using Test_OP_Web.Models;

namespace Test_OP_Web
{
    public class OptionContext : IdentityDbContext<UserAxe>
    {


        public OptionContext(DbContextOptions<OptionContext> options) : base(options)
        {


        }


        public Session GetSessionById(int Id, UserAxe userAxe)
        {

            var ses = Sessions.Where(x => x.UserAxe == userAxe).Include(x => x.SessionQuestions).ThenInclude(x => x.Question).FirstOrDefault(x => x.Id == Id);


            if (ses == null)
                return null;

            return ses;
        }
        public Session GetSessionById(int Id)
        {
            var ses = Sessions.Include(x => x.SessionQuestions).ThenInclude(x => x.Question).FirstOrDefault(x => x.Id == Id);


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

        public DbSet<Option> Options { get; set; }
        public DbSet<Question> Questions { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<SessionQuestion> SessionQuestion { get; set; }


        public List<Session> GetSessionsByFilter(FilterModel filterModel)
        {
            
            if (filterModel.Name == null)
                filterModel.Name = "";




            var sessions = Sessions.Include(x => x.SessionQuestions).ThenInclude(x => x.Question).
                Where(
                x => x.Name.Contains(filterModel.Name) &&
                filterModel.DateTimeStart <= x.TimeStart && x.TimeStart <= filterModel.DateTimeFinish &&
                filterModel.NumVStart <= x.NumVar && x.NumVar <= filterModel.NumVFinish
                );



            return sessions.ToList();

        }
    }
}
