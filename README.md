## DotMim.Sync Filtering Example

This C# project demonstrates how to use the DotMim.Sync library to synchronize data between a "Master" SQL Server database and a "Local" SQL Server database, with a focus on filtering data based on a `TenantId`.

**Project Goal:**

The primary aim is to showcase the implementation of filtering within the DotMim.Sync framework. This allows you to selectively synchronize data for specific tenants, ensuring that only relevant information is transferred between databases.

**Key Features:**

* **Tenant-based Filtering:** The synchronization process is configured to only download data related to a specific `TenantId`, which is defined in the `appsettings.json` file.
* **Change Tracking:** Both the Master and Local databases are enabled for SQL Server Change Tracking, which efficiently captures data modifications for synchronization.
* **Data Seeding:** The project includes functionality to seed sample data into the Master database, including data for the specific tenant and some randomized data for testing purposes.
* **Error Handling:** A basic error handling mechanism is implemented to retry the synchronization process after cleaning up metadata in case of errors. (This can be removed or refined for production environments)
* **Download-Only Synchronization:** The setup is configured to only download data from the Master database to the Local database.

**Code Structure:**

* **Program.cs:**
    *  Sets up configuration and retrieves the `TenantId` from `appsettings.json`.
    *  Establishes database contexts for both Master and Local databases.
    *  Enables Change Tracking on both databases.
    *  Seeds sample data into the Master database.
    *  Initiates the synchronization process using `SyncDatabases`.
* **SampleSeedMaster.cs:**
    *  Handles the seeding of sample data into the Master database.
    *  Inserts data for the specified tenant and additional random data.
* **EnableChangeTrackingMsSql.cs:**
    *  Contains methods to enable Change Tracking on the Master and Local SQL Server databases.
* **SyncDatabases.cs:**
    *  Manages the core synchronization logic using DotMim.Sync.
    *  Configures the `SyncAgent`, `SyncSetup`, and `SetupFilter` for tenant-based filtering.
    *  Executes the synchronization process with error handling and progress reporting.

**How to Run:**

1. **Configure Database Connections:** Update the connection strings in the `appsettings.json` file to point to your Master and Local SQL Server databases.
2. **Set TenantId:** Specify the desired `TenantId` in the `appsettings.json` file.
3. **Build and Run:** Build the project and run the `Program.cs` file.

**Notes:**

* The `debugClean` flag in `SyncDatabases.SyncTest()` is for debugging purposes. In a production environment, you would likely implement more robust error handling and logging.
* This example focuses on download-only synchronization. You can modify the `SyncDirection` in `SyncSetup` to enable other synchronization modes (e.g., bidirectional, upload-only).
* Consider exploring more advanced features of DotMim.Sync, such as conflict resolution, custom providers, and different synchronization strategies, to tailor the solution to your specific needs.

This README provides a basic overview of the project. You can expand it further by adding details about:

* Installation instructions
* Dependencies
* Contributing guidelines
* License information
* Contact information
* And any other relevant information for users or contributors.
