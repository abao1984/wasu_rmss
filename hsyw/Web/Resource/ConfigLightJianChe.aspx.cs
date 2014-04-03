using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_ConfigLightJianChe : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            GridView1.PageSize = Convert.ToInt32(PageSize.SelectedValue);
            GridView2.PageSize = Convert.ToInt32(PageSize1.SelectedValue);
        }
    }
    protected void BtnQuery_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGrid1());
        BindGridPage1(BindGrid2());
        //string strSql = "select * from rmss t where t.SUBSCRIBER_CODE not in (select subscriber_code from t_con_light_business  where  ywlx <>'骨干业务' and ZYHS_BJ=1)";
        //DataSet ds1 = DataFunction.FillDataSet(strSql);
        //GridView1.DataSource = ds1;
        //GridView1.DataBind();
        //strSql = "select * from t_con_bone_business t where t.ywbm not in (select ywbm from t_con_bone_business t where  zyhs_bj=1)";
        //DataSet ds2 = DataFunction.FillDataSet(strSql);
        //GridView2.DataSource = ds2;
        //GridView2.DataBind();
    }

    private int BindGrid1()
    {
        string strSql = "select * from rmss t where t.SUBSCRIBER_CODE not in (select subscriber_code from t_con_light_business  where  ywlx <>'骨干业务' and ZYHS_BJ=1)";
        DataSet ds1 = DataFunction.FillDataSet(strSql);
        GridView1.DataSource = ds1;
        int num = ds1.Tables[0].Rows.Count;
        if (num == 0)
        {
            ds1.Tables[0].Rows.Add(ds1.Tables[0].NewRow());
        }
        GridView1.DataBind();
        return num;
    }

    private int BindGrid2()
    {
        string strSql = "select * from t_con_bone_business t where t.ywbm not in (select ywbm from t_con_bone_business t where  zyhs_bj=1)";
        DataSet ds1 = DataFunction.FillDataSet(strSql);
        GridView2.DataSource = ds1;
        int num = ds1.Tables[0].Rows.Count;
        if (num == 0)
        {
            ds1.Tables[0].Rows.Add(ds1.Tables[0].NewRow());
        }
        GridView2.DataBind();
        return num;
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
        PageIndexLab.Text = Convert.ToString(GridPageList.SelectedIndex + 1);
        BindGrid1();
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
        PageIndexLab.Text = Convert.ToString(GridPageList.SelectedIndex + 1);
        BindGrid1();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage(BindGrid1());
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridPageList.SelectedIndex + 1);
        BindGrid1();
    }
    #endregion

    #region 分页管理1
    private void BindGridPage1(int DataCount)
    {
        GridView2.PageIndex = 0;
        int PageCount = DataCount / Convert.ToInt32(PageSize1.SelectedValue);
        if (DataCount % Convert.ToInt32(PageSize1.SelectedValue) > 0)
        {
            PageCount++;
        }
        GridPageList1.Items.Clear();
        for (int i = 1; i <= PageCount; i++)
        {
            ListItem LI = new ListItem(i.ToString() + "/" + PageCount.ToString(), Convert.ToString(i - 1));
            GridPageList1.Items.Add(LI);
        }
        DataCountLab1.Text = DataCount.ToString();
        PageCountLab1.Text = PageCount.ToString();
        PageIndexLab1.Text = "1";
    }


    protected void PrevButton1_Click(object sender, System.EventArgs e)
    {
        int PageIndex = GridPageList1.SelectedIndex;
        if (PageIndex == 0)
        {
            GridPageList1.SelectedIndex = GridPageList1.Items.Count - 1;
        }
        else
        {
            GridPageList1.SelectedIndex = PageIndex - 1;
        }
        GridView2.PageIndex = Convert.ToInt32(GridPageList1.SelectedValue);
        PageIndexLab1.Text = Convert.ToString(GridPageList1.SelectedIndex + 1);
        BindGrid2();
    }

    protected void NextButton1_Click(object sender, System.EventArgs e)
    {
        int PageIndex = GridPageList.SelectedIndex;
        if (GridPageList1.Items.Count - 1 == PageIndex)
        {
            GridPageList1.SelectedIndex = 0;
        }
        else
        {
            GridPageList1.SelectedIndex = PageIndex + 1;
        }
        GridView2.PageIndex = GridPageList1.SelectedIndex;
        PageIndexLab1.Text = Convert.ToString(GridPageList1.SelectedIndex + 1);
        BindGrid2();
    }

    protected void PageSize1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView2.PageSize = Convert.ToInt32(PageSize1.SelectedValue);
        BindGridPage(BindGrid2());
    }
    protected void GridPageList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView2.PageIndex = GridPageList1.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridPageList1.SelectedIndex + 1);
        BindGrid2();
    }
    #endregion
}
