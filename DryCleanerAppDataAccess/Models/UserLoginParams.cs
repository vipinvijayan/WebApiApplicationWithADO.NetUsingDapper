using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryCleanerAppDataAccess.Models
{
    public class UserLoginParams
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int CompanyId { get; set; }
        public string UserAgent { get; set; } = string.Empty;
    }
}
