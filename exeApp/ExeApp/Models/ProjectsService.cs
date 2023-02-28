using System.Collections;
using Microsoft.EntityFrameworkCore;
using ExeApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExeApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;



namespace ExeApp.Models;

public class ProjectsService : IProjectsService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    public ProjectsService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public async Task<IEnumerable> AllProjects(int? subjectId)
    {
        if (subjectId == null)
        {
            var applicationDbContext = _context.Projects.Include(p => p.Subject).Include(p => p.IdentityUser).Where(p => p.IsPrivate == false);
            return await applicationDbContext.ToListAsync();
        }
        else
        {
            var applicationDbContext = _context.Projects.Include(p => p.Subject).Where(p => p.SubjectId == subjectId).Include(p => p.IdentityUser).Where(p => p.IsPrivate == false);
            return await applicationDbContext.ToListAsync();
        }

    }

    public async Task<IEnumerable> MyProjects(IdentityUser user)
    {
        var applicationDbContext = _context.Projects.Include(p => p.Subject).Include(p => p.IdentityUser).Where(p => p.IdentityUserId == user.Id);
        return await applicationDbContext.ToListAsync();
    }

    public async Task<IEnumerable> MyPrivateProjects(IdentityUser user)
    {

        var applicationDbContext = _context.Projects.Include(p => p.Subject).Include(p => p.IdentityUser).Where(p => p.IdentityUserId == user.Id && p.IsPrivate == true);
        return await applicationDbContext.ToListAsync();
    }

    public async Task<Project?> Get(int? id)
    {
        return await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
    }

    public IEnumerable<Subject> GetAllSubjects()
    {
        return _context.Subjects.ToList();
    }

    public async Task<int> Insert(Project? project)
    {
        if (project != null)
        {
            _context.Add(project);
            return await _context.SaveChangesAsync();
        }
        return -1;
    }

    public async Task<bool> Update(Project? project)
    {
        if (project != null)
        {
            try
            {
                _context.Update(project);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }
        return false;
    }

    public async Task<int> Delete(int? id)
    {
        var project = await Get(id);
        _context.Projects.Remove(project);
        return await _context.SaveChangesAsync();
    }
}

