using Dotmim.Sync;
using Dotmim.Sync.Enumerations;
using Dotmim.Sync.SqlServer;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace POC_DotMim.Sync;

internal class SyncDatabases
{

    public string ServerConnectionString { get; set; }
    public string LocalConnectionString { get; set; }
    public IConfiguration _iconfiguration { get; }
    public int ErroCount { get; set; } = 0;
    private Guid _myTennantId;

    public SyncDatabases(IConfiguration iconfiguration, Guid myTennantId)
    {

        _iconfiguration = iconfiguration;
        _myTennantId = myTennantId;
        ServerConnectionString = _iconfiguration.GetConnectionString("MasterConnection");
        LocalConnectionString = _iconfiguration.GetConnectionString("LocalConnection");
    }
    public async Task SyncTest(bool debugClean = false)
    {
        try
        {

            SqlSyncProvider serverProvider = new SqlSyncChangeTrackingProvider(ServerConnectionString);
            SqlSyncProvider clientProvider = new SqlSyncChangeTrackingProvider(LocalConnectionString);

            var syncOptions = new SyncOptions { ErrorResolutionPolicy = ErrorResolution.RetryOnNextSync };

            if (debugClean)
            {
                //If there is an error, delete everything and try from scratch
                var localOrchestrator = new LocalOrchestrator(clientProvider);
                await localOrchestrator.DropAllAsync();
                await localOrchestrator.DeleteMetadatasAsync();
                var serverOrchestrator = new LocalOrchestrator(serverProvider);
                await serverOrchestrator.DropAllAsync();
                await serverOrchestrator.DeleteMetadatasAsync();
            }

            var agent = new SyncAgent(clientProvider, serverProvider, syncOptions);

            var setup = new SyncSetup("dbo.Tennants", "dbo.Audios", "dbo.Led", "dbo.LedEffect");

            setup.Tables["dbo.Tennants"].SyncDirection = SyncDirection.DownloadOnly;
            setup.Tables["dbo.Audios"].SyncDirection = SyncDirection.DownloadOnly;
            setup.Tables["dbo.Led"].SyncDirection = SyncDirection.DownloadOnly;
            setup.Tables["dbo.LedEffect"].SyncDirection = SyncDirection.DownloadOnly;


            //FILTER
            var filterTennants = new SetupFilter("Tennants");
            filterTennants.AddParameter("ID", DbType.Guid);
            filterTennants.AddWhere("TennantId", "Tennants", "ID");
            setup.Filters.Add(filterTennants);

            var filterAudios = new SetupFilter("Audios");
            filterAudios.AddParameter("ID", DbType.Guid);
            filterAudios.AddWhere("TennantId", "Audios", "ID");
            setup.Filters.Add(filterAudios);

            var filterLeds = new SetupFilter("Led");
            filterLeds.AddParameter("ID", DbType.Guid);
            filterLeds.AddWhere("TennantId", "Led", "ID");
            setup.Filters.Add(filterLeds);

            var parameters = new SyncParameters
            {
                { "ID", _myTennantId }
            };

            var progress = new SynchronousProgress<ProgressArgs>(s =>
            {
                var message = $"{s.ProgressPercentage:F0}% [{s.Source}] {s.TypeName}=> {s.Message}";
                Console.WriteLine(message);
            });

            var result = await agent.SynchronizeAsync(setup, parameters, progress);

            Console.WriteLine("Sync executed successfully");
        }
        catch (Exception)
        {
            ErroCount++;
            if (ErroCount < 2)
            {
                await SyncTest(true);
            }
            throw;
        }
    }

}
