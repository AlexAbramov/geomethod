using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Geomethod.Data
{
    public class GmTransaction : IDisposable
    {
        GmConnection conn;
        DbTransaction trans = null;
        bool isCommitted = false;

        public Stack<string> rollbackCommand = null;

        public GmConnection Connection { get { return conn; } }
        public DbTransaction DbTransaction { get { return trans; } }
        public bool IsCommitted { get { return isCommitted; } }
        public GmTransaction( GmConnection conn, IsolationLevel isolationLevel )
        {
            this.conn = conn;
            conn.Open( );

            //      DZ      28.01.09
            if ( conn.ProviderFactory.SupportsProperty(GmProviderProperty.Transaction))
                trans = conn.DbConnection.BeginTransaction( isolationLevel );
            else
                trans = conn.DbConnection.BeginTransaction( IsolationLevel.Chaos );


            if (!conn.ProviderFactory.SupportsProperty(GmProviderProperty.DDLRollback))
                rollbackCommand = new Stack<string>( );

        }

        public void Commit( )
        {
            //      DZ      28.01.09
            //            if( conn.ProviderFactory.TransactionSupported )
            trans.Commit( );

            if ( rollbackCommand != null )
                rollbackCommand.Clear( );

            isCommitted = true;
        }

        #region Commands
        public GmCommand CreateCommand( ) { return conn.CreateCommand( this ); }
        public GmCommand CreateCommand( string cmdText ) { return conn.CreateCommand( cmdText, this ); }
        public GmCommand CreateCommandById( string cmdId ) { return conn.CreateCommandById( cmdId, this ); }
        #endregion

        #region IDisposable Members

        public void Dispose( )
        {
            if ( !isCommitted )
            {
                trans.Rollback( );
                if ( !conn.ProviderFactory.SupportsProperty(GmProviderProperty.DDLRollback))
                {
                    RollbackDDL( );
                    if ( rollbackCommand != null )
                        rollbackCommand.Clear( );
                }
            }
        }

        private void RollbackDDL( )
        {
            if ( rollbackCommand != null )
                foreach ( string sql in rollbackCommand )
                    CreateCommand( sql ).ExecuteNonQuery( );

        }

        #endregion

        public static implicit operator DbTransaction( GmTransaction trans )
        {
            return trans.DbTransaction;
        }
    }
}
