using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace POC_DotMim.Sync.Models;

public class MasterDbContext : DbContext
{
    private string? strConn;

    public MasterDbContext()
    {

    }
    public MasterDbContext(string? strConn)
    {
        this.strConn = strConn;
    }



    public DbSet<Tennant> Tennants { get; set; }
    public DbSet<Audio> Audios { get; set; }
    public DbSet<Led> Led { get; set; }
    public DbSet<LedEffect> LedEffects { get; set; }




    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (string.IsNullOrEmpty(strConn))
        {
            var builder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json",
              optional: false, reloadOnChange: true);

            var _iconfiguration = builder.Build();
            strConn = _iconfiguration.GetConnectionString("MasterConnection");

        }
        optionsBuilder.UseSqlServer(strConn);
    }

}