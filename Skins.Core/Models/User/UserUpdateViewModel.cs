using System.ComponentModel.DataAnnotations;
using static Skins.Infrastructure.Data.Constants.DbConstants.UserConstants;

namespace Skins.Core.Models.User

{
    public class UserUpdateViewModel
    {
        [StringLength(MaxUsernameLength)]
        public string? Username {get;set;}

        [EmailAddress]
        [StringLength(MaxEmailLength)]  
        public string? Email {get;set;}
        public string? Password { get; internal set; }
    }
}