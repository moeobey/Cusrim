using Cusrim.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cusrim.Data
{
    public class ReportRepository:Repository<Report>
    {
        readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }
        public IEnumerable<Report> GetReports(long facultyId)
        {
            var report = _context.Reports.Where(u => u.FacultyId == facultyId ).Include(c=>c.Student);
            return report;
        }
        public IEnumerable<Report> GetByStudentId(long studentId)
        {
            var report = _context.Reports.Where(u => u.StudentId == studentId).Include(c => c.Student) ;
            return report;
        }
        public IEnumerable<Report> GetAllReports()
        {
            var report = _context.Reports.Include(c => c.Student);
            return report;
        }
        
    }
}
