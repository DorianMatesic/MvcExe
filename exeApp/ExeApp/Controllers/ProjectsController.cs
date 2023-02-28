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
        private readonly IProjectsService _projectsService;
        private readonly UserManager<IdentityUser> _userManager;

        public ProjectsController(IProjectsService projectsService, UserManager<IdentityUser> userManager)
        {
            _projectsService = projectsService;
            _userManager = userManager;
        }



        // GET: Projects
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? subjectId)
        {
            return View(await _projectsService.AllProjects(subjectId));

        }

        public async Task<IActionResult> MyProjects()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(await _projectsService.MyProjects(user));
        }

        public async Task<IActionResult> MyPrivateProjects()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(await _projectsService.MyPrivateProjects(user));
        }

        // GET: Projects/Details/5
        /*[AllowAnonymous]*/
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            Project? project;
            if ((project = await _projectsService.Get(id)) != null)
            {
                return View(project);
            }
            else
            {
                return NotFound();
            }
        }

        /*var user = await _userManager.GetUserAsync(User);
        if (project.IdentityUserId != user.Id)
        {
            return Unauthorized();
        }
*/


        // GET: Projects/Create
        [Authorize(Roles = "Student")]
        public IActionResult Create()
        {
            ViewData["SubjectId"] = new SelectList(_projectsService.GetAllSubjects(), "Id", "Name");
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
                await _projectsService.Insert(project);
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectId"] = new SelectList(_projectsService.GetAllSubjects(), "Id", "Id", project.SubjectId);
            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project? project;
            if ((project = await _projectsService.Get(id)) != null)
            {
                ViewData["SubjectId"] = new SelectList(_projectsService.GetAllSubjects(), "Id", "Id", project.SubjectId);
                return View(project);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Link,GithubLink,IsPrivate,CreatedAt,SubjectId")] Project project)
        {
            var user = await _userManager.GetUserAsync(User);
            if (id != project.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if (project.IdentityUserId != user.Id)
                {
                    return Unauthorized();
                }
                else
               if (await _projectsService.Update(project) == true)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["SubjectId"] = new SelectList(_projectsService.GetAllSubjects(), "Id", "Id", project.SubjectId);
            return View(project);
        }




        // GET: Projects/Delete/5
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            Project? project;
            var user = await _userManager.GetUserAsync(User);


            if ((project = await _projectsService.Get(id)) != null)
            {
                if (project.IdentityUserId != user.Id)
                {
                    return Unauthorized();
                }
                else
                {
                    return View(project);
                }
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _projectsService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}

