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

public partial class Admin_LogList : System.Web.UI.Page
{
    private int intCount = 0;
    public string strWhere = "", strSql = "", strLink = "", strMsg = "", strSessCode = "";
    public string UserName = "";
    classLog log = new classLog();

    protected void Page_Load(object sender, EventArgs e)
    {
        ////导航栏=================================================================================
        //if (Session["UserGroup"].ToString().Trim() != "0")
        //{
        //    strLink = "Msg.aspx?code=" + publ.GetUrlToSend("1") + "&msg=" + publ.GetUrlToSend("不允许你操作本模块！");
        //    Response.Redirect(strLink, false);
        //} 
        
        //Session["Msg"] = ""; Session["PageNavigator"] = ""; Session["PageSubTite"] = "系统日志";
        //Session["PageNavigator"] += "<a href='#' target='_self'>" + Session["PageSubTite"].ToString().Trim() + "</a>";
        ////导航栏=================================================================================

        if (!IsPostBack)
        {
            //初始化
            DropData_GridView.DataCode = "GridView";        //显示GridView的显示条数
            ZLTextBox_DateBegin.Text = (Convert.ToDateTime(DateTime.Now.ToString().Trim()).AddMonths(-1).ToString("yyyy-MM-dd")).ToString().Trim();
            ZLTextBox_DateEnd.Text = (Convert.ToDateTime(DateTime.Now.ToString().Trim()).ToString("yyyy-MM-dd")).ToString().Trim();
            

            GridView1.PageSize = int.Parse(Session["PageSize"].ToString().Trim());
            AspNetPager1.PageSize = int.Parse(Session["PageSize"].ToString().Trim());

            GridViewShow();

        }

    }

    public void byWhere()
    {
        strWhere = " and 1 = 1 ";
        //查询条件===============================================================================


        //日期条件
        string UserDateTimeBegin = ZLTextBox_DateBegin.Text.ToString().Trim();
        string UserDateTimeEnd = ZLTextBox_DateEnd.Text.ToString().Trim();
        if (UserDateTimeBegin != "" && UserDateTimeEnd != "")
        {
            UserDateTimeEnd = (Convert.ToDateTime(UserDateTimeEnd.ToString().Trim()).AddHours(24).ToString("yyyy-MM-dd")).ToString().Trim();
            strWhere += " and ( l.UserDateTime >= to_date( '" + UserDateTimeBegin + "','YYYY-MM-DD') and l.UserDateTime <= to_date('" + UserDateTimeEnd + "','YYYY-MM-DD') ) ";
        }
        else if (UserDateTimeBegin == "" && UserDateTimeEnd != "")
        {
            UserDateTimeEnd = (Convert.ToDateTime(UserDateTimeEnd.ToString().Trim()).AddHours(24).ToString("yyyy-MM-dd")).ToString().Trim();
            strWhere += " and ( l.UserDateTime <= to_date('" + UserDateTimeEnd + "','YYYY-MM-DD') ) ";
        }
        else if (UserDateTimeBegin != "" && UserDateTimeEnd == "")
        {
            strWhere += " and ( l.UserDateTime >= to_date( '" + UserDateTimeBegin + "','YYYY-MM-DD') ) ";
        }

        //关键字条件
        string Memo = TextBox1.Text.ToString().Trim();
        if (Memo.Length > 0)
        {
            strWhere += " and ( l.UserName like '%" + Memo + "%' or l.Ip like '%" + Memo + "%' or l.Title like '%" + Memo + "%' or l.Memo like '%" + Memo + "%' ) ";
        }

        strWhere += " ORDER by l.UserDateTime DESC ";

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
        
        tmpStr = "<a target='_self' href='LogPart.aspx?code=" + publ.GetUrlToSend(str.Trim()) + "'><font color='#0000FF'>查看</font></a>";
        return tmpStr;
    }


    //============================================================================================
    public void GridViewShow()
    {
        byWhere();
        //SqlDataSource1.SelectCommand = sysData.GetQueryStr(BY_WHERE);
        strSql = log.GetQueryStr(strWhere);

        //Response.Write(strSql);
        //return;

        DataSet ds = DataFunction.FillDataSet(string.Format("select * from (select rownum as rn,a.* from (" + strSql + ")a) where rn > {0} and rn <= {1}", AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1), AspNetPager1.PageSize * AspNetPager1.CurrentPageIndex));

        AspNetPager1.RecordCount = DataFunction.GetIntResult("select count(*) from (" + strSql + ")");

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


        /*
        DataView dv_Source = um.GetAllUsersByExtStr(strExt, AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
        DataTable tbUsers = dv_Source.ToTable();
        TTGridView.Bind(ref gvUser, ref tbUsers);
        */
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

    protected void Button_Delete_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in GridView1.Rows)
        {
            CheckBox check = (CheckBox)gvr.FindControl("ItemCheckBox");
            if (check.Checked)
            {
                log.Delete(GridView1.DataKeys[gvr.DataItemIndex].Value.ToString().Trim(), out strMsg);
            }
        }



        GridViewShow();
        Session["Msg"] = "<script>alert('删除成功！')</script>";
    }
}
