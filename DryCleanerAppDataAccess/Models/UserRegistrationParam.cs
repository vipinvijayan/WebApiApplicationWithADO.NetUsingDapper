using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryCleanerAppDataAccess.Models
{
    public class UserRegistrationParam
    {
        public string FirstName { get; set; } = string.Empty;
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        [StringLength(15)]
        public string MobileNo { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Username { get; set; }
        [Required]
        [StringLength(15)]
        public string Password { get; set; }
        public int CompanyId { get; set; }
        public string UserAgent { get; set; } = string.Empty;
    }
}
