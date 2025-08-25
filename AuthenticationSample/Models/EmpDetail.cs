using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationSample.Models
{
    public class EmpDetail
    {
        public int EmpId { get; set; }

        public string EmpName { get; set; } = null!;

        public string Status { get; set; } = null!;

        public string? InMachine { get; set; }

        public int LocationID { get; set; }

        public string? LocationName { get; set; }
    }
}
