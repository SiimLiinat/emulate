using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using PublicApi.DTO.v1.APIs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API Controller for Configurations.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ConfigurationsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        
        /// <summary>
        /// API Controller for Configurations.
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        public ConfigurationsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Configurations
        /// <summary>
        /// Get all configurations.
        /// </summary>
        /// <returns>List of Configurations</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Configuration>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Configuration>>> GetConfigurations(Guid? userId)
        {
            var configurations = await _bll.Configurations.GetAllAsync(userId.GetValueOrDefault());
            return configurations.Select(ConfigurationMapper.MapToApi).ToList();
        }

        // GET: api/Configurations/5
        /// <summary>
        /// Get one type of configuration. Based on parameter: Id
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>Configuration entity from database</returns>
        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Configuration), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<Configuration>> GetConfiguration(Guid id)
        {
            var configuration = await _bll.Configurations.FirstOrDefaultAsync(id);
            if (configuration == null)
            {
                return NotFound();
            }

            return ConfigurationMapper.MapToApi(configuration);
        }

        // PUT: api/Configurations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put one configuration into database.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="configuration">Configuration entity</param>
        /// <returns>No content</returns>
        [HttpPut("{id:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutConfiguration(Guid id, Configuration configuration)
        {
            if (id != configuration.Id)
            {
                return BadRequest();
            }

            _bll.Configurations.Update(ConfigurationMapper.MapToBll(configuration));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Configurations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post one configuration.
        /// </summary>
        /// <param name="configuration">Configuration entity</param>
        /// <returns>CreatedAtAction</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Configuration), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Configuration>> PostConfiguration(ConfigurationAdd configuration)
        {
            var bllEntity = ConfigurationMapper.MapToBll(configuration);
            var addedEntity = _bll.Configurations.Add(bllEntity);
            await _bll.SaveChangesAsync();
            var returnEntity = ConfigurationMapper.MapToApi(addedEntity);

            return CreatedAtAction("GetConfiguration", 
                new { id = returnEntity.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, configuration);
        }

        // DELETE: api/Configurations/5
        /// <summary>
        /// Delete one configuration. Based on parameter: Id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteConfiguration(Guid id)
        {
            var configuration = await _bll.Configurations.FirstOrDefaultAsync(id);
            if (configuration == null)
            {
                return NotFound();
            }
            _bll.Configurations.Remove(configuration);
            await _bll.SaveChangesAsync();
            return NoContent();
        }
    }
}
