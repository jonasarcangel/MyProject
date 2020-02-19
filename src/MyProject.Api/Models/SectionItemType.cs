using GraphQL.Types;
using MyProject.Core.Entities;
using MyProject.Core.Repositories;

namespace MyProject.Api.Models
{
    public class SectionItemType : ObjectGraphType<SectionItem>
    {
        public SectionItemType(ISectionItemRepository sectionItemRepository)
        {
            Field(x => x.Tenant, nullable:true);
            Field(x => x.Id, nullable:true);
            Field(x => x.Name, nullable:true);
            Field(x => x.CreatedBy, nullable:true);
            Field(x => x.CreatedDate, nullable:true);
            Field(x => x.UpdatedBy, nullable:true);
            Field(x => x.UpdatedDate, nullable:true);
            Field(x => x.ParentId, nullable:true);
            Field(x => x.ChildCount, nullable:true);
            
            Field(x => x.Section, nullable:true);
            Field(x => x.PathUrl, nullable:true);
            Field(x => x.PathName, nullable:true);
            Field(x => x.Alias, nullable:true);
            Field(x => x.Description, nullable:true);
            //Field<StringGraphType>("modules", resolve: context => context.Source.Modules);
        }
    }
}