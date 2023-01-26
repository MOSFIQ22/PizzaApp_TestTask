using System.ComponentModel.DataAnnotations;

namespace EPOS.BlazorClient.Authentication
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; } = default!;
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; } = default!;
    }
}
