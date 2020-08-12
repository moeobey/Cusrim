using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cusrim.Core.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string password { get; set; }
        public string UserRole { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
