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

public partial class RadioData : System.Web.UI.UserControl
{
    protected string str_RadioButtonList_sel = "", str_RadioButtonList_all = "", str_RadioButtonList_datacode = "", str_RadioButtonList_RepeatDirection = "0";
    protected bool bool_RadioButtonList_vis = true, bool_RadioButtonList_ena = true, bool_RadioButtonList_top = true;
    protected int int_RadioButtonList_Col = 0;
    protected int int_RadioButtonList_Width = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RadioButtonList1_show();
            RadioButtonList1.SelectedValue = str_RadioButtonList_sel;

            if (str_RadioButtonList_RepeatDirection == "0")
            {
                RadioButtonList1.RepeatDirection = RepeatDirection.Horizontal; 
            }
            else
            {
                RadioButtonList1.RepeatDirection = RepeatDirection.Vertical;
            }

            RadioButtonList1.Visible = bool_RadioButtonList_vis;
            RadioButtonList1.Enabled = bool_RadioButtonList_ena;

            RadioButtonList1.RepeatColumns = int_RadioButtonList_Col;
            
            if (int_RadioButtonList_Width != 0)
            {
                RadioButtonList1.Width = int_RadioButtonList_Width;
            }


        }
    }

    public string SelectedValue
    {
        get
        {
            return RadioButtonList1.SelectedValue.ToString().Trim();
        }
    }

    public int Col
    {
        get { return int_RadioButtonList_Col; }
        set { int_RadioButtonList_Col = value; }
    }

    public int Width
    {
        get { return int_RadioButtonList_Width; }
        set { int_RadioButtonList_Width = value; }
    }

    public string Sel
    {
        get { return str_RadioButtonList_sel; }
        set { str_RadioButtonList_sel = value; }
    }

    public string Repeat
    {
        get { return str_RadioButtonList_RepeatDirection; }
        set { str_RadioButtonList_RepeatDirection = value; }
    }

    public bool Vis
    {
        get { return bool_RadioButtonList_vis; }
        set { bool_RadioButtonList_vis = value; }
    }

    public bool Ena
    {
        get { return bool_RadioButtonList_ena; }
        set { bool_RadioButtonList_ena = value; }
    }

    public string DataCode
    {
        get { return str_RadioButtonList_datacode; }
        set { str_RadioButtonList_datacode = value; }
    }


    public void RadioButtonList1_show()
    {
        RadioButtonList1.Items.Clear();

        string mySql = "";
        mySql = "select * from t_sys_Data where DataCode = '" + str_RadioButtonList_datacode.ToString().Trim() + "' and IsUse = '1' and IsVisible = '1' order by DisplayOrder asc";

        DataSet ds = DataFunction.FillDataSet(mySql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            ListItem li = new ListItem();
            li.Text = dr["DataMc"].ToString().Trim();
            li.Value = dr["DataDm"].ToString().Trim();
            RadioButtonList1.Items.Add(li);
        }

    }

}
