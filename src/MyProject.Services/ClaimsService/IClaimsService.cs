using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MyProject.Services
{
    public interface IClaimsService
    {
        string GetUsername(AuthorizationHandlerContext context);
        string GetUsername(ClaimsPrincipal user);
        bool IsSuperAdmin(AuthorizationHandlerContext context);
        bool IsAdmin(AuthorizationHandlerContext context);
        bool IsSuperAdmin(ClaimsPrincipal user);
        bool IsAdmin(ClaimsPrincipal user);
        bool IsSuperAdmin(string username);
        string GetUserId(AuthorizationHandlerContext context);
        string GetUserId(ClaimsPrincipal user);
    }
}