using System.ComponentModel.DataAnnotations;

namespace LOS.RatingLog.Domain
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
