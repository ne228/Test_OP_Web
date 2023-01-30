using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_OP_Web.Data.Options
{
    public class Option
    {
        [Key]
        public int Id { get; set; }

        public int NumVar { get; set; }

        public List<Question> Questions { get; set; } = new();

    }
}
