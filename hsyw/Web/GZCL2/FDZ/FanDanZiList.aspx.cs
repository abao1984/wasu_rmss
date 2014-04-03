using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class Web_GZCL_FDZ_FanDanZiList : System.Web.UI.Page
{
    private ShareGZCL gzcl = new ShareGZCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            KHQY.Attributes.Add("ReadOnly", "true");
            ShowList();
            BindGridPage(BindGrid());
        }
    }

    private void ShowList()
    {
        string type = Request.QueryString["Type"];
        switch (type)
        {
            case "dhsl":
                GridView1.Columns[2].Visible = false;
                GridView1.Columns[3].Visible = false;
                GridView1.Columns[4].Visible = false;
                //GridView1.Columns[11].Visible = false;
                GridView1.Columns[13].Visible = false;
                GridView1.Columns[8].Visible = true;
                GridView1.Columns[17].Visible = false;
                GridView1.Columns[18].Visible = false;
                break;
            case "ddfd":
                GridView1.Columns[3].Visible = false;
                GridView1.Columns[4].Visible = false;
                GridView1.Columns[13].Visible = false;
               // GridView1.Columns[12].Visible = false;
                break;
            case "wxfd":
                GridView1.Columns[4].Visible = false;
               // GridView1.Columns[13].Visible = false;
                GridView1.Columns[17].Visible = false;
                GridView1.Columns[18].Visible = false;
                break;
            case "yld":
               // GridView1.Columns[2].Visible = false;
                GridView1.Columns[3].Visible = false;
                //GridView1.Columns[13].Visible = false;
                GridView1.Columns[17].Visible = false;
                GridView1.Columns[18].Visible = false;
                break;
        }
    }

    private int BindGrid()
    {
        string sql = getQuerySql();
        DataSet ds = DataFunction.FillDataSet(sql);
        return gzcl.BindGridView(GridView1, ds);
    }

    private string getQuerySql()
    {
        string type = Request.QueryString["Type"];
        string sql = "";
        string userID = Session["UserID"].ToString();
        DateTime datetime = DateTime.Now;

        switch (type)
        {
            case "dhsl":
                sql = string.Format(@"select t.*, t.rowid from t_fau_zb2 t where t.fdzzt='电话处理' and ( ldsj is null or to_char(t.ldsj,'yyyy-mm-dd')<='{0}')", datetime.AddDays(-1).ToString("yyyy-MM-dd"));
                per.Text = "dhsl";
                //GridView1.Columns[13].HeaderText = "锁定人员";
                break;
            case "ddfd":
                sql = "select t.*, t.rowid from t_fau_zb2 t where t.fdzzt='调度发单'";
                per.Text = "ddfd";
                //GridView1.Columns[13].HeaderText = "锁定人员";
                break;
            case "wxfd":
                sql = "select t.*, t.rowid from t_fau_zb2 t where t.fdzzt='维修返单'";
                per.Text = "wxfd";
                //GridView1.Columns[7].Visible = true;
                break;
            case "yld":
                sql = "select t.*, t.rowid from t_fau_zb2 t where t.fdzzt='遗单'";
                per.Text = "yld";
                break;
        }

        if (KHMC.Text.Trim() != "")
        {
            sql += " and GZMC like '%" + KHMC.Text.Trim() + "%'";
        }

        if (TSBH.Text != "")
        {
            sql += " and GZBH like '%" + TSBH.Text.Trim() + "%'";
        }

        if (KHQY.Text != "")
        {
            sql += " and KHQY='" + KHQY.Text.Trim() + "'";
        }
        if (Session["ISSUPER"].ToString() != "1")
        {
            if (Session["FWQY"] != null)
            {
                string sql1 = "";
                string[] fwqy = Session["FWQY"].ToString().Split(',');
                foreach (string fwqy1 in fwqy)
                {

                    if (sql1 != "")
                    {
                        sql1 += " or t.KHQYID like '" + fwqy1 + "%'";
                    }
                    else
                    {
                        sql1 += "t.KHQYID like '" + fwqy1 + "%'";
                    }
                }
                sql += " and (" + sql1 + ")";
            }
            else
            {
                sql += " and 1<>1";
            }
        }

        if (type == "dhsl")
        {
            sql += " order by tssj";
        }
        return sql;
    }

    #region 分页管理
    private void BindGridPage(int DataCount)
    {
        GridView1.PageIndex = 0;
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
        GridView1.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(GridView1.PageIndex + 1);
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
        GridView1.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridView1.PageIndex + 1);
        BindGrid();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage(BindGrid());
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridView1.PageIndex + 1);
        BindGrid();
    }
    #endregion

    protected void BtnJS_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGrid());
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            //string sql = "select CLRY from t_fau_cllc2 t where zbguid='" + GridView1.DataKeys[e.Row.RowIndex].Value + "' and LCCZ ='发单'";
            //System.Web.UI.WebControls.Label lable = e.Row.Cells[7].FindControl("Lable1") as System.Web.UI.WebControls.Label;
            //lable.Text = DataFunction.GetStringResult(sql);
            string clr=e.Row.Cells[13].Text;
            string clsm=e.Row.Cells[15].Text;
            string zbguid = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
            
            string sql = string.Format(@"select t.* from t_fau_cllc2 t where t.clsj=(select max(clsj) from t_fau_cllc2  where zbguid='{0}') and t.zbguid='{0}'", zbguid);
            DataRow dr = DataFunction.GetSingleRow(sql);
            DataRow drs = DataFunction.GetSingleRow("select t.sdry,t.* from t_fau_zb2 t where t.zbguid='" + zbguid + "'");
            //if(clr==""||clr=="&nbsp;")
            //{
            //    if (per.Text == "dhsl" || per.Text == "ddfd")
            //    {
            //        e.Row.Cells[13].Text = drs["sdry"].ToString();
            //    }
            //    else
            //    {
            //        e.Row.Cells[13].Text = dr["clry"].ToString();
            //    }
            //}
            //switch (per.Text)
            //{
            //    case "dhsl":
            //    case "ddfd":
            //        e.Row.Cells[13].Text = drs["sdry"].ToString();
            //        break;
            //    case "wxfd":
            //        e.Row.Cells[13].Text = clr;
            //        break;
            //    default :
            //        e.Row.Cells[13].Text = dr["clry"].ToString();
            //        break;

            //}
            if (per.Text == "dhsl" || per.Text == "ddfd")
            {
                //e.Row.Cells[13].Text = drs["sdry"].ToString();
                if (per.Text == "ddfd")
                {
                    e.Row.Cells[18].Text = DataFunction.GetStringResult("select clry from t_fau_cllc2 where zbguid='" + zbguid + "' order by clsj desc");
                }
            }
            //*by hangyt@12.3.1
            else if (per.Text == "wxfd")
            {
                e.Row.Cells[13].Text = clr;
            }
            //*by hangyt@12.3.1
            else
            {
                e.Row.Cells[13].Text = dr["clry"].ToString();
            }

            if (clsm == "" || clsm == "&nbsp;")
            {
                e.Row.Cells[15].Text = dr["clsm"].ToString();
            }
            //故障时长
            if (drs["tssj"]==DBNull.Value)
            {
                return;
            }
            if (drs["jdsj"] == DBNull.Value)
            {
                DateTime dt = DateTime.Now;
                TimeSpan ts =dt - Convert.ToDateTime(drs["tssj"]);
                e.Row.Cells[16].Text = ts.TotalMinutes.ToString("f0");
            }
            else
            {
                TimeSpan ts = Convert.ToDateTime(drs["jdsj"]) - Convert.ToDateTime(drs["tssj"]);
                e.Row.Cells[16].Text = ts.TotalMinutes.ToString("f0");
            }
            //留单
            if (drs["LDSJ"].ToString() != "" && drs["fdzzt"].ToString() == "电话处理")
            {
                e.Row.BackColor = Color.Wheat;
            }
        }
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string order = e.SortExpression;
        string sql = getQuerySql();
        DataView dv = DataFunction.FillDataSet(sql).Tables[0].DefaultView;
        if (this.SortAscending)
        {
            this.SortAscending = false;
            order = e.SortExpression + " asc ";
        }
        else
        {
            this.SortAscending = true;
            order = e.SortExpression + " desc ";
        }
        dv.Sort = order;
        ViewState["SortName"] = order;
        GridView1.DataSource = dv;
        GridView1.DataBind();
        BindGridPage(dv.Table.Rows.Count);
    }

    bool SortAscending
    {
        get
        {
            object o = ViewState["SortAscending"];
            if (o == null)
            {
                return true;
            }
            return (bool)o;
        }
        set
        {
            ViewState["SortAscending"] = value;
        }
    }
}
