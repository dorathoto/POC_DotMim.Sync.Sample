using Microsoft.EntityFrameworkCore;
using POC_DotMim.Sync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC_DotMim.Sync
{
    public class SampleSeedMaster
    {

        public  MasterDbContext _context { get; set; }
        public async Task SeedCreate(MasterDbContext context)
        {

            var myTennant = Program.MyTennantId;
            this._context = context;

            Insert_Tennant(myTennant);      //by my tennant
            Insert_Tennant(Guid.NewGuid()); //random data

            Insert_Audio(myTennant);
            Insert_Audio(Guid.NewGuid());

            Insert_Led(myTennant);
            Insert_Led(Guid.NewGuid());
        }

        private void Insert_Led(Guid myTennant)
        {
            var count = _context.Led.Count();
            if (count < 2)
            {
                var effect = new Models.LedEffect();
                _context.LedEffects.Add(effect);

                _context.Led.Add(new Models.Led
                {
                    TennantId = myTennant,
                    LedEffect = effect
                });
                _context.SaveChanges();
            }
        }

        private void Insert_Audio(Guid myTennant)
        {
            var count = _context.Audios.Count();
            if (count < 2)
            {
                _context.Audios.Add(new Models.Audio
                {
                    TennantId = myTennant
                });
                _context.SaveChanges();
            }
        }

        private void Insert_Tennant(Guid myTennantt)
        {
            var count = _context.Tennants.Count();
            if (count < 2)
            {
                _context.Tennants.Add(new Models.Tennant
                {
                    TennantId = myTennantt
                });
                _context.SaveChanges();
            }
        }
    }
}
