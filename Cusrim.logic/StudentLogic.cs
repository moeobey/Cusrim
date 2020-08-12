using Cusrim.Core.Models;
using Cusrim.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cusrim.logic
{
    public class StudentLogic
    {
        private readonly StudentRepository _db = new StudentRepository(new ApplicationDbContext());
        public Student Get(long id)
        {
            var values = _db.Get(id);
            return values;
        }
        public void Update(Student student)
        {
            _db.Save(student);
        }

        public void Save(Student student)
        {
            _db.Add(student);
            _db.Save(student);
        }
        public IEnumerable<Student> GetAll()
        {
            var values = _db.GetAll().OrderByDescending(x => x.Id); ;
            return values;
        }
        public bool MatricIsUnique(string username)
        {
            bool isUnique = false;

            var user = _db.GetByMaticNo(username);
            if (user == null)
                isUnique = true;

            return isUnique;
        }
        public Student GetByUserId(long userId)
        {
            return _db.GetByUserId(userId);
        }
        public IEnumerable<Student> GetSupervisees(long facultyId)
        {
            return _db.GetSupervisees(facultyId).OrderByDescending(x => x.Id); ;
        }
    }
}
