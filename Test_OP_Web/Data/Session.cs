
using System;
using System.Collections.Generic;

namespace Test_OP_Web.Data
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

        public int GetRight()
        {
            int count = 0;
            foreach (var item in SessionQuestions)
            {
                if (item.Enter == item.Question.Right)
                    count++;
            }

            return count;
        }

        public int GetEnterd()
        {
            int count = 0;
            foreach (var item in SessionQuestions)
            {
                if (item.Enter != 0)
                    count++;
            }

            return count;
        }


        

    }
}
