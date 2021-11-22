using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.User.Api.Database;
using CMS.User.Api.Exceptions;
using Microsoft.EntityFrameworkCore;
using DbUser = CMS.User.Api.Database.Models.User;

namespace CMS.User.Api.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<DbUser>> GetUsersAsync();

        Task<DbUser> AddUserAsync(DbUser user);

        Task UpdateUserAsync(DbUser user);

        Task DeleteUserAsync(int userId);

        Task SaveAsync();
    }

    public class UserRepository : IUserRepository
    {
        private UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<DbUser> AddUserAsync(DbUser user)
        {
            if (await IsUserNameExist(user.UserName))
                throw new UserNameAlreadyExistException($"UserName already exist, UserName:{user.UserName}");

            var result = await _context.Users.AddAsync(user);

            return result.Entity;
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                throw new UserNotFoundException($"User Not Found. User Id: {userId}");

            _context.Users.Remove(user);
        }

        public async Task<IEnumerable<DbUser>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(DbUser user)
        {
            if (await IsUserNameExist(user.UserName, user.UserId))
                throw new UserNameAlreadyExistException($"UserName already exist, UserName:{user.UserName}");

            _context.Entry(user).State = EntityState.Modified;
        }

        private async Task<bool> IsUserNameExist(string userName, int? userId = null)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName && (userId == null || x.UserId != userId));

            return user is not null;

        }
    }
}
