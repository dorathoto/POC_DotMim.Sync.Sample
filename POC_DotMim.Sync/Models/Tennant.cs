namespace POC_DotMim.Sync.Models;
public class Tennant
{
    public Guid TennantId { get; set; }
    public virtual ICollection<Led>? Leds { get; set; }
    public virtual ICollection<Audio>? Audios { get; set; }
}