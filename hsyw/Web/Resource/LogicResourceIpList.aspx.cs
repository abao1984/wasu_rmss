using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_LogicResourceIpList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            GridViewLogicEquIP.Attributes.Add("BorderColor", "#5B9ED1");
            BindDropIPFD();
            ShareFunction.BindEnumDropList(YWDL, "YWDL");
            ShareFunction.BindEnumDropList(IPYWLX, "IPYWLX");
            ShareFunction.BindEnumDropList(DZFL, "DZFL");
            GridViewLogicEquIP.PageSize =Convert.ToInt32( PageSize.SelectedValue);
            BindGridPage(BindGridViewLogicEquIP());
        }
    }
    private void BindDropIPFD()
    {
        IPFD.Items.Clear();
        IPFD.Items.Add("");
        for (int i = 8; i <= 32; i++)
        {
            IPFD.Items.Add(i.ToString());
        }
       
    }
    private int BindGridViewLogicEquIP()
    {
        string sql = @"select p.ip as P_ip, t.* from 
t_logic_equ_ip t left join  t_logic_equ_ip p on t.p_guid=p.guid where 1=1";

        if (!string.IsNullOrEmpty(SSJF_GUID.Text))
        {
            sql += " and t.SSJF_GUID = '" + SSJF_GUID.Text + "'";
        }
        else
        {
            if (!string.IsNullOrEmpty(SSJF_CODE.Text))
            {
                sql += " and t.SSJF_CODE like '%" + SSJF_CODE.Text + "%'";
            }
            if (!string.IsNullOrEmpty(SSJF.Text))
            {
                sql += " and t.SSJF like '%" + SSJF.Text + "%'";
            }
        }

        if (!string.IsNullOrEmpty(SSQY_CODE.Text))
        {
            sql += " and t.SSQY_CODE = '" + SSQY_CODE.Text + "'";
        }
        else if (!string.IsNullOrEmpty(SSQY.Text))
        {
            sql += " and t.SSQY like '%" + SSQY.Text + "%'";
        }

        if (!string.IsNullOrEmpty(ISGH.SelectedValue))
        {
            sql += " and t.ISGH='" + ISGH.SelectedValue + "'";
        }
        if (!string.IsNullOrEmpty(IPYWLX.SelectedValue))
        {
            sql += " and t.IPYWLX='" + IPYWLX.SelectedValue + "'";
        }
        if (!string.IsNullOrEmpty(IPFPZT.SelectedValue))
        {
            sql += " and t.IPFPZT='" + IPFPZT.SelectedValue + "'";
        }
        if (!string.IsNullOrEmpty(IP1.Text))
        {
            sql += " and t.IP1='"+IP1.Text+"'";
        }
        if (!string.IsNullOrEmpty(IP2.Text))
        {
            sql += " and t.IP2='" + IP2.Text + "'";
        }
        if (!string.IsNullOrEmpty(IP3.Text))
        {
            sql += " and t.IP3='" + IP3.Text + "'";
        }
        if (!string.IsNullOrEmpty(IP4.Text))
        {
            sql += " and t.IP4='" + IP4.Text + "'";
        }
        if (!string.IsNullOrEmpty(IPFD.SelectedValue))
        {
            sql += " and t.ipfd='" + IPFD.SelectedValue + "'";
        }
        if (!string.IsNullOrEmpty(DZFL.SelectedValue))
        {
            sql += " and t.DZFL='" + DZFL.SelectedValue + "'";
        }

        if (!string.IsNullOrEmpty(YWDL.SelectedValue))
        {
            sql += " and t.YWDL='" + YWDL.SelectedValue + "'";
        }
        if (!string.IsNullOrEmpty(SFQY.SelectedValue))
        {
            sql += " and t.SFQY='" + SFQY.SelectedValue + "'";
        }

        sql += " order by t.ipfd ,t.ip1,t.ip2,t.ip3,t.ip4";
        int count = DataFunction.GetIntResult(string.Format("select count(*) from ({0})", sql));
        int n = 0;
        if (GridPageList.SelectedIndex > -1)
        {
            n = GridPageList.SelectedIndex;
        }
        int s = n * Convert.ToInt32(PageSize.SelectedValue);
        int e = s + Convert.ToInt32(PageSize.SelectedValue);
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from  (select rownum as rn,a.* from ({0}) a ) where rn>{1} and rn<={2}", sql, s, e));

        //DataSet ds = DataFunction.FillDataSet(sql);
        GridViewLogicEquIP.DataSource = ds;
        GridViewLogicEquIP.DataBind();
        return count;
    }
    protected void GridViewLogicEquIP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string guid = GridViewLogicEquIP.DataKeys[e.Row.RowIndex].Value.ToString();
            string name = e.Row.Cells[1].Text;
            int n = 0;
            if (GridPageList.SelectedIndex > -1)
            {
                n = GridPageList.SelectedIndex;
            }
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1+Convert.ToInt32(PageSize.SelectedValue)*n);
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
            e.Row.Attributes.Add("ondblclick", "windowOpen('" + guid + "','"+name+"')");
            e.Row.Cells[e.Row.Cells.Count - 1].Text = "<a href=# onclick=\"windowOpen('" + guid + "','"+name+"')\">详细</a>";

        }
    }
    protected void QueryButton_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGridViewLogicEquIP());
    }

    #region 分页管理
    private void BindGridPage(int DataCount)
    {
        GridViewLogicEquIP.PageIndex = 0;
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
       // GridViewLogicEquIP.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(GridPageList.SelectedIndex + 1);
        BindGridViewLogicEquIP();
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
       // GridViewLogicEquIP.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridPageList.SelectedIndex + 1);
        BindGridViewLogicEquIP();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
       // GridViewLogicEquIP.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage( BindGridViewLogicEquIP());
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  GridViewLogicEquIP.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridPageList.SelectedIndex + 1);
        BindGridViewLogicEquIP();
    }
    #endregion
}
