namespace POC_DotMim.Sync.Models;

public class Audio
{
    public Guid AudioId { get; set; }

    public Guid? TennantId { get; set; }


    public virtual Tennant? Tennant { get; set; }
}
