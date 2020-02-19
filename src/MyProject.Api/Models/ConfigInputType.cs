using GraphQL.Types;

namespace MyProject.Api.Models
{
    public class ConfigInputType : InputObjectGraphType
    {
        public ConfigInputType()
        {
            Name = "ConfigInput";

            Field<StringGraphType>("tenant");
            Field<StringGraphType>("id");
            Field<StringGraphType>("name");
            
            Field<StringGraphType>("module");
            Field<StringGraphType>("value");
        }
    }
}