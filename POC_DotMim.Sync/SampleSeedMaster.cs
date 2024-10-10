using Microsoft.EntityFrameworkCore;
using POC_DotMim.Sync.Models;

namespace POC_DotMim.Sync;

public class SampleSeedMaster
{

    public MasterDbContext _context { get; set; }
    public async Task SeedCreate(MasterDbContext context)
    {

        var myTennant = Program.MyTennantId;
        this._context = context;

        await Insert_Tennant(myTennant);      //by my tennant
        await Insert_Tennant(Guid.NewGuid()); //random data
    }

    private async Task Insert_Tennant(Guid myTennant)
    {
        var exist = await _context.Tennants.AnyAsync(v => v.TennantId == myTennant);
        if (!exist)
        {
            _context.Tennants.Add(new Models.Tennant
            {
                TennantId = myTennant
            });

            await _context.SaveChangesAsync();
            await Insert_Audio(myTennant);
            await Insert_Led(myTennant);
            await _context.SaveChangesAsync();
        }
    }

    //------------------------------------------------------------------------------------

    private async Task Insert_Audio(Guid myTennant)
    {
        var exist = await _context.Audios.AnyAsync(v => v.TennantId == myTennant);
        if (!exist)
        {
            _context.Audios.Add(new Models.Audio
            {
                TennantId = myTennant
            });
        }
    }

    private async Task Insert_Led(Guid myTennant)
    {
        var exist = await _context.Led.AnyAsync(v => v.TennantId == myTennant);
        if (!exist)
        {
            var effect = new Models.LedEffect();
            _context.LedEffect.Add(effect);

            _context.Led.Add(new Models.Led
            {
                TennantId = myTennant,
                LedEffect = effect
            });
        }
    }

}
