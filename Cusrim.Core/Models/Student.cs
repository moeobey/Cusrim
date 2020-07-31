using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cusrim.Core.Models
{
    public class Student
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string MatricNo { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string Level { get; set; }
        public string Grade { get; set; }
        public long? UserId { get; set; }
        public User User { get; set; }

        public long? FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        public bool profile_status { get; set; }

    }
}
