using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_OP_Web.Services
{
    public interface IStatisticsService 
    {

        public Stat GetStatVar(int NumVar, int currentSessionId);
    }
}
