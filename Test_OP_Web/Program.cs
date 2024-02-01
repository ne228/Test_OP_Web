using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using TelegramSink;
using Test_OP_Web.Data;
using Test_OP_Web.Data.Options;

namespace Test_OP_Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = CreateHostBuilder(args);

            var logger = GetLogger();
            app.UseSerilog(logger);

            var host = app.Build();
            CreateDbIfNotExists(host).Wait();

            host.Run();

           
        }

        private static async Task CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<OptionContext>();

                    var userManager = services.GetRequiredService<UserManager<UserAxe>>();
                    var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var logger = services.GetRequiredService<Test_OP_Web.Logging.ILogger>();
                    DbInitializer.Initialize(context,logger);
                    await DbInitializer.RoleInitialize(userManager, rolesManager,logger);

                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        private static Serilog.Core.Logger GetLogger()
        {
            var logger = new LoggerConfiguration()
                       .MinimumLevel.Debug()
                       // Add console (Sink) as logging target
                       .WriteTo.Console()

                       // Write logs to a file for warning and logs with a higher severity
                       // Logs are written in JSON
                       .WriteTo.File(
                           new JsonFormatter(),
                           $"{Directory.GetCurrentDirectory()}/logs/important-logs.json",
                           restrictedToMinimumLevel: LogEventLevel.Warning)

                       .WriteTo.TeleSink(
                         telegramApiKey: "5982966206:AAGxc5e5eL8VXRleimsx7l5boPJIPIWcrfE",
                         telegramChatId: "467719960",
                         minimumLevel: LogEventLevel.Error
                         )
                       // Add a log file that will be replaced by a new log file each day
                       .WriteTo.File(
                                   $"{Directory.GetCurrentDirectory()}/logs/all-daily-.logs",
                                  rollingInterval: RollingInterval.Day)

                   .CreateLogger();

            return logger;
        }
    }
}
