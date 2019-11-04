using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AlfieCodes
{
    using AlfieCodes.Data;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization.Infrastructure;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Pioneer.Pagination;
    using Serilog;

    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddDbContextPool<BlogDbContext>( options => { options.UseSqlServer( Configuration.GetConnectionString( "AlfieCodesDb" ) ); } );

            services.AddTransient<IPaginatedMetaService, PaginatedMetaService>();

            services.AddHttpContextAccessor();
            services.AddAuthentication( CookieAuthenticationDefaults.AuthenticationScheme )
                    .AddCookie( options => options.LoginPath = "/Login" );

            services.AddAuthorization( options =>
                                       {
                                           options.AddPolicy( "Admin", policy =>
                                                                       {
                                                                           policy.Requirements.Add( new RolesAuthorizationRequirement( new []{"Admin"} ) );
                                                                       } );
                                       } );

            services.AddRazorPages()
                    .AddRazorPagesOptions( options =>
                                           {
                                               options.Conventions.AuthorizeFolder( "/" );
                                               options.Conventions.AuthorizeAreaFolder( "Administration", "/", "Admin" );
                                               options.Conventions.AllowAnonymousToPage( "/Index" );
                                               options.Conventions.AllowAnonymousToPage( "/Login" );
                                               options.Conventions.AllowAnonymousToPage( "/Register" );
                                               options.Conventions.AllowAnonymousToPage( "/Fail" );
                                               options.Conventions.AllowAnonymousToPage( "/Search" );
                                               options.Conventions.AllowAnonymousToPage( "/Tags" );
                                           } ).SetCompatibilityVersion( CompatibilityVersion.Latest );

            services.AddHostedService<StartupActions>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler( "/Error" );
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMiddleware<StackifyMiddleware.RequestTracerMiddleware>();

            app.UseStaticFiles()
               .UseSerilogRequestLogging()
               .UseHttpsRedirection()
               .UseRouting()
               .UseAuthentication()
               .UseAuthorization();


            app.UseEndpoints( endpoints => { endpoints.MapRazorPages(); } );
        }
    }
}
