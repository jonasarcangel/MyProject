using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Identity.ViewModels.Users
{
    public class UpdateRoleModel
    {
        [Required]
        public string Id { get; set; }
        public string RoleId { get; set; }
    }
}
