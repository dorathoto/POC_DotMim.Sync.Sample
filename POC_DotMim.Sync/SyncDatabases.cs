using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC_DotMim.Sync
{
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
        public async Task Sync(bool debugClean = false)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
    }
