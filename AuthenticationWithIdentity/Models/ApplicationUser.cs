using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationWithIdentity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int IsAdmin { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public virtual Group? Group { get; set; }
    }
}
