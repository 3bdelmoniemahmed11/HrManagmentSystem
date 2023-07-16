using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationWithIdentity.Models
{
    public class GroupPageAction
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public virtual Group? Group { get; set; }
        [ForeignKey("Page")]
        public int pageId { get; set; }
        public virtual Page? Page { get; set; }

        [ForeignKey("PageAction")]
        public int pageActionId { get; set; }
        public virtual PageAction? PageAction { get; set; }
    }
}
