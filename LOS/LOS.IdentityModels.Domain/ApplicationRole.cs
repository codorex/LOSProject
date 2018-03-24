using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace LOS.Domain.Models.Entities.IdentityModels
{
    [NotMapped]
    public class ApplicationRole : IdentityRole { }
}