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

public partial class Admin_Include_Ascx_DropData : System.Web.UI.UserControl
{
    protected string str_DropDownlist_sel = "", str_DropDownlist_all = "", str_DropDownlist_datacode = "";
    protected bool bool_DropDownlist_vis = true, bool_DropDownlist_ena = true, bool_DropDownlist_top = true;
    protected int int_DropDownlist_Width = 0;

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

    public string DataCode
    {
        get { return str_DropDownlist_datacode; }
        set { str_DropDownlist_datacode = value; }
    }

    public bool Top
    {
        get { return bool_DropDownlist_top; }
        set { bool_DropDownlist_top = value; }
    }

    public int Width
    {
        get { return int_DropDownlist_Width; }
        set { int_DropDownlist_Width = value; }
    }

    public void DropDownList1_show()
    {

        DropDownList1.Items.Clear();

        if (bool_DropDownlist_top)
        {
            ListItem select = new ListItem("----请选择----", "ZZ");
            DropDownList1.Items.Add(select);
        }

        string mySql = "";
        mySql = "select * from t_sys_Data where DataCode = '" + str_DropDownlist_datacode.ToString().Trim() + "' and IsUse = '1' and IsVisible = '1' order by DisplayOrder asc";

        DataSet ds = DataFunction.FillDataSet(mySql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            ListItem li = new ListItem();
            li.Text = dr["DataMc"].ToString().Trim();
            li.Value = dr["DataDm"].ToString().Trim();
            DropDownList1.Items.Add(li);
        }
    }

}
