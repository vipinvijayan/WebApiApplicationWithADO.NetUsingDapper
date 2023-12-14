using DryCleanerAppDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryCleanerAppDataAccess.IRepository
{
    public interface ISecurityRepository
    {
        Task<string> SaveRefreshToken(RefreshTokenEntity param);
        Task<bool> GetActiveStatusOfToken(string token);
    }
}
