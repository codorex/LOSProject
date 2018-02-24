using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOS.Bussiness.Entities
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
