using System.Threading.Tasks;
using CMS.User.Api.Builders.Response;
using CMS.User.Api.Mappers;
using CMS.User.Api.RequestModels;
using CMS.User.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CMS.User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly IUserCoreMapper _userCoreMapper;
        private readonly IUserGateway _userGateway;
        private readonly IUserResponseBuilder _userResponseBuilder;

        public UserController(IUserGateway userGateway, IUserResponseBuilder userResponseBuilder, IUserCoreMapper userCoreMapper)
        {
            _userGateway = userGateway;
            _userResponseBuilder = userResponseBuilder;
            _userCoreMapper = userCoreMapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var users = await _userGateway.GetUsersAsync();

            return Ok(_userResponseBuilder.ToResponseUsers(users));
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] UpdateUserRequest userRequest)
        {
            var coreUserRequest = _userCoreMapper.ToUserCore(userRequest);
            var result = await _userGateway.AddUserAsync(coreUserRequest);

            if (result.HasError)
            {
                return BadRequestResponse(result.ErrorCode, result.ErrorMessage);
            }

            return Ok(_userResponseBuilder.ToResponseUser(result.User));
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult> PutAsync(int userId, [FromBody] UpdateUserRequest userRequest)
        {
            var coreUserRequest = _userCoreMapper.ToUserCore(userRequest, userId);
            var result = await _userGateway.UpdateUserAsync(coreUserRequest);

            if (result.HasError)
            {
                return BadRequestResponse(result.ErrorCode, result.ErrorMessage);
            }

            return Ok(_userResponseBuilder.ToResponseUser(result.User));
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteAsync(int userId)
        {
            var result = await _userGateway.DeleteUserAsync(userId);

            if (result.HasError)
            {
                return BadRequestResponse(result.ErrorCode, result.ErrorMessage);
            }

            return Ok();
        }
    }
}
