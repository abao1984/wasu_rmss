using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_UserCopy : System.Web.UI.Page
{
    int intCount = 0;
    string strWhere = "";
    classUser user = new classUser();
    classBranch branch = new classBranch();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SSQY.Attributes.Add("readonly", "true");
            DropData_GridView.DataCode = "GridView";        //显示GridView的显示条数

            GridViewShow();
        }
    }
    //确定
    protected void BtnSure_Click(object sender, EventArgs e)
    {
        string username = publ.GetUrlToReceive(Request.QueryString["code"]);
        string fwqy = DataFunction.GetStringResult(string.Format("select FWQY from T_SYS_USER where USERNAME = '{0}'",username));
        string usernames = GetUserName1();
        if (usernames != "''" && CheckBox2.Checked)//访问区域
        {
            string sql = "";
            if (fwqy != "")
            {
                sql = string.Format("update T_SYS_USER set FWQY = '{0}' where USERNAME in ({1})", fwqy, usernames);
            }
            else
            {
                sql = string.Format("update T_SYS_USER set FWQY = null where USERNAME in ({0})",usernames);
            }
            DataFunction.ExecuteNonQuery(sql);     
        }
        if (usernames != "''" && CheckBox1.Checked)//权限
        {
            DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_SYS_R_USERGROUP where USERNAME='{0}'", username));
            string[] uname = GetUserName2().Split(',');
            foreach (string name in uname)
            {
                DataSet ds1 = DataFunction.FillDataSet(string.Format("select * from T_SYS_R_USERGROUP where USERNAME = '{0}'", name));
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DataRow dr = ds1.Tables[0].NewRow();
                    dr["ID"] = Guid.NewGuid().ToString();
                    dr["USERNAME"] = name;
                    dr["GROUPCODE"] = row["GROUPCODE"];
                    ds1.Tables[0].Rows.Add(dr);
                }
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    DataFunction.SaveData(ds1, "T_SYS_R_USERGROUP");
                }
            } 
        }
        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('操作成功！');</script>");
    }
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserList.aspx", false);
    }
    protected void Button_Query_Click(object sender, EventArgs e)
    {
        GridViewShow();
    }
    private bool ISQY(string branchcode)
    {
        return DataFunction.HasRecord(string.Format("select * from T_SYS_BRANCH where BRANCHCODE = '{0}' and ISQY = '1'", branchcode));
    }
    public void byWhere()
    {
        strWhere = " and 1 = 1 ";
        if (Txt_UserName.Text != "")
        {
            strWhere += " and u.USERNAME like '%" + Txt_UserName.Text + "%'";
        }
        if (Txt_Name.Text != "")
        {
            strWhere += " and u.USERREALNAME like '%" + Txt_Name.Text + "%'";
        }
        string BranchCode = SSQY_CODE.Text;
        if (BranchCode == "")
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
    public void GridViewShow()
    {
        byWhere();
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
    protected void CheckAll(object sender, EventArgs e)
    {
        CheckBox cbx = (CheckBox)sender;
        foreach (GridViewRow gvr in GridView1.Rows)
        {
            CheckBox ch = (CheckBox)gvr.FindControl("ItemCheckBox");
            ch.Checked = cbx.Checked;
        }
    }
    //得到USERNAME，带单引号的
    public string GetUserName1()
    {
        string tmpUserName = "";
        string UserName = "''";

        foreach (GridViewRow gvr in GridView1.Rows)
        {
            CheckBox check = (CheckBox)gvr.FindControl("ItemCheckBox");
            if (check.Checked)
            {
                tmpUserName = ", '" + GridView1.DataKeys[gvr.DataItemIndex].Value.ToString().Trim() + "'";
                UserName += tmpUserName;


            }
        }
        return UserName;
    }
    //得到USERNAME，不带单引号的
    public string GetUserName2()
    {
        string tmpUserName = "";
        string UserName = "";

        foreach (GridViewRow gvr in GridView1.Rows)
        {
            CheckBox check = (CheckBox)gvr.FindControl("ItemCheckBox");
            if (check.Checked)
            {
                tmpUserName = "," + GridView1.DataKeys[gvr.DataItemIndex].Value.ToString().Trim();
                UserName += tmpUserName;

            }
        }
        return UserName.Substring(1);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

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
    public int GetCount()
    {
        intCount = intCount + 1;
        return Convert.ToInt32(AspNetPager1.CurrentPageIndex - 1) * Convert.ToInt32(AspNetPager1.PageSize) + intCount;
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GridViewShow();
    }
    static public string strTrim(string str)
    {
        string tmpStr;
        tmpStr = str.Trim();
        return tmpStr;
    }
}
