using GraphQL;
using GraphQL.Types;

namespace MyProject.Api.Models
{
    public class MyProjectSchema : Schema
    {
        public MyProjectSchema(IDependencyResolver resolver): base(resolver)
        {
            Query = resolver.Resolve<MyProjectQuery>();
            Mutation = resolver.Resolve<MyProjectMutation>();
        }
    }
}