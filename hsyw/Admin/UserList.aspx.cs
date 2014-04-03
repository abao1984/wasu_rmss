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

public partial class Admin_UserList : System.Web.UI.Page
{
    private int intCount = 0;
    public string strWhere = "", strSql = "", strLink = "", strMsg = "", UserName = "";

    classUser user = new classUser();
    classBranch branch = new classBranch();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            SSQY.Attributes.Add("readonly", "true");
            DropData_GridView.DataCode = "GridView";        //显示GridView的显示条数

            GridView1.PageSize = int.Parse(Session["PageSize"].ToString().Trim());
            AspNetPager1.PageSize = int.Parse(Session["PageSize"].ToString().Trim());

            GridViewShow();

        }

    }
    public void byWhere()
    {
        strWhere = " and 1 = 1 ";
        if (Txt_UserName.Text != "")
        {
            strWhere += " and u.USERNAME like '%"+Txt_UserName.Text+"%'";
        }
        if (Txt_Name.Text != "")
        {
            strWhere += " and u.USERREALNAME like '%"+Txt_Name.Text+"%'";
        }
        string BranchCode = SSQY_CODE.Text;
        if (BranchCode == "")
        {
            if(Session["ISSUPER"].ToString() != "1")
            {
                if (Session["FWQY"] != null)
                {
                    string sql1 = "";
                    string[] fwqy = Session["FWQY"].ToString().Split(',');
                    foreach (string fwqy1 in fwqy)
                    {

                        if (sql1 != "")
                        {
                            sql1 += " or u.BranchCode like '" + fwqy1 + "%'";
                        }
                        else
                        {
                            sql1 += "u.BranchCode like '" + fwqy1 + "%'";
                        }
                    }
                    strWhere += " and (" + sql1 + ")";
                }           
                else
                {
                    strWhere += " and 1<>1";
                }
            }
        }
        else if (ISQY(BranchCode))//是区域
        {
            strWhere += " and u.BranchCode like '" + BranchCode + "%'";
        }
        else//是部门
        {
            if (CheckBox_SubBranch.Checked)
            {
                strWhere += " and u.BranchCode like '" + BranchCode + "%'"; //and u.BranchCode <> ''";        //系统管理员的部门代码为空，所以在具体单位不显示
            }
            else
            {
                strWhere += " AND u.BranchCode = '" + BranchCode + "' ";
            }
        }
        strWhere += " ORDER BY u.DisplayOrder, u.UserName ASC ";
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

    static public string urlUpdate(string str,string fwqy)
    {
        string tmpStr = "";
        //tmpStr = "<a target='_self' href='UserUpdate.aspx?code=" + publ.GetUrlToSend(str.Trim()) + "'><font color='#0000FF'>编辑</font></a>";
        tmpStr = "<a target='_self' href='UserEdit.aspx?code=" + publ.GetUrlToSend(str.Trim()) + "&fwqy=" + publ.GetUrlToSend(fwqy.Trim()) + "'><font color='#0000FF'>编辑</font></a>";
        return tmpStr;
    }
    static public string urlCopy(string str)
    {
        string tmpStr;
        tmpStr = "<a target='_self' href='UserCopy.aspx?code=" + publ.GetUrlToSend(str.Trim()) + "'><font color='#0000FF'>复制</font></a>";
        return tmpStr;
    }
    static public string urlPass(string str)
    {
        string tmpStr;
        tmpStr = "<a target='_self' href='UserPassUpdate.aspx?code=" + publ.GetUrlToSend(str.Trim()) + "'><font color='#0000FF'>改密</font></a>";
        return tmpStr;
    }

    static public string urlGroup(string str)
    {
        string tmpStr;
        tmpStr = "<a target='_self' href='UserGroup.aspx?code=" + publ.GetUrlToSend(str.Trim()) + "'><font color='#0000FF'>用户权限</font></a>";
        return tmpStr;
    }
    protected string urlFWQY(string str,string fwqy)
    {
        string tmpStr;
        tmpStr = "<a target='_self' href='FWQYtree.aspx?code=" + publ.GetUrlToSend(str.Trim()) + "&fwqy=" + publ.GetUrlToSend(fwqy.Trim()) + "'><font color='#0000FF'>访问区域</font></a>";
        return tmpStr;
    }
    //============================================================================================
    public void GridViewShow()
    {
        byWhere();

        //DataSet ds = dbSys.ExecuteDataSet(user.GetQueryStr(strWhere), AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1), AspNetPager1.PageSize);
        //DataSet ds_count = dbSys.ExecuteDataSet(user.GetQueryStr(strWhere));

        //AspNetPager1.RecordCount = ds_count.Tables[0].Rows.Count;
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from (select rownum as rn,a.* from (" + user.GetQueryStr(strWhere) + ")a) where rn > {0} and rn <= {1}", AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1), AspNetPager1.PageSize * AspNetPager1.CurrentPageIndex));

        AspNetPager1.RecordCount = DataFunction.GetIntResult("select count(*) from (" + user.GetQueryStr(strWhere) + ")");


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
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    e.Row.Attributes.Add("style", "background-image:url('../App_Themes/" + this.StyleSheetTheme + "/Images/bbs_title_bg.gif')");
        //}
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


    protected void Button_GridView_Click(object sender, EventArgs e)
    {
        if (DropData_GridView.SelectedValue.ToString().Trim() != "ZZ" && DropData_GridView.SelectedValue.ToString().Trim() != "")
        {
            GridView1.PageSize = Convert.ToInt32(DropData_GridView.SelectedValue.ToString().Trim());
            AspNetPager1.PageSize = Convert.ToInt32(DropData_GridView.SelectedValue.ToString().Trim());
            GridViewShow();
        }
    }
    protected void Button_AddUser_Click(object sender, EventArgs e)
    {
        strLink = "UserInsert.aspx";
        Response.Redirect(strLink, false);
    }

    protected void Button_DeleteUser_Click(object sender, EventArgs e)
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
                    user.Delete(UserName, out strMsg);
                }
            }
        }


        GridViewShow();
        Session["Msg"] = "<script>alert('删除成功！')</script>";

    }
    protected void Button_Query_Click(object sender, EventArgs e)
    {
        GridViewShow();
    }



    //得到USERNAME，带单引号的
    public void GetUserName1()
    {
        string tmpUserName = "";
        UserName = "''";

        foreach (GridViewRow gvr in GridView1.Rows)
        {
            CheckBox check = (CheckBox)gvr.FindControl("ItemCheckBox");
            if (check.Checked)
            {
                tmpUserName = ", '" + GridView1.DataKeys[gvr.DataItemIndex].Value.ToString().Trim() + "'";
                UserName += tmpUserName;


            }
        }
    }
    //得到USERNAME，不带单引号的
    public void GetUserName2()
    {
        string tmpUserName = "";
        UserName = "";

        foreach (GridViewRow gvr in GridView1.Rows)
        {
            CheckBox check = (CheckBox)gvr.FindControl("ItemCheckBox");
            if (check.Checked)
            {
                tmpUserName = "," + GridView1.DataKeys[gvr.DataItemIndex].Value.ToString().Trim();
                UserName += tmpUserName;

            }
        }
    }
  
   
    private bool ISQY(string branchcode)
    {
        return DataFunction.HasRecord(string.Format("select * from T_SYS_BRANCH where BRANCHCODE = '{0}' and ISQY = '1'",branchcode));
    }
   
    //批量设置角色，密码，访问区域
    protected void BtnPLSetUp_Click(object sender, EventArgs e)
    {
        GetUserName1();
        Response.Redirect("UserEditAll.aspx?code=" + publ.GetUrlToSend(UserName), false);
    }
}
