using System;
using System.Windows.Forms;
using Geomethod;

namespace Geomethod.Windows.Forms
{
	/// <summary>
	/// Summary description for Utils.
	/// </summary>
	public class LocaleUtils
	{
//		public static string HelpFilePath{get{return CommonLib.Utils.BaseDirectory+"\\"+"Help\\"+Application.ProductName+".chm";}}

		public static void Localize(Control ctl)
		{
			if(ctl.Text.StartsWith("_"))
			{
				string key=ctl.Text.TrimEnd();
				bool dd=key.EndsWith(":");
				if(dd) key=key.Remove(key.Length-1,1);
			  string val=Locale.Get(key);
				if(!(ctl is Button && ctl is Label))
				{
					StringUtils.RemoveHotKey(ref val);
				}
				if(dd) val+=':';
				ctl.Text=val;
			}
			if(ctl.ContextMenu!=null)
			{
				foreach(MenuItem mi in ctl.ContextMenu.MenuItems) Localize(mi);
			}

			if(ctl is Form)
			{
				Form f=(Form)ctl;
				if(f.Menu!=null)
				{
					foreach(MenuItem mi in f.Menu.MenuItems) Localize(mi);
				}
			}
			else if(ctl is Button)
			{
				((Button)ctl).FlatStyle=FlatStyle.System;
			}
			else if(ctl is Label)
			{
				((Label)ctl).FlatStyle=FlatStyle.System;
			}
			else if(ctl is LinkLabel)
			{
				((LinkLabel)ctl).FlatStyle=FlatStyle.System;
			}
			else if(ctl is ListView)
			{
				ListView lv=(ListView)ctl;
				foreach(ColumnHeader ch in lv.Columns)
				{
					Localize(ch);
				}
			}

			foreach(Control c in ctl.Controls)
			{
				Localize(c);
			}
		}
		static void Localize(MenuItem mi)
		{
			if(mi.Text.StartsWith("_"))	mi.Text=Locale.Get(mi.Text);
			foreach(MenuItem mi2 in mi.MenuItems) Localize(mi2);
		}		
		static void Localize(ColumnHeader ch)
		{
			if(ch.Text.StartsWith("_"))
			{
				string s=Locale.Get(ch.Text);
				Geomethod.StringUtils.RemoveHotKey(ref s);
				ch.Text=s;
			}
		}		
	}

	public class DateTimePickerUtils
	{
		public static void Init(DateTimePicker dtp, DateTime dateTime)
		{
			bool isChecked=dateTime != DateTime.MinValue;
			dtp.Value =isChecked ?  dateTime:DateTime.Now;
			dtp.Checked = isChecked;
		}

		public static DateTime GetDateTime(DateTimePicker dtp)
		{
			return dtp.Checked ? dtp.Value : DateTime.MinValue;
		}
	}

	public class MessageBoxUtils
	{
		public static bool AskLocalized(string s) { return Ask(Locale.Get(s)); }
		public static bool Ask(string s){ return MessageBox.Show(s, Application.ProductName, MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes; }
	}
}
