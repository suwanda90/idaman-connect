using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities.Config;
using ApplicationCore.Interfaces.Logging;

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

                if (!applicationDbContext.ClientApi.Any())
                {
                    applicationDbContext.ClientApi.AddRange(GetClientApi());

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

        static IEnumerable<ClientApi> GetClientApi()
        {
            var clientApis = new List<ClientApi>();

            //INSERT [dbo].[ClientApi] ([Id], [DateCreated], [CreatedBy],
            //[DateModified], [ModifiedBy], [IsActive], [Name], [ClientId], [ClientSecret], [Token], [ExpiredToken])
            //VALUES (N'45dcca97-77d0-4d59-bbb6-b13c55844238',
            //CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'system',
            //CAST(N'2020-05-15T15:13:54.3966307' AS DateTime2), N'system',
            //1, N'ProjectManagement', N'ProjectManagement', N'ProjectManagement',
            //N'', CAST(N'2020-05-15T16:53:54.0000000' AS DateTime2))


            var clientApiDefault = new ClientApi {
                Name = "ProjectManagement",
                ClientId = "ProjectManagement",
                ClientSecret = "ProjectManagement",
                IsActive = true
            };
            clientApis.Add(clientApiDefault);

            return clientApis;
        }
    }
}