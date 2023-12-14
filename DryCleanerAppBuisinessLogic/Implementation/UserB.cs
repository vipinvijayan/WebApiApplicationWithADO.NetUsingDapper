using DryCleanerAppBuisinessLogic.Interfaces;
using DryCleanerAppDataAccess.Entities;
using DryCleanerAppDataAccess.IRepository;
using DryCleanerAppDataAccess.Models;
using DryCleanerAppDataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryCleanerAppBuisinessLogic.Implementation
{
    public class UserB : IUserB
    {
        readonly IUserRepository _userRepository;
        public UserB(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> RegisterUser(UserRegistrationParam param, int companyId)
        {
            UserEntity? userEntity = new UserEntity();
            userEntity.Username = param.Username;
            userEntity.Password = param.Password;
            userEntity.Email = param.Email;
            userEntity.FirstName = param.FirstName;
            userEntity.LastName = param.LastName;
            userEntity.MobileNo = param.MobileNo;
            userEntity.CompanyId = companyId;
            return await _userRepository.RegisterUser(userEntity, companyId);
        }
        public async Task<UserProfileModel?> UserLogin(UserLoginParams param, int companyId)
        {

            return await _userRepository.UserLogin(param, companyId);
        }

        public async Task<string> DeleteAllRefreshTokenOfUser(int userId)
        {

            return await _userRepository.DeleteAllRefreshTokenOfUser(userId);
        }
    }
}
