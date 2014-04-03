using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_ConfigOLTPortEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            PORT_GUID.Text = Request.QueryString["PORT_GUID"];
            BindGrid();
        }
    }

    private void BindGrid()
    {
        string strSql = "select t.* from t_res_equ_olt_port t where  t.port_guid='" + PORT_GUID.Text + "'";
        //string strSql = "select t.temp_xndk,case when t.temp_xndk is null then '空闲' else '占用' end as zt,  p.* from t_res_equ_olt_port p left join  t_con_logic_equ_ip t   on t.jrdk_guid=p.port_guid and t.temp_xndk=p.virtualport where p.port_guid='" + PORT_GUID.Text + "' order by zt,to_number(p.virtualport)";
        DataSet ds = DataFunction.FillDataSet(strSql);
        GridViewPhyResource.DataSource = ds;
        GridViewPhyResource.DataBind();
    }
    protected void OKButton_Click(object sender, EventArgs e)
    {
        //string guid = "";
        string port_name = "";
        foreach(GridViewRow gvr in GridViewPhyResource.Rows)
        {
            for (int i = 1; i <= 4; i++)
            {
                CheckBox cbx = (CheckBox)gvr.FindControl("CheckBox"+i.ToString());
                if (cbx.Checked)
                {
                    port_name = gvr.Cells[0].Text +"-"+ i.ToString() ;
                    break;
                }
            }
            if (port_name!="")
            {
                break;
            }
        }
        string TXT_NAME= Request.QueryString["TXT_NAME"];
        if(TXT_NAME!="")
        {
            ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>parent.document.getElementById('" + TXT_NAME + "').value='" + port_name + "'</script>");
        }
        
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>window.close();parent.WindowClose();</script>");
        
    }
    protected void GridViewPhyResource_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string onu1 = GridViewPhyResource.DataKeys[e.Row.RowIndex]["ONU1"].ToString();
            string onu2 = GridViewPhyResource.DataKeys[e.Row.RowIndex]["ONU2"].ToString();
            string onu3 = GridViewPhyResource.DataKeys[e.Row.RowIndex]["ONU3"].ToString();
            string onu4 = GridViewPhyResource.DataKeys[e.Row.RowIndex]["ONU4"].ToString();
            for (int i = 1; i <= 4;i++ )
            {
                string onu = GridViewPhyResource.DataKeys[e.Row.RowIndex][i].ToString();
                if (onu == "0")
                {
                    ((Image)e.Row.FindControl("Image" + i.ToString())).ImageUrl = "~/Web/Images/Small/r.gif";
                    //e.Row.FindControl("CheckBox"+i.ToString()).Visible=false;
                }
                else
                {
                    ((Image)e.Row.FindControl("Image" + i.ToString())).ImageUrl = "~/Web/Images/Small/g.gif";
                }
                
            }
        }
    }
}
