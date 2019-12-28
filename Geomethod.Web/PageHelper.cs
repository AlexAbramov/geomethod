using System;
using System.Reflection;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Geomethod.Web
{
	public class SessionObjectAttribute : Attribute
	{
	}

	public class PageHelper
	{
		public static string sessionObjectsCollName="PageSessionObjects";
		Page page;
		Dictionary<string,object> sessionObjects;

		#region Construction
		public PageHelper(Page page)
		{
			this.page = page;
			if (!page.IsPostBack)
			{
			}
			else
			{
				sessionObjects = (Dictionary<string, object>)page.Session[sessionObjectsCollName];
				if (sessionObjects!=null)
				{
					foreach (FieldInfo fi in page.GetType().GetFields())
					{
						if (Attribute.IsDefined(fi, typeof(SessionObjectAttribute)))
						{
							object obj = sessionObjects[fi.Name];
							fi.SetValue(page, obj);
						}
					}
				}
			}
			page.Unload += new EventHandler(page_Unload);
		}
		#endregion

		void page_Unload(object sender, EventArgs e)
		{
			if (!page.IsPostBack)
			{
				sessionObjects = new Dictionary<string, object>();
				foreach (FieldInfo fi in page.GetType().GetFields())
				{
					if (Attribute.IsDefined(fi, typeof(SessionObjectAttribute)))
					{
						object obj = fi.GetValue(page);
						sessionObjects.Add(fi.Name, obj);
					}
					//				Console.WriteLine(.ToString());
				}
				page.Session[sessionObjectsCollName] = sessionObjects;
			}
		}
	}
}
