 using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using StatlerWaldorfCorp.TeamService.Persistence;

namespace StatlerWaldorfCorp.TeamService
{
    public class Startup{
       public Startup(IHostEnvironment env){
           Console.WriteLine("Startup is instanced");
       }

       public void Configure(IApplicationBuilder app,
       IHostEnvironment env, ILoggerFactory loggerFactory){
           app.Run(async(context) => {
               await context.Response.WriteAsync("Hello, World!");
           });
       }

       public void ConfigureServices(IServiceCollection services){
           services.AddMvc();
           services.AddScoped<ITeamRepository, MemoryTeamRepository>();
       }
    }
}
