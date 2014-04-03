using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_ConfigResourceIDCList : System.Web.UI.Page
{
    private ShareResource shareResource = new ShareResource();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            ZYHS_BJ.Text = Request.QueryString["ZYHS_BJ"];
            if (ZYHS_BJ.Text == "0")
            {
                BtnAdd.Visible = false;
            }
            ShareFunction.BindEnumDropList(YWLX, "IDC_YWLX");
            gvLogicEquIp.Attributes.Add("BorderColor", "#5B9ED1");
            gvLogicEquIp.PageSize = Convert.ToInt32(PageSize.SelectedValue);
            BindGridPage(BindgvLogicEquIp());
        }
    }

    protected void BtnQuery_Click(object sender, EventArgs e)
    {
        BindgvLogicEquIp();
    }

    protected void BtnDel_Click(object sender, EventArgs e)
    {
        string guids = "''";
        string SUBSCRIBER_ID = "''";
        foreach (GridViewRow gvr in gvLogicEquIp.Rows)
        {
            System.Web.UI.WebControls.CheckBox ch = (System.Web.UI.WebControls.CheckBox)gvr.FindControl("CheckBox1");
            //ch.Checked = cbx.Checked;
            if (ch.Checked)
            {
                guids += ",'" + gvLogicEquIp.DataKeys[gvr.RowIndex]["GUID"].ToString() + "'";
                string SUBSCRIBER = gvLogicEquIp.DataKeys[gvr.RowIndex]["SUBSCRIBER_ID"].ToString();
                if (SUBSCRIBER != "")
                {
                    //SUBSCRIBER=SUBSCRIBER.Substring(0, SUBSCRIBER.IndexOf('_'));
                    if (SUBSCRIBER.IndexOf("SGD") > -1)
                    {
                        int num1 = SUBSCRIBER.IndexOf("_") + 1;
                        SUBSCRIBER = SUBSCRIBER.Substring(num1);
                        SUBSCRIBER_ID += ",'" + SUBSCRIBER + "'";
                    }

                }

            }
        }

        if (guids != "''")
        {
            string sql = "delete T_CON_LOGIC_IDC where guid in(" + guids + ")";
            DataFunction.ExecuteNonQuery(sql);
        }
        if (SUBSCRIBER_ID != "''")
        {
            string sql = "delete RMSS_SGD where SUBSCRIBER_ID in(" + SUBSCRIBER_ID + ")";
            DataFunction.ExecuteNonQuery(sql);
        }
        BindGridPage(BindgvLogicEquIp());
    }

    protected void BtnExport_Click(object sender, EventArgs e)
    {

    }

    protected void gvLogicEquIp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string guid = gvLogicEquIp.DataKeys[e.Row.RowIndex].Value.ToString();
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
            e.Row.Attributes.Add("ondblclick", "windowOpen('" + guid + "')");
        }
    }

    protected void BtnRmss_Click(object sender, EventArgs e)
    {
        string sql = "select * from rmss t where t.SUBSCRIBER_ID='" + SUBSCRIBER_ID.Text + "'";
        ShareFunction.FillControlData(this.Page, DataFunction.GetSingleRow(sql));


    }

    protected void gvLogicEquIp_Sorting(object sender, GridViewSortEventArgs e)
    {
        string order = e.SortExpression;
        string sql = GetBaseQuerySQL();
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
        gvLogicEquIp.DataSource = dv;
        gvLogicEquIp.DataBind();
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

    private int BindgvLogicEquIp()
    {
        string sql = GetBaseQuerySQL();
        DataSet ds = DataFunction.FillDataSet(sql);
        gvLogicEquIp.DataSource = ds;
        gvLogicEquIp.DataBind();
        return ds.Tables[0].Rows.Count;
    }

    private string GetBaseQuerySQL()
    {
        string sql = @"select distinct IDC.*,
                R.CUSTOMER_NAME,
                r.customer_code,
                r.CUSTOMER_LEVEL,
                r.CUSTTYPE1,
                r.CUSTTYPE,
                r.region,
                r.sub_name,
                r.LINKMAN
  from T_CON_LOGIC_IDC IDC
  left join RMSS R on IDC.Subscriber_ID = R.SUBSCRIBER_ID where 1=1 ";
        if (!string.IsNullOrEmpty(SUBSCRIBER_CODE.Text.Trim()))
        {
            sql += " and R.SUBSCRIBER_CODE like '%" + SUBSCRIBER_CODE.Text + "%'";
        }
        if (!string.IsNullOrEmpty(YWLX.SelectedValue))
        {
            sql += " and YWLX = '" + YWLX.SelectedValue + "'";
        }
        if (!string.IsNullOrEmpty(JFMC.Text.Trim()))
        {
            sql += " and (IDC.WLJF like '%" + JFMC.Text + "%')";
        }
        if (!string.IsNullOrEmpty(JF_CODE.Text.Trim()))
        {
            sql += " and (IDC.wljf_code like '%" + JF_CODE.Text + "%')";
        }
        if (!string.IsNullOrEmpty(CUSTOMER_LEVEL.SelectedValue))
        {
            sql += " and r.CUSTOMER_LEVEL = '" + CUSTOMER_LEVEL.SelectedValue + "'";
        }
        if (!string.IsNullOrEmpty(CUSTOMER_CODE.Text.Trim()))
        {
            sql += " and r.CUSTOMER_CODE like '%" + CUSTOMER_CODE.Text + "%'";
        }
        if (!string.IsNullOrEmpty(ADDRESS.Text.Trim()))
        {
            sql += " and r.ADDRESS like '%" + ADDRESS.Text + "%'";
        }
        if (!string.IsNullOrEmpty(JRDW.Text.Trim()))
        {
            sql += " and IDC.JRDW like '%" + JRDW.Text + "%'";
        }
        if (!string.IsNullOrEmpty(PZKSSJ.Text.Trim()))
        {
            if (string.IsNullOrEmpty(PZJSSJ.Text.Trim()))
            {
                sql += " and to_char(IDC.DEV_START_TIME,'YYYY-MM-DD') = '" + PZKSSJ.Text + "'";
            }
            else
            {
                sql += " and to_char(IDC.DEV_START_TIME,'YYYY-MM-DD') >= '" + PZKSSJ.Text + "'";
                sql += " and to_char(IDC.DEV_END_TIME,'YYYY-MM-DD') <= '" + PZJSSJ.Text + "'";
            }
        }
        return sql;
    }

    #region 分页管理
    private void BindGridPage(int DataCount)
    {
        gvLogicEquIp.PageIndex = 0;
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
        gvLogicEquIp.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(gvLogicEquIp.PageIndex + 1);
        BindgvLogicEquIp();
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
        gvLogicEquIp.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(gvLogicEquIp.PageIndex + 1);
        BindgvLogicEquIp();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvLogicEquIp.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage(BindgvLogicEquIp());
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvLogicEquIp.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(gvLogicEquIp.PageIndex + 1);
        BindgvLogicEquIp();
    }
    #endregion
}
