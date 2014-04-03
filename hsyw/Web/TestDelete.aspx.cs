using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_TestDelete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string strSql = @"select p.propery_name, q.* from
(select u.unit_id,a.* from user_tab_columns a,t_res_sys_unit u where a.TABLE_NAME=u.table_name ) q
left join t_res_sys_property p on q.unit_id=p.unit_id and 
(q.column_name=p.filed_name  or q.column_name=p.filed_name||'_GUID' or  q.column_name=p.filed_name||'_CODE') 
 where p.propery_id is null
and q.column_name not in ('GUID','UPDATEDATETIME','CREATEDATETIME','UPDATEUSERNAME')";
        DataSet ds = DataFunction.FillDataSet(strSql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string sql = "alter table " + dr["table_name"] + " drop COLUMN " + dr["column_name"];
            DataFunction.ExecuteNonQuery(sql);
            sql = "alter table " + dr["table_name"] + "_LS drop COLUMN " + dr["column_name"];
            DataFunction.ExecuteNonQuery(sql);
        }
    }
}
