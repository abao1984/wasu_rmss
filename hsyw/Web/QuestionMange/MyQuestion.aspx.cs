using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_QuestionMange_MyQuestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        if (!Page.IsPostBack)
        {
            BindDDL();
            InitPrivate();
            GVQuestion.Attributes.Add("BorderColor", "#5B9ED1");
            GVQuestion.PageSize = Convert.ToInt32(PageSize.SelectedValue);
            BindGridPage(BindGrid());
        }
    }
    private void BindDDL()
    {
        ShareFunction.BindEnumDropList(WTLY, "WTLY");
        ShareFunction.BindEnumDropList(ZXTLB, "ZXTLB");
        ShareFunction.BindEnumDropList(WTYXJ, "WTYXJ");
        ShareFunction.BindEnumDropList(WTZT, "WTZT");
    }
    private int BindGrid()
    {
        string sql = "select t.* from t_ques_info t left join t_ques_clr c on t.ID = c.QUESID and c.ISDQ = '1' where 1=1";
        if (WTMC.Text != "")
        {
            sql += " and WTMC like '%" + WTMC.Text + "%'";
        }
        if (WTLY.SelectedValue != "")
        {
            sql += " and WTLY = '" + WTLY.SelectedValue + "'";
        }
        if (ZXTLB.SelectedValue != "")
        {
            sql += " and ZXTLB = '" + ZXTLB.SelectedValue + "'";
        }
        if (WTYXJ.SelectedValue != "")
        {
            sql += " and WTYXJ = '" + WTYXJ.SelectedValue + "'";
        }
        if (WTZT.SelectedValue != "")
        {
            sql += " and WTZT = '" + WTZT.SelectedValue + "'";
        }
        if (FZBM.Text != "")
        {
            sql += " and FZBM like '%" + FZBM.Text + "%'";
        }
        //else
        //{
        //    if (ZTXZ.Text != "问题指派" && ZTXZ.Text != "新建")
        //    {
        //        sql += " and FZBM like '%" + Session["BranchName"].ToString() + "%'";
        //    }
        //}
        if (WCSJ1.Text != "")
        {
            sql += " and WCSJ >=  to_date('" + WCSJ1.Text + "','YYYY-MM-DD')";
        }
        if (WCSJ2.Text != "")
        {
            sql += " and WCSJ <= to_date('" + WCSJ2.Text + "','YYYY-MM-DD')";
        }
        if (Request.QueryString["SORT"] != "WTPS")
        {
            sql += " and c.UNAME = '" + Session["UserName"].ToString() + "'";
        }
        if (!string.IsNullOrEmpty(ZTXZ.Text))
        {
            sql += " and t.ztxz='" + ZTXZ.Text + "'";
        }
        sql += " order by c.SJ desc";
        DataSet ds = DataFunction.FillDataSet(sql);
        int num = ds.Tables[0].Rows.Count;
        if (num == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
        }
        GVQuestion.DataSource = ds;
        GVQuestion.DataBind();
        if (num == 0)
        {
            int count = GVQuestion.Columns.Count;
            GVQuestion.Rows[0].Cells.Clear();
            GVQuestion.Rows[0].Cells.Add(new TableCell());
            GVQuestion.Rows[0].Cells[0].ColumnSpan = count;
            GVQuestion.Rows[0].Cells[0].Text = "没有相关数据！";
            GVQuestion.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            return 0;
        }
        return ds.Tables[0].Rows.Count;
    }
    #region 分页管理
    private void BindGridPage(int DataCount)
    {
        GVQuestion.PageIndex = 0;
        int PageCount = DataCount / Convert.ToInt32(PageSize.SelectedValue);
        if (DataCount % Convert.ToInt32(PageSize.SelectedValue) > 0)
        {
            PageCount++;
        }
        GridPageList.Items.Clear();
        for (int i = 1; i <= PageCount; i++)
        {
            ListItem LI = new ListItem(i.ToString() + "/" + PageCount.ToString(), Convert.ToString(i - 1));
            GridPageList.Items.Add(LI);
        }
        DataCountLab.Text = DataCount.ToString();
        PageCountLab.Text = PageCount.ToString();
        PageIndexLab.Text = "1";
    }


    protected void PrevButton_Click(object sender, System.EventArgs e)
    {
        int PageIndex = GridPageList.SelectedIndex;
        if (PageIndex == 0)
        {
            GridPageList.SelectedIndex = GridPageList.Items.Count - 1;
        }
        else
        {
            GridPageList.SelectedIndex = PageIndex - 1;
        }
        GVQuestion.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(GVQuestion.PageIndex + 1);
        BindGrid();
    }

    protected void NextButton_Click(object sender, System.EventArgs e)
    {
        int PageIndex = GridPageList.SelectedIndex;
        if (GridPageList.Items.Count - 1 == PageIndex)
        {
            GridPageList.SelectedIndex = 0;
        }
        else
        {
            GridPageList.SelectedIndex = PageIndex + 1;
        }
        GVQuestion.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GVQuestion.PageIndex + 1);
        BindGrid();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVQuestion.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage(BindGrid());
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVQuestion.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GVQuestion.PageIndex + 1);
        BindGrid();
    }
    #endregion
    protected void GVQuestion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string sort = Convert.ToString(Request.QueryString["SORT"]);
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (sort != "WTPS")
            {
                //只在评审页面里把是否已评审显示出来 其它页面不显示 罗耀斌
                e.Row.Cells[e.Row.Cells.Count - 2].Attributes.Add("class", "isshow");
            }
        }
        
        if (e.Row.RowIndex > -1)
        {
            string id = GVQuestion.DataKeys[e.Row.RowIndex].Value.ToString();
            string name = e.Row.Cells[1].Text;
            //e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
            string op = "";
            if (sort=="WTPS")
            {
                string url = "", tooltip = ""; ;
                string ph = DataFunction.GetStringResult("select pf from t_ques_info t where t.id='" + id + "'");
                if (string.IsNullOrEmpty(ph))
                {
                    url="../Images/Small/y.gif";
                    tooltip = "未评审";
                }
                else
                {
                    url = "../Images/Small/success-sm.gif";
                    tooltip = "已评审";
                }
                ((Image)e.Row.Cells[e.Row.Cells.Count - 2].FindControl("ImgTp")).ImageUrl = url;
                ((Image)e.Row.Cells[e.Row.Cells.Count - 2].FindControl("ImgTp")).ToolTip = tooltip;
                op = "PS";
            }
            else
            {
                if (sort == "XJWT")
                {
                    op = "XJ";
                }
                else if (sort == "WDWT")
                {
                    op = "CL";
                }
                else if (sort == "WTZP")
                {
                    op = "ZP";
                }
                else
                {
                    op = "Query";
                }
                //只在评审页面里把是否已评审显示出来 其它页面不显示 罗耀斌
                e.Row.Cells[e.Row.Cells.Count - 2].Attributes.Add("class", "isshow");
            }
            e.Row.Attributes.Add("ondblclick", "windowOpen('" + id + "','" + name + "','"+op+"')");
            e.Row.Cells[e.Row.Cells.Count - 1].Text = "<a href=# onclick=\"windowOpen('" + id + "','" + name + "','"+op+"')\">详细</a>";
        }
    }

    protected void CheckAll(object sender, EventArgs e)
    {
        CheckBox cbx = (CheckBox)sender;
        foreach (GridViewRow gvr in GVQuestion.Rows)
        {
            CheckBox ch = (CheckBox)gvr.FindControl("ItemCheckBox");
            ch.Checked = cbx.Checked;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string ids = "''";
        foreach (GridViewRow gvr in GVQuestion.Rows)
        {
            CheckBox ch = (CheckBox)gvr.FindControl("ItemCheckBox");
            //ch.Checked = cbx.Checked;
            if (ch.Checked)
            {
                ids += ",'" + GVQuestion.DataKeys[gvr.RowIndex]["ID"].ToString() + "'";
            }
        }

        if (ids != "''")
        {
            string[] sql = new string[3];
            sql[0] = string.Format("delete from T_QUES_INFO where ID in ({0})", ids);
            sql[1] = string.Format("delete from T_QUES_CLR where QUESID in ({0})", ids);
            sql[2] = string.Format("delete from T_QUES_JJJH where QUESID in ({0})", ids);
            DataFunction.ExecuteTransaction(sql);
        }
        BindGridPage(BindGrid());
    }
    protected void QueryButton_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGrid());
    }
    private void InitPrivate()
    { 
        switch(Request.QueryString["SORT"])
        {
            case "XJWT":
                BtnNew.Visible = true;
                ZTXZ.Text = "新建";
                break;
            case "WTZP":
                BtnNew.Visible = false;
                WTZT.SelectedValue = "未完成";
                ZTXZ.Text = "问题指派";
                WTZT.Enabled = false;
                break;
            case"WDWT":
                BtnNew.Visible = false;
                WTZT.SelectedValue = "未完成";
                ZTXZ.Text = "问题处理";
                WTZT.Enabled = false;
                break;
            case "WTPS":
                BtnNew.Visible = false;
                WTZT.SelectedValue = "已完成";
                ZTXZ.Text = "问题评审";
                WTZT.Enabled = false;
                break;
          
        }
    }
}
