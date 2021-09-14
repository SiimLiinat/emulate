using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(WebApp.Areas.Identity.IdentityHostingStartup))]
namespace WebApp.Areas.Identity
{
    /// <summary>
    /// Platform specific configuration that will be applied to a IWebHostBuilder when building an IWebHost.
    /// </summary>
    public class IdentityHostingStartup : IHostingStartup
    {
        /// <summary>
        /// Configure platform specific services.
        /// </summary>
        /// <param name="builder">A builder for IWebHost.</param>
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}