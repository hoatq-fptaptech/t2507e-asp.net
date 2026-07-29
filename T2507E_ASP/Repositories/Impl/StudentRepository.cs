using Microsoft.EntityFrameworkCore;
using T2507E_ASP.Data;
using T2507E_ASP.DTOs.Student;
using T2507E_ASP.Entities;

namespace T2507E_ASP.Repositories.Impl;

public class StudentRepository : IStudentRepository
{
    private readonly T2507EASPDbContext _context;
    public StudentRepository(T2507EASPDbContext context)
    {
        _context = context;
    }

    public async Task<(List<Student> Items, int TotalItems)> GetAllAsync(
        StudentQueryParameters parameters)
    {
        IQueryable<Student> query = _context.Students.AsNoTracking();
        query = ApplySearch(query, parameters.Keyword);
        query = ApplySorting(query,parameters.SortBy,
            parameters.SortDirection);
        var totalItems = await query.CountAsync();
        var skip = (parameters.Page - 1) * parameters.PageSize;
        var items = await query.Skip(skip)
                        .Take(parameters.PageSize)
                        .ToListAsync();
        return (items, totalItems);
    }

    private static IQueryable<Student> ApplySearch(
        IQueryable<Student> query, string? keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return query;
        }
        var normalizedKeyword = keyword.Trim().ToUpper();
        return query.Where(s=>
            s.Name.ToUpper().Contains(normalizedKeyword) ||
            s.Email.ToUpper().Contains(normalizedKeyword) || 
            s.StudentCode.ToUpper().Contains(normalizedKeyword)
            );
    }

    private static IQueryable<Student> ApplySorting(
        IQueryable<Student> query, string sortBy,
        string sortDirection
    )
    {
        var descending = sortDirection.ToUpper().Equals("DESC");
        return sortBy.Trim().ToLowerInvariant() switch
        {
            "name" => descending
                ? query.OrderByDescending(s => s.Name)
                    .ThenByDescending(s => s.StudentCode)
                : query.OrderBy(s => s.Name)
                    .ThenBy(s => s.StudentCode),
            _ => descending ? query.OrderByDescending(s => s.StudentCode)
                : query.OrderBy(s => s.StudentCode)
        };
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