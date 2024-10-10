using Microsoft.Data.SqlClient;

namespace POC_DotMim.Sync;

public class EnableChangeTrackingMsSql
{

    public static async Task EnableMaster(string strConnMaster)
    {
        using var connMaster = new SqlConnection(strConnMaster);
        try
        {
            var commMaster = "ALTER DATABASE [SqlMaster] SET CHANGE_TRACKING = ON (CHANGE_RETENTION = 14 DAYS, AUTO_CLEANUP = ON)";
            using var cmdMaster = new SqlCommand(commMaster, connMaster);
            connMaster.Open();
            await cmdMaster.ExecuteNonQueryAsync();
        }
        catch (Exception)
        {
            Console.WriteLine("Change Tracking was already enabled");
        }
        finally
        {
            await connMaster.CloseAsync();
        }
    }

    internal static async Task EnableLocal(string? strConnLocal)
    {
        using var connMaster = new SqlConnection(strConnLocal);
        try
        {
            var commMaster = "ALTER DATABASE [SqlLocal] SET CHANGE_TRACKING = ON (CHANGE_RETENTION = 14 DAYS, AUTO_CLEANUP = ON)";
            using var cmdMaster = new SqlCommand(commMaster, connMaster);
            connMaster.Open();
            await cmdMaster.ExecuteNonQueryAsync();
        }
        catch (Exception)
        {
            Console.WriteLine("Change Tracking was already enabled");
        }
        finally
        {
            await connMaster.CloseAsync();
        }
    }
}
