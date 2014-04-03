using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_FDZ_ChuLiJieShu : System.Web.UI.Page
{
    private ShareGZCL gzcl = new ShareGZCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            BindGridPage(BindGrid());
        }
    }

    private int BindGrid()
    {
        string sql = getQuerySql();
        DataSet ds = DataFunction.FillDataSet(sql);
        return gzcl.BindGridView(GridView1, ds);
    }

    private static string getQuerySql()
    {
        string sql = @"select * from t_fau_zb where zbguid in (
    select t.zbguid from t_fau_cllc t where to_char(t.clsj,'yyyy-mm-dd')=to_char(sysdate,'yyyy-mm-dd') group by zbguid )
    and fdzzt is not null and  (fdzzt = '结单' or (fdzzt='电话处理' and ldsj is not null))";
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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            //string sql = "select CLRY from t_fau_cllc t where zbguid='" + GridView1.DataKeys[e.Row.RowIndex].Value + "' and LCCZ ='发单'";
            //System.Web.UI.WebControls.Label lable = e.Row.Cells[7].FindControl("Lable1") as System.Web.UI.WebControls.Label;
            //lable.Text = DataFunction.GetStringResult(sql);
            string clr = e.Row.Cells[11].Text;
            string clsm = e.Row.Cells[12].Text;
            string zbguid = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();

            string sql = string.Format(@"select t.* from t_fau_cllc t where t.clsj=(select max(clsj) from t_fau_cllc  where zbguid='{0}') and t.zbguid='{0}'", zbguid);
            DataRow dr = DataFunction.GetSingleRow(sql);
            DataRow drs = DataFunction.GetSingleRow("select t.sdry,t.* from t_fau_zb t where t.zbguid='" + zbguid + "'");
            //if (clr == "" || clr == "&nbsp;")
            //{
            //    e.Row.Cells[11].Text = dr["clry"].ToString();
            //}
            if (clsm == "" || clsm == "&nbsp;")
            {
                e.Row.Cells[12].Text = dr["clsm"].ToString();
            }
            //故障时长
            if (drs["tssj"] == DBNull.Value)
            {
                return;
            }
            if (drs["jdsj"] == DBNull.Value)
            {
                DateTime dt = DateTime.Now;
                TimeSpan ts = dt - Convert.ToDateTime(drs["tssj"]);
                e.Row.Cells[13].Text = ts.TotalMinutes.ToString("f0");
            }
            else
            {
                TimeSpan ts = Convert.ToDateTime(drs["jdsj"]) - Convert.ToDateTime(drs["tssj"]);
                e.Row.Cells[13].Text = ts.TotalMinutes.ToString("f0");
            }

            //故障状态
            string clztStr=e.Row.Cells[14].Text;
            if (clztStr == "电话处理")
            {
                e.Row.Cells[14].Text = "留单";
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
