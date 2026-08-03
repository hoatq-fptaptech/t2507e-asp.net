using Microsoft.EntityFrameworkCore;
using T2507E_ASP.Data;
using T2507E_ASP.Entities;

namespace T2507E_ASP.Repositories.Impl;

public class UserRepository : IUserRepository
{
    private readonly T2507EASPDbContext _context;
    public UserRepository(T2507EASPDbContext context)
    {
        _context = context;
    }
    public async Task<User?> FindByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<int> SaveChangeAsync()
    {
        return await _context.SaveChangesAsync();
    }
}