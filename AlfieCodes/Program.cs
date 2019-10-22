using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AlfieCodes
{
    using Serilog;
    using Serilog.Events;

    public class Program
    {
        public static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
                         .MinimumLevel.Information()
                         .MinimumLevel.Override( "Microsoft", LogEventLevel.Warning )
                         .Enrich.FromLogContext()
                         .WriteTo.Seq( Environment.GetEnvironmentVariable( "SEQ_URL" ) ?? "http://localhost:5341" )
                         .CreateLogger();
        
            try
            {
                Log.Information("Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder( string[] args ) =>
            Host.CreateDefaultBuilder( args )
                .UseSerilog()
                .ConfigureWebHostDefaults( webBuilder =>
                   {
                       webBuilder
                           .UseStartup<Startup>();
                   } );
    }
}
