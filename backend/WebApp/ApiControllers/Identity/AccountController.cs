using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.App.Identity;
using Extensions.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers.Identity
{
    /// <summary>
    /// API Controller for Accounts.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="signInManager">Sign-in manager - provides API for user sign in</param>
        /// <param name="userManager">User manager - provides the APIs for managing user in a persistence store</param>
        /// <param name="logger">Logger - generic interface for logging</param>
        /// <param name="configuration">Configuration</param>
        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ILogger<AccountController> logger, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
        }
        
        /// <summary>
        /// Register account.
        /// </summary>
        /// <param name="dto">Data Transfer Object</param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("register")]
        [ProducesResponseType(typeof(JwtResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] Register dto)
        {
            var appUser = await _userManager.FindByEmailAsync(dto.Email);
            if (appUser != null)
            {
                _logger.LogWarning("User {User} already registered", dto.Email);
                return BadRequest(new Message("User already registered"));
            }

            appUser = new AppUser
            {
                Email = dto.Email,
                UserName = dto.UserName
            };
            var result = await _userManager.CreateAsync(appUser, dto.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User {UserName} created a new account with password", dto.UserName);

                var user = await _userManager.FindByEmailAsync(appUser.Email);
                if (user != null)
                {
                    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
                    var jwt = IdentityExtensions.GenerateJwt(
                        claimsPrincipal.Claims,
                        _configuration["JWT:Key"],
                        _configuration["JWT:Issuer"],
                        _configuration["JWT:Issuer"],
                        DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
                    );
                    _logger.LogInformation("WebAPI login.User {User}", dto.Email);
                    return Ok(new JwtResponse
                    {
                        Token = jwt
                    });
                }

                _logger.LogInformation("User {UserName} not found after creation", appUser.UserName);
                return BadRequest(new Message("User not found after creation"));
            }

            var errors = result.Errors.Select(error => error.Description).ToList();
            return BadRequest(new Message { Messages = errors});
        }
        
        /// <summary>
        /// Log in account.
        /// </summary>
        /// <param name="dto">Data Transfer Object</param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(JwtResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
        [HttpPost("login")]
        public async Task<ActionResult<JwtResponse>> Login([FromBody] Login dto)
        {
            var appUser = await _userManager.FindByNameAsync(dto.UserName);
            if (appUser == null)
            {
                _logger.LogWarning("WebAPI login. User {User} not found", dto.UserName);
                return NotFound(new Message("User/Password error!"));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(appUser, dto.Password, false);
            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
                var jwt = IdentityExtensions.GenerateJwt(
                    claimsPrincipal.Claims,
                    _configuration["JWT:Key"],
                    _configuration["JWT:Issuer"],
                    _configuration["JWT:Issuer"],
                    DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
                    );
                _logger.LogInformation("WebAPI login.User {User}", dto.UserName);
                return Ok(new JwtResponse
                {
                    Token = jwt
                });
            }
            
            _logger.LogWarning("WebAPI login. User {User} - Bad password", dto.UserName);
            return NotFound(new Message("User/Password error!"));
        }
    }
}