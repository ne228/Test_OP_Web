using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_OP_Web.Models;

namespace Test_OP_Web.Data
{
    public class SessionQuestion
    {
        public int Id { get; set; }

        public int NumQ { get; set; }
        public int SessionId { get; set; }
        public Question Question { get; set; }

        public int Enter { get; set; }
    }
}
