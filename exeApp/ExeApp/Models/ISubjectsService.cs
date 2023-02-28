using System.Collections;

namespace ExeApp.Models;

public interface ISubjectsServices
{
    public Task<IEnumerable> AllSubjects();
    public Task<IEnumerable> Racunarstvo();
    public Task<IEnumerable> Mehatronika();
    public Task<Subject?> Get(int? id);
    public Task<int> Insert(Subject? subject);
    public Task<bool> Update(Subject?sSubject);
    public Task<int> Delete(int? id);
   
}