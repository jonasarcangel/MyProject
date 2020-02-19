

using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MyProject.Identity.Auth;
using MyProject.Identity.Models;
using Newtonsoft.Json;

namespace MyProject.Identity.Helpers
{
    public class Tokens
    {
      public static async Task<string> GenerateJwt(
        ClaimsIdentity identity, 
        IJwtFactory jwtFactory,
        string userName, 
        string tenant,
        string email,
        bool isAdmin,
        bool isSuperAdmin,
        JwtIssuerOptions jwtOptions, 
        JsonSerializerSettings serializerSettings)
      {
        string avatarHash = string.Empty;
        using (var md5 = MD5.Create())
        {
            var result = md5.ComputeHash(Encoding.ASCII.GetBytes(email));
            avatarHash = BitConverter.ToString(result).Replace("-", "").ToLower();
        }

        var response = new
        {
          auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
          expires_in = (int)jwtOptions.ValidFor.TotalSeconds,
          id = identity.Claims.Single(c => c.Type == "id").Value,
          username = userName,
          tenant = tenant,
          avatar_hash = avatarHash,          
          is_superadmin = isSuperAdmin,
          is_admin = isAdmin
        };

        return JsonConvert.SerializeObject(response, serializerSettings);
      }
    }
}
