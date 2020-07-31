using Cusrim.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Cusrim.Data
{
    public class FacultyRepository:Repository<Faculty>
    {
        readonly ApplicationDbContext _context;

        public FacultyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }
        public Faculty GetByStaffNo(string staffNo)
        {
            var faculty = _context.Faculties.FirstOrDefault(u => u.StaffNo == staffNo );
            return faculty;

        }
    }
}
