using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MyProject.Identity.Auth;
using MyProject.Identity.Data;
using MyProject.Identity.Helpers;
using MyProject.Identity.Models;
using MyProject.Core.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using MyProject.Services;
using kedzior.io.ConnectionStringConverter;

namespace MyProject.Identity
{
  public class IdentityStartup
  {
    private bool developmentEnvironment = false;
    private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
    private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

    public IdentityStartup(IConfiguration configuration, bool developmentEnvironment)
    {
      Configuration = configuration;
      this.developmentEnvironment = developmentEnvironment;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      // Add framework services.
      string appIdentityDbContextConnectionString = Configuration.GetConnectionString("AppIdentityDbContext");

      // Override with Azure connection string if exists
      var azureConnectionStringEnvironmentVariable = this.Configuration["AzureConnectionStringEnvironmentVariable"];
      if (!string.IsNullOrEmpty(azureConnectionStringEnvironmentVariable))
      {
          appIdentityDbContextConnectionString = Environment.GetEnvironmentVariable(azureConnectionStringEnvironmentVariable);
          appIdentityDbContextConnectionString = AzureMySQL.ToMySQLStandard(appIdentityDbContextConnectionString);
      }

      var provider = Configuration["AppIdentityDBProvider"];
      if (string.IsNullOrEmpty(provider))
      {
          provider = "sqlite";
      }
      switch(provider.ToLower())
      {
          case "mssql":
              services.AddDbContext<AppIdentityDbContext>(options =>
                  options.UseSqlServer(appIdentityDbContextConnectionString,
                  optionsBuilder => optionsBuilder.MigrationsAssembly("MyProject.Identity")));
              break;
          case "mysql":
              services.AddDbContext<AppIdentityDbContext>(options =>
                  options.UseMySql(appIdentityDbContextConnectionString,
                  optionsBuilder => optionsBuilder.MigrationsAssembly("MyProject.Identity")));
              break;
          case "sqlite":
              if (string.IsNullOrEmpty(appIdentityDbContextConnectionString))
              {
                  var connectionStringBuilder = new Microsoft.Data.Sqlite.SqliteConnectionStringBuilder { DataSource = "myproject-identity.db" };
                  appIdentityDbContextConnectionString = connectionStringBuilder.ToString();
              }
              services.AddDbContext<AppIdentityDbContext>(options =>
                  options.UseSqlite(appIdentityDbContextConnectionString,
                  optionsBuilder => optionsBuilder.MigrationsAssembly("MyProject.Identity")));
              break;
      }

      services.AddSingleton<IJwtFactory, JwtFactory>();

      // Register the ConfigurationBuilder instance of FacebookAuthSettings
      services.Configure<FacebookAuthSettings>(Configuration.GetSection(nameof(FacebookAuthSettings)));

      // jwt wire up
      // Get options from app settings
      var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

      // Configure JwtIssuerOptions
      services.Configure<JwtIssuerOptions>(options =>
      {
        options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
        options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
        options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
      });

      var tokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

        ValidateAudience = true,
        ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = _signingKey,

        RequireExpirationTime = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
      };

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

      }).AddJwtBearer(configureOptions =>
      {
        configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
        configureOptions.TokenValidationParameters = tokenValidationParameters;
        configureOptions.SaveToken = true;
      });

      // api user claim policy
      // services.AddAuthorization(options =>
      // {
      //   options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
      // });

      // add identity
      var builder = services.AddIdentity<AppUser, AppRole>(o =>
      {
        // configure identity options
        o.Password.RequireDigit = false;
        o.Password.RequireLowercase = false;
        o.Password.RequireUppercase = false;
        o.Password.RequireNonAlphanumeric = false;
        o.Password.RequiredLength = 6;
        o.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        o.User.RequireUniqueEmail = true;
      })
      .AddSignInManager<SignInManager<AppUser>>()
      .AddEntityFrameworkStores<AppIdentityDbContext>()
      .AddDefaultTokenProviders();

      services.AddAuthorization(options =>
      {
          options.AddPolicy("SuperAdmin",
              policy => policy.Requirements.Add(new SuperAdminRequirement()));
          options.AddPolicy("Admin",
              policy => policy.Requirements.Add(new AdminRequirement()));
      });
      services.AddSingleton<IAuthorizationHandler, AdminHandler>();
      services.AddSingleton<IAuthorizationHandler, SuperAdminHandler>();

      services.ConfigureApplicationCookie(options =>
        {
            options.Events = new CookieAuthenticationEvents
            {
                OnRedirectToAccessDenied = ReplaceRedirector(HttpStatusCode.Forbidden, context => options.Events.RedirectToAccessDenied(context)),
                OnRedirectToLogin = ReplaceRedirector(HttpStatusCode.Unauthorized, context => options.Events.RedirectToLogin(context))
            };
        });
        
      services.AddAutoMapper();
      // builder = new IdentityBuilder(builder.UserType, typeof(AppRole), builder.Services);
    }

    private Func<RedirectContext<CookieAuthenticationOptions>, Task> ReplaceRedirector(HttpStatusCode statusCode, Func<RedirectContext<CookieAuthenticationOptions>, Task> existingRedirector) =>
    context =>
    {
      context.Response.StatusCode = (int)statusCode;
      return Task.CompletedTask;
    };

    private async Task CreateUserRoles(IServiceProvider serviceProvider)
    {
      var RoleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
      var UserManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
        
      IdentityResult roleResult;
      //Adding Admin Role
      var roleCheck = await RoleManager.RoleExistsAsync("Admin");
      if (!roleCheck)              
      {
      //create the roles and seed them to the database
      roleResult = await RoleManager.CreateAsync(new AppRole("Admin"));
      }
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
    {
      using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
      {
        var context = serviceScope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
        context.Database.Migrate();
      }

      // app.UseEndpoints(endpoints =>
      // {
      //     endpoints.MapControllerRoute(
      //         "api", "api/{controller}/{action=Index}");
      // });

      this.CreateUserRoles(services).Wait();
    }
  }
}
