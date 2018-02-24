using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOS.Bussiness.Entities
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
