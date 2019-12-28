using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace Geomethod.Windows.Forms
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public partial class HtmlBrowser : UserControl
    {
        protected HtmlDocument doc = null;
        public WebBrowser WebBrowser { get { return webBrowser; } }
        public HtmlDocument Document { get { return doc; } }
        public HtmlBrowser()
        {
            InitializeComponent();

        }


        private void HtmlBrowser_Load(object sender, EventArgs e)
        {
            // The following events are not visible in the designer, so 
            // you must associate them with their event-handlers in code.
            webBrowser.CanGoBackChanged += new EventHandler(webBrowser_CanGoBackChanged);
            webBrowser.CanGoForwardChanged += new EventHandler(webBrowser_CanGoForwardChanged);
            webBrowser.DocumentTitleChanged += new EventHandler(webBrowser_DocumentTitleChanged);
            webBrowser.StatusTextChanged += new EventHandler(webBrowser_StatusTextChanged);
            webBrowser.Navigate("about:blank");
            doc = this.webBrowser.Document;
            tsFile.Location = new Point(0, 0);
            cbUrl.Width = 0;
            ResizeCombo();
            UpdateControls();
         }

        void webBrowser_StatusTextChanged(object sender, EventArgs e)
        {
            lblStatus.Text = webBrowser.StatusText;
        }

        void webBrowser_DocumentTitleChanged(object sender, EventArgs e)
        {
            this.Text = webBrowser.DocumentTitle;
        }

        void webBrowser_CanGoForwardChanged(object sender, EventArgs e)
        {
            btnForward.Enabled = webBrowser.CanGoForward;
        }

        void webBrowser_CanGoBackChanged(object sender, EventArgs e)
        {
            btnBack.Enabled = webBrowser.CanGoBack;
        }

        void UpdateControls()
        {
            lblStatus.Text = webBrowser.StatusText;
            btnForward.Enabled = webBrowser.CanGoForward;
            btnBack.Enabled = webBrowser.CanGoBack;
        }

        // Navigates to the given URL if it is valid.
        private void Navigate(String address)
        {
            if (String.IsNullOrEmpty(address)) return;
            if (address.Equals("about:blank")) return;
            if (!address.StartsWith("http://") &&
                !address.StartsWith("https://"))
            {
                address = "http://" + address;
            }
            try
            {
                webBrowser.Navigate(new Uri(address));
            }
            catch (System.UriFormatException)
            {
                return;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            New();
        }

        public void New()
        {
            webBrowser.Navigate("about:blank");
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Open();
        }

        public void Open()
        {
            if (dlgOpenFile.ShowDialog() == DialogResult.OK)
            {
                webBrowser.Navigate(dlgOpenFile.FileName);
            }
        }

        public void Open(string url)
        {
            if (url == null || url.Trim().Length == 0) Open();
            else 
            {
                webBrowser.Navigate(url);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        public void Save()
        {
            webBrowser.ShowSaveAsDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            // Prints the current document using the current print settings.
            Print();
        }

        public void Print()
        {
            webBrowser.Print();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            webBrowser.GoBack();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            webBrowser.GoForward();
        }

        private void btnProperties_Click(object sender, EventArgs e)
        {
            webBrowser.ShowPropertiesDialog();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            // Halts the current navigation and any sounds or animations on 
            // the page.
            webBrowser.Stop();
            btnStop.Enabled = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Skip refresh if about:blank is loaded to avoid removing
            // content specified by the DocumentText property.
            if (!webBrowser.Url.Equals("about:blank"))
            {
                webBrowser.Refresh();
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(cbUrl.Text);
        }

        private void cbUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Navigate(cbUrl.Text);
            }
        }

        private void cbUrl_Click(object sender, EventArgs e)
        {
        }

        private void cbUrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            Navigate(cbUrl.Text);
        }

        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            cbUrl.Text = webBrowser.Url.ToString();// mem!
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Navigates webBrowser1 to the search page of the current user.
            GoSearch();
        }

        public void GoSearch()
        {
            webBrowser.GoSearch();
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            PrintPreview();
        }

        public void PrintPreview()
        {
            webBrowser.ShowPrintPreviewDialog();
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
        }

        private void btnPageSetup_Click(object sender, EventArgs e)
        {
            webBrowser.ShowPageSetupDialog();
        }

        private void ResizeCombo()
        {
            ToolStripPanel tsp = tsNavigate.Parent as ToolStripPanel;
            if (tsp != null)
            {
                Rectangle bounds=tsNavigate.Bounds;
                bounds.Width=tsp.Width-10-bounds.Left;
                foreach(Control ctl in tsp.Controls)
                {
                    if (ctl != tsNavigate && ctl.Bounds.IntersectsWith(bounds))
                    {
                        bounds.Width = ctl.Left - bounds.Left-1;
                    }
                }
                int dx = bounds.Width - tsNavigate.Width;
                int w = cbUrl.Width + dx;
                if (w < 100) w = 100;
                cbUrl.Width = w;
            }
            
        }

        private void cbUrl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks > 1)
            {
                cbUrl.SelectAll();
            }
        }

        private void pnlTop_Resize(object sender, EventArgs e)
        {
        }

        private void tsNavigate_LayoutCompleted(object sender, EventArgs e)
        {
        }

        private void tsNavigate_EndDrag(object sender, EventArgs e)
        {
            ResizeCombo();
        }

        private void toolStripContainer_SizeChanged(object sender, EventArgs e)
        {
            ResizeCombo();
        }

        private void tsNavigate_Move(object sender, EventArgs e)
        {
        }

        private void toolStripContainer_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            btnStop.Enabled = false;
        }

        private void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            btnStop.Enabled = true;
        }

    }
}
