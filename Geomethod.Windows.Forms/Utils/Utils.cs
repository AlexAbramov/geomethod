using System;
using System.Windows.Forms;
using Geomethod;

namespace Geomethod.Windows.Forms
{

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

        public static void Init(DateTimePicker dtp, object dateTime)
        {
            bool isChecked = !(dateTime is DBNull);
            dtp.Value = isChecked ? (DateTime)dateTime : DateTime.Now;
            dtp.Checked = isChecked;
        }

        public static object GetDateTimeObject(DateTimePicker dtp)
        {
            return dtp.Checked ? (object)dtp.Value : (object)DBNull.Value;
        }
    }

	public class MessageBoxUtils
	{
		public static bool AskLocalized(string textId) { return Ask(Locale.Get(textId)); }
		public static bool Ask(string text) { return Show(text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes; }
		public static DialogResult ShowLocalized(string textId) { return Show(Locale.Get(textId)); }
		public static DialogResult Show(string text) { return Show(text, MessageBoxButtons.OK, MessageBoxIcon.None); }
		public static DialogResult Show(string text, MessageBoxIcon icon) { return Show(text, MessageBoxButtons.OK, icon); }
		public static DialogResult Show(string text, MessageBoxButtons buttons, MessageBoxIcon icon) 
		{ 
			return MessageBox.Show(text, Application.ProductName, buttons, icon); 
		}
	}
}
