using System.Collections.Generic;
using System.Linq;
using CMS.User.Api.CoreModels;
using DbUser = CMS.User.Api.Database.Models.User;

namespace CMS.User.Api.Mappers
{
    public interface IDbUserMapper
    {
        DbUser ToUser(UserCore userCore);

        IEnumerable<DbUser> ToUsers(IEnumerable<UserCore> usersCore);
    }
    public class DbUserMapper : IDbUserMapper
    {
        public DbUser ToUser(UserCore userCore)
        {
            if (userCore == null)
                return null;

            return new DbUser()
            {
                UserId = userCore.UserId,
                UserName = userCore.UserName
            };
        }

        public IEnumerable<DbUser> ToUsers(IEnumerable<UserCore> usersCore)
        {
            if (usersCore == null || !usersCore.Any())
                return Enumerable.Empty<DbUser>();

            return usersCore.Select(x => ToUser(x));
        }
    }
}
