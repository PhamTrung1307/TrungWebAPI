using Data;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public static class MigrationManager
    {
        public static WebApplication MigrateDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<IDbContext>();
                    context.Database.Migrate();
                    new DataSeeder().SeedAsync(context).Wait();
                    //var seeder = new Data.DataSeeder();
                    //seeder.SeedAsync(context).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");
                }
            }
            return app;
        }
    }
}
