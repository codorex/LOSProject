namespace LOS.Domain.Models.Entities
{
    public class File
    {
        public int FileID { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public long Length { get; set; }
        public byte[] Content { get; set; }
    }
}
