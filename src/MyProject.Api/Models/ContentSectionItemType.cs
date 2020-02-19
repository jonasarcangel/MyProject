using GraphQL.Types;
using MyProject.Core.Entities;
using MyProject.Core.Repositories;

namespace MyProject.Api.Models
{
    public class ContentSectionItemType : ObjectGraphType<ContentSectionItem>
    {
        public ContentSectionItemType()
        {
            Field(x => x.SectionItem, type:typeof(SectionItemType),  nullable:true);
        }
    }
}