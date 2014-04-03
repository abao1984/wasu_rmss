using System;
using System.Collections;
using System.Configuration;
using System.Data;
////using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
////using System.Xml.Linq;

using System.Data.Common;
using System.Data.OracleClient;

public partial class Admin_Include_Ascx_DropBranch : System.Web.UI.UserControl
{
    protected string str_DropDownlist_sel = "", str_DropDownlist_all = "", str_DropDownlist_datacode = "0";
    protected bool bool_DropDownlist_vis = true, bool_DropDownlist_ena = true, bool_DropDownlist_top = true, bool_DropDownlist_self = true;
    protected int int_DropDownlist_Width = 0;
    

    classBranch branch = new classBranch();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownList1_show();
            DropDownList1.SelectedValue = str_DropDownlist_sel;

            DropDownList1.Visible = bool_DropDownlist_vis;
            DropDownList1.Enabled = bool_DropDownlist_ena;

            if (int_DropDownlist_Width != 0)
            {
                DropDownList1.Width = int_DropDownlist_Width;
            }

        }
    }

    public string SelectedValue
    {
        get
        {
            return DropDownList1.SelectedValue.ToString().Trim();
        }
    }

    public string Sel
    {
        get { return str_DropDownlist_sel; }
        set { str_DropDownlist_sel = value; }
    }

    public bool Vis
    {
        get { return bool_DropDownlist_vis; }
        set { bool_DropDownlist_vis = value; }
    }

    public bool Ena
    {
        get { return bool_DropDownlist_ena; }
        set { bool_DropDownlist_ena = value; }
    }

    public bool Self
    {
        get { return bool_DropDownlist_self; }
        set { bool_DropDownlist_self = value; }
    }

    public string DataCode
    {
        get { return str_DropDownlist_datacode; }
        set { str_DropDownlist_datacode = value; }
    }

    public int Width
    {
        get { return int_DropDownlist_Width; }
        set { int_DropDownlist_Width = value; }
    }

    public void DropDownList1_show()
    {
        DropDownList1.Items.Clear();

        if (bool_DropDownlist_self)
        {
            ListItem li = new ListItem();
            string mySql = "";

            mySql = "select * from t_sys_Branch where BranchCode = '" + str_DropDownlist_datacode.ToString().Trim() + "' and IsVisible = '1' and IsUse = '1' order by DisplayOrder asc";

            DataRow dr = DataFunction.GetSingleRow(mySql);
            if (dr != null)
            {
                li.Text = dr["BranchName"].ToString().Trim();
                li.Value = dr["BranchCode"].ToString().Trim();
            }
            else
            {
                li.Text = Session["ClientName"].ToString().Trim();
                li.Value = Session["ClientCode"].ToString().Trim();
            }

            DropDownList1.Items.Add(li);

        }


        string strWhere = "";
        strWhere += " and 1 = 1 ";
        strWhere += " order by b.DisplayOrder asc ";


        DataSet ds = DataFunction.FillDataSet(branch.GetQueryStr(strWhere));
        BindData(ds.Tables[0], str_DropDownlist_datacode.ToString().Trim(), "");

    }

    private void BindData(DataTable dt, string str_branch_dm, string blank)
    {
        DataView dv = new DataView(dt);
        dv.RowFilter = " PBranchCode = '" + str_branch_dm.ToString() + "' ";

        if (str_branch_dm != "")
        {
            blank += "　";
        }
        foreach (DataRowView drv in dv)
        {
            ListItem li = new ListItem();
            li.Text = blank + drv["BranchName"].ToString();
            li.Value = drv["BranchCode"].ToString();
            this.DropDownList1.Items.Add(li);
            BindData(dt, Convert.ToString(drv["BranchCode"]), blank);
        }
    }


}