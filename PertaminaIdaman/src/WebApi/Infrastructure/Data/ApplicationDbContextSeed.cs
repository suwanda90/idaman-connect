using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContextSeed 
    {
        public static async Task SeedAsync(ApplicationDbContext applicationDbContext, IAppLogger<ApplicationDbContext> appLogger, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                // TODO: Only run this if using a real database
                // context.Database.Migrate();

                if (!applicationDbContext.Role.Any())
                {
                    applicationDbContext.Role.AddRange(GetRole());

                    await applicationDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    appLogger.LogError(ex.Message.ToString());

                    await SeedAsync(applicationDbContext, appLogger, retryForAvailability);
                }
                throw;
            }
        }

        static IEnumerable<Role> GetRole()
        {
            var clientApis = new List<Role>();

            var clientApiDefault = new Role {
                Name = "Super.Admin",
                CreatedBy = "system",
                ModifiedBy = "system",
                IsActive = true
            };
            clientApis.Add(clientApiDefault);

            return clientApis;
        }
    }
}