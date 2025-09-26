using Authentication.Entities;

namespace Authentication.Services
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsync(Guid id);
        Task UpdateAsync(User user);
    }
}
