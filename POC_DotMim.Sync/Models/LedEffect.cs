using System.ComponentModel.DataAnnotations;

namespace POC_DotMim.Sync.Models;

public class LedEffect
{
    [Key]
    public short LedEffectId { get; set; }

    public  virtual ICollection<Led> Leds { get; set; }

}
