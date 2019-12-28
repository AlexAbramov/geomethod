using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Windows.Forms;
using Geomethod;
using Geomethod.Data;

namespace Geomethod.Data.Windows.Forms
{
	public partial class GroupingUserControl : UserControl
	{
		// groups
		string groupsTableName = "Groups";
		string groupsIdColName = "Id";
		string groupsNameColName = "Name";
		// ties
		string tiesTableName = "GroupTies";
		string tiesItemIdColName = "CompanyId";
		string tiesGroupIdColName = "GroupId";
		// param
		int itemId=0;
		string groupsWhereCondition = "";

		DataTable dt = new DataTable();
		DataColumn dcId;
		DataColumn dcChecked;

        public int ItemId{get { return itemId;}}

		public GroupingUserControl()
		{
			InitializeComponent();
		}

		public void Init(GmConnection conn, int itemId, string groupsTableName, string groupsIdColName, string groupsNameColName, string tiesTableName, string tiesItemIdColName, string tiesGroupIdColName, string groupsWhereCondition)
		{ 
			this.itemId = itemId;
			this.groupsTableName = groupsTableName;
			this.groupsIdColName = groupsIdColName;
			this.groupsNameColName = groupsNameColName;
			this.tiesTableName = tiesTableName;
			this.tiesItemIdColName = tiesItemIdColName;
			this.tiesGroupIdColName = tiesGroupIdColName;
			this.groupsWhereCondition = groupsWhereCondition;
			LoadData(conn);
		}
		
		private void TiesUserControl_Load(object sender, EventArgs e)
		{
            if (!DesignMode)
            {
            }
		}

        private void LoadData(GmConnection conn)
        {
            List<int> groupIds = new List<int>();// matching itemId
			string cmdText;
            if (itemId != 0)
            {
                cmdText = string.Format("select {0} from {1} where {2}={3}", tiesGroupIdColName, tiesTableName, tiesItemIdColName, itemId);
                using (DbDataReader dr = conn.ExecuteReader(cmdText))
                {
                    while (dr.Read())
                    {
                        groupIds.Add(dr.GetInt32(0));
                    }
                }
            }
			cmdText=string.Format("select {0}, {1} from {2}",groupsIdColName, groupsNameColName, groupsTableName);
			if (groupsWhereCondition.Length > 0) cmdText += " where " + groupsWhereCondition;
            conn.Fill(dt, cmdText);
            dcId = dt.Columns[0];
            dcChecked = dt.Columns.Add("Checked", typeof(bool));
            dcChecked.DefaultValue = false;
            foreach (DataRow dr in dt.Rows)
            {
                int groupId = (int)dr[dcId];
                dr[dcChecked] = groupIds.Contains(groupId);
            }
            gridView.DataSource = dt;
        }

		public void Save(GmConnection conn, int itemId)
		{
			this.itemId=itemId;
			if (itemId != 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					if (dr.RowState == DataRowState.Modified)
					{
						int groupId = (int)dr[dcId];
						bool isChecked = (bool)dr[dcChecked];
						GmCommand cmd = conn.CreateCommand();
						if (isChecked)
						{							
							cmd.CommandText = string.Format("insert into {0} ({1},{2}) values(@{1},@{2})",tiesTableName,tiesItemIdColName,tiesGroupIdColName);
						}
						else
						{
							cmd.CommandText = string.Format("delete from {0} where {1}=@{1} and {2}=@{2}", tiesTableName, tiesItemIdColName, tiesGroupIdColName);
						}
						cmd.AddInt(tiesItemIdColName, itemId);
						cmd.AddInt(tiesGroupIdColName, groupId);
						cmd.ExecuteNonQuery();
					}
				}
				dt.AcceptChanges();
			}
		}
	}
}
