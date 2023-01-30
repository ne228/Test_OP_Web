using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_OP_Web.Data.Options
{
    public class Question
    {

        [Key]
        public int Id { get; set; }
        public string QuestionString { get; set; }

        public bool NoVariant { get; set; } = false;
        public int NumQ { get; set; }
        public int NumVar { get; set; }
        public List<Anwser> Anwsers { get; set; }



    }
}
