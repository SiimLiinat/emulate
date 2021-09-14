using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebApp.Helpers
{
    /// <summary>
    /// Configures localization for ModelBinding
    /// </summary>
    public class ConfigureModelBindingLocalization: IConfigureOptions<MvcOptions>
    {
        /// <summary>
        /// Configures localization for ModelBinding
        /// </summary>
        /// <param name="options">Provides programmatic configuration for the MVC framework.</param>
        public void Configure(MvcOptions options)
        {
            // Kui siia midagi lisad, siis lisa service Startup.cs-i
            // services.AddSingleton<IConfigureOptions<MvcOptions>, ConfigureModelBindingLocalization>();
        }
    }
}