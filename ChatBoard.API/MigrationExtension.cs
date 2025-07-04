
using ChatBoard.DataBase.Context;
using Microsoft.EntityFrameworkCore;

namespace ChatBoard.API
{
    public static class MigrationExtension
    {
        public static void ApplyMigrations(this WebApplicationBuilder builder)
        {
            var app = builder.Build();
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
            db.Database.Migrate();
            Environment.Exit(0);
        }
    }
}
