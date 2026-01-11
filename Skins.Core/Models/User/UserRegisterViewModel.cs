using System.ComponentModel.DataAnnotations;
using static Skins.Infrastructure.Data.Constants.DbConstants.UserConstants;

namespace Skins.Core.Models.User;

public class UserRegisterViewModel
{
    [Required(ErrorMessage = "Username is required.")]
    [StringLength(
        MaxUsernameLength,
        ErrorMessage = "Username must be between {2} and {1} characters.",
        MinimumLength = 3)]
    [RegularExpression("[A-Za-z0-9_-]+", ErrorMessage = "Username can only contain letters, numbers, underscores, and hyphens.")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [StringLength(MaxEmailLength, ErrorMessage = $"Email cannot exceed Max Email Length characters.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(250, ErrorMessage = "Password must be between {2} and {1} characters.", MinimumLength = 5)]
    public string Password { get; set; } = string.Empty;
}
