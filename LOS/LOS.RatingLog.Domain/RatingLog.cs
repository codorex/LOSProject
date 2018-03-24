using System.ComponentModel.DataAnnotations;

namespace LOS.RatingLogModel.Domain
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
