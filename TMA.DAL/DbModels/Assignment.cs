using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMA.DAL.DbModels
{
    public class Assignment : BaseEntity
    {
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set;}
        public DateTime CreatedAt { get; set;}
        public int UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
