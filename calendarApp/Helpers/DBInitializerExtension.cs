using CalendarApp.Data;

namespace CalendarApp.Helpers
{
    public static class DBInitializerExtension
    {
        public static IApplicationBuilder UseSeedDB(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>();
            DBSeeder.Seed(context);

            return app;
        }
    }
}