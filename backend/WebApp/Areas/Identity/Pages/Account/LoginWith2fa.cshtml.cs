using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebApp.Areas.Identity.Pages.Account
{
    /// <summary>
    /// PageModel for LoginWith2fa
    /// </summary>
    [AllowAnonymous]
    public class LoginWith2faModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<LoginWith2faModel> _logger;

        /// <summary>
        /// Constructor for LoginWith2fa PageModel
        /// </summary>
        /// <param name="signInManager">Provides the APIs for user sign in.</param>
        /// <param name="logger">A generic interface for logging.</param>
        public LoginWith2faModel(SignInManager<AppUser> signInManager, ILogger<LoginWith2faModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        /// <summary>
        /// Input model
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; } = default!;

        /// <summary>
        /// If user should be remembered
        /// </summary>
        public bool RememberMe { get; set; }

        /// <summary>
        /// Return URL
        /// </summary>
        public string? ReturnUrl { get; set; }

        /// <summary>
        /// Class for Input model
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// TwoFactor authentication code.
            /// </summary>
            [Required]
            [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Text)]
            [Display(Name = "Authenticator code")]
            public string TwoFactorCode { get; set; } = default!;

            /// <summary>
            /// If machine should be remembered.
            /// </summary>
            [Display(Name = "Remember this machine")]
            public bool RememberMachine { get; set; }
        }

        /// <summary>
        /// Gets 2FA login page while ensuring the user has gone through the username and password screen first.
        /// </summary>
        /// <param name="rememberMe">If user should be remembered</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns>2FA Login Page</returns>
        /// <exception cref="InvalidOperationException">Unable to load two-factor authentication user.</exception>
        public async Task<IActionResult> OnGetAsync(bool rememberMe, string? returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            ReturnUrl = returnUrl;
            RememberMe = rememberMe;

            return Page();
        }

        /// <summary>
        /// Logs in user using 2FA
        /// </summary>
        /// <param name="rememberMe">If user should be remembered</param>
        /// <param name="returnUrl">Return URL</param>
        /// <returns>Page</returns>
        /// <exception cref="InvalidOperationException">Unable to load two-factor authentication user.</exception>
        public async Task<IActionResult> OnPostAsync(bool rememberMe, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            var authenticatorCode = Input.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, Input.RememberMachine);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID '{UserId}' logged in with 2fa.", user.Id);
                return LocalRedirect(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID '{UserId}' account locked out.", user.Id);
                return RedirectToPage("./Lockout");
            }
            else
            {
                _logger.LogWarning("Invalid authenticator code entered for user with ID '{UserId}'.", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return Page();
            }
        }
    }
}
