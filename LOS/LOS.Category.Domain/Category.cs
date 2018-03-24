namespace LOS.Category.Domain
{
    public class Category
    {
        public int CategoryID { get; set; }
        public int? ParentCategoryID { get; set; }
        public string Name { get; set; }
    }
}
