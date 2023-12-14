using DryCleanerAppDataAccess.Entities;
using DryCleanerAppDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryCleanerAppDataAccess.IRepository
{
    public interface IUserRepository
    {
        Task<UserProfileModel?> UserLogin(UserLoginParams param, int companyId);
        Task<string> RegisterUser(UserEntity param, int companyId);
        Task<string> DeleteAllRefreshTokenOfUser(int userId);
    }
}
