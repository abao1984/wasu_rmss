using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_ResourceCut_ResourceCutOver : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            SBLX.Text = "64602091-d4fe-4c89-ac6a-52f6acdd836d,9e2393f1-931d-4b14-b44f-0ba9ff846853,41d1081d-7925-485b-996c-72f4519c7898";
            BusinessGridView.Attributes.Add("BorderColor", "#5B9ED1");
        }
    }

    protected void PrevCutButton_Click(object sender, EventArgs e)
    {
        string jdFiled = "";
        string ydFiled = "";
        string strValue = "";
        GetJydFiled(ref jdFiled, ref ydFiled, ref strValue);
        BindGrid(ydFiled, strValue);        
    }
    protected void NextCutButton_Click(object sender, EventArgs e)
    {
        string jdFiled = "";
        string ydFiled = "";
        string strValue = "";
        GetJydFiled(ref jdFiled, ref ydFiled, ref strValue);
        BindGrid(jdFiled, strValue);       
    }

    private void BindGrid(string strFiled,string strValue)
    {
        string sql = string.Format("select * from v_business t where t.{0}='{1}'",
          strFiled, strValue);
        DataSet ds = DataFunction.FillDataSet(sql);
        BusinessGridView.DataSource = ds;
        BusinessGridView.DataBind();
    }

    private void GetJydFiled(ref string jdFiled, ref string ydFiled, ref string strValue)
    {
        if (!string.IsNullOrEmpty(DK_GUID.Text))
        {
            jdFiled = "jdsbdk_guid";
            ydFiled = "ydsbdk_guid";
            strValue = DK_GUID.Text;
            GJLJ.Text = JF.Text+"【"+SB.Text+"】〖"+DK.Text+"〗";
        }
        else if (!string.IsNullOrEmpty(SB_GUID.Text))
        {
            jdFiled = "jdsb_guid";
            ydFiled = "ydsb_guid";
            strValue = SB_GUID.Text;
            GJLJ.Text = JF.Text + "【" + SB.Text + "】";
        }
        else if (!string.IsNullOrEmpty(JF_GUID.Text))
        {
            jdFiled = "jdjrjf_guid";
            ydFiled = "ydjrjf_guid";
            strValue = JF_GUID.Text;
            GJLJ.Text = JF.Text;
        }
    }
    protected void BusinessGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
        }
    }
    protected void BusinessGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Int32 n = Convert.ToInt32(e.CommandArgument);
        System.Web.UI.WebControls.DataKey dk = BusinessGridView.DataKeys[n];
        switch (e.CommandName)
        {
            case "YDJRJF":                
                BindGrid("jdjrjf_guid", dk.Values["YDJRJF_GUID"].ToString());
                GJLJ.Text = GJLJ.Text + "→" + dk.Values["YDJRJF"].ToString();
                break;
            case "JDJRJF":
                BindGrid("ydjrjf_guid", dk.Values["JDJRJF_GUID"].ToString());
                GJLJ.Text = GJLJ.Text + "←" + dk.Values["JDJRJF"].ToString();
                break;
            case "YDSB":
                BindGrid("jdsb_guid", dk.Values["YDSB_GUID"].ToString());
                GJLJ.Text = GJLJ.Text + "→" + dk.Values["YDJRJF"] + "【" + dk.Values["YDSB"] + "】"; 
                break;
            case "JDSB":
                BindGrid("ydsb_guid", dk.Values["JDSB_GUID"].ToString());
                GJLJ.Text = GJLJ.Text + "←" + dk.Values["JDJRJF"].ToString() + "【" + dk.Values["JDSB"] + "】"; ;
                break;
            case "YDSBDK":
                BindGrid("jdsbdk_guid", dk.Values["YDSBDK_GUID"].ToString());
                GJLJ.Text = GJLJ.Text + "→" + dk.Values["YDJRJF"].ToString() + "【" + dk.Values["YDSB"] + "】〖" + dk.Values["YDSBDK"] + "〗";
                break;
            case "JDSBDK":
                BindGrid("ydsbdk_guid", dk.Values["JDSBDK_GUID"].ToString());
                GJLJ.Text = GJLJ.Text + "←" + dk.Values["JDJRJF"].ToString() + "【" + dk.Values["JDSB"] + "】〖" + dk.Values["JDSBDK"] + "〗";
                break;
        }
    }
}
