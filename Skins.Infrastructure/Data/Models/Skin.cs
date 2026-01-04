using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Skins.Infrastructure.Data.Constants.DbConstants.SkinConstants;

namespace Skins.Infrastructure.Data.Models;

public class Skin
{
    public Skin()
    {
        Id = Guid.NewGuid().ToString();
    }

    [Key]
    public string Id { get; set; }

    [Required]
    [MaxLength(MaxNameLength)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public float Float { get; set; }

    [Required]
    public SkinQuality Quality { get; set; }

    [Required]
    [MaxLength(MaxPatternLength)]
    public string Pattern { get; set; } = string.Empty;

    public float? MaxFloat { get; set; } 

    [NotMapped]
    public bool IsFloatCapped => MaxFloat.HasValue;

    [Required]
    public string OwnerId { get; set; } = string.Empty;

    [ForeignKey(nameof(OwnerId))]
    public User Owner { get; set; } = null!;
}