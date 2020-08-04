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

    }
}
