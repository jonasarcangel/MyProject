using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MyProject.Core.Entities;
using MyProject.Core.Repositories;
using Microsoft.AspNetCore.Http;

namespace MyProject.Api.Services
{
    public interface IAccessService
    {
        string GetUserId();
        bool IsUser();
        bool IsSuperAdmin();
        bool IsAdmin();
        bool IsAdminOrSuperAdmin();
        string GetUsername();
        Task<bool> IsEnabled(string moduleAndFunction);
        Task<bool> Allowed(string module, string type, bool enabledRequired);
        Task<bool> Allowed(ContentItem contentItem);
        string DomainTenant();
        Task<string> GetUserDomain();
    }
}
