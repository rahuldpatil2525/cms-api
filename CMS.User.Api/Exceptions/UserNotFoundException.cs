using System;

namespace CMS.User.Api.Exceptions
{
    public class UserNotFoundException : ApplicationException
    {
        public UserNotFoundException(string message) : base(message)
        {

        }
    }
}
