using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
 

namespace MyProject.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, logging) =>
                    {
                        //logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                        //logging.AddConsole();
                    })
                .UseStartup<IdentityStartup>();
    }
}
