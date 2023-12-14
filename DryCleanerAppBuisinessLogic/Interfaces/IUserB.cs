using DryCleanerAppDataAccess.Entities;
using DryCleanerAppDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryCleanerAppBuisinessLogic.Interfaces
{
    public interface IUserB
    {
        Task<UserProfileModel?> UserLogin(UserLoginParams param, int companyId);
        Task<string> RegisterUser(UserRegistrationParam param, int companyId);
        Task<string> DeleteAllRefreshTokenOfUser(int userId);
    }
}
