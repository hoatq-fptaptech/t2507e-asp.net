using Microsoft.EntityFrameworkCore;
using T2507E_ASP.Data;
using T2507E_ASP.Entities;

namespace T2507E_ASP.Repositories.Impl;

public class StudentRepository : IStudentRepository
{
    private readonly T2507EASPDbContext _context;
    public StudentRepository(T2507EASPDbContext context)
    {
        _context = context;
    }
    public async Task<List<Student>> GetAllAsync()
    {
        return await _context.Students.AsNoTracking()
            .OrderBy(s=>s.StudentCode)
            .ToListAsync();
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        return await  _context.Students
            .FirstOrDefaultAsync(s=>s.Id == id);
    }

    public async Task<bool> ExistsByCodeAsync(string studentCode)
    {
        return await _context.Students
            .AnyAsync(s => s.StudentCode == studentCode);
    }

    public async Task AddAsync(Student student)
    {
        await _context.Students.AddAsync(student);
    }

    public void Remove(Student student)
    {
        _context.Students.Remove(student);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}