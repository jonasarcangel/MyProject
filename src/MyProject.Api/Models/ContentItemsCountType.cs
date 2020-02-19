using GraphQL.Types;
using MyProject.Core.Entities;
using MyProject.Core.Repositories;

namespace MyProject.Api.Models
{
    public class ContentItemsCountType : ObjectGraphType<ContentItemsCount>
    {
        public ContentItemsCountType()
        {
            Field(x => x.Count, nullable:true);
        }
    }
}