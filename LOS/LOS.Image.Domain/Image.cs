using System.ComponentModel.DataAnnotations;

namespace LOS.Image.Domain
{
    public class Image
    {
        [Key]
        public int ImageID { get; set; }
        public string Name { get; set; }
        public int ProductID { get; set; }
        public int? FileID { get; set; }
    }
}
