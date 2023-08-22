
using System;
using System.Collections.Generic;
using System.Linq;

namespace Test_OP_Web.Data.Options
{
    public class Session
    {

        public int Id { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeFinsih { get; set; }

        public int NumVar { get; set; }

        public string Name { get; set; }

        public List<SessionQuestion> SessionQuestions { get; set; } = new();

        public bool Сompleted { get; set; }


        public UserAxe UserAxe { get; set; }

        //public int GetRight { get; set; } = 0;

        public int getRight()
        {
            int count = 0;
            foreach (var question in SessionQuestions)
            {
                if (question.GetRight())
                    count++;
                else
                {
                    var test = "asd";
                }
            }

            return count;
        }

        public int GetEnterd()
        {
            int count = 0;
            foreach (var item in SessionQuestions)
            {
                var nullAnwsers = item.Question.Anwsers.FirstOrDefault();
                if (nullAnwsers == null)
                    count++;
            }

            return count;
        }
    }
}
