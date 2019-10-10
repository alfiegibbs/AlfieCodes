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

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Required]
        [StringLength(60, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
