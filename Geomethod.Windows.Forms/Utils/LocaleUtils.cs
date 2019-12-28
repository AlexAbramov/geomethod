using System;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Forms;
using Geomethod;

namespace Geomethod.Windows.Forms
{
	/// <summary>
	/// Summary description for Utils.
	/// </summary>
	public class LocaleUtils
	{
        public static int Fill<EnumType>(DataGridViewComboBoxColumn comboBox)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            Array keys = Enum.GetValues(typeof(EnumType));
            string[] values = Enum.GetNames(typeof(EnumType));
            int count = keys.Length;
            for (int i = 0; i < count; i++)
            {
                string s=values[i];
                s=Locale.Get(typeof(EnumType).Name+"."+s);
                dict.Add((int)keys.GetValue(i), s);
            }
            ArrayList ar = new ArrayList(dict);
            comboBox.DataSource = ar;
            comboBox.ValueMember = "Key";
            comboBox.DisplayMember = "Value";
            return count;
        }
        public static int Fill<EnumType>(ComboBox comboBox)
        {
            Dictionary<EnumType, string> dict = new Dictionary<EnumType, string>();
            Array keys = Enum.GetValues(typeof(EnumType));
            string[] values = Enum.GetNames(typeof(EnumType));
            int count=keys.Length;
            for (int i = 0; i < count; i++)
            {
                string s=values[i];
                s = Locale.Get(typeof(EnumType).Name + "." + s);
                dict.Add((EnumType)keys.GetValue(i), s);
            }
            ArrayList ar = new ArrayList(dict);
            comboBox.DataSource = ar;
            comboBox.ValueMember = "Key";
            comboBox.DisplayMember = "Value";
            return count;
        }

	}
}
