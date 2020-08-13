using Cusrim.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cusrim.Core.ViewModels
{
    public class AdminDashboard
    {
        public IEnumerable<Faculty> Faculties  { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Report> Report { get; set; }
        public IEnumerable<Company> Company { get; set; }
        public Company company { get; set; }
        public Student  Student{ get; set; }
        public Faculty Faculty { get; set; }


    }
}
