namespace POC_DotMim.Sync.Models;

public class Led
{
    public Guid LedId { get; set; }
    public Guid? TennantId { get; set; }
    public short LedEffectId { get; set; }

    public virtual Tennant? Tennant { get; set; }
    public virtual LedEffect LedEffect { get; set; }
}
