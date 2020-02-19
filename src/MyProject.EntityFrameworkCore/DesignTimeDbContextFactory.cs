using System;
using System.IO;
using MyProject.EntityFrameworkCore.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MyProject.EntityFrameworkCore
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var basePath = Directory.GetCurrentDirectory();
            var path = basePath + "/../MyProject.Web/Config/";
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
                .Build();
    
            var configDefaultSettings = new ConfigDefaultSettings();
            configuration.Bind(nameof(ConfigDefaultSettings), configDefaultSettings);

            return new AppDbContext(configuration, configDefaultSettings);
        }

    }
}
