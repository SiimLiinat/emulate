using System;
using System.Linq;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DbSet<AppRole> AppRoles { get; set; } = default!;
        public DbSet<AppUserRole> AppUserRoles { get; set; } = default!;
        public DbSet<AppUser> AppUsers { get; set; } = default!;
        public DbSet<Company> Companies { get; set; } = default!;
        public DbSet<Compatibility> Compatibilities { get; set; } = default!;
        public DbSet<CompatibilityType> CompatibilityTypes { get; set; } = default!;
        public DbSet<Configuration> Configurations { get; set; } = default!;
        public DbSet<Domain.App.Emulator> Emulators { get; set; } = default!;
        public DbSet<Game> Games { get; set; } = default!;
        public DbSet<GameGenre> GameGenres { get; set; } = default!;
        public DbSet<GameOnPlatform> GameOnPlatforms { get; set; } = default!;
        public DbSet<Genre> Genres { get; set; } = default!;
        public DbSet<Media> Medias { get; set; } = default!;
        public DbSet<MediaType> MediaTypes { get; set; } = default!;
        public DbSet<Platform> Platforms { get; set; } = default!;
        public DbSet<PlatformType> PlatformTypes { get; set; } = default!;
        public DbSet<Progress> Progresses { get; set; } = default!;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
           
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // disable cascade delete initially for everything
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<Company>()
                .HasMany(m => m.PublishedGames)
                .WithOne(o => o.PubCompany!)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}