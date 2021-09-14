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
    /// API Controller for Emulators.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmulatorsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        
        /// <summary>
        /// API Controller for Emulators.
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        public EmulatorsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Emulators
        /// <summary>
        /// Get all emulators.
        /// </summary>
        /// <returns>List of Emulators</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Emulator>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Emulator>>> GetEmulators()
        {
            var emulators = await _bll.Emulators.GetAllAsync();
            return emulators.Select(EmulatorMapper.MapToApi).ToList();
        }

        // GET: api/Emulators/5
        /// <summary>
        /// Get one type of emulator. Based on parameter: Id
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>Emulator entity from database</returns>
        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Emulator), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<Emulator>> GetEmulator(Guid id)
        {
            var emulator = await _bll.Emulators.FirstOrDefaultAsync(id);

            if (emulator == null)
            {
                return NotFound();
            }

            return EmulatorMapper.MapToApi(emulator);
        }

        // PUT: api/Emulators/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put one emulator into database.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="emulator">Emulator entity</param>
        /// <returns>No content</returns>
        [HttpPut("{id:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutEmulator(Guid id, Emulator emulator)
        {
            if (id != emulator.Id)
            {
                return BadRequest();
            }

            _bll.Emulators.Update(EmulatorMapper.MapToBll(emulator));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Emulators
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post one emulator.
        /// </summary>
        /// <param name="emulator">Emulator entity</param>
        /// <returns>CreatedAtAction</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Emulator), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<BLL.App.DTO.Emulator>> PostEmulator(EmulatorAdd emulator)
        {
            var bllEntity = EmulatorMapper.MapToBll(emulator);
            var addedEntity = _bll.Emulators.Add(bllEntity);
            await _bll.SaveChangesAsync();
            var returnEntity = EmulatorMapper.MapToApi(addedEntity);

            return CreatedAtAction("GetEmulator", 
                new { id = returnEntity.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, emulator);
        }

        // DELETE: api/Emulators/5
        /// <summary>
        /// Delete one emulator. Based on parameter: Id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteEmulator(Guid id)
        {
            var emulator = await _bll.Emulators.FirstOrDefaultAsync(id);
            if (emulator == null)
            {
                return NotFound();
            }
            _bll.Emulators.Remove(emulator);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
