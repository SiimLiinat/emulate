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
    /// API Controller for CompatibilityTypes.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CompatibilityTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        
        /// <summary>
        /// API Controller for CompatibilityTypes.
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        public CompatibilityTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/CompatibilityTypes
        /// <summary>
        /// Get all types of compatibility.
        /// </summary>
        /// <returns>List of CompatibilityTypes</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CompatibilityType>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CompatibilityType>>> GetCompatibilityTypes()
        {
            var compatibilityTypes = await _bll.CompatibilityTypes.GetAllAsync();
            return compatibilityTypes.Select(CompatibilityTypeMapper.MapToApi).ToList();
        }

        // GET: api/CompatibilityTypes/5
        /// <summary>
        /// Get one type of compatibility. Based on parameter: Id
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>CompatibilityType entity from database</returns>
        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CompatibilityType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CompatibilityType>> GetCompatibilityType(Guid id)
        {
            var compatibilityType = await _bll.CompatibilityTypes.FirstOrDefaultAsync(id);

            if (compatibilityType == null)
            {
                return NotFound();
            }

            return CompatibilityTypeMapper.MapToApi(compatibilityType);
        }

        // PUT: api/CompatibilityTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put one type of compatibility into database.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="compatibilityType">CompatibilityType entity</param>
        /// <returns>No content</returns>
        [HttpPut("{id:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutCompatibilityType(Guid id, CompatibilityType compatibilityType)
        {
            if (id != compatibilityType.Id)
            {
                return BadRequest();
            }

            _bll.CompatibilityTypes.Update(CompatibilityTypeMapper.MapToBll(compatibilityType));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/CompatibilityTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post one type of compatibility.
        /// </summary>
        /// <param name="compatibilityType">CompatibilityType entity</param>
        /// <returns>CreatedAtAction</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(CompatibilityType), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CompatibilityType>> PostCompatibilityType(CompatibilityTypeAdd compatibilityType)
        {
            var bllEntity = CompatibilityTypeMapper.MapToBll(compatibilityType);
            var addedEntity = _bll.CompatibilityTypes.Add(bllEntity);
            await _bll.SaveChangesAsync();
            var returnEntity = CompatibilityTypeMapper.MapToApi(addedEntity);

            return CreatedAtAction("GetCompatibilityType", 
                new { id = returnEntity.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, compatibilityType);
        }

        // DELETE: api/CompatibilityTypes/5
        /// <summary>
        /// Delete one type of compatibility. Based on parameter: Id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteCompatibilityType(Guid id)
        {
            var compatibilityType = await _bll.CompatibilityTypes.FirstOrDefaultAsync(id);
            if (compatibilityType == null)
            {
                return NotFound();
            }
            _bll.CompatibilityTypes.Remove(compatibilityType);
            await _bll.SaveChangesAsync();
            return NoContent();
        }
    }
}
