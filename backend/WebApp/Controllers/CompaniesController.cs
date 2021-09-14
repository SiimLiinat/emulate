using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using DAL.App.DTO;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller for Companies.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class CompaniesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        /// <summary>
        /// Controller for Companies.
        /// </summary>
        /// <param name="uow">Unit of Work</param>
        public CompaniesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Companies
        /// <summary>
        /// Get all companies.
        /// </summary>
        /// <returns>List of Companies</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var res = await _uow.Companies.GetAllAsync();
            return View(res);
        }

        // GET: Companies/Details/5
        /// <summary>
        /// Get one company. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of company to retrieve, Guid</param>
        /// <returns>Company entity from database</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _uow.Companies.FirstOrDefaultAsync(id.Value);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        /// <summary>
        /// Get company for create.
        /// </summary>
        /// <returns>View of company</returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create a company.
        /// </summary>
        /// <param name="company">Company entity</param>
        /// <returns>View of company</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Company company)
        {
            if (ModelState.IsValid)
            {
                _uow.Companies.Add(company);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Companies/Edit/5
        /// <summary>
        /// Get company by id for edit.
        /// </summary>
        /// <param name="id">Id of company to edit, Guid</param>
        /// <returns>View of company</returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _uow.Companies.FirstOrDefaultAsync(id.Value);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edit a company.
        /// </summary>
        /// <param name="id">Id of company to edit, Guid</param>
        /// <param name="company">Company entity</param>
        /// <returns>View of company</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Companies.Update(company);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Companies/Delete/5
        /// <summary>
        /// Get company by id for delete.
        /// </summary>
        /// <param name="id">Id of company to delete, Guid</param>
        /// <returns>View of company</returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _uow.Companies.FirstOrDefaultAsync(id.Value);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        /// <summary>
        /// Delete company.
        /// </summary>
        /// <param name="id">Id of company to delete, Guid</param>
        /// <returns>Redirect</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Companies.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
