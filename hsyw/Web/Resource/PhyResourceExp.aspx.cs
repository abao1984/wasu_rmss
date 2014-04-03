using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Aspose.Cells;
using System.Drawing;

public partial class Web_Resource_PhyResourceExp : BasePage
{
    private ShareResource shareResource = new ShareResource();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            UNIT_ID.Text = Request.QueryString["UNIT_ID"];
            ExpName.Text = Request.QueryString["TXT_NAME"];
            FillPage();
        }
    }

    private DataSet GetProperyData()
    {
        string sql = "select t.filed_name,t.propery_name,t.sequence,t.isgridshow from T_RES_SYS_PROPERTY t where t.unit_id='" + UNIT_ID.Text + "' order by SEQUENCE";
        DataSet ds = DataFunction.FillDataSet(sql);
        return ds;
    }

    private void FillPage()
    {
        DataSet ds = GetProperyData();
        ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["filed_name"] };
        CheckBoxList1.DataSource = ds;
        CheckBoxList1.DataTextField = "propery_name";
        CheckBoxList1.DataValueField = "filed_name";
        CheckBoxList1.DataBind();

        foreach (ListItem item in CheckBoxList1.Items)
        {
            DataRow dr=ds.Tables[0].Rows.Find(item.Value);
            if (dr!=null&&dr["isgridshow"].ToString()=="1")
            {
                item.Selected = true;
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>parent.WindowClose();</script>");
    }
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    ExpExcel();
    //    //string ColumMc = "";
    //    //string ColumZd = "";
    //    //foreach (ListItem item in CheckBoxList1.Items)
    //    //{
    //    //    if (item.Selected)
    //    //    {
    //    //        ColumMc += item.Value + ",";
    //    //        ColumZd += item.Text + ",";
    //    //    }
    //    //}
    //}


    private void ExpExcel()
    {
        try
        {
            License lic = new License();
            lic.SetLicense(Server.MapPath("../../Aspose.Total.lic"));
            WorkbookDesigner designer1 = new WorkbookDesigner();
            Workbook book = designer1.Workbook;
            DataSet ds = shareResource.GetResourceUnitData(this.form1.Parent.Page, UNIT_ID.Text, shareResource.GetQueryStr(this.form1.Parent.Page, UNIT_ID.Text));
            ds.Tables[0].TableName = "T1";
            object filePath = Server.MapPath("WLZY.xls");
            if (System.IO.File.Exists(Convert.ToString(filePath)))
            {
                designer1.Open(Convert.ToString(filePath));
                Cells cells = book.Worksheets[0].Cells;
                int i = 1;
                foreach (ListItem item in CheckBoxList1.Items)
                {
                    if (item.Selected)
                    {
                        cells[0, i].PutValue(item.Text);
                        cells[0, i].Style.BackgroundColor = Color.PowderBlue;
                        cells[1, i].PutValue("&=[T1]." + item.Value);
                        i++;
                    }
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    designer1.SetDataSource(ds);
                }
                else
                {
                    return;
                }
                designer1.Process();
                designer1.Save(HttpUtility.UrlEncode("物理资源.xls"), Aspose.Cells.SaveType.OpenInExcel, FileFormatType.Default, Response);
                Response.End();
            }
            else
                return;
        }
        catch (Exception ex)
        {
            string aa = ex.Message;
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        string ColumMc = "";
        string ColumZd = "";
        foreach (ListItem item in CheckBoxList1.Items)
        {
            if (item.Selected)
            {
                ColumZd += item.Value + ",";
                ColumMc += item.Text + ",";
            }
        }
        if (ColumMc != "" && ColumZd != "")
        {
            Session["ColumMc"] = ColumMc;
            Session["ColumZd"] = ColumZd;
        }
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>parent.WindowClose();</script>");
    }
}
