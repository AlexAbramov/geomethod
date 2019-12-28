using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace Geomethod.SqlServer
{
//    public delegate void PercentComplete(object source, PercentCompleteEventArgs a);

    public class SqlServerBackup
    {
        #region Events
        public event EventHandler<PercentCompleteEventArgs> OnPercentComplete;
        #endregion

        #region Fields
        ServerConnection serverconn;
        Server server;
        int step = 0;
        #endregion

        #region Properties
        #endregion

        #region Construction
        public SqlServerBackup(SqlConnection conn)
        {
            serverconn = new ServerConnection(conn);
            server = new Server(serverconn);
        }
        #endregion

        #region Backup
        public void BackupDatabase(string databaseName, string path){BackupDatabase(databaseName, path, BackupActionType.Database);}
        public void BackupDatabase(string databaseName, string path, BackupActionType actionType)
        {
            Backup bk = new Backup();
            bk.Database = databaseName;
            bk.Action = actionType;
            bk.Devices.AddDevice(path, DeviceType.File);
            bk.Incremental = false;
            bk.PercentCompleteNotification = step;
            bk.NoRecovery = true;
            bk.Initialize = true;
            if (OnPercentComplete != null) bk.PercentComplete += new PercentCompleteEventHandler(PercentComplete);
            bk.SqlBackup(server);
        }

        void PercentComplete(object sender, PercentCompleteEventArgs e)
        {
            if (OnPercentComplete != null) OnPercentComplete(this, e);
        }
        #endregion

        #region Restore
        public void RestoreDatabase(string databaseName, string path){RestoreDatabase(databaseName, path, RestoreActionType.Database);}
        public void RestoreDatabase(string databaseName, string path, RestoreActionType actionType)
        {
            Restore rs = new Restore();
            rs.Action = actionType;
            rs.Database = databaseName;
            rs.Devices.AddDevice(path, DeviceType.File);
            rs.ReplaceDatabase = true;
            //            rs.NoRecovery = true;
            rs.PercentCompleteNotification = step;
            if (OnPercentComplete != null) rs.PercentComplete += new PercentCompleteEventHandler(PercentComplete);
            rs.SqlRestore(server);
        }

        public void VerifyDatabase(string databaseName, string path)
        {
            Restore rs = new Restore();
            rs.Action = RestoreActionType.Database;
            rs.Database = databaseName;
            rs.Devices.AddDevice(path, DeviceType.File);
            rs.PercentCompleteNotification = step;
            if (OnPercentComplete != null) rs.PercentComplete += new PercentCompleteEventHandler(PercentComplete);
            rs.SqlVerify(server);
        }
        #endregion
    }
}
