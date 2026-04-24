using Microsoft.EntityFrameworkCore.Design;
using MyFullstackApp.DataAccess.Context;

namespace MyFullstackApp.DataAccess;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // Дизайн-тайм: переменная окружения или LocalDB (как в типовой лабораторной с MSSQL).
        var conn = Environment.GetEnvironmentVariable("MemoryLane_DesignTimeConnection")
            ?? "Server=(localdb)\\mssqllocaldb;Database=MemoryLaneDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";
        DbSession.ConnectionStrings = conn;
        DbSession.Provider = conn.Contains("Server=", StringComparison.OrdinalIgnoreCase) ? "sqlserver" : "sqlite";
        return new AppDbContext();
    }
}
