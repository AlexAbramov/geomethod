using System;
using System.Text;
using System.Web;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Geomethod;

namespace Geomethod.Windows.Forms
{
	public partial class MessageForm : System.Windows.Forms.Form
    {
        const int maxBodyLength=512;

        #region Static
		static LogType lastType=LogType.Exception;
		static string lastSubType=null;
		static bool ignoreRepeated=false;
		static bool CheckIgnore(LogType logType,string subtype)
		{
			if(IsRepeated(logType, subtype)) return true;
			lastType=logType;
			lastSubType=subtype;
			ignoreRepeated=false;
            return false;
        }
        public static bool IsIgnored(LogType logType,string subtype)
		{
			return ignoreRepeated && IsRepeated(logType, subtype);
		}
		static bool IsRepeated(LogType logType,string subtype)
		{
			return logType==lastType && subtype==lastSubType && subtype!=null && subtype.Length>0;
		}
        #endregion

		const int gap=8;
		LogRecord logRecord;
		bool showDetails=false;

//		public string Message{get{return tbMessage.Text;} set{tbMessage.Text=value;}}

		void SetTitle(string s)
		{
            string productName = Locale.StringSet.ContainsKey("_productName") ? Locale.Get("_productName") : Application.ProductName;
            if (s == null || s.Trim().Length == 0) s = productName;
            else s+=" - " + productName;
            base.Text = s;
		}

		public MessageForm(LogRecord logRecord)
		{
			InitializeComponent();
			GmApplication.Initialize(this);
			this.logRecord = logRecord;

            string header = logRecord.header;
            tbMessage.Text = logRecord.message;
            switch (logRecord.logType)
			{
				case LogType.Info:
					SetTitle(Locale.Get("_message"));
					break;
				case LogType.Warning:
					SetTitle(Locale.Get("_warning"));
					break;
				case LogType.Error:
					SetTitle(Locale.Get("_error"));
					btnReport.Visible=true;
					break;
				case LogType.Exception:
					SetTitle(Locale.Get("_exception"));
					btnReport.Visible=true;
                    btnDetails.Visible = !showDetails;
                    tbMessage.Text = GetExceptionText(logRecord.exception, showDetails);
                    header = logRecord.exception.GetType().Name;
					break;
                default:
                    SetTitle(null);
                    break;
			}
			btnIgnore.Visible=CheckIgnore(logRecord.logType, header);
		}

		private void MessageForm_Resize(object sender, System.EventArgs e)
		{
			OnResize();
		}

		void OnResize()
		{
            List<Button> ar = new List<Button>();
			if(btnOk.Visible) ar.Add(btnOk);
			if(btnDetails.Visible) ar.Add(btnDetails);
			if(btnReport.Visible) ar.Add(btnReport);
			if(btnIgnore.Visible) ar.Add(btnIgnore);
			
			int w=ar.Count*btnOk.Width+(ar.Count-1)*gap;
			int x=(base.Width-w)/2;

			foreach(Button btn in ar)
			{
				btn.Left=x;
				x+=btnOk.Width+gap;
			}
		}

		private void MessageForm_Load(object sender, System.EventArgs e)
		{
            if (!base.DesignMode)
            {
                try
                {
					btnOk.Select();
                    OnResize();
                }
                catch
                {
                }
            }
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void btnDetails_Click(object sender, System.EventArgs e)
		{
			showDetails=!showDetails;
            tbMessage.Text = GetExceptionText(logRecord.exception, showDetails);
			btnDetails.Text = showDetails ? Locale.Get("_hideDetails") : Locale.Get("_details");
		}

        private string GetExceptionText(Exception ex, bool showDetails)
        {
            if (showDetails)
            {
                return ex.ToString();
            }
            else
            {
                return ex.GetType().ToString() + Environment.NewLine + ex.Message;
            }
        }

		string GetExceptionReport(Exception ex)
		{
			string text=ex.ToString();
			if(text.Length>maxBodyLength)
			{
				char[] sep={'\r','\n'};
                List<String> ar = new List<String>(text.Split(sep,StringSplitOptions.RemoveEmptyEntries));
				bool updated=false;
				while(GetLength(ar)>maxBodyLength && ar.Count>2)
				{
					ar.RemoveAt(2);
					updated=true;
				}
				if(updated)
				{
					ar.Insert(2,"...");
					StringBuilder sb=new StringBuilder(maxBodyLength);
					for(int i=0;i<ar.Count;i++)
					{
						if(i>0) sb.Append("\r\n");
						sb.Append((string)ar[i]);
					}
					return sb.ToString();
				}
			}
			return text;
		}

		int GetLength(List<String> ar)
		{
			int count=0;
			foreach(string s in ar)
			{
				count+=s.Length+2;
			}
			return count;
		}

		private void btnReport_Click(object sender, System.EventArgs e)
		{
            string text = logRecord.exception == null ? tbMessage.Text : GetExceptionReport(logRecord.exception);
			if(text.Length>maxBodyLength) text=text.Substring(0,maxBodyLength);
			StringBuilder sb=new StringBuilder(2048);
			sb.Append("mailto:");
			// subject
			sb.Append("?subject=");
            sb.Append(base.Text);
			// body
			sb.Append("&body=");
			byte[] bytes=Encoding.UTF8.GetBytes(text);
//			bytes=Encoding.Convert(Encoding.UTF8,Encoding.GetEncoding(1251),bytes);
			foreach(byte b in bytes)
			{
				sb.Append(System.Uri.HexEscape((char)b));
			}
			string cmdStr=sb.ToString();
			System.Diagnostics.Process.Start(cmdStr);
		}

		private void btnIgnore_Click(object sender, System.EventArgs e)
		{
			ignoreRepeated=true;
		}
	}
}
