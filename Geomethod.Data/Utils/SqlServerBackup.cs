using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace Geomethod.Data
{
    public delegate void PercentComplete(object source, PercentCompleteEventArgs a);

    public static class SqlServerBackup
    {
        public static void BackupDatabase(SqlConnection conn,
                string database_name, string path,
                PercentComplete percentComplete, int step)
        {
            BackupDatabase(database_name, path, percentComplete, conn, step,
                    BackupActionType.Database);
        }

        private static void BackupDatabase(string database_name, string path,
                PercentComplete percentComplete, SqlConnection conn, int step,
                BackupActionType type)
        {
            ServerConnection serverconn = new ServerConnection(conn);
            Server server = new Server(serverconn);
            Backup bk = new Backup();

            bk.Database = database_name;
            bk.Action = type;
            bk.Devices.AddDevice(path, DeviceType.File);
            bk.Incremental = false;
            bk.PercentCompleteNotification = step;
            bk.NoRecovery = true;
            bk.Initialize = true;
            if (percentComplete != null) bk.PercentComplete += new PercentCompleteEventHandler(percentComplete);

            bk.SqlBackup(server);
        }

        public static void RestoreDatabase(SqlConnection conn,
                string database_name, string path,
                PercentComplete percentComplete, int step)
        {
            RestoreDatabase(database_name, path, percentComplete, conn, step,
                RestoreActionType.Database);
        }

        private static void RestoreDatabase(string database_name, string path,
            PercentComplete percentComplete, SqlConnection conn, int step,
            RestoreActionType type)
        {
            ServerConnection serverconn = new ServerConnection(conn);
            Server server = new Server(serverconn);

            Restore rs = new Restore();

            rs.Action = type;
            rs.Database = database_name;
            rs.Devices.AddDevice(path, DeviceType.File);
            rs.ReplaceDatabase = true;
            //            rs.NoRecovery = true;

            rs.PercentCompleteNotification = step;
            rs.PercentComplete += new PercentCompleteEventHandler(percentComplete);

            rs.SqlRestore(server);
        }

        public static void VerifyDatabase(SqlConnection conn,
                string database_name, string path,
                PercentComplete percentComplete, int step)
        {
            ServerConnection serverconn = new ServerConnection(conn);
            Server server = new Server(serverconn);
            Restore rs = new Restore();

            rs.Action = RestoreActionType.Database;
            rs.Database = database_name;
            rs.Devices.AddDevice(path, DeviceType.File);
            rs.PercentCompleteNotification = step;
            rs.PercentComplete += new PercentCompleteEventHandler(percentComplete);

            rs.SqlVerify(server);
        }
    }
}
