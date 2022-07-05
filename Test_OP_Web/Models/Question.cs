using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_OP_Web.Models
{
    public class Question
    {

        [Key]
        public int Id { get; set; }
        public string QuestionString { get; set; }
        public string One { get; set; } 
        public string Two { get; set; } 
        public string Three { get; set; }
        public string Four { get; set; }


        public int NumVar { get; set; }
        public int NumQ { get; set; }


        public int Enter { get; set; }
        public int Right { get; set; }
        public bool Correctly { get; set; } = false;



        public string GetNum(int num)
        {

            if (num == 1)
                return One;

            if (num == 2)
                return Two;

            if (num == 3)
                return Three;

            if (num == 4)
                return Four;

            return "No insert";

        }
  
    }
}
