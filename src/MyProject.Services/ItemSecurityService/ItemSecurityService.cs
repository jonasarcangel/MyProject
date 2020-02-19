using MyProject.Core.Entities;
using MyProject.Services.Interfaces;

namespace MyProject.Services
{
    public class ItemSecurityService : IItemSecurityService
    {
        public IClaimsService _claimsService;

        public ItemSecurityService(IClaimsService claimsService)
        {
            _claimsService = claimsService;
        }

        public bool IsAuthorized(Item item, AppUser user)
        {
            if (item.CreatedBy == user.Id)
            {
                return true;
            }

            if (_claimsService.IsSuperAdmin(user.UserName))
            {
                return true;
            }

            return false;
        }
    }
}