using System;
using System.Text;
using System.Web;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Geomethod;

namespace Geomethod.Windows.Forms
{
	public class MessageForm : System.Windows.Forms.Form
	{
		const int maxBodyLength=512;

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

		const int gap=8;
		Exception exception=null;
		bool showDetails=false;
		LogType logType;

		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TextBox tbMessage;
		private System.Windows.Forms.Button btnReport;
		private System.Windows.Forms.Button btnDetails;
		private System.Windows.Forms.Button btnIgnore;
		private System.ComponentModel.Container components = null;

//		public string Message{get{return tbMessage.Text;} set{tbMessage.Text=value;}}

		void SetTitle(string s)
		{
   		base.Text=Application.ProductName+' '+s;
		}

		public MessageForm(LogType logType, string subtype, string msg)
		{
			InitializeComponent();
      this.logType=logType;

			tbMessage.Text=msg;
			switch(logType)
			{
				case LogType.Info:
					SetTitle(Locale.Get("_info"));
					break;
				case LogType.Warning:
					SetTitle(Locale.Get("_warning"));
					break;
				case LogType.Error:
					SetTitle(this.Text = Locale.Get("_error"));
					btnReport.Visible=true;
					break;
				case LogType.Exception:
					SetTitle(Locale.Get("_exception"));
					btnReport.Visible=true;
					break;
			}
			btnIgnore.Visible=CheckIgnore(logType,subtype);
		}

		public MessageForm(Exception exception)
		{
			InitializeComponent();

			logType=LogType.Exception;
			SetTitle(Locale.Get("_exception"));
			this.exception=exception;
			btnDetails.Visible=true;
			btnReport.Visible=true;
			tbMessage.Text=exception.Message;
			btnIgnore.Visible=CheckIgnore(LogType.Exception,exception.GetType().Name);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tbMessage = new System.Windows.Forms.TextBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnReport = new System.Windows.Forms.Button();
			this.btnDetails = new System.Windows.Forms.Button();
			this.btnIgnore = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// tbMessage
			// 
			this.tbMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tbMessage.CausesValidation = false;
			this.tbMessage.Location = new System.Drawing.Point(8, 8);
			this.tbMessage.Multiline = true;
			this.tbMessage.Name = "tbMessage";
			this.tbMessage.ReadOnly = true;
			this.tbMessage.Size = new System.Drawing.Size(344, 168);
			this.tbMessage.TabIndex = 0;
			this.tbMessage.Text = "";
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(8, 184);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(80, 23);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "_ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnReport
			// 
			this.btnReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnReport.Location = new System.Drawing.Point(184, 184);
			this.btnReport.Name = "btnReport";
			this.btnReport.Size = new System.Drawing.Size(80, 23);
			this.btnReport.TabIndex = 2;
			this.btnReport.Text = "_report";
			this.btnReport.Visible = false;
			this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
			// 
			// btnDetails
			// 
			this.btnDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDetails.Location = new System.Drawing.Point(96, 184);
			this.btnDetails.Name = "btnDetails";
			this.btnDetails.Size = new System.Drawing.Size(80, 23);
			this.btnDetails.TabIndex = 3;
			this.btnDetails.Text = "_details";
			this.btnDetails.Visible = false;
			this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
			// 
			// btnIgnore
			// 
			this.btnIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnIgnore.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnIgnore.Location = new System.Drawing.Point(272, 184);
			this.btnIgnore.Name = "btnIgnore";
			this.btnIgnore.Size = new System.Drawing.Size(80, 23);
			this.btnIgnore.TabIndex = 4;
			this.btnIgnore.Text = "_ignore";
			this.btnIgnore.Visible = false;
			this.btnIgnore.Click += new System.EventHandler(this.btnIgnore_Click);
			// 
			// MessageForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CausesValidation = false;
			this.ClientSize = new System.Drawing.Size(360, 214);
			this.Controls.Add(this.btnIgnore);
			this.Controls.Add(this.btnDetails);
			this.Controls.Add(this.btnReport);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.tbMessage);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(368, 248);
			this.Name = "MessageForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Resize += new System.EventHandler(this.MessageForm_Resize);
			this.Load += new System.EventHandler(this.MessageForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void MessageForm_Resize(object sender, System.EventArgs e)
		{
			OnResize();
		}

		void OnResize()
		{
			ArrayList ar=new ArrayList();
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
			LocaleUtils.Localize(this);
			btnOk.Select();
			OnResize();
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void btnDetails_Click(object sender, System.EventArgs e)
		{
			showDetails=!showDetails;
			tbMessage.Text=showDetails ? exception.ToString() : exception.Message;
			btnDetails.Text = showDetails ? Locale.Get("_hideDetails") : Locale.Get("_details");
		}

		string GetExceptionText(Exception ex)
		{
			string text=ex.ToString();
			if(text.Length>maxBodyLength)
			{
				char[] sep={'\r','\n'};
				ArrayList ar=new ArrayList();
				foreach(string s in text.Split(sep))
				{
					if(s.Length>0) ar.Add(s);
				}
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

		int GetLength(ArrayList ar)
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
			string text=exception==null ? tbMessage.Text : GetExceptionText(exception);
			if(text.Length>maxBodyLength) text=text.Substring(0,maxBodyLength);
			StringBuilder sb=new StringBuilder(2048);
			sb.Append("mailto:");
			// subject
			sb.Append("?subject=");
			sb.Append(Application.ProductName+" "+logType.ToString()+" report ");
			// body
			sb.Append("&body=");
			byte[] bytes=Encoding.UTF8.GetBytes(text);
			bytes=Encoding.Convert(Encoding.UTF8,Encoding.GetEncoding(1251),bytes);
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
