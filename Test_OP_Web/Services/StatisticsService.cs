using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_OP_Web.Data;
using Test_OP_Web.Data.Options;

namespace Test_OP_Web.Services
{
    public class StatisticsService : IStatisticsService
    {
        private OptionContext Context { get; }
        public StatisticsService(OptionContext dbContext)
        {
            this.Context = dbContext;
        }


        private double GetPercent(Session session)
        {

            int countRight = 0;
            foreach (var q in session.SessionQuestions)
            {
                if (q.GetRight())
                    countRight++;
            }
                       

            double res = Math.Round(Convert.ToDouble(countRight) / Convert.ToDouble(session.SessionQuestions.Count) * 100);
            return res;
        }





        public Stat GetStatVar(int NumVar, int currentSessionId)
        {
            Stat stat = new Stat();
            var ses = Context.GetSessions().
                Where(x => x.NumVar == NumVar).ToList();

            if (ses == null)
                return stat;

            var curSes = ses.FirstOrDefault(x => x.Id == currentSessionId);


            stat.CurrentPercent = GetPercent(curSes);
            stat.AveragePercent = 0;

            foreach (var item in ses)
            {
                if (item.Id != currentSessionId)
                {
                    double percent = GetPercent(item);
                    stat.AveragePercent += percent;

                    if (percent > stat.CurrentPercent)
                        stat.CountMoreThenYou++;

                    if (percent < stat.CurrentPercent)
                        stat.CountLessThenYou++;

                }

            }

            if (ses.Count > 1)
                stat.AveragePercent = Math.Round(stat.AveragePercent / (ses.Count - 1));



            stat.YouLooser = stat.CountMoreThenYou > stat.CountLessThenYou;

            return stat;

        }



        public async Task<PersonStat> PersonStat(string email, int NumVar=-1)
        {
            PersonStat personStat = new();
            UserAxe user = await Context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());

            if (user == null)
                return personStat;

            personStat.Name = email;

            List<Session> sessions;


            if (NumVar == -1)
                sessions = Context.GetSessions().Where(x => x.Name.ToLower() == email.ToLower()).ToList();
            else
                sessions = Context.GetSessions().Where(x => x.Name.ToLower() == email.ToLower()
                                                       && x.NumVar == NumVar).ToList();
                                                        


            double averageAll = 0;

            foreach (var session in sessions)
            {
                var percent = GetPercent(session);
                averageAll += percent;

            }

            
            if (sessions.Count != 0)
                averageAll = averageAll / sessions.Count;

            personStat.AveragePercent = averageAll;
            personStat.Count = sessions.Count;

            return personStat;

        }

        public async Task<List<PersonStat>> PersonsStat( int NumVar = -1)
        {

            var users = await Context.Users.ToListAsync();

            List<PersonStat> personsStat = new();

            foreach (var user in users)
            {
                personsStat.Add(await PersonStat(user.Email));
            }

            return personsStat.Where(x=>x.Count>=3).OrderByDescending(x=>x.AveragePercent).ToList();
        }

    }
}
