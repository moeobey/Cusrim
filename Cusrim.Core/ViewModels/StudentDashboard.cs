using Cusrim.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cusrim.Core.ViewModels
{
    public class StudentDashboard
    {
        public Student Student { get; set; }
        public Faculty Faculty { get; set; }

        public Report Report { get; set; }
        public IEnumerable<Report> Reports { get; set; }
        public bool HasStaff { get; set; }
    }
}
