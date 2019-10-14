namespace AlfieCodes.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterRequest
    {
        [StringLength(16, MinimumLength = 3)]
        [Required]
        public string Username { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [RegularExpression(@"^(?=.*[a-zA-Z\d].*)[a-zA-Z\d!@#$;%&*]{8,}$", ErrorMessage = "Please enter a valid password. A valid password has a minimum of 8 characters, can only contain, letters, numbers and the following special characters: !@#$;%&*")]
        [Required]
        [StringLength(60, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
