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
    public class SubjectsController : Controller
    {
        private readonly ISubjectsServices _subjectsServices;
        private readonly UserManager<IdentityUser> _userManager;

        public SubjectsController(ISubjectsServices subjectsServices, UserManager<IdentityUser> userManager)
        {
            _subjectsServices = subjectsServices;
            _userManager = userManager;
        }

        // GET: Subjects
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _subjectsServices.AllSubjects());

        }

        public async Task<IActionResult> Racunarstvo()
        {
            return View(await _subjectsServices.Racunarstvo());
        }

        public async Task<IActionResult> Mehatronika()
        {
            return View(await _subjectsServices.Mehatronika());

        }

        // GET: Subjects/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            Subject? subject;
            if ((subject = await _subjectsServices.Get(id)) != null)
            {
                return View(subject);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: Subjects/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Department,Semester")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                await _subjectsServices.Insert(subject);
                return RedirectToAction(nameof(Index));
            }
            return View(subject);

        }

        // GET: Subjects/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            Subject? subject;
            if ((subject = await _subjectsServices.Get(id)) != null)
            {
                return View(subject);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Department,Semester")] Subject subject)
        {
            if (id != subject.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if (await _subjectsServices.Update(subject) == true)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(subject);

        }

        // GET: Subjects/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            Subject? subject;
            if ((subject = await _subjectsServices.Get(id)) != null)
            {
                return View(subject);
            }
            else
            {
                return NotFound();
            }

        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _subjectsServices.Delete(id);
            return RedirectToAction(nameof(Index));

        }

    }
}
