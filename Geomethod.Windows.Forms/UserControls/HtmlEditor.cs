using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using mshtml;

namespace Geomethod.Windows.Forms.UserControls
{
    public partial class HtmlEditor : Geomethod.Windows.Forms.HtmlBrowser
    {
        bool loaded = false;
        bool edited = false;
//        protected IHTMLDocument2 doc2 = null;

        public bool EditMode { get { return btnEdit.Checked; } set { SetEditMode(value); } }
        public HtmlEditor()
        {
            InitializeComponent();
        }

        private void HtmlEditor_Load(object sender, EventArgs e)
        {
//            doc2=Document.DomDocument as mshtml.IHTMLDocument2;
            toolStripContainer.Dock = DockStyle.Fill;
            base.sepEdit.Visible = true;
            base.btnEdit.Visible = true;
            btnEdit.Click += new EventHandler(btnEdit_Click);
            webBrowser.Navigating += new WebBrowserNavigatingEventHandler(webBrowser_Navigating);
//            FixLayout(toolStripContainer.TopToolStripPanel);
            toolStripContainer.TopToolStripPanel.Controls.Add(tsEdit);
            toolStripContainer.TopToolStripPanel.Controls.Add(tsSelection);
            SetEditMode(EditMode);
            loaded = true;
        }

        void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (EditMode) SetEditMode(false);
        }

        void btnEdit_Click(object sender, EventArgs e)
        {
            SetEditMode(btnEdit.Checked);
        }

        private void SetEditMode(bool editMode)
        {
            btnEdit.Checked = editMode;
//            if (doc2 != null) doc2.designMode = editMode ? "On" : "Off";
            if (!editMode)
            {
                tsEdit.Visible = editMode;
                tsSelection.Visible = editMode;
            }
            if (editMode) tsEdit.Location = tsSelection.Location;
            if (editMode)
            {
                edited = true;
            }
            else
            {
                if (edited)
                {
                    if (MessageBoxUtils.AskLocalized("_saveDoc"))
                    {
                        Save();
                    }
                    edited = false;
                }
            }
        }

        private void FixLayout(ToolStripPanel toolStripPanel)
        {
            toolStripPanel.SuspendLayout();
            //			List<ToolStrip> l = new List<ToolStrip>();
            List<Control> controls = new List<Control>();
            foreach(Control c in toolStripPanel.Controls) controls.Add(c);
            controls.Reverse();
            foreach (Control ctl in controls)
            {
                ToolStrip ts = ctl as ToolStrip;
                if (ts != null)
                {
                    ts.LayoutStyle = ToolStripLayoutStyle.Flow;
 //                   ts.Location = new Point(0, 0);
                }
            }
            toolStripPanel.ResumeLayout();
        }


    }
}

