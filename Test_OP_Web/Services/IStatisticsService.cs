namespace Test_OP_Web.Services
{
    public interface IStatisticsService 
    {

        public Stat GetStatVar(int NumVar, int currentSessionId);
    }
}
