using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test_OP_Web.Data.Options
{
    public class CopyQuestion
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string QuestionString { get; set; }

        public bool NoVariant { get; set; } = false;
        public int NumQ { get; set; }
        public int NumVar { get; set; }
        public List<SessionAnwser> Anwsers { get; set; }

    }
}
