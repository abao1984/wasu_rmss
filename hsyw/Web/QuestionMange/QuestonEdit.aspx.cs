using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_QuestionMange_QuestonEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 0;
        Response.CacheControl = "no-cache";

        Ajax.Utility.RegisterTypeForAjax(typeof(Web_QuestionMange_QuestonEdit));
        if (!Page.IsPostBack)
        { 
            ID.Text = Request.QueryString["id"];
            BindDDL();
            FillPage();
            InitPrivate();
        }
    }
    private void BindDDL()
    {
        ShareFunction.BindEnumDropList(WTLY, "WTLY");
        ShareFunction.BindEnumDropList(ZXTLB, "ZXTLB");
        ShareFunction.BindEnumDropList(WTYXJ, "WTYXJ");
        ShareFunction.BindEnumDropList(WTZT, "WTZT");
    }
    private void FillPage()
    {
        string sql = string.Format("select * from T_QUES_INFO where ID = '{0}'",ID.Text);
        DataSet ds = DataFunction.FillDataSet(sql);
        if (ds.Tables[0].Rows.Count > 0)
        {
           ShareFunction.FillControlData(Page,ds.Tables[0].Rows[0]);
        }

        
    }
    private void InitPrivate()
    {
        TxtPo.Text = Convert.ToString(Request.QueryString["op"]);
        switch (Request.QueryString["op"])
        {
            case "XJ": //新建（新建问题）
                //我不知道下面这样写有什么特殊含义，所以也不对他进行修改 罗耀斌
                if (!Page.IsPostBack)
                {
                    BtnJJJH.Visible = true;
                    BtnCL.Visible = false;
                    BtnZJ.Visible = false;
                    BtnWC.Visible = false;
                }
                td_wtps.Style.Add("display", "none");
                //这样表示已经有数据的
                if (!string.IsNullOrEmpty(ID.Text))
                {
                    BtnDel.Visible = true;
                    BtnZJ.Visible = true;
                }
                ZTXZ.Text = "新建";
                if (WTZT.SelectedValue == "已完成")
                {
                    SaveButton.Visible = false;
                    BtnJJJH.Visible = false;
                    BtnDel.Visible = false;
                    BtnCL.Visible = false;
                    BtnZJ.Visible = false;
                    BtnWC.Visible = false;
                    td_tackle.Style.Add("display", "block");
                }
                //退回按钮
                BtnBack.Visible = false;
                break;
            case "CL": // 处理(我的问题)
                BtnDel.Visible = false;
                BtnZJ.Visible = false;
                BtnCL.Visible = false;
                td_new.Style.Add("display", "block");
                td_tackle.Style.Add("display", "block");
                td_wtps.Style.Add("display", "none");
                SetCtrlReadOnly(td_new);
                if (string.IsNullOrEmpty(FZR.Text))
                {
                    FZR.Text = Convert.ToString(Session["USERREALNAME"]);
                }
                ZTXZ.Text = "问题处理";
                break;
            case "PS": //评审（问题评审）
                BtnDel.Visible = false;
                BtnZJ.Visible = false;
                BtnWC.Visible = false;
                BtnCL.Visible = false;
                BtnBack.Visible = false;
                td_tackle.Style.Add("display", "block");
                SetCtrlReadOnly(td_new);
                SetCtrlReadOnly(td_tackle);
                PSR.Text = Session["USERREALNAME"].ToString();
                PSSJ.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                PF.ReadOnly = false;
                PY.ReadOnly = false;
                ZTXZ.Text = "问题评审";
                break;
            case "ZP": //问题指派
                ZTXZ.Text = "问题指派";
                BtnWC.Visible = false;
                td_new.Style.Add("display", "block");
                td_tackle.Style.Add("display", "none");
                SetCtrlReadOnly(td_new);
                td_wtps.Style.Add("display", "none");
                ZPR.Text = Session["UserName"].ToString();
                break;
                //这个是我加的 用于查询时查看明细用的 罗耀斌 2011-6-14 11:00
            case "Query":
                SaveButton.Visible = false;
                BtnJJJH.Visible = false;
                BtnDel.Visible = false;
                BtnCL.Visible = false;
                BtnZJ.Visible = false;
                BtnWC.Visible = false;
                BtnBack.Visible = false;
                td_tackle.Style.Add("display", "block");
                break;
            default:
                BtnDel.Visible = false;
                BtnWC.Visible = false;
                BtnBack.Visible = false;
                //新建后，如果该问题已结束，那就把所有的按钮都禁用掉，不能编辑了的
                
                break;
        }
    }

    //保存方法
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        string sql = string.Format("select * from T_QUES_INFO where ID = '{0}'", ID.Text);
        DataSet ds = DataFunction.FillDataSet(sql);
        if (ds.Tables[0].Rows.Count <= 0)
        { 
            ID.Text = Guid.NewGuid().ToString();
            WTZT.SelectedValue = "未完成";
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
        }
        //问题环节已经到了问题评审，当点保存的时候就把状态改成已完成  罗耀斌
        if (ZTXZ.Text == "问题评审")
        {
            ZTXZ.Text = "已完成";
        }
        ShareFunction.GetControlData(Page, ds.Tables[0].Rows[0]);
        DataFunction.SaveData(ds, "T_QUES_INFO");
        if (Request.QueryString["op"].Equals("XJ"))
        {
            sql = string.Format("select * from t_ques_clr where QUESID = '{0}' and LX = 'CJR'",ID.Text);
            DataSet ds1 = DataFunction.FillDataSet(sql);
            if (ds1.Tables[0].Rows.Count <= 0)
            {
                DataRow dr = ds1.Tables[0].NewRow();
                dr["ID"] = Guid.NewGuid().ToString();
                dr["QUESID"] =ID.Text;
                dr["SJ"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dr["UREALNAME"] = Session["UserRealName"].ToString();
                dr["UNAME"] = Session["UserName"].ToString();
                dr["LX"] = "CJR";
                dr["ISDQ"] = "1";
                dr["BZ"] = "新建";
                ds1.Tables[0].Rows.Add(dr);
                DataFunction.SaveData(ds1, "t_ques_clr");
            }
            BtnDel.Visible = true;
            BtnZJ.Visible = true;
        }
        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('保存 成功...');window.close();</script>");
    }

    //删除方法
    protected void BtnDel_Click(object sender, EventArgs e)
    {
        if (ID.Text != "")
        {
            string[] sql = new string[3];
            sql[0] = string.Format("delete from T_QUES_INFO where ID = '{0}'",ID.Text);
            sql[1] = string.Format("delete from T_QUES_CLR where QUESID = '{0}'", ID.Text);
            sql[2] = string.Format("delete from T_QUES_JJJH where QUESID = '{0}'", ID.Text);
            DataFunction.ExecuteTransaction(sql);
        }
        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('删除成功...');window.close();parent.WindowClose();</script>");
    }

    //完成方法
    protected void BtnWC_Click(object sender, EventArgs e)
    {
        string sql = string.Format("update T_QUES_INFO set WTZT = '已完成', WCSJ = to_date('{1}','YYYY-MM-DD HH24:MI') where ID = '{0}'",ID.Text,DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        DataFunction.ExecuteNonQuery(sql);

        WTZT.SelectedValue = "已完成";
        //处理人已经完成了，所以把状态性质改成问题评审，等待问题评审中，一评审后ZTXZ就改成已完成   罗耀斌
        ZTXZ.Text = "问题评审";
        sql = string.Format("select * from T_QUES_INFO where ID = '{0}'", ID.Text);
        DataSet ds = DataFunction.FillDataSet(sql);
        ShareFunction.GetControlData(Page, ds.Tables[0].Rows[0]);
        DataFunction.SaveData(ds, "T_QUES_INFO");

        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('保存成功....');window.close();parent.WindowClose();</script>");
    }

   
    private void SetCtrlReadOnly(Control ctrl)
    {
        foreach (Control c in ctrl.Controls)
        {
            if (c.GetType().Name == "TextBox")
            {
                (c as TextBox).Attributes.Add("readonly", "true");
            }
            else if (c.GetType().Name == "DropDownList")
            {
                (c as DropDownList).Enabled = false;
            }
            else if (c.GetType().Name == "HtmlImage")
            {
                c.Visible = false;
            }
        }
    }

    #region 刷新方法 罗耀斌
    protected void BtnRef_Click(object sender, EventArgs e)
    {
        FillPage();
    }
    #endregion

    #region 判断该问题是否已完成 点处理和转交时能过这个方法验证 罗耀斌
    [Ajax.AjaxMethod()]
    public string Validator(string id)
    {
        string mc = DataFunction.GetStringResult("select wtzt from t_ques_info t where t.id='"+id+"'");
        return mc;
    }
    #endregion

    #region 判断该问题是否已移交他人 点处理和转交时能过这个方法验证 罗耀斌
    [Ajax.AjaxMethod()]
    public string ValidatorYj(string id)
    {
        string mc = DataFunction.GetStringResult("select fzr from t_ques_info t where t.id='" + id + "'");
        return mc;
    }
    #endregion

    #region 退回事件
    [Ajax.AjaxMethod()]
    public void Back(string ztxz,string id)
    {
        string[] sql = new string[3];;
        string sql1 = "", sql2 = "", sql3 = "";
        if (ztxz == "问题指派")
        {
            sql1 = "update t_ques_info set ztxz='新建' where id='" + id + "'";
            sql2 = "update t_ques_clr set bz='问题指派(退回)',isdq='0' where isdq='1' and quesid='" + id + "'";
            sql3 = @"insert into t_ques_clr(id,quesid,sj,urealname,uname,lx,isdq,bz) 
                    select * from (select sys_guid(),quesid,sysdate,urealname,uname,lx,'1',bz from t_ques_clr where quesid='" + id + "' and lx='CJR' order by sj desc) where rownum=1";
            sql[0] = sql1;
            sql[1] = sql2;
            sql[2] = sql3;
        }
        else if (ztxz == "问题处理")
        {
            sql1 = "update t_ques_info set ztxz='问题指派',zpr='' where id='" + id + "'";
            sql2 = "update t_ques_clr set bz='问题处理(退回)',isdq='0' where isdq='1' and quesid='" + id + "'";
            sql3 = @"insert into t_ques_clr(id,quesid,sj,urealname,uname,lx,isdq,bz) 
              select * from (select sys_guid(),quesid,sysdate,urealname,uname,lx,'1',bz from t_ques_clr where quesid='"+id+"' and lx='ZPR' order by sj desc) where rownum=1";
            sql[0] = sql1;
            sql[1] = sql2;
            sql[2] = sql3;
        }
        DataFunction.ExecuteTransaction(sql);
    }
    #endregion

}
