using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace LOS.Bussiness.Entities.IdentityModels
{
	[NotMapped]
	public class ApplicationRole : IdentityRole { }
}