using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryCleanerAppDataAccess.Models
{
    public class RefreshTokenModel
    {
        public int UserId { get; set; }
        [MaxLength(300)]
        public string RefreshToken { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        [MaxLength(30)]
        public string? CreatedByID { get; set; }
        [MaxLength(300)]
        public string? ReplaceByToken { get; set; }
        public int CompanyId { get; set; }
        public string CreatedByIp { get; set; }
    }
}
