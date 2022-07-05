using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_OP_Web.Data;

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
                if (q.Enter == q.Question.Right)
                    countRight++;
            }

            double res = Math.Round(Convert.ToDouble(countRight) / Convert.ToDouble(session.SessionQuestions.Count) * 100);
            return res;
        }





        public Stat GetStatVar(int NumVar, int currentSessionId)
        {
            Stat stat = new Stat();
            var ses = Context.Sessions.Include(x => x.SessionQuestions).ThenInclude(x => x.Question).
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
    }
}
