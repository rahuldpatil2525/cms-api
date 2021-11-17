using System;
using System.Collections.Generic;
using System.Linq;
using CMS.User.Api.CoreModels;
using CMS.User.Api.ResponseModels;

namespace CMS.User.Api.Builders.Response
{
    public interface IUserResponseBuilder
    {
        UserResponse ToResponseUser(UserCore userCore);

        IEnumerable<UserResponse> ToResponseUsers(IEnumerable<UserCore> usersCore);
    }

    public class UserResponseBuilder : IUserResponseBuilder
    {
        public UserResponse ToResponseUser(UserCore userCore)
        {
            if (userCore == null)
                return null;

            return new UserResponse()
            {
                UserId = userCore.UserId,
                UserName = userCore.UserName
            };
        }

        public IEnumerable<UserResponse> ToResponseUsers(IEnumerable<UserCore> usersCore)
        {
            if(usersCore==null || !usersCore.Any())
            {
                return Enumerable.Empty<UserResponse>();
            }

            return usersCore.Select(x => ToResponseUser(x));
        }
    }
}
