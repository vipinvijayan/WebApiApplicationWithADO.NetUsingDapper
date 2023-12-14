using DryCleanerAppDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryCleanerAppBussinessLogic.Interfaces
{
    public interface ISecurityB
    {
        Task<string> GenerateJWTToken(string userName, int userId, string userAgent);
        Task<string> SaveRefreshToken(UserProfileModel param, string ipAddress);
        Task<bool> GetActiveStatusOfToken(string token);

    }
}
