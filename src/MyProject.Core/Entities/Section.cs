using System;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Core.Entities
{
    public class Section : Item
    {
        public string Modules { get; set; }
    }
}