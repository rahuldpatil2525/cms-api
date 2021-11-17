using System;

namespace CMS.User.Api.CoreModels
{
    public class UserResult
    {
        public UserResult()
        {
            HasError = false;
        }

        public UserResult(UserCore user)
        {
            User = user;
        }

        public UserResult(string errorCode, string errorMessage)
        {
            HasError = true;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public UserResult(string errorCode, string errorMessage, Exception ex)
        {
            HasError = true;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Exception = ex;
        }

        public UserCore User { get; }
        public bool HasError { get; }

        public string ErrorCode { get; }

        public string ErrorMessage { get; }

        public Exception Exception { get; }
    }
}
