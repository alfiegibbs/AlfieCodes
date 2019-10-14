namespace AlfieCodes
{
    using System.Threading;
    using System.Threading.Tasks;
    using AlfieCodes.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class StartupActions : IHostedService
    {
        private readonly IServiceScopeFactory scopeFactory;

        public StartupActions( IServiceScopeFactory  scopeFactory )
        {
            this.scopeFactory = scopeFactory;
        }

        public async Task StartAsync( CancellationToken cancellationToken )
        {
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
            await context.Database.MigrateAsync( cancellationToken : cancellationToken );
        }

        public Task StopAsync( CancellationToken cancellationToken )
        {
            return Task.CompletedTask;
        }
    }
}
