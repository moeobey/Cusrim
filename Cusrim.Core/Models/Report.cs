using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cusrim.Core.Models
{
    public class Report
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public long? StudentId { get; set; }

        public Student Student { get; set; }

        public long? FacultyId{get;set;}

        public Faculty Faculty { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
