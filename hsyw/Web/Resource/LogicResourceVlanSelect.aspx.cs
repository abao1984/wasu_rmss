using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_LogicResourceVlanSelect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PK_GUID.Text = Request.QueryString["PK_GUID"];
            SSQY.Text = Request.QueryString["SSQY"];
            SSQY_CODE.Text = Request.QueryString["SSQY_CODE"];
            SSJF.Text = Request.QueryString["SSJF"];
            SSJF_GUID.Text = Request.QueryString["SSJF_GUID"];

            VlanList.Attributes.Add("BorderColor", "#5B9ED1");
            VlanList.PageSize = Convert.ToInt32(PageSize.SelectedValue);
            //8.23 dsh修改 （初始化不需要全部加载）
            //BindGridPage(BindGV());
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
        if (!string.IsNullOrEmpty(BH1.Text))
        {
            sql += " and t.VLANBH >= '" + BH1.Text + "'";
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
        sql += " order by VLANBH";
        DataSet ds = DataFunction.FillDataSet(sql);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            VlanList.DataSource = ds;
            VlanList.DataBind();
            int count1 = VlanList.Columns.Count;
            VlanList.Rows[0].Cells.Clear();
            //VlanList.Rows[0].Cells.Add(new TableCell());
            //VlanList.Rows[0].Cells[0].Text = "没有相关的信息！";
            //VlanList.Rows[0].Cells[0].ColumnSpan = count1;
            //VlanList.Rows[0].Cells[0].Style.Add("text-align", "center");
        }
        else
        {
            count = ds.Tables[0].Rows.Count;
            VlanList.DataSource = ds;
            VlanList.DataBind();
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

        e.Row.Cells[6].Visible = false;
    }

    //查询
    protected void BtnQuery_Click(object sender, EventArgs e)
    {
        //8.23 dsh 改 机房为必要条件
        if (SSJF.Text.IsNullOrEmpty())
        {
            ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('请选择机房')</script>");
        }
        else
        {
            BindGridPage(BindGV());
        }
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
        VlanList.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(VlanList.PageIndex + 1);
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
        VlanList.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(VlanList.PageIndex + 1);
        BindGV();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        VlanList.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage(BindGV());
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        VlanList.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(VlanList.PageIndex + 1);
        BindGV();
    }
    #endregion
    protected void Btn1_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGV());
    }
    protected void OKButton_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["PK_GUID"] == null)
        {
            string vlan = "";
            foreach (GridViewRow gr in VlanList.Rows)
            {
                CheckBox ch = (CheckBox)gr.FindControl("XZ");
                if (ch.Checked)
                {
                    vlan = gr.Cells[1].Text;
                    break;
                }
            }
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>parent.document.getElementById('VLAN').value = '" + vlan + "';parent.WindowClose();</script>");
        }
        else
        {
            string sql = string.Format("select * from t_con_logic_equ_vlan t where t.pk_guid='{0}'", PK_GUID.Text);
            DataSet ds = DataFunction.FillDataSet(sql);
            ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["VLANBH"] };
            string VlanName="";
            string VlanGuid="";
            foreach (GridViewRow gr in VlanList.Rows)
            {
                CheckBox ch = (CheckBox)gr.FindControl("XZ");
                if (ch.Checked)
                {
                    string strVpn = gr.Cells[1].Text;
                    string strVpnGuid = gr.Cells[6].Text;
                    DataRow dr = ds.Tables[0].Rows.Find(strVpn);
                    if (dr == null)
                    {
                        dr = ds.Tables[0].NewRow();
                        dr["GUID"] = Guid.NewGuid().ToString();
                        dr["PK_GUID"] = PK_GUID.Text;
                        dr["VLANBH"] = strVpn;
                        dr["VLANGUID"] = strVpnGuid;
                        ds.Tables[0].Rows.Add(dr);
                    }
                    string strZYZT = gr.Cells[5].Text;
                    string strSFFY = gr.Cells[4].Text;
                    if (strZYZT == "占用" && strSFFY != "是")
                    {
                        VlanGuid += "'" + strVpnGuid + "',";
                        VlanName += strVpn + ",";
                    }
                    //by hanyt@8.15

                    if (strZYZT != "占用")
                    {
                        sql = "update t_logic_equ_vlan set zyzt ='占用' where GUID = '" + strVpnGuid + "'";
                        DataFunction.ExecuteNonQuery(sql);
                    }
                    
                    //
                    
                    
                }
            }
            DataFunction.SaveData(ds, "t_con_logic_equ_vlan");

            if (VlanGuid != "")
            {
                txtVlanGuids.Text = VlanGuid.Substring(0,VlanGuid.Length-1);
                ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>sfqy('" + VlanName.Substring(0,VlanName.Length-1) + "')</script>");
            }
            string strScript = string.Format(@"<script>test()</script>");
            //string strScript = "<script> window.close();parent.WindowClose(); parent.document.getElementById('BtnVlan').click(); </script>";
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), strScript);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string sql = "update t_logic_equ_vlan set sfkfy ='Y' where GUID in (" + txtVlanGuids.Text + ")";
        DataFunction.ExecuteNonQuery(sql);
    }
}