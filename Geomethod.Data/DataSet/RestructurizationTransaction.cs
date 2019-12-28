 using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Geomethod.Data
{
	public class RestructurizationTransaction: IDisposable
	{
		GmDataSet dataSet;
		GmTransaction trans;
		MemoryStream initialSchema;

		public GmTransaction Transaction { get { return trans; } }
		public GmConnection Connection { get { return trans.Connection; } }
        public RestructurizationTransaction(GmDataSet dataSet, ConnectionFactory fact) 
		{
			this.dataSet = dataSet;
			if (fact == null) trans = null;
			else 
			{
				GmConnection conn = fact.CreateConnection();
				trans = conn.BeginTransaction();
			}
			initialSchema = new MemoryStream(1 << 16);
			dataSet.WriteXmlSchema(initialSchema);
		}

		#region IDisposable Members

		public void Dispose()
		{
			try
			{
				if (trans != null)
					trans.Dispose();
			}
			finally
			{
				if (!trans.IsCommitted)
				{
					dataSet.ReadXmlSchema(initialSchema);// restore the schema if transaction failed 
				}
			}
		}

		#endregion
	}
}
