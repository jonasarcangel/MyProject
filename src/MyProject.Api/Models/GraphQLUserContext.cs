using System.Security.Claims;
using GraphQL.Authorization;

namespace MyProject.Api.Models
{
    public class GraphQLUserContext : IProvideClaimsPrincipal
    {
        public ClaimsPrincipal User { get; set; }
    }
}