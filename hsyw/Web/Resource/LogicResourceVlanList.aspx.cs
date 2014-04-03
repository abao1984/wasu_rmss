using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_LogicResourceVlanList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            VlanList.Attributes.Add("BorderColor", "#5B9ED1");
            VlanList.PageSize = Convert.ToInt32(PageSize.SelectedValue);
            BindGridPage(BindGV());
        }
    }
    private int BindGV()
    {
        int count = 0;
        string sql = "select t.* from t_logic_equ_vlan t where 1=1";
        if (!string.IsNullOrEmpty(SSJF.Text))
        {
            sql += " and t.SSJF like '%" + SSJF.Text + "%'";
        }
        if (!string.IsNullOrEmpty(SSQY.Text))
        {
            sql += " and t.SSQY like '%" + SSQY.Text + "%'";
        }
        if (!string.IsNullOrEmpty(JF_CODE.Text))
        {
            sql += " and t.ssjf_code like '%" + JF_CODE.Text + "%'";
        }
        if (!string.IsNullOrEmpty(BH1.Text))
        {
            //只填一个时，按单个模糊查询
            if (string.IsNullOrEmpty(BH2.Text))
            {
                sql += " and t.VLANBH like '%" + BH1.Text + "%'";
            }
            else
            {
                sql += " and t.VLANBH >= '" + BH1.Text + "'";
            }
        }
        if (!string.IsNullOrEmpty(BH2.Text))
        {
            sql += " and t.VLANBH <= '" + BH2.Text + "'";
        }
        if (!string.IsNullOrEmpty(SFKFY.SelectedValue))
        {
            sql += " and t.SFKFY = '" + SFKFY.Text + "'";
        }
        if (!string.IsNullOrEmpty(ZYZT.Text))
        {
            sql += " and t.ZYZT = '" + ZYZT.Text + "'";
        }
        count =DataFunction.GetIntResult(string.Format( "select count(*) from ({0})",sql));
        int n = 0;
        if (GridPageList.SelectedIndex > -1)
        {
            n = GridPageList.SelectedIndex;
        }
        int s = n * Convert.ToInt32(PageSize.SelectedValue);
        int e = s + Convert.ToInt32(PageSize.SelectedValue);
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from  (select rownum as rn,a.* from ({0}) a ) where rn>{1} and rn<={2}", sql, s, e));
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            VlanList.DataSource = ds;
            VlanList.DataBind();
          //  int count1 = VlanList.Columns.Count;
            VlanList.Rows[0].Cells.Clear();
            BtnDel.Enabled = false;
        }
        else
        {
           // count = ds.Tables[0].Rows.Count;
           VlanList.DataSource = ds;
            VlanList.DataBind();
            BtnDel.Enabled = true;
        }
        return count;
    }
    protected void VlanList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string guid = VlanList.DataKeys[e.Row.RowIndex]["GUID"].ToString();
            if (!guid.Equals(""))
            {
                if (e.Row.Cells[4].Text.Equals("Y"))
                {
                    e.Row.Cells[4].Text = "是";
                }
                else if (e.Row.Cells[4].Text.Equals("N"))
                {
                    e.Row.Cells[4].Text = "否";
                }
                e.Row.Attributes.Add("ondblclick", "windowOpen('" + guid + "','')");
            }
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
        }
    }
  
    //查询
    protected void BtnQuery_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGV());
    }
    //删除
    protected void BtnDel_Click(object sender, EventArgs e)
    {
        string guids = "''";
        foreach (GridViewRow row in VlanList.Rows)
        {
            if (row.RowIndex > -1 && (row.FindControl("CheckBox1") as CheckBox).Checked)
            {
                guids += ",'" + VlanList.DataKeys[row.RowIndex]["GUID"].ToString() + "'";
            }
        }
        if (!guids.Equals("''"))
        {
            string sql = string.Format("delete from t_logic_equ_vlan where GUID in ({0})", guids);
            DataFunction.ExecuteNonQuery(sql);
            BindGV();
        }
    }
    #region 分页管理
    private void BindGridPage(int DataCount)
    {
        VlanList.PageIndex = 0;
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
      //  VlanList.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(GridPageList.SelectedIndex + 1);
        BindGV();
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
      //  VlanList.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridPageList.SelectedIndex + 1);
        BindGV();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        VlanList.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage(BindGV());
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  VlanList.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridPageList.SelectedIndex + 1);
        BindGV();
    }
    #endregion
    protected void Btn1_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGV());
    }

    //protected void BtnYwdl_Click(object sender, EventArgs e)
    //{
    //    ShareFunction.BindEnumDropList(YWDL, "YWDL");
    //}

    protected void CheckAll(object sender, EventArgs e)
    {
        CheckBox cbx = (CheckBox)sender;
        foreach (GridViewRow gvr in VlanList.Rows)
        {
            CheckBox ch = (CheckBox)gvr.FindControl("CheckBox1");
            ch.Checked = cbx.Checked;
        }
    }
   
}
