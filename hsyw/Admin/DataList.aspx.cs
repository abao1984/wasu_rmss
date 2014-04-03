using System;
using System.Collections;
using System.Configuration;
using System.Data;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;

using System.Data.Common;
//using yueue.ADOKeycap;

public partial class Web_sysData_DataList : System.Web.UI.Page
{
    private int intCount = 0;
    public string strWhere = "", strSql = "", strLink = "", strMsg = "", strSessCode = "";
    public string UserName = "";

    classData data = new classData();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            DropData_GridView.DataCode = "GridView";        //显示GridView的显示条数


            GridView1.PageSize = int.Parse(Session["PageSize"].ToString().Trim());
            AspNetPager1.PageSize = int.Parse(Session["PageSize"].ToString().Trim());

            GridViewShow();

        }

    }

    public void byWhere()
    {
        strWhere = " and 1 = 1 ";
        //查询条件===============================================================================

        string Memo = TextBox1.Text.ToString().Trim();

        if (Memo.Length > 0)
        {
            strWhere += " and ( d.DataCode like '%" + Memo + "%' or d.DataName like '%" + Memo + "%' or d.DataMc like '%" + Memo + "%' ) ";
        }

        strWhere += " ORDER by d.DataCode asc ";

    }
    public string strTrim(string str)
    {
        string tmpStr;
        tmpStr = str.Trim();
        return tmpStr;
    }

    public string strUrl(string str)
    {
        string tmpStr;
        //tmpStr = "<a target='_self' href='#' onclick=\"WindowOpen('/Admin/Data/Sys_Data_Sub_List.aspx?code=" + str.Trim() + "', '540px', '700px')\"><font color='#0000FF'>查看</font></a>";

        tmpStr = "<a target='_self' href='DataSubList.aspx?code=" + publ.GetUrlToSend(str.Trim()) + "'><font color='#0000FF'>查看字段</font></a>";
        return tmpStr;
    }


    public string strAdd(string str)
    {
        string tmpStr;
        tmpStr = "<a target='_self' href='DataInsert.aspx?code=" + publ.GetUrlToSend(str.Trim()) + "'><font color='#0000FF'>增加字段</font></a>";
        return tmpStr;
    }

    //============================================================================================
    public void GridViewShow()
    {
        byWhere();
        //SqlDataSource1.SelectCommand = sysData.GetQueryStr(BY_WHERE);
        strSql = "SELECT DISTINCT d.DataCode, d.DataName,d.menu from t_sys_Data d where 1 = 1 " + strWhere;
        //DataSet ds = dbSys.ExecuteDataSet(strSql, AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1), AspNetPager1.PageSize);
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from (select rownum as rn,a.* from (" + strSql + ") a ) where rn > {0} and rn <= {1}", AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1), AspNetPager1.PageSize * AspNetPager1.CurrentPageIndex));
        AspNetPager1.RecordCount = DataFunction.GetIntResult("select count(d.DataCode) from t_sys_Data d where 1 = 1"+strWhere);

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
            int nColumnCount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = nColumnCount;
            GridView1.Rows[0].Cells[0].Text = "无记录";
            GridView1.RowStyle.Height = 30;

            GridView1.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else
        {
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('../App_Themes/" + this.StyleSheetTheme + "/Images/bbs_title_bg.gif')");
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GridViewShow();
    }
    public int GetCount()
    {
        intCount = intCount + 1;
        return Convert.ToInt32(AspNetPager1.CurrentPageIndex - 1) * Convert.ToInt32(AspNetPager1.PageSize) + intCount;
    }
    protected void CheckAll(object sender, EventArgs e)
    {
        CheckBox cbx = (CheckBox)sender;
        foreach (GridViewRow gvr in GridView1.Rows)
        {
            CheckBox ch = (CheckBox)gvr.FindControl("ItemCheckBox");
            ch.Checked = cbx.Checked;
        }
    }
    //============================================================================================
    protected void Button_GridView_Click(object sender, EventArgs e)
    {
        if (DropData_GridView.SelectedValue.ToString().Trim() != "ZZ" && DropData_GridView.SelectedValue.ToString().Trim() != "")
        {
            GridView1.PageSize = Convert.ToInt32(DropData_GridView.SelectedValue.ToString().Trim());
            AspNetPager1.PageSize = Convert.ToInt32(DropData_GridView.SelectedValue.ToString().Trim());
            GridViewShow();
        }
    }

    protected void Button_Query_Click(object sender, EventArgs e)
    {
        GridViewShow();
    }
    protected void Button_AddData_Click(object sender, EventArgs e)
    {
        strLink = "DataInsert.aspx?code=" + publ.GetUrlToSend("0");
        Response.Redirect(strLink, false);
    }
}
