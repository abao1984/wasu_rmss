using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Aspose.Cells;
using System.IO;

public partial class Web_Resource_ConfigBackBoneEdit : System.Web.UI.Page
{
    private ShareResource shareResource = new ShareResource();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            YWGUID.Text = Request.QueryString["YWGUID"];          
            SetContrlReadonly();
            BindDDL();
            FillPage();

        }
    }

    private void SetContrlReadonly()
    {
        YWBM.Attributes.Add("readonly", "true");
        WGSJ.Attributes.Add("readonly", "true");
        QDSJ.Attributes.Add("readonly", "true");

        JDJRJF_CODE.Attributes.Add("readonly", "true");
        JDJRJF.Attributes.Add("readonly", "true");
        YDJRJF_CODE.Attributes.Add("readonly", "true");
        YDJRJF.Attributes.Add("readonly", "true");
        JDSB_CODE.Attributes.Add("readonly", "true");
        JDSB.Attributes.Add("readonly", "true");
        YDSB_CODE.Attributes.Add("readonly", "true");
        YDSB.Attributes.Add("readonly", "true");
        JDSBDK.Attributes.Add("readonly", "true");
        YDSBDK.Attributes.Add("readonly", "true");
    }

    private void BindDDL()
    {
        DataSet ds = DataFunction.FillDataSet("select * from T_RES_SYS_ENUMDATA where enum_sort = 'YWLX' order by sequence");
        YWLX.DataSource = ds;
        YWLX.DataTextField = "ENUM_NAME";
        YWLX.DataValueField = "ENUM_NAME";
        YWLX.DataBind();
        YWLX.Items.Insert(0, new ListItem("",""));
        YWLX.SelectedIndex = 0;

        ds = DataFunction.FillDataSet("select * from T_RES_SYS_ENUMDATA where enum_sort = 'WXZL' order by sequence");
        JDWXZL.DataSource = ds;
        JDWXZL.DataTextField = "ENUM_NAME";
        JDWXZL.DataValueField = "ENUM_NAME";
        JDWXZL.DataBind();
        JDWXZL.Items.Insert(0, new ListItem("", ""));
        JDWXZL.SelectedIndex = 0;

        ds = DataFunction.FillDataSet("select * from T_RES_SYS_ENUMDATA where enum_sort = 'WXZL' order by sequence");
        YDWXZL.DataSource = ds;
        YDWXZL.DataTextField = "ENUM_NAME";
        YDWXZL.DataValueField = "ENUM_NAME";
        YDWXZL.DataBind();
        YDWXZL.Items.Insert(0, new ListItem("", ""));
        YDWXZL.SelectedIndex = 0;
    }
    private void FillPage()
    {
        if (string.IsNullOrEmpty(YWGUID.Text))
        {
            YWGUID.Text = Guid.NewGuid().ToString();
            SQSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
            SQR.Text = Session["UserName"].ToString();
            SQRNAME.Text = Session["UserRealName"].ToString();
            ZYHS_BJ.Text = "1";
            CREATEDATETIME.Text = DateTime.Now.ToString();
        }
        else
        {
            DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_CON_BONE_BUSINESS where YWGUID = '{0}'", YWGUID.Text));
            if (ds.Tables[0].Rows.Count > 0)
            {
                ShareFunction.FillControlData(Page, ds.Tables[0].Rows[0]);
                if (SQSJ.Text != "") { SQSJ.Text = SQSJ.Text.Substring(0, 10); }
                if (WGSJ.Text != "") { WGSJ.Text = WGSJ.Text.Substring(0, 10); }
                if (QDSJ.Text != "") { QDSJ.Text = QDSJ.Text.Substring(0, 10); }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('您打开的页面没有数据！');</script>");
                BtnSave.Enabled = false;
            }
        }
        if (ZYHS_BJ.Text == "1")
        {
            BtnZyhs.Text = "资源回收";
        }
        else
        {
            BtnZyhs.Text = "资源恢复";
        }
        
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        SaveData(false);
        FillPage();
    }
    protected void Btn_Click(object sender, EventArgs e)
    {
        DropDownList ddl = Page.FindControl(DDLID.Text) as DropDownList;
        string sv = ddl.SelectedValue;
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_RES_SYS_ENUMDATA where enum_sort = '{0}' order by sequence", DDLLX.Text));
        if (ds.Tables[0].Select("ENUM_NAME = '" + sv + "'").Length == 0)
        {
            sv = "";
        }
        ddl.DataSource = ds;
        ddl.DataTextField = "ENUM_NAME";
        ddl.DataValueField = "ENUM_NAME";
        ddl.DataBind();
        ddl.Items.Insert(0,new ListItem("", ""));
        ddl.SelectedValue = sv;
    }
    protected void BtnZyhs_Click(object sender, EventArgs e)
    {
        if (ZYHS_BJ.Text == "1")
        {
            ZYHS_BJ.Text = "0";
        }
        else
        {
            ZYHS_BJ.Text = "1";
        }
        SaveData(true);
    }

    protected void SaveData(bool isZyhs)
    {
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_CON_BONE_BUSINESS where YWGUID = '{0}'", YWGUID.Text));
       
        string strTitle = "修改骨干资源配置";
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            strTitle = "新增骨干资源配置";
        }
        if (isZyhs)
        {
            if (ZYHS_BJ.Text == "0")
            {
                strTitle = "回收骨干资源配置";
            }
            else
            {
                strTitle = "恢复骨干资源配置";
            }
        }
        DataRow DR = ds.Tables[0].Rows[0];
        //string zyZt = "启用";
        //if (ZYHS_BJ.Text == "0")
        //{
        //    zyZt = "未启用";
        //}
        //else
        //{
        //    SetResChildPortZt(DR["JDSBDK_GUID"].ToString(), "未启用");
        //    SetResChildPortZt(DR["YDSBDK_GUID"].ToString(), "未启用");
        //}
        //SetResChildPortZt(JDSBDK_GUID.Text, zyZt);
        //SetResChildPortZt(YDSBDK_GUID.Text, zyZt);

        string strPortGuid = DR["JDSBDK_GUID"].ToString() + "," + DR["YDSBDK_GUID"].ToString() + "," + JDSBDK_GUID.Text + "," + YDSBDK_GUID.Text;
        GetYWBM();
        UPDATEDATETIME.Text = DateTime.Now.ToString();
        UPDATEUSERNAME.Text = "【" + Session["BranchName"].ToString().Trim() + "】" + Session["UserRealName"].ToString().Trim();
        string strComment = ShareFunction.GetControlData(Page, DR, "T_CON_BONE_BUSINESS");
        DataFunction.SaveData(ds, "T_CON_BONE_BUSINESS");
        shareResource.SetResourcePort(strPortGuid);
     
        ShareFunction.InsertLog(this.Page, YWGUID.Text, strTitle, strComment);
        FillPage();
    }

    private void GetYWBM()
    {
        if (string.IsNullOrEmpty(YWBM.Text))
        {
            string bm = "G_" + DateTime.Now.ToString("yyyyMM");
            string sql = "select nvl(max(to_number(substr(ywbm,9,4))),0)+1 as sxh  from t_con_bone_business t where t.ywbm like '" + bm + "%'";
            int sxh = DataFunction.GetIntResult(sql);
            YWBM.Text = bm + sxh.ToString("0000");
        }
    }



    //private void SetResChildPortZt(string dkGuid, string dkzt)
    //{
    //    string sql = string.Format("update t_res_child_port t set t.dkzt='{1}' where t.guid in ('{0}') ",
    //        dkGuid.Replace(",", "','"), dkzt);
    //    DataFunction.ExecuteNonQuery(sql);
    //}
    protected void BtnExp_Click(object sender, EventArgs e)
    {
        License lic = new License();
        lic.SetLicense(Server.MapPath("../../Aspose.Total.lic"));
        Workbook book = new Workbook();
        book.Open(Server.MapPath("../../template/骨干资源配置模板.xls"));
        Worksheet ws = book.Worksheets["Sheet1"];
        Names names = book.Worksheets.Names;
        DataTable dt = DataFunction.FillDataSet(@"select ywbm B1,ywlx D1,llmc F1,wzxh H1,
                                                   sj B2,glcd D2,sqsj F2,sqrname H2,
                                                   wgsj B3,qdsj D3,sjkh F3,
                                                   case llfx when 1 then
                                                      '甲端→乙端'
                                                     when -1 then
                                                      '甲端←乙端'
                                                     when 0 then
                                                      '甲端～乙端'
                                                   end B4, glzybz D4,
                                                   jdjrjf_code B6,
                                                   jdjrjf D6,
                                                   sblx.unit_name B7,
                                                   JDWXZL D7,
                                                   zb.jdsb_code B8,
                                                   jdsb D8,
                                                   jdsbdk B9,
                                                   jddkbz B10,
                                                   ydjrjf_code F6 ,
                                                   ydjrjf H6,
                                                   ydsblx.unit_name F7,
                                                    ydwxzl H7,
                                                   ydsb_code F8,
                                                   ydsb H8,
                                                   ydsbdk F9,
                                                   yddkbz F10
                                              from t_con_bone_business ZB
                                              left join T_RES_SYS_UNIT sblx on zb.jdsblx=sblx.unit_id
                                              left join T_RES_SYS_UNIT ydsblx on zb.jdsblx=ydsblx.unit_id
                                             where zb.ywguid='" + YWGUID.Text + "'").Tables[0];
        dt.Rows.Cast<DataRow>().ForEach(dr => {
            dt.Columns.Cast<DataColumn>().ForEach(col => {
                ws.Cells[col.ColumnName].PutValue(Convert.ToString(dr[col]));
                //Name name = names[col.ColumnName];
                //if (name != null)
                //{

                //    name.RefersTo=Convert.ToString(dr[col]);
             
                //}
                
            });
        });

        MemoryStream ms = new MemoryStream();
        book.Save(ms, FileFormatType.Excel2003);
        IDP.Common.WebUtils.ResponseWriteBinary(ms.ToArray(), "骨干资源配置.xls");

    }
  
}
