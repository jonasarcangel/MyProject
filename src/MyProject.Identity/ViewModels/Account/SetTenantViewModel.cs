using System.ComponentModel.DataAnnotations;

namespace MyProject.Identity.ViewModels.Account
{
    public class SetTenantViewModel
    {
        [Required]
        public string Tenant { get; set; }
    }
}
