using T2507E_ASP.DTOs.Student;
using T2507E_ASP.Entities;

namespace T2507E_ASP.Repositories;

public interface IStudentRepository
{
    Task<(List<Student> Items,int TotalItems)> GetAllAsync(
        StudentQueryParameters parameters);
    Task<Student?> GetByIdAsync(int id);
    Task<bool> ExistsByCodeAsync(string studentCode);
    Task AddAsync(Student student);
    void Remove(Student student);
    Task<int> SaveChangesAsync();
}