using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryCleanerAppDataAccess.Models
{
    public class CommonSearchParam
    {
        public string? Keyword { get; set; }
        public bool TakeAll { get; set; } = true;
        public int SkipCount { get; set; } = 0;
        public int TakeCount { get; set; } = 50;
    }
}
