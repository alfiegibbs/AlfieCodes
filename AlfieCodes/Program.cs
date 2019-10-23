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
        public static void Main( string[] args )
        {
            var configuration = new ConfigurationBuilder()
                                .AddJsonFile( "appsettings.json" )
                                .Build();

            Log.Logger = new LoggerConfiguration()
                         .Enrich.WithProperty( "Name", "Alfie" )
                         .Enrich.WithProperty( "Environment", Environment.GetEnvironmentVariable( "SEQ_ENV" ) ?? "Test" )
                         .Enrich.WithProperty( "Component", Environment.GetEnvironmentVariable( "SEQ_COMP" ) ?? "Blog" )
                         .ReadFrom.Configuration( configuration )
                         .MinimumLevel.Override( "Microsoft", LogEventLevel.Warning )
                         .Enrich.FromLogContext()
                         .WriteTo.Seq( serverUrl : Environment.GetEnvironmentVariable( "SEQ_URL" ) ?? "http://localhost:5341",
                                       apiKey : Environment.GetEnvironmentVariable( "SEQ_API_KEY" ) )
                         .CreateLogger();

            try
            {
                Log.Information( "Starting up" );
                CreateHostBuilder( args ).Build().Run();
            }
            catch ( Exception ex )
            {
                Log.Fatal( ex, "Application start-up failed" );
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
