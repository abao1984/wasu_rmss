using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_GuZhangShengGao : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if(!Page.IsPostBack)
        {
            GridView1.Attributes.Add("BorderColor", "#5B9ED1");
            GridView1.PageSize = Convert.ToInt32(PageSize.SelectedValue);
            KHQY.Attributes.Add("ReadOnly", "true");
            InitialControl();
        }
    }

    private void InitialControl()
    {
        string branchcode=Session["branchcode"].ToString();
        string sql = "select t.* from t_sys_branch t where t.branchcode='" + branchcode + "'";
        DataRow dr=DataFunction.GetSingleRow(sql);
        do
        {
            sql = "select t.* from t_sys_branch t where t.branchcode='" + dr["pbranchcode"].ToString() + "'";
            if (!DataFunction.HasRecord(sql))
            {
                break;
            }
            else
            {
                dr = DataFunction.GetSingleRow(sql);
            }
        }
        while (dr["isqy"].ToString() != "1");
        KHQY.Text = dr["PATH"].ToString();
        KHQYID.Text = dr["branchcode"].ToString();
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
    protected void BtnCJ_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>windowOpen(1)</script>");
    }

    private int BindGrid()
    {
        string sql = "select * from rmss t where  rownum <=100 ";
        if (YWBM.Text!="")
        {
            sql += " and subscriber_code like'%" + YWBM.Text.Trim() + "%'";
        }
        if (KHMC.Text!="")
        {
            sql += " and sub_name like '%" + KHMC.Text + "%'";
        }
        if (KHDZ.Text != "")
        {
            sql += " and ADDRESS like '%" + KHDZ.Text + "%'";
        }
        DataSet ds = DataFunction.FillDataSet(sql);
        int num = ds.Tables[0].Rows.Count;
        if(num == 0 )
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
        }
        GridView1.DataSource = ds;
        GridView1.DataBind();
        if(num==0)
        {
            GridView1.Rows[0].Cells.Clear();
            return 0;
        }
        return ds.Tables[0].Rows.Count;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string ywbm = GridView1.DataKeys[e.Row.RowIndex]["SUBSCRIBER_ID"].ToString();
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
            e.Row.Cells[0].Text = "<a href=# onclick=\"windowOpen('','" + ywbm + "')\">创建</a>";
        }
    }
    protected void QueryBtn_Click(object sender, EventArgs e)
    {
        if (YWBM.Text == "" && KHMC.Text==""&&KHDZ.Text=="")
        {
            ClientScript.RegisterStartupScript(GetType(),Guid.NewGuid().ToString(),"<script>alert('缺少必要条件')</script>");
            return;
        }
        BindGridPage(BindGrid());
    }
}
