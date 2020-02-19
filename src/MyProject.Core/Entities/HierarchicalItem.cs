using System;

namespace MyProject.Core.Entities
{
    public class HierarchicalItem : Item
    {
        public string ParentId { get; set; }
        public int ChildCount { get; set; }
   }
}