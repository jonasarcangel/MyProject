using Microsoft.Extensions.DependencyInjection;
using MyProject.Services.Interfaces;

namespace MyProject.Services
{
  public class ServicesStartup
  {
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IEmailSenderService, EmailSenderService>();
        services.AddTransient<ISmsSenderService, EmailSenderService>();
        services.AddScoped<IInviteService, InviteService>();
        services.AddScoped<IItemSecurityService, ItemSecurityService>();
        services.AddSingleton<IClaimsService, ClaimsService>();
    }
  }
}