using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1.APIs;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API Controller for Compatibilities.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CompatibilitiesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// API Controller for Compatibilities.
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        public CompatibilitiesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Compatibilities
        /// <summary>
        /// Get all compatibilities.
        /// </summary>
        /// <returns>List of Compatibilities</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Compatibility>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Compatibility>>> GetCompatibilities(Guid? gameId)
        {
            var compatibilities = await _bll.Compatibilities.GetAllCompatibilities(gameId);
            return compatibilities.Select(CompatibilityMapper.MapToApi).ToList();
        }

        // GET: api/Compatibilities/5
        /// <summary>
        /// Get one compatibility. Based on parameter: Id
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>Compatibility entity from database</returns>
        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Compatibility), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Compatibility>> GetCompatibility(Guid id)
        {
            var compatibility = await _bll.Compatibilities.FirstOrDefaultAsync(id);

            if (compatibility == null)
            {
                return NotFound();
            }

            return CompatibilityMapper.MapToApi(compatibility);
        }

        // PUT: api/Compatibilities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put one compatibility into database.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="compatibility">Compatibility entity</param>
        /// <returns>No content</returns>
        [HttpPut("{id:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutCompatibility(Guid id, Compatibility compatibility)
        {
            if (id != compatibility.Id)
            {
                return BadRequest();
            }
            _bll.Compatibilities.Update(CompatibilityMapper.MapToBll(compatibility));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Compatibilities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post one compatibility.
        /// </summary>
        /// <param name="compatibility">Compatibility entity</param>
        /// <returns>CreatedAtAction</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Compatibility), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Compatibility>> PostCompatibility(CompatibilityAdd compatibility)
        {
            var bllEntity = CompatibilityMapper.MapToBll(compatibility);
            var addedEntity = _bll.Compatibilities.Add(bllEntity);
            await _bll.SaveChangesAsync();
            var returnEntity = CompatibilityMapper.MapToApi(addedEntity);

            return CreatedAtAction("GetCompatibility", 
                new { id = returnEntity.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, compatibility);
        }

        // DELETE: api/Compatibilities/5
        /// <summary>
        /// Delete one compatibility. Based on parameter: Id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteCompatibility(Guid id)
        {
            var compatibility = await _bll.Compatibilities.FirstOrDefaultAsync(id);
            if (compatibility == null)
            {
                return NotFound();
            }

            _bll.Compatibilities.Remove(compatibility);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
