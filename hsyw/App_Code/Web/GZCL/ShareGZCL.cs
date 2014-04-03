using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

/// <summary>
///ShareGZCL 的摘要说明
/// </summary>
public class ShareGZCL
{
	public ShareGZCL()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public int BindGridView(GridView gv,DataSet ds)
    {
        int count = 0;
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gv.DataSource = ds;
            gv.DataBind();
            int count1 = gv.Columns.Count;
            gv.Rows[0].Cells.Clear();
            gv.Rows[0].Cells.Add(new TableCell());
            gv.Rows[0].Cells[0].Text = "没有相关的信息！";
            gv.Rows[0].Cells[0].ColumnSpan = count1;
            gv.Rows[0].Cells[0].Style.Add("text-align", "center");
        }
        else
        {
            count = ds.Tables[0].Rows.Count;
            gv.DataSource = ds;
            gv.DataBind();
        }
        return count;
    }

    [Ajax.AjaxMethod]
    public string GetModuleCode(string strhz)
    {
        string strtemp = "";
        int strlen = strhz.Length;
        for (int i = 0; i <= strlen - 1; i++)
        {
            strtemp += ShareFunction.hz2py(strhz.Substring(i, 1));
        }
        return strtemp;
    }
}
