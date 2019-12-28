using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using Geomethod;

namespace Geomethod.Web
{
/*	public class WebLogInformer : ILogInformer
	{
		public WebLogInformer()
		{
		}

		#region ILogInformer Members

		public void Show(Exception ex, object obj)
		{
			this.Show(LogType.Exception, ex.GetType().Name, ex.ToString(), obj);
		}

		public void Show(LogType logType, string subType, string msg, object obj)
		{
			HttpResponse response = null;
			if (obj is Control) response = (obj as Control).Page.Response;
			else if (obj is HttpApplication) response = (obj as HttpApplication).Response;
			if (response != null)
			{
				response.Write(msg);
				response.End();
			}
		}

		#endregion

        #region ILogInformer Members

        public void Show(LogRecord lr)
        {
            lr.
        }

        #endregion
    }*/
}
