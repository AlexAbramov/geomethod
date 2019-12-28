using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Geomethod.Data
{
	public abstract class GmProxy
	{
		#region Static
		#endregion

		#region Fields
		#endregion

		#region Properties
		public abstract GmProxyFactory ProxyFactory { get; }
		public int TableId { get{return this.ProxyFactory.TableId;} }
		public string TableName { get {return this.ProxyFactory.TableName; } }
		#endregion

		#region Construction
		#endregion

		#region Methods
		protected abstract void Init(GmDataReader dr);
		protected internal abstract void Init(DbDataReader dr);
		protected abstract void AddDbParameters(GmCommand cmd);
		public abstract int Save(GmConnection conn);
		#endregion
    }
}
