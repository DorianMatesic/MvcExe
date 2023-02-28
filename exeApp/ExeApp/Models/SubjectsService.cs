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

public class SubjectsService : ISubjectsServices
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    public SubjectsService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public async Task<IEnumerable> AllSubjects()
    {

        var applicationDbContext = _context.Subjects.Include(u => u.IdentityUser).Include(p => p.Projects);
        return await applicationDbContext.ToListAsync();


    }

    public async Task<IEnumerable> Racunarstvo()
    {
        var applicationDbContext = _context.Subjects.Include(u => u.IdentityUser).Include(p => p.Projects).Where(p => p.Department == "Racunarstvo");
        return await applicationDbContext.ToListAsync();
    }

    public async Task<IEnumerable> Mehatronika()
    {
        var applicationDbContext = _context.Subjects.Include(p => p.IdentityUser).Include(p => p.Projects).Where(p => p.Department == "Mehatronika");
        return await applicationDbContext.ToListAsync();
    }

    public async Task<Subject?> Get(int? id)
    {
        return await _context.Subjects.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<int> Insert(Subject? subject)
    {
        if (subject != null)
        {
            _context.Add(subject);
            return await _context.SaveChangesAsync();
        }
        return -1;
    }

    public async Task<bool> Update(Subject? subject)
    {
        if (subject != null)
        {
            try
            {
                _context.Update(subject);
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
        var subject = await Get(id);
        _context.Subjects.Remove(subject);
        return await _context.SaveChangesAsync();
    }
}

