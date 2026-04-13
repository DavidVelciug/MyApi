using Microsoft.EntityFrameworkCore;
using MyFullstackApp.BusinessLogic.Mapping;
using MyFullstackApp.DataAccess;
using MyFullstackApp.DataAccess.Context;

var builder = WebApplication.CreateBuilder(args);

ConfigureDbSession(builder);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddScoped<MyFullstackApp.BusinessLogic.BusinessLogic>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var db = new AppDbContext())
{
    db.Database.Migrate();
    DbInitializer.SeedIfEmpty(db);
}

app.UseCors("DevCorsPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

static void ConfigureDbSession(WebApplicationBuilder builder)
{
    var conn = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrWhiteSpace(conn))
    {
        conn = "Data Source=memorylane.db";
    }

    const string prefix = "Data Source=";
    if (conn.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
    {
        var relative = conn[prefix.Length..].Trim();
        var path = Path.IsPathRooted(relative)
            ? relative
            : Path.Combine(builder.Environment.ContentRootPath, relative);
        var dir = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(dir))
        {
            Directory.CreateDirectory(dir);
        }

        DbSession.ConnectionStrings = $"Data Source={path}";
    }
    else
    {
        DbSession.ConnectionStrings = conn;
    }
}
