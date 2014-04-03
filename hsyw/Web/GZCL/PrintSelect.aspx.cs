using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aspose.Cells;
using System.Data;

public partial class Web_GZCL_PrintSelect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            ZBGUID.Text = Request.QueryString["ZBGUID"];
        }
    }
    protected void ExpButton_Click(object sender, EventArgs e)
    {
        int j = 0;
        string strColum = "";
        string strName = "";
        foreach (ListItem li in Ch_Select.Items)
        {
            if (li.Selected)
            {               
                if (!string.IsNullOrEmpty(strColum))
                {
                    strColum += ",";
                    strName += ",";
                }
                strColum += li.Value;
                strName += li.Text;
                j++;
            }
        }
        string str = strColum + ":" + strName;
         this.ClientScript.RegisterStartupScript(this.GetType(),
                     Guid.NewGuid().ToString(), "<script>window.close();window.returnValue='" + str + "';</script>");


        //try
        //{
            //WorkbookDesigner designer1 = new WorkbookDesigner();
           
            //object filePath = Server.MapPath("YXYH.xls");
            //if (System.IO.File.Exists(Convert.ToString(filePath)))
            //{
            //    //designer1.Open(Convert.ToString(filePath));

            //    int j = 0;
            //    string strColum = "";
            //    foreach (ListItem li in Ch_Select.Items)
            //    {
            //        if (li.Selected)
            //        {
            //            designer1.Workbook.Worksheets[0].Cells[0, j].PutValue(li.Text);
            //            designer1.Workbook.Worksheets[0].Cells[0, j].Style.Font.IsBold = true;
            //            if (!string.IsNullOrEmpty(strColum))
            //            {
            //                strColum += ",";
            //            }
            //            strColum += li.Value;
            //            j++;
            //        }
            //    }
            //    if (string.IsNullOrEmpty(strColum))
            //    {
            //        this.ClientScript.RegisterStartupScript(this.GetType(),
            //            Guid.NewGuid().ToString(), "<script>alert('请选择需要导出列！');</script>");
            //        designer1.Process();
            //        return;
            //    }

            //    string sql=string.Format("select {0} from t_fau_yxyh a left join rmss b on a.ywid=b.SUBSCRIBER_ID where a.ZBGUID='{1}'",strColum,ZBGUID.Text);
            //    DataSet ds = DataFunction.FillDataSet(sql);

            //    int i = 1;
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        int k = 0;
            //        foreach(DataColumn dc in ds.Tables[0].Columns)
            //        {
            //            designer1.Workbook.Worksheets[0].Cells[i, k].PutValue(dr[dc.ColumnName]);
            //            designer1.Workbook.Worksheets[0].AutoFitRow(i);
                        
            //            k++;
            //        }
            //        i++;
            //    }

            //    designer1.Process();
            //    designer1.Save(Guid.NewGuid().ToString()+".xls", Aspose.Cells.SaveType.OpenInExcel, FileFormatType.Default, Response);
            //   // Response.End();
            //}
            //else
            //    return;
        //}
        //catch (Exception ex)
        //{
        //    string aa = ex.Message;
        //}
    }
    protected void AllButton_Click(object sender, EventArgs e)
    {
        foreach (ListItem li in Ch_Select.Items)
        {
            li.Selected = true;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        foreach (ListItem li in Ch_Select.Items)
        {
            li.Selected = false;
        }
    }
}
