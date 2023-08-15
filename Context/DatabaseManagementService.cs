using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Context;

public class DatabaseManagementService
{
    public static void MigrationInitialization(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            serviceScope.ServiceProvider.GetService<CatalogApiContext>().Database.Migrate();
        }
    }
}