using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_ConfigCupboardEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(Web_Resource_ConfigCupboardEdit));
        if(!Page.IsPostBack)
        {
            
            GUID.Text = Request.QueryString["GUID"];
            SB_UNIT_ID.Text = "64602091-d4fe-4c89-ac6a-52f6acdd836d,9e2393f1-931d-4b14-b44f-0ba9ff846853";
            FillPage();
           
        }
    }

    private void FillPage()
    {
        string strSql = "select * from t_con_cupboard t where guid='" + GUID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(strSql);
        DataRow dr;
        if (ds.Tables[0].Rows.Count == 0)
        {
            dr = ds.Tables[0].NewRow();
            dr["guid"] = Guid.NewGuid().ToString();
            ds.Tables[0].Rows.Add(dr);
        }
        else
        {
            dr = ds.Tables[0].Rows[0];
        }
        ShareFunction.FillControlData(Page, dr);
        Change();
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        string strSql= "select * from t_con_cupboard t where guid='" + GUID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(strSql);
        DataRow dr;

        if (ds.Tables[0].Rows.Count == 0)
        {

            dr = ds.Tables[0].NewRow();
            ds.Tables[0].Rows.Add(dr);
        }
        else
        {
            dr = ds.Tables[0].Rows[0];
        }
        ShareFunction.GetControlData(Page, dr);
        //if (Request.QueryString["GUID"] == "" && GUID.Text=="")
        //{
        //    dr["guid"] = Guid.NewGuid().ToString();
        //}
        //else if (GUID.Text == "")
        //{
        //    GUID.Text = Request.QueryString["GUID"];
        //    dr["guid"] = GUID.Text;
        //}
        
        DataFunction.SaveData(ds, "t_con_cupboard");
        
        strSql = "select * from " + tbname.Text + "  where guid='" + SBMC_GUID.Text + "'";
        DataSet ds1 = DataFunction.FillDataSet(strSql);
        DataRow dr1 ;
        if (ds1.Tables[0].Rows.Count == 0)
        {
            return;
        }
        else
        {
            dr1 = ds1.Tables[0].Rows[0];

        }
        ShareFunction.GetControlData(Page, dr1);
        dr1["guid"] = SBMC_GUID.Text;
        DataFunction.SaveData(dr1.Table.DataSet, tbname.Text);
        FillPage();
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        string strSql = string.Format(@"select guid as SBMC_GUID,HOUSE_NAME_GUID,HOUSE_NAME,CUPBOARD_NAME_GUID,CUPBOARD_NAME,CUPBOARD_U_GUID,CUPBOARD_U,POWER_CODE_GUID,POWER_CODE,ZRBM,ZRR,ZCBH,QYRQ,'T_RES_EQU_NET' as tbname from T_RES_EQU_NET t where guid='{0}'
union
select guid as SBMC_GUID,HOUSE_NAME_GUID,HOUSE_NAME,CUPBOARD_NAME_GUID,CUPBOARD_NAME,CUPBOARD_U_GUID,CUPBOARD_U,POWER_CODE_GUID,POWER_CODE,ZRBM,ZRR,ZCBH,QYRQ,'T_RES_EQU_TRANSFERS' as tbname from T_RES_EQU_TRANSFERS t where guid='{0}'", SBMC_GUID.Text);
        DataRow dr = DataFunction.GetSingleRow(strSql);
        ShareFunction.FillControlData(Page, dr);

        Change();
    }

    private void Change()
    {
        if (HOUSE_NAME.Text != "")
        {
            imghouse.Visible = false;
        }
        else
        {
            imghouse.Visible = true;
        }

        if (HOUSE_NAME.Text != "")
        {
            imghouse.Visible = false;
        }
        else
        {
            imghouse.Visible = true;
        }

        if (CUPBOARD_NAME.Text != "")
        {
            imgcupboard.Visible = false;
        }
        else
        {
            imgcupboard.Visible = true;
        }

        if (CUPBOARD_U.Text != "")
        {
            imgcupboardu.Visible = false;
        }
        else
        {
            imgcupboardu.Visible = true;
        }

        if (POWER_CODE.Text != "")
        {
            imgpower.Visible = false;
        }
        else
        {
            imgpower.Visible = true;
        }
    }

   
}
