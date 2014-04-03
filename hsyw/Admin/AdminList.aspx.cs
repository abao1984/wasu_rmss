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
////using System.Xml.Linq;
//51-A-s-p-x.com

using System.Data.Common;
//using yueue.ADOKeycap;

public partial class Admin_AdminList : System.Web.UI.Page
{
    private int intCount = 0;
    public string strWhere = "", strSql = "", strLink = "", strMsg = "";

    classAdmin admin = new classAdmin();

    protected void Page_Load(object sender, EventArgs e)
    {
        //导航栏=================================================================================
        if (Session["UserGroup"].ToString().Trim() != "0")
        {
            strLink = "Msg.aspx?code=" + publ.GetUrlToSend("1") + "&msg=" + publ.GetUrlToSend("不允许你操作本模块！");
            Response.Redirect(strLink, false);
        } 
        Session["Msg"] = ""; Session["PageNavigator"] = ""; Session["PageSubTite"] = "管理员设置";
        Session["PageNavigator"] += "<a href='#' target='_self'>" + Session["PageSubTite"].ToString().Trim() + "</a>";
        //导航栏=================================================================================

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

        strWhere += " ORDER BY a.UserGroup, a.UserName ASC ";
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

    static public string strTrim(string str)
    {
        string tmpStr;
        tmpStr = str.Trim();
        return tmpStr;
    }

    static public string urlUpdate(string str)
    {
        string tmpStr = "";
        tmpStr = "<a target='_self' href='AdminUpdate.aspx?code=" + publ.GetUrlToSend(str.Trim()) + "'><font color='#0000FF'>编辑</font></a>";

        return tmpStr;
    }

    static public string urlPass(string str)
    {
        string tmpStr;
        tmpStr = "<a target='_self' href='AdminPassUpdate.aspx?code=" + publ.GetUrlToSend(str.Trim()) + "'><font color='#0000FF'>改密</font></a>";
        return tmpStr;
    }
    //============================================================================================
    public void GridViewShow()
    {
        byWhere();

        //DataSet ds = dbSys.ExecuteDataSet(admin.GetQueryStr(strWhere), AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1), AspNetPager1.PageSize);
        string sql = string.Format("select * from (select rownum as rn,a.* from (" + admin.GetQueryStr(strWhere) + ") a) r where r.rn > {0} and r.rn <= {1}", AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1), AspNetPager1.PageSize * AspNetPager1.CurrentPageIndex);
        DataSet ds = DataFunction.FillDataSet(sql);

        AspNetPager1.RecordCount = DataFunction.GetIntResult("select count(*) from ("+admin.GetQueryStr(strWhere)+")");

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
    //============================================================================================


    protected void Button1_Click(object sender, EventArgs e)
    {
        GridViewShow();
    }

    protected void Button_GridView_Click(object sender, EventArgs e)
    {
        if (DropData_GridView.SelectedValue.ToString().Trim() != "ZZ" && DropData_GridView.SelectedValue.ToString().Trim() != "")
        {
            GridView1.PageSize = Convert.ToInt32(DropData_GridView.SelectedValue.ToString().Trim());
            AspNetPager1.PageSize = Convert.ToInt32(DropData_GridView.SelectedValue.ToString().Trim());
            GridViewShow();
        }
    }
    protected void Button_AddAdmin_Click(object sender, EventArgs e)
    {
        strLink = "AdminInsert.aspx";
        Response.Redirect(strLink, false);
    }

    protected void Button_DeleteAdmin_Click(object sender, EventArgs e)
    {
        string UserName = "";
        
        foreach (GridViewRow gvr in GridView1.Rows)
        {
            CheckBox check = (CheckBox)gvr.FindControl("ItemCheckBox");
            if (check.Checked)
            {
                UserName = GridView1.DataKeys[gvr.DataItemIndex].Value.ToString().Trim();
                strWhere = " and UserName = '" + UserName + "' ";

                //批量删除时不能删除自己
                if (Session["UserName"].ToString().Trim() != UserName)
                {
                    admin.Delete(strWhere, out strMsg);
                }
            }
        }


        GridViewShow();
        Session["Msg"] = "<script>alert('删除成功！')</script>";

    }
}
