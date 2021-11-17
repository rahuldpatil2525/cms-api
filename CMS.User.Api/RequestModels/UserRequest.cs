namespace CMS.User.Api.RequestModels
{
    public class UserRequest
    {
        public int UserId { get; set; }

        public string UserName { get; set; }
    }


    public class UpdateUserRequest
    {
        public string UserName { get; set; }
    }
}
