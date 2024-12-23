using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Test_OP_Web.Data.Options;

namespace Test_OP_Web.Data.OptionTemplate
{
    public class QuestionTemplate
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Автоинкремент
        public int Id { get; set; }
        [Required]
        public string QuestionString { get; set; }

        public bool NoVariant { get; set; } = false;
        public List<AnwserTemplate> Anwsers { get; set; }
    }
}
