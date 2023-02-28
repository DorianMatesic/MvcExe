using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExeApp.Data;
using ExeApp.Models;

namespace ExeApp.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ProjectsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Projects
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? subjectId)
        {
            if (subjectId == null)
            {
                var applicationDbContext = _context.Projects.Include(p => p.Subject).Include(p => p.IdentityUser).Where(p => p.IsPrivate == false);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = _context.Projects.Include(p => p.Subject).Where(p => p.SubjectId == subjectId).Include(p => p.IdentityUser).Where(p => p.IsPrivate == false);
                return View(await applicationDbContext.ToListAsync());
            }

        }
    
        public async Task<IActionResult> MyProjects()
        {
            var user = await _userManager.GetUserAsync(User);
            var applicationDbContext = _context.Projects.Include(p => p.Subject).Include(p => p.IdentityUser).Where(p => p.IdentityUserId == user.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> MyPrivateProjects()
        {
            var user = await _userManager.GetUserAsync(User);
            var applicationDbContext = _context.Projects.Include(p => p.Subject).Include(p => p.IdentityUser).Where(p => p.IdentityUserId == user.Id && p.IsPrivate == true);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Projects/Details/5
        /*[AllowAnonymous]*/
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            /*var user = await _userManager.GetUserAsync(User);
            if (project.IdentityUserId != user.Id)
            {
                return Unauthorized();
            }
*/
            return View(project);
        }

        // GET: Projects/Create
        [Authorize(Roles = "Student")]
        public IActionResult Create()
        {
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
         [Authorize(Roles = "Student")]
        public async Task<IActionResult> Create([Bind("Id,Name,Link,GithubLink,IsPrivate,CreatedAt,SubjectId")] Project project)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                project.IdentityUserId = user.Id;
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", project.SubjectId);
            return View(project);
        }

        // GET: Projects/Edit/5
         [Authorize(Roles = "Student")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (project.IdentityUserId != user.Id)
            {
                return Unauthorized();
            }

            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", project.SubjectId);
            return View(project);
        }


        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
         [Authorize(Roles = "Student")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Link,GithubLink,IsPrivate,CreatedAt,SubjectId")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", project.SubjectId);
            return View(project);
        }

        // GET: Projects/Delete/5
         [Authorize(Roles = "Student")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (project.IdentityUserId != user.Id)
            {
                return Unauthorized();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
         [Authorize(Roles = "Student")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Project'  is null.");
            }
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
