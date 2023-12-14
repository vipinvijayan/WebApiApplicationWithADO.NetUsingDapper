using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryCleanerAppDataAccess.Models
{
    public class UserProfileModel
    {
        public int Id { get; set; }
        [StringLength(100)]
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        [StringLength(15)]
        public string MobileNo { get; set; } = string.Empty;
        public int CreatedOn { get; set; } = (int)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds;
        [Required]
        [StringLength(100)]
        public string Username { get; set; }
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }
        public string AccessToken { get; set; }


    }
}
