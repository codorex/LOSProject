using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace LOS.IdentityModels.Domain
{
    [NotMapped]
    public class ApplicationRole : IdentityRole { }
}