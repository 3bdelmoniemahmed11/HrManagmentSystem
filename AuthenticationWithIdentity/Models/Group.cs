using System.ComponentModel.DataAnnotations;
using AuthenticationWithIdentity.DTO;

namespace AuthenticationWithIdentity.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<GroupPageAction> GroupPagesAction { get; set; } 
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
