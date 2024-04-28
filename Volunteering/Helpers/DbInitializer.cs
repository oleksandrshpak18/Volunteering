using Microsoft.AspNetCore.Builder;
using Volunteering.Data.Models;

namespace Volunteering.Helpers
{
    public class DbInitializer
    {
        public static void Initialize(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();

                if (!context.UserRoles.Any())
                {
                    var userRoles = new UserRole[]
                    {
                    new UserRole { UserRoleName = "Admin" },
                    new UserRole { UserRoleName = "Registered"}
                    };

                    foreach (var r in userRoles)
                    {
                        context.UserRoles.Add(r);
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
