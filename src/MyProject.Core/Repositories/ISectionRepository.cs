using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using MyProject.Core.Entities;

namespace MyProject.Core.Repositories
{
    public interface ISectionRepository
    {
        Task<List<Section>> Get(
            string tenant,
            string id,
            string module,
            string name);
        Task<Section> AddOrUpdate(Section section, bool isSectionAdmin);
        Task<Section> Add(Section section, bool isSectionAdmin);
        Task<Section> Update(Section section, bool isSectionAdmin);
        Task<Section> Delete(string tenant, string name, bool isSectionAdmin);
    }
}