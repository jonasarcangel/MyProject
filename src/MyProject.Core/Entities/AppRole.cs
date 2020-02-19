﻿using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Core.Entities
{
    public class AppRole : IdentityRole<string>
    {
        public AppRole()
        {
        }

        public AppRole(string role): base(role)
        {
        }

        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
