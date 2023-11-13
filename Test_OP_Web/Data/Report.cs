using System;
using System.ComponentModel.DataAnnotations.Schema;
using Test_OP_Web.Data.Options;

namespace Test_OP_Web.Data
{
    public class Report
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public bool Confirm { get; set; } = false;
        public int QuestionId { get; set; }
        [ForeignKey ("QuestionId")]
        public Question Question { get; set; }

        public DateTime DateTime { get; set; }

        public UserAxe UserAxe { get; set; }
    }
}
