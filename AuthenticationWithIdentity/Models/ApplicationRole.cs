using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationWithIdentity.Models
{
    public class ApplicationRole
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Role")]
        public string RoleID { get; set; }

        public virtual IdentityRole Role { get; set; }
        [ForeignKey("PageAction")]

        public int PageActionId { get; set; }
        public virtual PageAction PageAction { get; set; }
    }
}
