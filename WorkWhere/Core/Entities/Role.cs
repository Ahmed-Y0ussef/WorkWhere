using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Role:BaseEntity
    {
        public  ICollection<User> Users { get; set; }= new HashSet<User>();
    }
}
