using Microsoft.EntityFrameworkCore;
using SharedKernel.Filters;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.Persistence.DbContext;

namespace UserService.Infrastructure.Persistence.Repositories
{
    public class UserRepository(AppDbContext dbContext) : IUserRepository
    {
        public async Task AddAsync(User user)
        {
            await dbContext.AddAsync(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            User? user = await dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);

            if (user == null) return;

            dbContext.Remove(user);
        }

        public async Task<List<User>> GetAllAsync(UserFilter? userFilter = null)
        {
            IQueryable<User> query = dbContext.Users;

            if (userFilter != null)
            {
                if (!string.IsNullOrWhiteSpace(userFilter.EmailContains))
                    query = query.Where(u => u.Email!.Contains(userFilter.EmailContains));

                if (!string.IsNullOrWhiteSpace(userFilter.MobileContains))
                    query = query.Where(u => u.Mobile!.Contains(userFilter.MobileContains));

                if (!string.IsNullOrWhiteSpace(userFilter.NameContains))
                    query = query.Where(u => u.FirstName!.Contains(userFilter.NameContains) || u.LastName!.Contains(userFilter.NameContains));
            }

            return await query.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public void Update(User user)
        {
            dbContext.Update(user);
        }
    }
}
