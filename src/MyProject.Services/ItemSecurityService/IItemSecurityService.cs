using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyProject.Core.Entities;

namespace MyProject.Services.Interfaces
{
    public interface IItemSecurityService
    {
        bool IsAuthorized(Item item, AppUser user);
    }
}