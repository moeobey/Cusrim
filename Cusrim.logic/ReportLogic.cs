using Cusrim.Core.Models;
using Cusrim.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cusrim.logic
{
    public class ReportLogic
    {
        private readonly ReportRepository _db = new ReportRepository(new ApplicationDbContext());

        public IEnumerable<Report> GetReports(long facultyId)
        {
          return   _db.GetReports(facultyId);

        }
        public Report Get(long id)
        {
            var values = _db.Get(id);
            return values;
        }
        public void Update(Report faculty)
        {
            _db.Save(faculty);
        }

        public void Save(Report faculty)
        {
            _db.Add(faculty);
            _db.Save(faculty);
        }
        public IEnumerable<Report> GetAll()
        {
            var values = _db.GetAll();
            return values;
        }
        public IEnumerable<Report> GetByStudentId(long id)
        {
            var values = _db.GetByStudentId(id);
            return values;
        }
        

    }
}
