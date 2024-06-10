using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMA.DAL.DbModels
{
    public class User : BaseEntity
    {
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}
