﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_OP_Web.Data.Options;

namespace Test_OP_Web.Data
{
    public class UserAxe : IdentityUser
    {
        public List<Session> Sessions { get; set; }
    }
}
