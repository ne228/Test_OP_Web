using System.Threading.Tasks;

namespace Test_OP_Web.Services
{
    public interface IStatisticsService 
    {

        public Task<Stat> GetStatVar(int NumVar, int currentSessionId);
    }
}
