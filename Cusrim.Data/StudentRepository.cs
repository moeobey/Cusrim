using Cusrim.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;


namespace Cusrim.Data
{
    public class StudentRepository:Repository<Student>
    {
        readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }
        public Student GetByMaticNo(string matricNo)
        {
            var student = _context.Students.FirstOrDefault(u => u.MatricNo == matricNo);
            return student;

        }
        
    }
}
