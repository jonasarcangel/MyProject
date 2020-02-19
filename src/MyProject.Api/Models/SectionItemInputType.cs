using GraphQL.Types;

namespace MyProject.Api.Models
{
    public class SectionItemInputType : InputObjectGraphType
    {
        public SectionItemInputType()
        {
            Name = "SectionItemInput";

            Field<StringGraphType>("tenant");
            Field<StringGraphType>("id");
            Field<StringGraphType>("name");
            Field<StringGraphType>("parentId");

            Field<StringGraphType>("section");
            Field<StringGraphType>("pathUrl");
            Field<StringGraphType>("pathName");
            Field<StringGraphType>("alias");
            Field<StringGraphType>("description");
        }
    }
}