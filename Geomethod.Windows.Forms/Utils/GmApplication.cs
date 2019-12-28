using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Geomethod.Windows.Forms
{
	[FlagsAttribute]
	public enum FormInitFlags { Localize, SetStartPosition, SetMinimumSize }

	public class FormInitEventArgs: EventArgs
	{
		Form form;
		FormInitFlags formInitFlags;
		public Form Form{get{return form;}}
		public bool HasFlag(FormInitFlags flag) { return (formInitFlags & flag) == flag; }
		public FormInitEventArgs(Form form, FormInitFlags formInitFlags)
		{
			this.form=form;
			this.formInitFlags = formInitFlags;
		}
	}


	public class GmApplication
	{
		public static FormInitFlags formInitFlags = FormInitFlags.Localize | FormInitFlags.SetStartPosition | FormInitFlags.SetMinimumSize;
//		public static bool HasFlag(FormInitFlags flag) { return (formInitFlags & flag) == flag; }
		public static event EventHandler<FormInitEventArgs> OnFormInitializing;
		public static event EventHandler OnIterate;
		private GmApplication() { }
		public static void Initialize(Form form)
		{
			FormInitEventArgs args = new FormInitEventArgs(form, formInitFlags);
			if (OnFormInitializing != null)
			{
				OnFormInitializing(form, args);
			}
			if (args.HasFlag(FormInitFlags.SetStartPosition))
			{
				form.StartPosition = form.Parent != null ? FormStartPosition.CenterParent : FormStartPosition.CenterScreen;
			}
			if (args.HasFlag(FormInitFlags.SetMinimumSize))
			{
				form.MinimumSize = form.Size;
			}
			bool localize = args.HasFlag(FormInitFlags.Localize);
			ControlIterator ci = new ControlIterator(localize);
			if(OnIterate!=null) ci.OnIterate += new EventHandler(ci_OnIterate);
			ci.Iterate(form);
		}

		static void ci_OnIterate(object sender, EventArgs e)
		{
			if (OnIterate != null) OnIterate(sender, null);
		}
	}

	public class ControlIterator
	{
		bool localize;
		public event EventHandler OnIterate;
		public ControlIterator(bool localize) { this.localize = localize;  }
		void OnIterateHandler(object sender) { if (OnIterate != null) OnIterate(sender, null); }
		public void Iterate(Control ctl)
		{
			if (ctl == null) return;

			if (localize) ctl.Text = Localize(ctl.Text);

			OnIterateHandler(ctl);

			if (ctl.ContextMenu != null)
			{
				foreach (MenuItem mi in ctl.ContextMenu.MenuItems) Iterate(mi);
			}
			if (ctl.ContextMenuStrip != null)
			{
				foreach (ToolStripItem toolStripItem in ctl.ContextMenuStrip.Items) Iterate(toolStripItem);
			}
			foreach (Control childCtl in ctl.Controls)
			{
				Iterate(childCtl);
			}

			if (ctl is Form) IterateEx(ctl as Form);
			else if (ctl is ListView) IterateEx(ctl as ListView);
			else if (ctl is ToolStripContainer) IterateEx(ctl as ToolStripContainer);
			else if (ctl is ToolStrip) IterateEx(ctl as ToolStrip);
			else if (ctl is DataGridView) IterateEx(ctl as DataGridView);
			else if (ctl is ToolBar) IterateEx(ctl as ToolBar);
		}
		void IterateEx(Form form)
		{
			if (form.Menu != null)
			{
				Iterate(form.Menu);
			}
			if (form.MainMenuStrip != null)
			{
				Iterate(form.MainMenuStrip);
			}
		}
		void IterateEx(ListView lv)
		{
			foreach (ColumnHeader ch in lv.Columns)
			{
				Iterate(ch);
			}
		}
		void IterateEx(ToolStripContainer tsc)
		{
			Iterate(tsc.TopToolStripPanel);
			Iterate(tsc.BottomToolStripPanel);
			Iterate(tsc.LeftToolStripPanel);
			Iterate(tsc.RightToolStripPanel);
			Iterate(tsc.ContentPanel);
		}
		void IterateEx(ToolStrip toolStrip)
		{
			foreach (ToolStripItem toolStripItem in toolStrip.Items) Iterate(toolStripItem);
		}
		void IterateEx(DataGridView gridView)
		{
			gridView.AllowUserToResizeRows = false;
			foreach (DataGridViewColumn col in gridView.Columns) Iterate(col);
		}
		void IterateEx(ToolBar toolBar)
		{
			foreach (ToolBarButton toolBarButton in toolBar.Buttons) Iterate(toolBarButton);
		}

		private void Iterate(MainMenu mainMenu)
		{
			OnIterateHandler(mainMenu);
			foreach (MenuItem mi in mainMenu.MenuItems) Iterate(mi);
		}
		void Iterate(ToolBarButton toolBarButton)
		{
			if (localize)
			{
				toolBarButton.Text = Localize(toolBarButton.Text);
				toolBarButton.ToolTipText = Localize(toolBarButton.ToolTipText);
			}
			OnIterateHandler(toolBarButton);
			Iterate(toolBarButton.DropDownMenu);
		}
		private void Iterate(Menu menu)
		{
			OnIterateHandler(menu);
			foreach (MenuItem mi in menu.MenuItems) Iterate(mi);
		}
		void Iterate(DataGridViewColumn col)
		{
			if (localize)
			{
				col.HeaderText = LocalizeAndRemoveHotKey(col.HeaderText);
				col.ToolTipText = LocalizeAndRemoveHotKey(col.ToolTipText);
			}
			OnIterateHandler(col);
			Iterate(col.ContextMenuStrip);
		}
		void Iterate(MenuItem mi)
		{
			if (localize) mi.Text = Localize(mi.Text);
			OnIterateHandler(mi);
			foreach (MenuItem mi2 in mi.MenuItems) Iterate(mi2);
		}
		void Iterate(ToolStripItem tsi)
		{
			if (localize)
			{
				tsi.Text = Localize(tsi.Text);
				tsi.ToolTipText = LocalizeAndRemoveHotKey(tsi.ToolTipText);
			}
			OnIterateHandler(tsi);
			if (tsi is ToolStripMenuItem) Iterate(tsi as ToolStripMenuItem);
		}
		void Iterate(ToolStripMenuItem mi)
		{
			OnIterateHandler(mi);
			if (mi.HasDropDownItems)
			{
				foreach (ToolStripItem tsi in mi.DropDownItems)
				{
					Iterate(tsi);
				}
			}
		}
		void Iterate(ColumnHeader ch)
		{
			if (localize)
			{
				ch.Text = Localize(ch.Text);
			}
			OnIterateHandler(ch);
		}
		string Localize(string s)
		{
			if (s != null && s.StartsWith("_"))
			{
				s = Locale.Get(s);
			}
			return s;
		}
		string LocalizeAndRemoveHotKey(string s)
		{
			if (s != null && s.StartsWith("_"))
			{
				s = Localize(s);
				StringUtils.RemoveHotKey(ref s);
			}
			return s;
		}
	}
}
