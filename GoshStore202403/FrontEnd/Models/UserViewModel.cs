using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class UserViewModel
    {
        [Required]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }


        public bool RememberLogin { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
