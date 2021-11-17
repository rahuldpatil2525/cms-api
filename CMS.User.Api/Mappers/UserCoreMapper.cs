using System.Collections.Generic;
using System.Linq;
using CMS.User.Api.CoreModels;
using CMS.User.Api.RequestModels;
using DbUser = CMS.User.Api.Database.Models.User;

namespace CMS.User.Api.Mappers
{
    public interface IUserCoreMapper
    {
        UserCore ToUserCore(UserRequest userRequest);

        UserCore ToUserCore(UpdateUserRequest userRequest, int userId);

        UserCore ToUserCore(DbUser user);

        IEnumerable<UserCore> ToUsersCore(IEnumerable<DbUser> users);
    }

    public class UserCoreMapper : IUserCoreMapper
    {
        public UserCore ToUserCore(UserRequest userRequest)
        {
            if (userRequest == null)
                return null;

            return new UserCore()
            {
                UserId = userRequest.UserId,
                UserName = userRequest.UserName
            };
        }

        public UserCore ToUserCore(DbUser user)
        {
            if (user == null)
                return null;

            return new UserCore()
            {
                UserId = user.UserId,
                UserName = user.UserName
            };
        }

        public UserCore ToUserCore(UpdateUserRequest userRequest, int userId)
        {
            if (userRequest == null || userId <= 0)
                return null;

            return new UserCore()
            {
                UserId = userId,
                UserName = userRequest.UserName
            };
        }

        public IEnumerable<UserCore> ToUsersCore(IEnumerable<DbUser> users)
        {
            if (users == null || !users.Any())
            {
                return Enumerable.Empty<UserCore>();
            }

            return users.Select(x => ToUserCore(x));
        }
    }
}
