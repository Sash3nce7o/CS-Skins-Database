using System.ComponentModel.DataAnnotations;
using static Skins.Infrastructure.Data.Constants.DbConstants.UserConstants;

namespace Skins.Infrastructure.Data.Models;

public class User
{
    public User()
    {
        Id = Guid.NewGuid().ToString();
    }

    [Key]
    public string Id { get; set; }

    [Required]
    [MaxLength(MaxUsernameLength)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MaxLength(MaxEmailLength)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    
    public ICollection<Skin> Skins { get; set; } = new List<Skin>();
}
