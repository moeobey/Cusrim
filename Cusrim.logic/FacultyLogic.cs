using Cusrim.Core.Models;
using Cusrim.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cusrim.logic
{
    public class FacultyLogic
    {
        private readonly FacultyRepository _db = new FacultyRepository(new ApplicationDbContext());
        public Faculty Get(long id)
        {
            var values = _db.Get(id);
            return values;
        }
        public void Update(Faculty faculty)
        {
            _db.Save(faculty);
        }

        public void Save(Faculty faculty)
        {
            _db.Add(faculty);
            _db.Save(faculty);
        }
        public IEnumerable<Faculty> GetAll()
        {
            var values = _db.GetAll();
            return values;
        }
        public bool StaffNoIsUnique(string username)
        {
            bool isUnique = false;

            var user = _db.GetByStaffNo(username);
            if (user == null)
                isUnique = true;

            return isUnique;
        }
        public Faculty GetByUserId(long userId)
        {
            return _db.GetByUserId(userId);
        }
    }
}
