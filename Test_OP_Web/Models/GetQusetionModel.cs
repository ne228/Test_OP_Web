using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Test_OP_Web.Data.Options;

namespace Test_OP_Web.Models
{
    public class GetQusetionModel
    {
        public SessionQuestion Question { get; set; }

        public int NumQ { get; set; }
        public int SessionId { get; set; }
    }
}
