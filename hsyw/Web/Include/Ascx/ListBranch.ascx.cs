using System;
using System.Collections;
using System.Configuration;
using System.Data;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;

using System.Data.Common;
//using yueue.ADOKeycap;
using System.Data.OracleClient;

public partial class ListBranch : System.Web.UI.UserControl
{
    protected string str_ListBox_sel = "", str_ListBox_all = "", str_ListBox_datacode = "0";
    protected bool bool_ListBox_vis = true, bool_ListBox_ena = true, bool_ListBox_top = true, bool_ListBox_self = true;
    protected int int_ListBox_Width = 0, int_ListBox_Height = 0;
    classBranch branch = new classBranch();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListBox1_show();
            ListBox1.SelectedValue = str_ListBox_sel;
            //ListBox1.SelectedIndex = 2;

            ListBox1.Visible = bool_ListBox_vis;
            ListBox1.Enabled = bool_ListBox_ena;

            if (int_ListBox_Width != 0)
            {
                ListBox1.Width = int_ListBox_Width;
            }

            if (int_ListBox_Height != 0)
            {
                ListBox1.Height = int_ListBox_Height;
            }
        }
    }

    public string SelectedValue
    {
        get
        {
            return ListBox1.SelectedValue.ToString().Trim();
        }
    }

    public string Sel
    {
        get { return str_ListBox_sel; }
        set { str_ListBox_sel = value; }
    }

    public bool Vis
    {
        get { return bool_ListBox_vis; }
        set { bool_ListBox_vis = value; }
    }

    public bool Ena
    {
        get { return bool_ListBox_ena; }
        set { bool_ListBox_ena = value; }
    }

    public bool Self
    {
        get { return bool_ListBox_self; }
        set { bool_ListBox_self = value; }
    }

    public string DataCode
    {
        get { return str_ListBox_datacode; }
        set { str_ListBox_datacode = value; }
    }

    public int Width
    {
        get { return int_ListBox_Width; }
        set { int_ListBox_Width = value; }
    }

    public int Height
    {
        get { return int_ListBox_Height; }
        set { int_ListBox_Height = value; }
    }

    public void ListBox1_show()
    {
        ListBox1.Items.Clear();

        if (bool_ListBox_self)
        {
            ListItem li = new ListItem();
            string mySql = "";

            mySql = "select * from t_sys_Branch where BranchCode = '" + str_ListBox_datacode.ToString().Trim() + "' and IsUse = '1' and IsVisible = '1' order by DisplayOrder asc";

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

            ListBox1.Items.Add(li);

        }


        string BY_WHERE = "";
        //BY_WHERE += " and  branch_dm = '" + str_DropDownlist_datacode.ToString().Trim() + "' ";
        BY_WHERE += " and 1 = 1 ";
        BY_WHERE += " order by DisplayOrder asc";

        DataSet ds = DataFunction.FillDataSet(branch.GetQueryStr(BY_WHERE));
        BindData(ds.Tables[0], str_ListBox_datacode.ToString().Trim(), "");


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
            this.ListBox1.Items.Add(li);
            BindData(dt, Convert.ToString(drv["BranchCode"]), blank);
        }
    }
   

}