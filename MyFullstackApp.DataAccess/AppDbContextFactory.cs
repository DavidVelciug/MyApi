using Microsoft.EntityFrameworkCore.Design;
using MyFullstackApp.DataAccess.Context;

namespace MyFullstackApp.DataAccess;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "memorylane.design.db");
        DbSession.ConnectionStrings = $"Data Source={dbPath}";
        return new AppDbContext();
    }
}
