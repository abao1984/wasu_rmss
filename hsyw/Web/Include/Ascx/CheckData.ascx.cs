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

public partial class CheckData : System.Web.UI.UserControl
{
    protected string str_CheckBoxList_sel = "", str_CheckBoxList_all = "", str_CheckBoxList_datacode = "", str_CheckBoxList_RepeatDirection = "0";
    protected bool bool_CheckBoxList_vis = true, bool_CheckBoxList_ena = true, bool_CheckBoxList_top = true;
    protected int int_CheckBoxList_Col = 0;
    protected int int_CheckBoxList_Width = 0;
    
    protected string str_value = "''";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckBoxList1_show();
            //CheckBoxList1.SelectedValue = str_CheckBoxList_sel;

            str_CheckBoxList_sel += "'" + str_CheckBoxList_sel + "',";
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (str_CheckBoxList_sel.IndexOf(CheckBoxList1.Items[i].Value.ToString().Trim()) > -1)
                {
                    CheckBoxList1.Items[i].Selected = true;
                }
            }



            if (str_CheckBoxList_RepeatDirection == "0")
            {
                CheckBoxList1.RepeatDirection = RepeatDirection.Horizontal;
            }
            else
            {
                CheckBoxList1.RepeatDirection = RepeatDirection.Vertical;
            }

            CheckBoxList1.Visible = bool_CheckBoxList_vis;
            CheckBoxList1.Enabled = bool_CheckBoxList_ena;
            CheckBoxList1.RepeatColumns = int_CheckBoxList_Col;

            if (int_CheckBoxList_Width != 0)
            {
                CheckBoxList1.Width = int_CheckBoxList_Width;
            }



        }
    }

    public string SelectedValue
    {
        get
        {
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected == true)
                {
                    str_value += ",'" + CheckBoxList1.Items[i].Value + "'";
                }
            }
            
            return str_value;
        }
    }

    public string Sel
    {
        get { return str_CheckBoxList_sel; }
        set { str_CheckBoxList_sel = value; }
    }

    public int Width
    {
        get { return int_CheckBoxList_Width; }
        set { int_CheckBoxList_Width = value; }
    }

    public int Col
    {
        get { return int_CheckBoxList_Col; }
        set { int_CheckBoxList_Col = value; }
    }

    public string Repeat
    {
        get { return str_CheckBoxList_RepeatDirection; }
        set { str_CheckBoxList_RepeatDirection = value; }
    }

    public bool Vis
    {
        get { return bool_CheckBoxList_vis; }
        set { bool_CheckBoxList_vis = value; }
    }

    public bool Ena
    {
        get { return bool_CheckBoxList_ena; }
        set { bool_CheckBoxList_ena = value; }
    }

    public string DataCode
    {
        get { return str_CheckBoxList_datacode; }
        set { str_CheckBoxList_datacode = value; }
    }


    public void CheckBoxList1_show()
    {
        CheckBoxList1.Items.Clear();

        string mySql = "";
        mySql = "select * from t_sys_Data where DataCode = '" + str_CheckBoxList_datacode.ToString().Trim() + "' and IsUse = '1' and IsVisible = '1' order by DisplayOrder asc";

        DataSet ds = DataFunction.FillDataSet(mySql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            ListItem li = new ListItem();
            li.Text = dr["DataMc"].ToString().Trim();
            li.Value = dr["DataDm"].ToString().Trim();
            //DropDownList1.Items.Add(li);
            CheckBoxList1.Items.Add(li);
        }

    }

}
