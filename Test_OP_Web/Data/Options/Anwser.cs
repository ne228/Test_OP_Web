using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_OP_Web.Data.Options
{
    public class Anwser
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool Right { get; set; }       

        public Question question { get; set; }

        public SessionAnwser ToSessionAnwser()
        {
            var res = new SessionAnwser()
            {
                Text = this.Text,
                Right = this.Right,
            };

            return res;
        }

    }
}
