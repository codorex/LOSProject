using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOS.Domain.Models.Entities
{
    public class RatingLog
    {
        [Key]
        public int LogID { get; set; }
        public int ProductID { get; set; }
        public int Rating { get; set; }
        public string UserID { get; set; }
    }
}
