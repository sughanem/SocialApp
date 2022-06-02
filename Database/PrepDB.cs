using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SocialApp.Database
{
    public static class PerpDB
    {
        public static void perpPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<SocialAppDatabase>());
            }
            System.Console.WriteLine("Done");
        }

        public static void SeedData(SocialAppDatabase context)
        {
            System.Console.WriteLine("Appling Migrations...");
            context.Database.Migrate();
        }
    }
}