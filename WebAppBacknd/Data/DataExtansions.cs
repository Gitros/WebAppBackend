using Microsoft.EntityFrameworkCore;

namespace WebAppBacknd.Data;

public static class DataExtansions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate();
    }
}
