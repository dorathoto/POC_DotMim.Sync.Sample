using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using POC_DotMim.Sync.Models;
using System;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace POC_DotMim.Sync
{
    internal class Program
    {
        public static Guid MyTennantId { get; set; }


        static async Task Main(string[] args)
        {

            Console.WriteLine("Configuration appSettings...");
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json",
                optional: false, reloadOnChange: true);

            var _iconfiguration = builder.Build();

            MyTennantId = _iconfiguration.GetValue<Guid>("TennantId");
            //Master connection
            var masterBuilder = new DbContextOptionsBuilder<MasterDbContext>();
            var strConnMaster = _iconfiguration.GetConnectionString("MasterConnection");
            masterBuilder.UseSqlServer(strConnMaster);
            var _contextMaster = new MasterDbContext(strConnMaster);

            //local connection
            var localBuilder = new DbContextOptionsBuilder<LocalDbContext>();
            var strConnLocal = _iconfiguration.GetConnectionString("LocalConnection");
            localBuilder.UseSqlServer(strConnLocal);
            var _contextLocal = new MasterDbContext(strConnLocal);


            Console.WriteLine("Enable Change Tracking MSSQL...");
            //Enable Change Tracking MSSQL
            await EnableChangeTrackingMsSql.EnableMaster(strConnMaster);
            await EnableChangeTrackingMsSql.EnableLocal(strConnLocal);

            //Seed Random data 
            Console.WriteLine("Seed Random data...");
            var objSeed = new SampleSeedMaster();
            await objSeed.SeedCreate(_contextMaster);


            //Sync by Dotmin.Sync

            Console.WriteLine("Hello, World!");
        }
    }
}
