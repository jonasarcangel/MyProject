using GraphQL.Types;
using MyProject.Core.Entities;
using MyProject.Core.Repositories;

namespace MyProject.Api.Models
{
    public class MaxPageSizeType : ObjectGraphType<MaxPageSize>
    {
        public MaxPageSizeType()
        {
            Field(x => x.Count,  nullable:true);
        }
    }
}