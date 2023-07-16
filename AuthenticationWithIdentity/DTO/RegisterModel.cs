using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AuthenticationWithIdentity.Models;

namespace AuthenticationWithIdentity.DTO
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //[Required]
        public string UserName { get; set; }

        public string Password { get; set; }


        public int GroupId { get; set; }


    }
}
