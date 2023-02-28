using System.Collections;

namespace ExeApp.Models;

public interface IProjectsService
{
    public Task<IEnumerable> AllProjects(int? subjectId);
    public Task<IEnumerable> MyProjects(IdentityUser user);
    public Task<IEnumerable> MyPrivateProjects(IdentityUser user);
    public Task<Project?> Get(int? id);
    public IEnumerable<Subject> GetAllSubjects();
    public Task<int> Insert(Project? project);
    public Task<bool> Update(Project? project);
    public Task<int> Delete(int? id);
   
}