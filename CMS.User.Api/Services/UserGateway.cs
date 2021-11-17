using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.User.Api.CoreModels;
using CMS.User.Api.Database;
using CMS.User.Api.Exceptions;
using CMS.User.Api.Mappers;
using CMS.User.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CMS.User.Api.Services
{
    public interface IUserGateway
    {
        Task<IEnumerable<UserCore>> GetUsersAsync();

        Task<UserResult> AddUserAsync(UserCore userRequest);

        Task<UserResult> UpdateUserAsync(UserCore userRequest);

        Task<UserResult> DeleteUserAsync(int userId);
    }

    public class UserGateway : IUserGateway
    {
        private readonly IUserRepository _userRepository;
        private readonly IDbUserMapper _dbUserMapper;
        private readonly IUserCoreMapper _userCoreMapper;

        public UserGateway(IUserRepository userRepository, IDbUserMapper dbUserMapper, IUserCoreMapper userCoreMapper)
        {
            _userRepository = userRepository;
            _dbUserMapper = dbUserMapper;
            _userCoreMapper = userCoreMapper;
        }

        public async Task<UserResult> AddUserAsync(UserCore userRequest)
        {
            var userToAdd = _dbUserMapper.ToUser(userRequest);

            var userAdded = await _userRepository.AddUserAsync(userToAdd);

            await _userRepository.SaveAsync();

            return new UserResult(_userCoreMapper.ToUserCore(userAdded));
        }

        public async Task<IEnumerable<UserCore>> GetUsersAsync()
        {
            var users = await _userRepository.GetUsersAsync();

            return _userCoreMapper.ToUsersCore(users);
        }

        public async Task<UserResult> UpdateUserAsync(UserCore userRequest)
        {
            try
            {
                var userToUpdate = _dbUserMapper.ToUser(userRequest);

                await _userRepository.UpdateUserAsync(userToUpdate);

                await _userRepository.SaveAsync();

                return new UserResult(_userCoreMapper.ToUserCore(userToUpdate));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new UserResult("Concurrency-NotFound", ex.Message, ex);
            }
            // Handle specific exception here
        }

        public async Task<UserResult> DeleteUserAsync(int userId)
        {
            try
            {
                await _userRepository.DeleteUserAsync(userId);
                await _userRepository.SaveAsync();

                return new UserResult();
            }
            catch (UserNotFoundException ex)
            {
                return new UserResult("UserNotFound", ex.Message, ex);
            }

        }
    }
}
