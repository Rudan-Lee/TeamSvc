using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace StatlerWaldorfCorp.TeamService
{
    class Program{
        static void Main(string[] args){
            // var config = new ConfigurationBuilder()
            // .AddCommandLine(args)
            // .Build();

            // var host = new WebHostBuilder()
            // .UseKestrel()
            // .UseStartup<Startup>()
            // .UseConfiguration(config)
            // .Build();

            // host.Run();

            var host = Host.CreateDefaultBuilder(args).
            ConfigureWebHostDefaults(webBuilder => {
                webBuilder.UseStartup<Startup>();
            });
            host.Build()
            .Run();
        }
    }
}
