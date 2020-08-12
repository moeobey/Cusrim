using Cusrim.Core.Models;
using Cusrim.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cusrim.logic
{
    public class CompanyLogic
    {
        private readonly CompanyRepository _db = new CompanyRepository(new ApplicationDbContext());
        public Company Get(long id)
        {
            var values = _db.Get(id);
            return values;
        }
        public void Update(Company company)
        {
            _db.Save(company);
        }

        public void Save(Company company)
        {
            _db.Add(company);
            _db.Save(company);
        }
        public IEnumerable<Company> GetAll()
        {
            var values = _db.GetAll().OrderByDescending(x => x.Id); 
            return values;
        }
      
    }
}
