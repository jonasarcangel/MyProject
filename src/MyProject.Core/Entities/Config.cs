using System.ComponentModel.DataAnnotations;

namespace MyProject.Core.Entities
{
    public class Config : Item
    {
        public string Module { get; set; }
        public string Value { get; set; }
    }
}