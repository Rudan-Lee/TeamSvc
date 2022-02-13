using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.AspNetCore;

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

            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("hostsettings.json", optional: true)
            .AddCommandLine(args)
            .Build();

            var host = WebHost.CreateDefaultBuilder(args)
            .UseUrls("http://*:8080")
            .UseConfiguration(config)
            .UseStartup<Startup>();

            host.Build()
            .Run();
        }
    }
}
