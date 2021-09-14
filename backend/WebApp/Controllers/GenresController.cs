using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using DAL.App.DTO;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controller for Genres.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class GenresController : Controller
    {
        private readonly IAppUnitOfWork _uow;
        
        /// <summary>
        /// Controller for Genres.
        /// </summary>
        /// <param name="uow">Unit of Work</param>
        public GenresController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Genres
        /// <summary>
        /// Get all genres.
        /// </summary>
        /// <returns>List of Genres</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var res = await _uow.Genres.GetAllAsync();
            await _uow.SaveChangesAsync();
            return View(res);
        }

        // GET: Genres/Details/5
        /// <summary>
        /// Get one genre. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of genre to retrieve, Guid</param>
        /// <returns>genre entity from database</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _uow.Genres.FirstOrDefaultAsync(id.Value);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // GET: Genres/Create
        /// <summary>
        /// Get genre for create.
        /// </summary>
        /// <returns>View of genre</returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create a genre.
        /// </summary>
        /// <param name="genre">Genre entity</param>
        /// <returns>View of genre</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _uow.Genres.Add(genre);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Genres/Edit/5
        /// <summary>
        /// Get genre by id for edit.
        /// </summary>
        /// <param name="id">Id of genre to edit, Guid</param>
        /// <returns>View of genre</returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _uow.Genres.FirstOrDefaultAsync(id.Value);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edit a genre.
        /// </summary>
        /// <param name="id">Id of genre to edit, Guid</param>
        /// <param name="genre">Genre entity</param>
        /// <returns>View of genre</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Genre genre)
        {
            if (id != genre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Genres.Update(genre);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Genres/Delete/5
        /// <summary>
        /// Get genre by id for delete.
        /// </summary>
        /// <param name="id">Id of genre to delete, Guid</param>
        /// <returns>View of genre</returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _uow.Genres.FirstOrDefaultAsync(id.Value);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // POST: Genres/Delete/5
        /// <summary>
        /// Delete genre.
        /// </summary>
        /// <param name="id">Id of genre to delete, Guid</param>
        /// <returns>Redirect</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Genres.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
