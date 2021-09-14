using System;
using System.Linq;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.App.EF.Seeding
{
    public static class AppDataInit
    {
        public static void DropDatabase(AppDbContext context, ILogger logger)
        {
            logger.LogInformation("DropDatabase");
            context.Database.EnsureDeleted();
        }
        
        public static void MigrateDatabase(AppDbContext context, ILogger logger)
        {
            logger.LogInformation("MigrateDatabase");
            context.Database.Migrate();
        }

        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,
            ILogger logger)
        {
            logger.LogInformation("SeedIdentity");
            foreach (var roleName in InitialData.Roles)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;
                if (role == null)
                {
                    role = new AppRole
                    {
                        Name = roleName
                    };

                    var result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        Console.WriteLine("Errors:");
                        Console.Write(result.Errors);
                        throw new ApplicationException("Role creation failed");
                    }
                    logger.LogInformation("Seeded role {Role}", roleName);
                }
            }

            foreach (var userInfo in InitialData.Users)
            {
                var user = userManager.FindByNameAsync(userInfo.name).Result;
                if (user == null)
                {
                    user = new AppUser
                    {
                        UserName = userInfo.name,
                        Email = userInfo.email
                    };

                    var result = userManager.CreateAsync(user, userInfo.password).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("User creation failed");
                    }
                    logger.LogInformation("Seeded user {User}", userInfo.name);
                }
                
                var roleResult = userManager.AddToRolesAsync(user, userInfo.roles).Result;
            }
        }
        
        public static void SeedData(AppDbContext context, ILogger logger)
        {
            SeedCompanies(context, logger);
            SeedCompatibilityTypes(context, logger);
            SeedGenres(context, logger);
            SeedPlatformTypes(context, logger);
            SeedPlatforms(context, logger);
        }
        
        private static void SeedCompanies(AppDbContext context, ILogger logger)
        {
            if (context.Companies.Any())
            {
                logger.LogInformation("SeedOrganisations - existing data found");
                return;
            }
            logger.LogInformation("SeedOrganisations");

            foreach (var compData in InitialData.Companies)
            {
                var company = new Company()
                {
                    Name = compData
                };

                context.Companies.Add(company);
            }

            context.SaveChanges();
        }
        
        private static void SeedCompatibilityTypes(AppDbContext context, ILogger logger)
        {
            if (context.CompatibilityTypes.Any())
            {
                logger.LogInformation("SeedOrganisations - existing data found");
                return;
            }
            logger.LogInformation("SeedOrganisations");

            foreach (var (type, description, rating) in InitialData.CompatibilityTypes)
            {
                var compatibilityType = new CompatibilityType()
                {
                    Type = type,
                    Description = description,
                    Rating = rating
                };

                context.CompatibilityTypes.Add(compatibilityType);
            }

            context.SaveChanges();
        }
        
        private static void SeedGenres(AppDbContext context, ILogger logger)
        {
            if (context.Genres.Any())
            {
                logger.LogInformation("SeedOrganisations - existing data found");
                return;
            }
            logger.LogInformation("SeedOrganisations");

            foreach (var genData in InitialData.Genres)
            {
                var genre = new Genre()
                {
                    Type = genData
                };

                context.Genres.Add(genre);
            }

            context.SaveChanges();
        }
        
        private static void SeedPlatformTypes(AppDbContext context, ILogger logger)
        {
            if (context.PlatformTypes.Any())
            {
                logger.LogInformation("SeedOrganisations - existing data found");
                return;
            }
            logger.LogInformation("SeedOrganisations");

            foreach (var platTypeData in InitialData.PlatformTypes)
            {
                var platType = new PlatformType()
                {
                    Type = platTypeData
                };

                context.PlatformTypes.Add(platType);
            }

            context.SaveChanges();
        }
        
        private static void SeedPlatforms(AppDbContext context, ILogger logger)
        {
            if (context.Platforms.Any())
            {
                logger.LogInformation("SeedOrganisations - existing data found");
                return;
            }
            logger.LogInformation("SeedOrganisations");

            foreach (var platData in InitialData.Platforms)
            {
                var plat = new Platform()
                {
                    Name = platData.name,
                    Code = platData.code
                };
                
                var company = context.Companies.FirstOrDefault(c => c.Name == platData.company);
                var platformType = context.PlatformTypes.FirstOrDefault(p => p.Type == platData.platformType);

                if (company != null && platformType != null)
                {
                    plat.Company = company;
                    plat.PlatformType = platformType;
                    context.Platforms.Add(plat);
                }
            }

            context.SaveChanges();
        }
    }
}