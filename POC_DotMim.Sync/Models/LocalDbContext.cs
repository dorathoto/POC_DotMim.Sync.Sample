using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace POC_DotMim.Sync.Models;

public class LocalDbContext : DbContext
{
    private string? strConnLocal;

    public LocalDbContext()
    {

    }
    public LocalDbContext(string? strConnLocal)
    {
        this.strConnLocal = strConnLocal;
    }


    public DbSet<Tennant> Tennants { get; set; }
    public DbSet<Audio> Audios { get; set; }
    public DbSet<Led> Led { get; set; }
    public DbSet<LedEffect> LedEffects { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (string.IsNullOrEmpty(strConnLocal))
        {
            var builder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json",
              optional: false, reloadOnChange: true);

            var _iconfiguration = builder.Build();
            strConnLocal = _iconfiguration.GetConnectionString("LocalConnection");

        }
        optionsBuilder.UseSqlServer(strConnLocal);
    }

}