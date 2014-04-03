using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_LogicResourceVlanEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SSJF.Attributes.Add("readonly", "true");
            SSQY.Attributes.Add("readonly", "true");
            GUID.Text = Request.QueryString["GUID"];
            string plcz = Request.QueryString["PLCZ"];
            ShareFunction.BindEnumDropList(YWDL, "V_YWDL");
            if(plcz.Equals("1"))//批量操作
            {
                tr_plcz.Style.Add("display", "block");
                tr_vlan.Style.Add("display", "none");
            }
            else
            {
                tr_plcz.Style.Add("display", "none");
                tr_vlan.Style.Add("display", "block");
                FillPage(); 
            }
            
        }
    }
    private void FillPage()
    {
        if (GUID.Text.Equals(""))
        {
            GUID.Text = Guid.NewGuid().ToString();
            VLANBH.Text = GetBH();
        }
        else
        { 
            string sql = string.Format("select * from t_logic_equ_vlan where GUID = '{0}'",GUID.Text);
            DataSet ds = DataFunction.FillDataSet(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ShareFunction.FillControlData(Page, ds.Tables[0].Rows[0]);
            }
            else
            {
                BtnSave.Enabled = false;
                BtnDel.Enabled = false;
                ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('该记录不存在，可能已被删除！');</script>");
            }
        }
        //判断就否为全网
        if (SFQW.Checked)
        {
            img.Visible = false;
        }
        else
        {
            YWDL.Enabled = false;
        }

    }
    //保存
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["PLCZ"] == "1")//批量操作
        {
            if (string.IsNullOrEmpty(KSBH.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('开始编号不能为空！');</script>");
                return;
            }
            if (string.IsNullOrEmpty(JSBH.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('结束编号不能为空！');</script>");
                return;
            }
            string sql = string.Format("select * from t_logic_equ_vlan where VLANBH >= {0} and VLANBH<={1}", KSBH.Text,JSBH.Text);
            DataSet ds = DataFunction.FillDataSet(sql);
            ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["GUID"] };
            for (Int32 bh=Convert.ToInt32(KSBH.Text); bh<=Convert.ToInt32(JSBH.Text);bh++)
            {
                DataRow dr = ds.Tables[0].Rows.Find(bh);
                GUID.Text = Guid.NewGuid().ToString();
                VLANBH.Text = bh.ToString();
                if (dr == null)
                {
                    dr = ds.Tables[0].NewRow();
                    dr["GUID"] = GUID.Text;
                    dr["VLANBH"] = VLANBH.Text;
                    ds.Tables[0].Rows.Add(dr);
                }
                else
                {
                    GUID.Text = dr["GUID"].ToString();
                }
                ShareFunction.GetControlData(Page, dr);
               
                //dr["VLANBH"] = bh;
            }
            DataFunction.SaveData(ds, "t_logic_equ_vlan");

        }
        else
        {
            string sql = string.Format("select * from t_logic_equ_vlan where GUID = '{0}'", GUID.Text);
            DataSet ds = DataFunction.FillDataSet(sql);
            if (ds.Tables[0].Rows.Count == 0)
            {               
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            }
            ShareFunction.GetControlData(Page, ds.Tables[0].Rows[0]);
            DataFunction.SaveData(ds, "t_logic_equ_vlan");
        }


     /*   if (Request.QueryString["PLCZ"]=="1")//批量操作
        {
            string updatesql = "update t_logic_equ_vlan set";
            if (SSJF.Text != "")
            {
                updatesql += " SSJF = '"+SSJF.Text+"',SSJF_GUID = '"+SSJF_GUID.Text+"',SSJF_CODE = '"+SSJF_CODE.Text+"',";
            }
            if (SSQY.Text != "")
            {
                updatesql += "SSQY = '"+SSQY.Text+"',SSQY_CODE = '"+SSQY_CODE.Text+"',";
            }
            if (SFKFY.SelectedValue != "")
            {
                updatesql += "SFKFY = '"+SFKFY.SelectedValue+"',";
            }
            if (ZYZT.Text != "")
            {
                updatesql += "ZYZT = '"+ZYZT.Text+"'";
            }
            if (KSBH.Text != "" && JSBH.Text != "")
            {
                string sql1 = string.Format("select * from t_logic_equ_vlan where VLANBH >= {0}", KSBH.Text);
                if (DataFunction.HasRecord(sql1))//批量修改
                {
                    if (!updatesql.Equals("update t_logic_equ_vlan set"))
                    {
                        int len = updatesql.Length;
                        updatesql = updatesql.Substring(0, len - 1) + " where VLANBH >= " + KSBH.Text+" and VLANBH <= "+JSBH.Text;
                        DataFunction.ExecuteNonQuery(updatesql);
                    }
                }
                else//批量新增
                {
                    DataSet ds = DataFunction.FillDataSet("select * from t_logic_equ_vlan where 1=2");
                    int ksbh = Convert.ToInt32(KSBH.Text);
                    int jsbh =  Convert.ToInt32(JSBH.Text);
                    for (int i = ksbh; i <= jsbh; i++)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        dr["GUID"] = Guid.NewGuid().ToString();
                        dr["VLANBH"] = i;
                        if (SSJF.Text != "")
                        {
                            dr["SSJF"] = SSJF.Text;
                            dr["SSJF_GUID"] = SSJF_GUID.Text;
                            dr["SSJF_CODE"] = SSJF_CODE.Text;
                        }
                        if (SSQY.Text != "")
                        {
                            dr["SSQY"] = SSQY.Text;
                            dr["SSQY_CODE"] = SSQY_CODE.Text;
                        }
                        if (SFKFY.SelectedValue != "")
                        {
                            dr["SFKFY"] = SFKFY.SelectedValue;
                        }
                        if (ZYZT.SelectedValue != "")
                        {
                            dr["ZYZT"] = ZYZT.SelectedValue;
                        }

                        ds.Tables[0].Rows.Add(dr);
                    }
                    DataFunction.SaveData(ds,"t_logic_equ_vlan");
                }
            }
            else if (KSBH.Text != "")
            {
                if (!updatesql.Equals("update t_logic_equ_vlan set"))
                {
                    int len = updatesql.Length;
                    updatesql = updatesql.Substring(0,len-1)+" where VLANBH >= "+KSBH.Text;
                    DataFunction.ExecuteNonQuery(updatesql);
                }
            }
            else
            {
                if (!updatesql.Equals("update t_logic_equ_vlan set"))
                {
                    int len = updatesql.Length;
                    updatesql = updatesql.Substring(0, len - 1) + " where VLANBH <= " + JSBH.Text;
                    DataFunction.ExecuteNonQuery(updatesql);
                }
            }
        }
        else
        {
            string sql = string.Format("select * from t_logic_equ_vlan where GUID = '{0}'", GUID.Text);
            DataSet ds = DataFunction.FillDataSet(sql);
            if (ds.Tables[0].Rows.Count == 0)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            }
            ShareFunction.GetControlData(Page, ds.Tables[0].Rows[0]);
            DataFunction.SaveData(ds, "t_logic_equ_vlan");
        }*/
    }
    //删除
    protected void BtnDel_Click(object sender, EventArgs e)
    {
        string sql = "";
        if (tr_plcz.Visible)//批量删除
        {
            sql = string.Format("delete from t_logic_equ_vlan where VLANBH >= {0} and VLANBH <= {1}", KSBH.Text,JSBH.Text);
        }
        else
        {
            sql = string.Format("delete from t_logic_equ_vlan where GUID = '{0}'", GUID.Text);
           
        }
        DataFunction.ExecuteNonQuery(sql);
        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>parent.WindowClose();</script>");
    }
    private string GetBH()
    {
        string whSql = "";
        if (SFQW.Checked)
        {
            whSql = "sfqw='1'";
        }
        else
        {
            whSql = "sfqw='0' or sfqw is null";
        }
        string sql = string.Format("select VLANBH from t_logic_equ_vlan where  "+whSql+" order by VLANBH desc");
        int vlanbh = DataFunction.GetIntResult(sql);
        if (vlanbh == -1)
        {
            vlanbh = 1;
        }
        else
        {
            vlanbh += 1;
        }
        return vlanbh.ToString();
    }

    protected void BtnYwdl_Click(object sender, EventArgs e)
    {
       ShareFunction.BindEnumDropList(YWDL, "V_YWDL");
    }
    protected void SFQW_CheckedChanged(object sender, EventArgs e)
    {
        //判断就否为全网

        img.Visible = true;
        YWDL.Enabled = true;
        if (SFQW.Checked)
        {
            img.Visible = false;
        }
        else
        {
            YWDL.Enabled = false;
        }
        if (DataFunction.GetStringResult("select count(*) from t_logic_equ_vlan where guid='" + GUID.Text + "'") == "0")
        {
            VLANBH.Text = GetBH();
        }
    }
}
