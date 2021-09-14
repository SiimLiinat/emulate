using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BLL.App;
using DAL.App.EF;
using DAL.App.EF.Seeding;
using Domain.App.Identity;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApp
{
    /// <summary>
    /// Executed at application start
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuration dependency injection
        /// </summary>
        /// <param name="configuration">Configuration properties</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration properties
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Register dependent classes with built-in IoC container
        /// </summary>
        /// <param name="services">Service descriptors</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
            services.AddScoped<IAppBLL, AppBLL>();
            
            services.AddDatabaseDeveloperPageExceptionFilter();
            
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication()
                .AddCookie(options =>
                    {
                        options.SlidingExpiration = true;
                    }
                    )
                .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidIssuer = Configuration["JWT:Issuer"],
                            ValidAudience = Configuration["JWT:Issuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"])),
                            ClockSkew = TimeSpan.Zero
                        };
                    }
                    );
            
            services.AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddDefaultUI()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders()
                .AddRoles<AppRole>();
            
            services.AddControllersWithViews();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsAllowAll", builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            });
            
            services.AddAutoMapper
                (typeof(DAL.App.DTO.MappingProfiles.AutoMapperProfile), typeof(BLL.App.DTO.MappingProfiles.AutoMapperProfile));
            
            services.AddApiVersioning(options => {options.ReportApiVersions = true;});
            services.AddVersionedApiExplorer(options => {options.GroupNameFormat = "'v'VVV";});
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var appSupportedCultures = new[]
                {
                    new CultureInfo("et"),
                    new CultureInfo("en")
                };
                options.SupportedCultures = appSupportedCultures;
                options.SupportedUICultures = appSupportedCultures;
                options.DefaultRequestCulture = new RequestCulture("en-GB", "en");
                options.SetDefaultCulture("en-GB");
                options.RequestCultureProviders = new List<IRequestCultureProvider>()
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Configure application request pipeline
        /// </summary>
        /// <param name="app">Class that provides the mechanisms to configure an application's request pipeline.</param>
        /// <param name="env">Provides information about the web hosting environment an application is running in.</param>
        /// <param name="provider">Defines the behavior of a provider that discovers and describes API version information within an application.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            SetupAppData(app, env, Configuration);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });
            
            app.UseStaticFiles();

            app.UseCors("CorsAllowAll");
            
            app.UseRouting();

            app.UseRequestLocalization(app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>()?.Value);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapRazorPages();
            });
        }

        private static void SetupAppData(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            
            using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            
            // in case of testing, avoid setup
            if(context!.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory") return;
            
            using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
            using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();

            var logger = serviceScope.ServiceProvider.GetService<ILogger<Startup>>();
            if (logger == null)
            {
                throw new ApplicationException("Problem in services. Can't initialize logger");
            }

            if (context == null)
            {
                throw new ApplicationException("Problem in services. Can't initialize AppDbContext");
            }

            if (userManager == null || roleManager == null)
            {
                throw new ApplicationException(
                    $"Problem in services. UserManager {(userManager == null ? "null" : "ok")} RoleManager {(roleManager == null ? "null" : "ok")}");
            }

            if (configuration.GetValue<bool>("DataInitialization:DropDatabase"))
            {
                AppDataInit.DropDatabase(context, logger);
            }

            if (configuration.GetValue<bool>("DataInitialization:MigrateDatabase"))
            {
                AppDataInit.MigrateDatabase(context, logger);
            }

            if (configuration.GetValue<bool>("DataInitialization:SeedIdentity"))
            {
                AppDataInit.SeedIdentity(userManager, roleManager, logger);
            }

            if (configuration.GetValue<bool>("DataInitialization:SeedData"))
            {
                AppDataInit.SeedData(context, logger);
            }

        }
    }
}