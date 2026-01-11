using System.ComponentModel.DataAnnotations;
using static Skins.Infrastructure.Data.Constants.DbConstants.SkinConstants;

namespace Skins.Core.Models.Skin;

public class SkinCreateViewModel
{
    [Required(ErrorMessage = "Skin name is required.")]
    [StringLength(MaxNameLength, ErrorMessage = "Skin name cannot exceed the maximum length.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Float value is required.")]
    [Range(0.0f, 1.0f, ErrorMessage = "Float must be between 0.0 and 1.0")]
    public float Float { get; set; }

    [Required(ErrorMessage = "Pattern is required.")]
    [StringLength(MaxPatternLength, ErrorMessage = "Pattern cannot exceed the maximum length.")]
    public string Pattern { get; set; } = string.Empty;

    [Range(0.0f, 1.0f, ErrorMessage = "Max float must be between 0.0 and 1.0 if provided.")]
    public float? MaxFloat { get; set; }

    [Required(ErrorMessage = "Owner ID is required.")]
    public string OwnerId { get; set; } = string.Empty;
}