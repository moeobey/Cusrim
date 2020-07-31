using Cusrim.Core.Models;
using Cusrim.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cusrim.logic
{
    public class UserLogic
    {

        private readonly UserRepository _db = new UserRepository(new ApplicationDbContext());
        public User Get(long id)
        {
            var values = _db.Get(id);
            return values;
        }
        public void Update(User user)
        {
            _db.Save(user);
        }

        public bool Save(User user)
        {
            _db.Add(user);
          return  _db.Save(user);
        }
        public IEnumerable<User> GetAll()
        {
            var values = _db.GetAll();
            return values;
        }
        public long GetLast()
        {
            return _db.GetLast();
        }
        public User GetByUserID(string userId)
        {
            return _db.GetByUserID(userId);
        }
        public bool Authenticate(User user)
        {
            bool isUser = false;
            var getUser = _db.GetByUserID(user.Username);
            if (getUser != null)
            {        if (String.CompareOrdinal(getUser.password, user.password) == 0)
                {
        
                    isUser = true;
                }
            }

            return isUser;
        }
    }
}
