using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Geomethod.Web
{
	public class WebUtils
	{
		public static string GetPageName(Control ctl)
		{
			string path = ctl.Page.Request.PhysicalPath;
			return Path.GetFileName(path);
		}
		public static string GetPageNameWithoutExtension(Control ctl)
		{
			string path = ctl.Page.Request.PhysicalPath;
			return Path.GetFileNameWithoutExtension(path);
		}
		public static string GetUrl(object urlObj) { return GetUrl(urlObj, false); }
		public static string GetHost(object urlObj) { return GetUrl(urlObj, true); }
		public static string GetUrl(object urlObj, bool host)
		{
			string url = urlObj as string;
			if (url != null)
			{
				if (url.StartsWith("www")) url = "http://" + url;
				if(Uri.IsWellFormedUriString(url,UriKind.Absolute))
				{
					Uri u = new Uri(url);
					return host ? u.Host : u.AbsoluteUri;
				}
			}
			return "";
		}
		public static string FixText(object strObj)
		{
			string s = strObj as string;
			if (s != null)
			{
				s = s.Replace("\r\n", "<br/>");
				s = s.Replace("\n", "<br/>");
			}
			return s;
		}
		
		public static void RedirectHome(Control ctl)
		{
			Redirect(ctl, "Default.aspx");
		}

		public static string GetRootUrl(Control ctl)
		{
			string rootUrl = ctl.Page.Request.ApplicationPath;
			if (!rootUrl.EndsWith("/")) rootUrl += '/';
			return rootUrl;
		}
		public static string GetCurrentUrl(Control ctl)
		{
			return ctl.Page.Request.Url.AbsoluteUri;
		}
		public static void Redirect(Control ctl, string url) { Redirect(ctl,url,false); }
		public static void Redirect(Control ctl, string url, bool endResponse)
		{
			Page page = ctl.Page;
			url = url.Trim();
			if (url.StartsWith("www")) url = "http://" + url;
			else if (!url.StartsWith("http"))
			{
				url = GetRootUrl(ctl) + url;
			}
			try
			{
				page.Response.Redirect(url, false);
			}
			catch
			{
//!!!				Log.Exception(ex, page);
			}
		}
		public static CheckBox GetCheckBox(ControlCollection controls)
		{
			foreach (Control ctl in controls) if (ctl is CheckBox) return (CheckBox)ctl;
			return null;
		}


	}

}

