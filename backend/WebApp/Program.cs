using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WebApp
{
    /// <summary>
    /// Builds web host
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Calls CreateHostBuilder to build web host with pre-configured defaults.
        /// </summary>
        /// <param name="args">Array of string arguments</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Builds web host with pre-configured defaults.
        /// </summary>
        /// <param name="args">Array of string arguments</param>
        /// <returns>IHostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}