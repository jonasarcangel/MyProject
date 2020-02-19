using System.ComponentModel.DataAnnotations;

namespace MyProject.Core.Entities
{
    public class Invite
    {
        [Key]
        public string Id { get; set; }
        public string Inviter { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
    }
}