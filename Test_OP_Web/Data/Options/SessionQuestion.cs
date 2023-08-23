using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Test_OP_Web.Data.Options
{
    public class SessionQuestion
    {
        public int Id { get; set; }

        public int SessionId { get; set; }
        public Question Question { get; set; }

        public List<Anwser> Enter { get; set; } = new();

        public bool Right { get; set; }

        public bool Blocked { get; set; } = false;

        public bool Correctly { get; set; } = false;


        public string Normilize(string str)
        {
            if (str == null || str.Length == 0)
                return str;
            str = str.Replace(" ", "");
            str = str.Replace("\n", "");
            str = str.Replace("\t", "");
            str = str.ToLower();

            return str;
        }

        // Получить верный ответ или нет
        public bool GetRight()
        {

            if (Question.NoVariant)
            {
                var enter = Enter.FirstOrDefault();
                if (enter == null)
                    return false;


                if (Question.Anwsers.Any(x => Normilize(x.Text) == Normilize(enter.Text)))
                    return true;
                else
                    return false;
            }


            int countTrue = 0;

            foreach (var anwser in Question.Anwsers)
            {
                foreach (var enter in Enter)
                {
                    if (enter.Id == anwser.Id && anwser.Right)
                        countTrue++;
                }

            }
            var countEnterTrue = Enter.Count(x => x.Right);

            var countTrueTrue = Question.Anwsers.Count(x => x.Right);

            if (countEnterTrue == countTrueTrue && Enter.ToList().Count == countTrueTrue)
                return true;
            else
                return false;


        }

        public bool GetEnter()
        {
            if (Enter.Count > 0)
                return true;
            else
                return false;

        }
    }
}
