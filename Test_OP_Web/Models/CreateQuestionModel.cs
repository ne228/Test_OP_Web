using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Test_OP_Web.Data.Options;
using Test_OP_Web.Data.OptionTemplate;

namespace Test_OP_Web.Models
{

    public class AnwserModel
    {

        public string AnwserString { get; set; }

       
        public bool Right { get; set; }
    }

    public class CreateQuestionModel
    {

        [Required(ErrorMessage = "Не указан вариант")]
        public string QuestionString { get; set; }

        public List<AnwserModel> Anwsers { get; set; } = new List<AnwserModel>
        {
            new AnwserModel(),
             new AnwserModel(),
              new AnwserModel(),
               new AnwserModel(),
                new AnwserModel(),
                 new AnwserModel(),
        };

        public QuestionTemplate toQuestion()
        {
            var question = new QuestionTemplate();
            if (Anwsers.Count == 1)
                question.NoVariant = true;
            question.QuestionString = QuestionString;
            question.Anwsers = new List<AnwserTemplate>();
            foreach (var anwserModel in Anwsers.Where(anwser => anwser.AnwserString != null))
            {
                var anwser = new AnwserTemplate();
                anwser.question = question;
                anwser.Text = anwserModel.AnwserString;
                anwser.Right = anwserModel.Right;
                question.Anwsers.Add(anwser);

            }

            return question;
        }


    }


}
