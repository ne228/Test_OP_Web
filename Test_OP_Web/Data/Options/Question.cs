using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_OP_Web.Data.Options
{
    public class Question
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Автоинкремент
        public int Id { get; set; }

        [Required]
        public string QuestionString { get; set; }


        public bool NoVariant { get; set; } = false;
        public int NumQ { get; set; }
        public int NumVar { get; set; }
        public List<Anwser> Anwsers { get; set; }

        public CopyQuestion ToCopyQuestion()
        {
            var res = new CopyQuestion()
            {
                QuestionString = QuestionString,
                NoVariant = NoVariant,
                NumQ = NumQ,
                NumVar = NumVar,
                Anwsers = new List<SessionAnwser>()
            };

            foreach (var anwser in Anwsers)            
                res.Anwsers.Add(anwser.ToSessionAnwser());
            
            return res;
        }

    }
}
