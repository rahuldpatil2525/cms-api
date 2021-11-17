using System;

namespace CMS.User.Api.Exceptions
{
    public class UserNotFoundException : ApplicationException
    {
        public UserNotFoundException(string message) : base(message)
        {

        }
    }

    public class UserNameAlreadyExistException : ApplicationException
    {
        public UserNameAlreadyExistException(string message) : base(message)
        {

        }
    }
}
