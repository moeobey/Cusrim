using Cusrim.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cusrim.Data
{
    public class UserRepository : Repository<User>
    {
        readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }
        public long GetLast()
        {
            return _context.User.OrderByDescending(x => x.Id).Take(1).Select(x => x.Id)
                              .ToList()
                              .FirstOrDefault();
        }
        public User GetByUserID(string UserID)
        {
            return _context.User.Where(c => c.Username == UserID).FirstOrDefault();
        }
    }
}
