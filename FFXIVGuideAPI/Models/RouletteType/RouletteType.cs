using System.ComponentModel.DataAnnotations;

namespace FFXIVGuideAPI.Models.RouletteType;

public class RouletteType
{
    /// <summary>
    /// The unique identifier for the RouletteType
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The display name of the RouletteType. Must be unique.
    /// </summary>
    [Required]
    [MaxLength(64)]
    public string Name { get; set; }

    public RouletteType()
    {
        Name = string.Empty;
    }
}
