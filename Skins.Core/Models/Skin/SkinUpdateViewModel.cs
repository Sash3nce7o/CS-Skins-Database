using System.ComponentModel.DataAnnotations;
using static Skins.Infrastructure.Data.Constants.DbConstants.SkinConstants;

namespace Skins.Core.Models.Skin;

public class SkinUpdateViewModel
{
    [Range(0.0f, 1.0f, ErrorMessage = "Float must be between 0.0 and 1.0")]
    public float? Float { get; set; }

    [StringLength(MaxPatternLength, ErrorMessage = "Pattern cannot exceed the maximum allowed length.")]
    public string? Pattern { get; set; }

    [Range(0.0f, 1.0f, ErrorMessage = "Max float must be between 0.0 and 1.0 if provided.")]
    public float? MaxFloat { get; set; }
}