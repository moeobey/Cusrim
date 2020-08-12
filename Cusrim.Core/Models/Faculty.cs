using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cusrim.Core.Models
{
    public class Faculty
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string StaffNo { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public long StudentCount { get; set; }

        public long? UserId { get; set; }
        public User User { get; set; }
        public bool profile_status { get; set; }
    }
}
