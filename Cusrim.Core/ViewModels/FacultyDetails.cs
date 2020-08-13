using Cusrim.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cusrim.Core.ViewModels
{
    public class FacultyDetails
    {
        public Faculty      Faculty{ get; set; }
        public IEnumerable<Student> Students { get; set; }
    }
}
