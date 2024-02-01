using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Test_OP_Web.Data.Options
{
    public class SessionQuestion
    {
        [Key]
        public int Id { get; set; }

        public int SessionId { get; set; }
        public CopyQuestion Question { get; set; }

        //public List<SessionAnwser> Enter { get; set; } = new();

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
                var enter = Question.Anwsers.FirstOrDefault();
                if (enter == null)
                    return false;

                if (Question.Anwsers.Count >= 2)
                    if (Normilize(Question.Anwsers.LastOrDefault().Text) ==
                         Normilize(Question.Anwsers.FirstOrDefault().Text))
                        return true;


                return false;
            }


            int countTrue = 0;

            foreach (var anwser in Question.Anwsers)
            {
                if (anwser.Enter == anwser.Right)
                    continue;

                return false;
            }

            return true;


        }

        public bool GetEnter()
        {
            if (Question.Anwsers.Count(x => x.Enter) > 0)
                return true;
            else
                return false;

        }
    }
}
