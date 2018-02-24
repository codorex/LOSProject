using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOS.Bussiness.Entities
{
    public class Category
    {
        public int CategoryID { get; set; }
        public int? ParentCategoryID { get; set; }
        public string Name { get; set; }
	}
}
