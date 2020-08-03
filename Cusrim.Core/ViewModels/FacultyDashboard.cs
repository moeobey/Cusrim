using Cusrim.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cusrim.Core.ViewModels
{
    public class FacultyDashboard
    {
        public Faculty Faculty { get; set; }
        public IList<Student> Students { get; set; }
        public bool HasStudent { get; set; }
    }
}
