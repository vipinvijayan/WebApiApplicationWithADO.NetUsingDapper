using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryCleanerAppDataAccess.Entities
{
    public class RefreshTokenEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [MaxLength(300)]
        public string RefreshToken { get; set; }
        public DateTime Expires { get; set; }
        public DateTime CreatedOn { get; set; }
        [MaxLength(30)]
        public string? CreatedByID { get; set; }
        [MaxLength(300)]
        public string? ReplaceByToken { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public int CompanyId { get; set; }

    }
}
