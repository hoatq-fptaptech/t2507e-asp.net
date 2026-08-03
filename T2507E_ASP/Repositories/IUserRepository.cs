using T2507E_ASP.Entities;

namespace T2507E_ASP.Repositories;

public interface IUserRepository
{
    Task<User?> FindByEmailAsync(string email);
    Task CreateAsync(User user);
    Task<int> SaveChangeAsync();
}