using GraphQL.Types;
using MyProject.Core.Entities;
using MyProject.Core.Repositories;

namespace MyProject.Api.Models
{
    public class SectionType : ObjectGraphType<Section>
    {
        public SectionType(ISectionRepository sectionRepository)
        {
            Field(x => x.Tenant, nullable:true);
            Field(x => x.Id, nullable:true);
            Field(x => x.Name, nullable:true);
            Field(x => x.CreatedBy, nullable:true);
            Field(x => x.CreatedDate, nullable:true);
            Field(x => x.UpdatedBy, nullable:true);
            Field(x => x.UpdatedDate, nullable:true);
            
            Field(x => x.Modules, nullable:true);

            //Field<StringGraphType>("modules", resolve: context => context.Source.Modules);
        }
    }
}