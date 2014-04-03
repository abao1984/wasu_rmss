using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_PhyResourceBatchEdit : System.Web.UI.Page
{
    private ShareResource shareResource = new ShareResource();
    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(ShareResource));
        if (!this.IsPostBack)
        {
            UNIT_ID.Text = Request.QueryString["UNIT_ID"];
            PARENT_UNIT_ID.Text = Request.QueryString["PARENT_UNIT_ID"];
            NAME_FILED_GUID.Text = Request.QueryString["NAME_FILED_GUID"];
            NAME_FILED_NAME.Text=Request.QueryString["NAME_FILED_NAME"];
            NAME_FILED.Text = shareResource.GetRelationNameFild(PARENT_UNIT_ID.Text, UNIT_ID.Text);
            DataRow drt = shareResource.GetResUnitData(UNIT_ID.Text);
            CODE_MODE.Text = drt["CODE_MODE"].ToString();
            CODE_FILED.Text = drt["CODE_FILED"].ToString();
            if (CODE_FILED.Text.IndexOf(":") > -1)
            {
                string[] str = CODE_FILED.Text.Split(':');
                P_CODE_FILED.Text = str[0];
                CODE_FILED.Text = str[1];
            }
            TABLE_NAME.Text = drt["TABLE_NAME"].ToString();
            BindDropDownCode(PHY_START_CODE);
            BindDropDownCode(PHY_END_CODE);
            PHY_START_NUM.Attributes.Add("onchange", "PhyChange(false,'NUM')");
            PHY_END_NUM.Attributes.Add("onchange", "PhyChange(false,'NUM')");
            PHY_TOTAL_NUM.Attributes.Add("onchange", "PhyChange(true,'NUM')");
            PHY_START_CODE.Attributes.Add("onchange", "PhyChange(false,'CODE')");
            PHY_END_CODE.Attributes.Add("onchange", "PhyChange(false,'CODE')");
            PHY_TOTAL_CODE.Attributes.Add("onchange", "PhyChange(true,'CODE')");
            if (CODE_MODE.Text == "数字编码")
            {
                PHY_TR_CODE.Style.Add("display", "none");
            }
            else
            {
                PHY_TR_NUM.Style.Add("display", "none");
            }
        }
        shareResource.CreateResourceTable(TD_PROPERTY, UNIT_ID.Text, CODE_FILED.Text);
        if (!this.IsPostBack)
        {
            DataSet ds = shareResource.GetSessionUnitData(UNIT_ID.Text);
            foreach (DataRow DR in ds.Tables[0].Rows)
            {
                string filedValue = ShareFunction.GetControlValue(Page.FindControl(DR["FILED_NAME"].ToString())).ToString();
                if (Session[DR["UNIT_ID"].ToString()] == null)
                {
                    Session[DR["UNIT_ID"] + "_GUID"] = ShareFunction.GetControlValue(Page.FindControl(DR["FILED_NAME"] + "_GUID"));
                    Session[DR["UNIT_ID"].ToString()] = filedValue;
                }
                else if (string.IsNullOrEmpty(filedValue))
                {
                    ShareFunction.SetControlValue(Page.FindControl(DR["FILED_NAME"] + "_GUID"), Session[DR["UNIT_ID"] + "_GUID"]);
                    ShareFunction.SetControlValue(Page.FindControl(DR["FILED_NAME"].ToString()), Session[DR["UNIT_ID"].ToString()]);
                }
            }

            if (!string.IsNullOrEmpty(NAME_FILED.Text))
            {
                string nameFiled = getNameFiled();
                if (!string.IsNullOrEmpty(nameFiled))
                {
                    SetControlData(nameFiled, NAME_FILED_NAME.Text);
                    SetControlData(nameFiled + "_GUID", NAME_FILED_GUID.Text);
                }
            }
            //string sql = "select up.unit_id,p.filed_name from t_res_sys_property p,T_RES_SYS_PROPERTY_UNIT up where p.propery_id=up.propery_id and p.unit_id='" + UNIT_ID.Text + "' ";
            //DataSet ds = DataFunction.FillDataSet(sql);
            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    if (dr["unit_id"].ToString() == shareResource.comp_house_unit_id || dr["unit_id"].ToString() == shareResource.house_unit_id)
            //    {
            //        ((TextBox)Page.FindControl(dr["filed_name"].ToString() + "_GUID")).Text = Convert.ToString(Session["HOUSE_ID"]);
            //        ((TextBox)Page.FindControl(dr["filed_name"].ToString())).Text = Convert.ToString(Session["HOUSE_NAME"]);
            //    }
            //    else if (dr["unit_id"].ToString() == shareResource.cupboard_unit_id)
            //    {
            //        ((TextBox)Page.FindControl(dr["filed_name"].ToString() + "_GUID")).Text = Convert.ToString(Session["CUPBOARD_C_ID"]);
            //        ((TextBox)Page.FindControl(dr["filed_name"].ToString())).Text = Convert.ToString(Session["CUPBOARD_C_NAME"]);
            //    }
            //}

            //Control cn = Page.FindControl("HOUSE_AREA");
            //if (cn != null && Session["HOUSE_AREA"] != null)
            //{
            //    ((TextBox)cn).Text = Convert.ToString(Session["HOUSE_AREA"]);
            //    ((TextBox)Page.FindControl("HOUSE_AREA_CODE")).Text = Convert.ToString(Session["HOUSE_AREA_CODE"]);
            //}
        }
    }

    private string getNameFiled()
    {
        string sql = string.Format(@"select p.filed_name from t_res_sys_property_unit up,T_RES_SYS_PROPERTY p 
        where up.propery_id=p.propery_id and up.unit_id='{0}' and p.unit_id='{1}'", PARENT_UNIT_ID.Text, UNIT_ID.Text);
        return DataFunction.GetStringResult(sql);
    }

    private void SetControlData(string filed_name, string filed_value)
    {
        ShareFunction.SetControlValue(Page.FindControl(filed_name), filed_value);
    }

    private void BindDropDownCode(DropDownList drp)
    {
        drp.Items.Clear();
        for(int i=65;i<=90;i++)
        {
            char a = (char)i;
            ListItem li = new ListItem(a.ToString(),i.ToString());
            drp.Items.Add(li);
        }
    }
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        if (!CheckPageData())
        {
            return;
        }
        string sql =string.Format( "select * from {0} where {1}='{2}'",
            TABLE_NAME.Text, NAME_FILED.Text+"_GUID",NAME_FILED_GUID.Text);
        DataSet ds = DataFunction.FillDataSet(sql);
        if (string.IsNullOrEmpty(P_CODE_FILED.Text))
        {
            ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns[CODE_FILED.Text] };
        }
        else
        {
            ds.Tables[0].PrimaryKey = new DataColumn[] {  ds.Tables[0].Columns[P_CODE_FILED.Text] ,ds.Tables[0].Columns[CODE_FILED.Text] };
        }
        int s = 0;
        int n = 0;
        if (CODE_MODE.Text == "数字编码")
        {
          s=  Convert.ToInt32(PHY_START_NUM.Text);
          n=  Convert.ToInt32(PHY_END_NUM.Text);
        }
        else
        {
            s = Convert.ToInt32(PHY_START_CODE.SelectedValue);
            n = Convert.ToInt32(PHY_END_CODE.SelectedValue);
        }
        for(int i=s; i<=n;i++)
        {
            string code = i.ToString();
            if (CODE_MODE.Text == "字母编码")
            {
                code = Convert.ToString((char)i);
            }
            DataRow dr = null;
            string p_code = "";
            if (string.IsNullOrEmpty(P_CODE_FILED.Text))
            {
                dr = ds.Tables[0].Rows.Find(code);
            }
            else
            {
                p_code = ShareFunction.GetControlValue(Page.FindControl(P_CODE_FILED.Text)).ToString();
                dr = ds.Tables[0].Rows.Find(new string[] { p_code,code});
            }
            
            if (dr == null)
            {
               dr= ds.Tables[0].NewRow();
               dr["GUID"] = Guid.NewGuid().ToString();
               dr["CREATEDATETIME"] = DateTime.Now;
               if (!string.IsNullOrEmpty(P_CODE_FILED.Text))
               {
                   
                   dr[P_CODE_FILED.Text] = p_code;
                   if (dr[P_CODE_FILED.Text] == DBNull.Value)
                   {
                       p_code = "P";
                   }
               }
               
               dr[CODE_FILED.Text] = code;
               ds.Tables[0].Rows.Add(dr);
            }
            ShareFunction.GetControlData(this.Page, dr);
            dr["UPDATEDATETIME"] = DateTime.Now;            
            dr[NAME_FILED.Text+"_GUID"] = NAME_FILED_GUID.Text;
            dr[NAME_FILED.Text ] = NAME_FILED_NAME.Text;
            
        }
        DataFunction.SaveData(ds, TABLE_NAME.Text);

        SaveDataByFormula();
        SaveDataByPecuLier();
    }
   
    private bool CheckPageData()
    {
        string Message = "";
        string sql = "select * from t_res_sys_property t where t.unit_id='" + UNIT_ID.Text + "' and t.isempty=0  and t.FILED_NAME<>'" + CODE_FILED.Text + "'  order by t.sequence";
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (string.IsNullOrEmpty(ShareFunction.GetControlValue(Page.FindControl(dr["FILED_NAME"].ToString())).ToString()))
            {
                if (!string.IsNullOrEmpty(Message))
                {
                    Message += "、";
                }
                Message += dr["PROPERY_NAME"].ToString();
            }
        }
        if (string.IsNullOrEmpty(Message))
        {
            return true;
        }
        else
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('" + Message + "不能为空！');</script>");
            return false;
        }
    }

    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        string s = PHY_START_NUM.Text;
        string n = PHY_END_NUM.Text;
        string f = CODE_FILED.Text;
        if (CODE_MODE.Text == "字母编码")
        {
            s = PHY_START_CODE.SelectedValue;
            n = PHY_END_CODE.SelectedValue;
            f = "ascii(" + f + ")";
        }
        string p_code = "";
        
        if (!string.IsNullOrEmpty(P_CODE_FILED.Text))
        {
            p_code =" and "+ P_CODE_FILED.Text+"='"+((TextBox)Page.FindControl(P_CODE_FILED.Text)).Text+"'";
        }
        string sql = string.Format("delete from {0} where {1}='{2}' and {3}>={4} and {3}<={5} {6}",
                   TABLE_NAME.Text, NAME_FILED.Text + "_GUID", NAME_FILED_GUID.Text, f, s, n, p_code);
        DataFunction.ExecuteNonQuery(sql);
    }

    #region 计算公式
    private void SaveDataByFormula()
    {
        string sql = "select * from t_res_sys_property t where t.formula is not null and t.unit_id='" + UNIT_ID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow DR in ds.Tables[0].Rows)
        {
            sql = " update " + TABLE_NAME.Text + " set " + DR["FILED_NAME"].ToString() + "=" + DR["FORMULA"].ToString() + string.Format(" where {0}='{1}'", NAME_FILED.Text + "_GUID", NAME_FILED_GUID.Text);
            DataFunction.ExecuteNonQuery(sql);
        }
    }
    #endregion 

    #region 计算特殊公式
    private void SaveDataByPecuLier()
    {
        try
        {

            string sql = "select * from t_res_sys_property t where t.tsgs is not null and t.unit_id='" + UNIT_ID.Text + "'";
            DataSet gsds = DataFunction.FillDataSet(sql);
            gsds.Tables[0].Rows.Cast<DataRow>().ForEach(dr =>
            {
                string tsgs = Convert.ToString(dr["TSGS"]);
                string filed_Name = Convert.ToString(dr["FILED_NAME"]);
               string formula = Convert.ToString(dr["FORMULA"]); 
                DataSet ds = DataFunction.FillDataSet("select GUID from  " + TABLE_NAME.Text + " where " + NAME_FILED.Text + "_GUID='" + NAME_FILED_GUID.Text + "'");
                ds.Tables[0].Rows.Cast<DataRow>().ForEach(r =>
                {
                    string dkh = DataFunction.GetStringResult(string.Format(tsgs, Convert.ToString(r["GUID"])));
                    sql = " update " + TABLE_NAME.Text + " set " + filed_Name + "='" + dkh + "'" + string.Format(" where {0}='{1}'", "GUID", Convert.ToString(r["GUID"]));
                    DataFunction.ExecuteNonQuery(sql);
                });
            });
        }
        catch (Exception)
        {
            this.Alert("计算特殊工式时发生异常");
        }
    }
    #endregion
}
