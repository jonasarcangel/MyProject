using GraphQL;
using GraphQL.Http;
using GraphQL.Server;
using GraphQL.Server.Transports.AspNetCore;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyProject.Core.Repositories;
using MyProject.EntityFrameworkCore.Repositories;
using MyProject.Api.Models;
using MyProject.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace MyProject.Api
{
  public class ApiStartup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
      services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
      services.AddSingleton<IDocumentWriter, DocumentWriter>();
      
      services.AddSingleton<IAccessService, AccessService>();
      services.AddSingleton<MyProjectQuery>();
      services.AddSingleton<MyProjectMutation>();
      services.AddSingleton<SectionType>();
      services.AddSingleton<SectionInputType>();
      services.AddSingleton<SectionItemType>();
      services.AddSingleton<SectionItemInputType>();
      services.AddSingleton<ContentItemType>();
      services.AddSingleton<ContentItemsCountType>();
      services.AddSingleton<ContentSectionItemType>();
      services.AddSingleton<ContentItemInputType>();
      services.AddSingleton<MaxPageSizeType>();
      services.AddSingleton<ConfigType>();
      services.AddSingleton<ConfigInputType>();
      var sp = services.BuildServiceProvider();
      services.AddSingleton<ISchema>(new MyProjectSchema(new FuncDependencyResolver(type => sp.GetService(type))));

      services.AddGraphQL(_ =>
      {
        _.EnableMetrics = true;
        _.ExposeExceptions = true;
      });

      services.AddAuthorization(configuration =>
      {
        configuration.AddPolicy("AnonymousOrJwt", new AuthorizationPolicyBuilder()
          .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
          .RequireAssertion(_ => true)
          .Build());
      });
      //.AddUserContextBuilder(httpContext => new GraphQLUserContext { User = httpContext.User });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      // add http for Schema at default url /graphql
      app.UseGraphQL<ISchema>("/graphql");

      // use graphql-playground at default url /ui/playground
      app.UseGraphQLPlayground(new GraphQLPlaygroundOptions
      {
          Path = "/ui/playground"
      });
    }
  }
}