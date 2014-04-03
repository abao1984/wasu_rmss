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

public partial class Web_sysData_DataSubList : System.Web.UI.Page
{
    private int intCount = 0, n = 0;
    public string strWhere = "", strSql = "", strLink = "", strMsg = "", strSessCode = "";
    public string UserName = "";

    classData data = new classData();

    protected void Page_Load(object sender, EventArgs e)
    {
        ////导航栏=================================================================================
        //if (Session["UserGroup"].ToString().Trim() != "0")
        //{
        //    strLink = "Msg.aspx?code=" + publ.GetUrlToSend("1") + "&msg=" + publ.GetUrlToSend("不允许你操作本模块！");
        //    Response.Redirect(strLink, false);
        //} 
        
        //Session["Msg"] = ""; Session["PageNavigator"] = ""; Session["PageSubTite"] = "查看字段";
        //Session["PageNavigator"] += "<a href='DataList.aspx' target='_self'>数据字典</a>";
        //Session["PageNavigator"] += "<a href='#' target='_self'>" + Session["PageSubTite"].ToString().Trim() + "</a>";
        ////导航栏=================================================================================
        strSessCode = publ.GetUrlToReceive(Request.QueryString["code"].ToString().Trim());

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
        strWhere = " and d.DataCode = '" + strSessCode + "' ";
        strWhere += " ORDER by d.DisplayOrder asc ";

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
        tmpStr = "<a target='_self' href='DataUpdate.aspx?code=" + publ.GetUrlToSend(str.Trim()) + "' ><font color='#0000FF'>编辑</font></a>";

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

        DataSet ds = DataFunction.FillDataSet(string.Format("select rownum,a.* from (" + data.GetQueryStr(strWhere) + ") a where rownum > {0} and rownum <= {1}", AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1), AspNetPager1.PageSize * AspNetPager1.CurrentPageIndex));

        AspNetPager1.RecordCount = DataFunction.GetIntResult("select count(*) from (" + data.GetQueryStr(strWhere) + ")");

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

    protected void Button_AddData_Click(object sender, EventArgs e)
    {
        strLink = "DataInsert.aspx?code=" + publ.GetUrlToSend(strSessCode);
        Response.Redirect(strLink, false);
    }
    protected void Button_Delete_Click(object sender, EventArgs e)
    {
        string tmp_id = "";
        foreach (GridViewRow gvr in GridView1.Rows)
        {
            CheckBox check = (CheckBox)gvr.FindControl("ItemCheckBox");
            if (check.Checked)
            {
                tmp_id = GridView1.DataKeys[gvr.DataItemIndex].Value.ToString().Trim();
                n = data.Delete(tmp_id, out strMsg);
            }
        }
        GridViewShow();
        Session["Msg"] = "<script>alert('删除提交成功！')</script>";

        return;
    }
    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
        strLink = "DataList.aspx";
        Response.Redirect(strLink, false);
    }
}
