using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Threading.Tasks;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace WebApp.Areas.Identity.Pages.Account
{
    /// <summary>
    /// PageModel for RegisterConfirmation
    /// </summary>
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _sender;

        /// <summary>
        /// Constructor for RegisterConfirmation PageModel
        /// </summary>
        /// <param name="userManager">Provides the APIs for managing user in a persistence store.</param>
        /// <param name="sender">API for sending emails</param>
        public RegisterConfirmationModel(UserManager<AppUser> userManager, IEmailSender sender)
        {
            _userManager = userManager;
            _sender = sender;
        }

        /// <summary>
        /// Email address string
        /// </summary>
        public string Email { get; set; } = default!;

        /// <summary>
        /// Boolean for whether to display account confirmation link
        /// </summary>
        public bool DisplayConfirmAccountLink { get; set; }
        
        /// <summary>
        /// Email confirmation url
        /// </summary>
        public string? EmailConfirmationUrl { get; set; }

        /// <summary>
        /// Gets and displays account register confirmation.
        /// </summary>
        /// <param name="email">Email address</param>
        /// <param name="returnUrl">Return url</param>
        /// <returns>Page</returns>
        public async Task<IActionResult> OnGetAsync(string email, string? returnUrl = null)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            Email = email;
            // Once you add a real email sender, you should remove this code that lets you confirm the account
            DisplayConfirmAccountLink = true;
            if (DisplayConfirmAccountLink)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                EmailConfirmationUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                    protocol: Request.Scheme);
            }

            return Page();
        }
    }
}
