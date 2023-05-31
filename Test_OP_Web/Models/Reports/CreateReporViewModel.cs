using System.ComponentModel.DataAnnotations;
using Test_OP_Web.Data.Options;

namespace Test_OP_Web.Models.Reports
{
    public class CreateReporViewModel
    {
        [Required]
        public int QuestionId { get; set; }

        [Required]
        public string Message { get; set; }


        public Question Question { get; set; }

    }
}
