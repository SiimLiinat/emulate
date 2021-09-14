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
    /// API Controller for Companies.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CompaniesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        
        /// <summary>
        /// API Controller for Companies.
        /// </summary>
        /// <param name="bll">Business logic layer</param>
        public CompaniesController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/Companies
        /// <summary>
        /// Get all companies.
        /// </summary>
        /// <returns>List of Companies</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Company>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            var companies = await _bll.Companies.GetAllAsync();
            return companies.Select(CompanyMapper.MapToApi).ToList();
        }
        
        // GET: api/Companies/5
        /// <summary>
        /// Get one company. Based on parameter: Id
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>Company entity from database</returns>
        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Company), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Company>> GetCompany(Guid id)
        {
            var company = await _bll.Companies.FirstOrDefaultAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return CompanyMapper.MapToApi(company);
        }
        
        // PUT: api/Companies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Put one company into database.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="company">Company entity</param>
        /// <returns>No content</returns>
        [HttpPut("{id:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutCompany(Guid id, Company company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }

            _bll.Companies.Update(CompanyMapper.MapToBll(company));
            await _bll.SaveChangesAsync();

            return NoContent();
        }
        
        // POST: api/Companies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post one company.
        /// </summary>
        /// <param name="company">Company entity</param>
        /// <returns>CreatedAtAction</returns>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Company), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Company>> PostCompany(CompanyAdd company)
        {
            var bllEntity = CompanyMapper.MapToBll(company);
            var addedEntity = _bll.Companies.Add(bllEntity);
            await _bll.SaveChangesAsync();
            var returnEntity = CompanyMapper.MapToApi(addedEntity);

            return CreatedAtAction("GetCompany", 
                new { id = returnEntity.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, company);
        }
        
        // DELETE: api/Companies/5
        /// <summary>
        /// Delete one company. Based on parameter: Id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            var company = await _bll.Companies.FirstOrDefaultAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            _bll.Companies.Remove(company);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
