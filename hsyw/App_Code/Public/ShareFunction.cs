using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections;
using Wuqi.Webdiyer;

/// <summary>
///ShareFunction 的摘要说明
/// </summary>
public class ShareFunction
{
	public ShareFunction()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
    }
    #region 取回数据,支持在tab中的页面
    public static DataRow GetControlData(Control page, DataRow dataRow)
    {
        if (dataRow == null) return dataRow;
        int i = 0;
        string columnName;
        object columnValue;

        //System.Web.UI.WebControls.WebControl webControl;
        object webControl;
        while (i < dataRow.Table.Columns.Count)
        {
            columnName = dataRow.Table.Columns[i].ColumnName;
            webControl = ((System.Web.UI.Control)page).FindControl(columnName);           
            if (!(webControl == null))
            {
                columnValue = GetControlValue(webControl);
                dataRow[columnName] = columnValue;
            }
            i += 1;
        }
        return dataRow;
    }

    public static string GetControlData(Control page, DataRow dataRow,string TableName)
    {
        if (dataRow == null) return "";
        int i = 0;
        string strComment = "";
        string columnName;
        object columnValue;
        object webControl;
        string sql = " select t.column_name,nvl(t.comments,t.column_name) as comments from user_col_comments t where t.table_name='" + TableName + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["column_name"] };
        while (i < dataRow.Table.Columns.Count)
        {
            columnName = dataRow.Table.Columns[i].ColumnName;
            webControl = ((System.Web.UI.Control)page).FindControl(columnName);
            if (!(webControl == null))
            {
                DataRow dr = ds.Tables[0].Rows.Find(columnName);
                columnValue = GetControlValue(webControl);

                if (columnName != "UPDATEDATETIME" && dataRow.RowState==DataRowState.Modified)
                 {
                     string valueType =dataRow.Table.Columns[i].DataType.ToString();
                     string oldValue = dataRow[columnName].ToString();
                     string newValue = columnValue.ToString();
                     string comments = "【"+dr["comments"].ToString()+"】";
                     if (valueType == "System.DateTime" )
                     {
                         if (!string.IsNullOrEmpty(oldValue) && !string.IsNullOrEmpty(newValue))
                         {
                             DateTime oldTime = Convert.ToDateTime(oldValue);
                             DateTime newTime = Convert.ToDateTime(newValue);
                             if (oldTime != newTime)
                             {
                                 if (!string.IsNullOrEmpty(strComment)) { strComment += "<br>"; }
                                 strComment += comments + "：由〖" + oldTime.ToString() + "〗改为〖" + newTime.ToString() + "〗；";
                             }                            
                         }
                         else if (!string.IsNullOrEmpty(oldValue) || !string.IsNullOrEmpty(newValue))
                         {
                             if (!string.IsNullOrEmpty(strComment)) { strComment += "<br>"; }
                             strComment += comments + "：由〖" + oldValue + "〗改为〖" + newValue + "〗；";
                         }
                     }
                     else if (dataRow[columnName].ToString() != columnValue.ToString())
                     { 
                         if (oldValue.Length > 100)
                         {
                             oldValue = oldValue.Substring(0, 97) + "...";
                         }
                         if (newValue.Length > 100)
                         {
                             newValue = newValue.Substring(0, 97) + "...";
                         }
                         if (!string.IsNullOrEmpty(strComment)) { strComment += "<br>"; }
                         strComment += comments + "：由〖" + oldValue + "〗改为〖" + newValue + "〗；";
                     }
                 }
                dataRow[columnName] = columnValue;
            }
            i += 1;
        }
        if (strComment.Length > 2000)
        {
            strComment = strComment.Substring(0, 2000);
        }
        return strComment;
    }
    #endregion 取回数据

    #region 填充页面数据 ，支持设置tab页中的内容
    public static void FillControlData(Control page, DataRow dataRow)
    {
        if (dataRow == null)
        {
            return;
        }
        int i = 0;
        string columnName;
        object webControl;

        while (i < dataRow.Table.Columns.Count)
        {
            columnName = dataRow.Table.Columns[i].ColumnName;           
            webControl = ((System.Web.UI.Control)page).FindControl(columnName);
            if (!(webControl == null))
            {
                SetControlValue(webControl, dataRow.ItemArray[i]);
            }
            i += 1;
        }
    }
    #endregion 填充页面数据
    #region 取得控件值，返回object类型
   
    /// <summary>
    /// 根据控件的类型取得控件值
    /// </summary>
    /// <param name="webControl">控件</param>
    /// <returns>控件的返回值对象</returns>
    public static object GetControlValue(object webControl)
    {
        if (webControl == null)
        {
            return Convert.DBNull;
        }

        string controlType = webControl.GetType().FullName;
        object controlValue = Convert.DBNull;
        switch (controlType)
        {
            case "System.Web.UI.WebControls.TextBox":
                controlValue = ((System.Web.UI.WebControls.TextBox)webControl).Text;
                break;
            case "System.Web.UI.WebControls.ListBox":
                controlValue = ((System.Web.UI.WebControls.ListBox)webControl).SelectedValue;
                break;
            case "System.Web.UI.WebControls.Label":
                controlValue = ((System.Web.UI.WebControls.Label)webControl).Text;
                break;
            case "System.Web.UI.HtmlControls.HtmlInputHidden":
                controlValue = ((System.Web.UI.HtmlControls.HtmlInputHidden)webControl).Value;
                break;
            case "System.Web.UI.WebControls.DropDownList":
                controlValue = ((System.Web.UI.WebControls.DropDownList)webControl).SelectedValue;
                break;
            case "System.Web.UI.WebControls.CheckBox":
                controlValue = ((System.Web.UI.WebControls.CheckBox)webControl).Checked ? "1" : "0";
                break;
            case "System.Web.UI.WebControls.RadioButtonList":
                controlValue = ((System.Web.UI.WebControls.RadioButtonList)webControl).SelectedValue;
                break;
            case "System.Web.UI.WebControls.CheckBoxList":
                CheckBoxList chekcboxlist = (System.Web.UI.WebControls.CheckBoxList)webControl;
                if (chekcboxlist != null)
                {
                    for (int i = 0; i < chekcboxlist.Items.Count; i++)
                    {
                        if (chekcboxlist.Items[i].Selected)
                        {
                            controlValue += chekcboxlist.Items[i].Value + ",";
                        }
                    }
                    if (Convert.ToString(controlValue).Length != 0)
                    {
                        controlValue = Convert.ToString(controlValue).Substring(0, Convert.ToString(controlValue).Length - 1);
                    }

                }
                break;
        }
        if (!((Convert.ToString(controlValue)).Length > 0))
        {
            controlValue = Convert.DBNull;
        }
        return controlValue;
    }
    #endregion
    #region 设置页面控件的值
    /// <summary>
    /// 设置控件的值
    /// </summary>
    /// <param name="webControl">控件</param>
    /// <param name="value">值对象</param>
    public static void SetControlValue(object webControl, object value)
    {
        if (webControl == null)
        {
            return;
        }
        string controlType = webControl.GetType().FullName;
        string valueType = value.GetType().FullName;
        string valueString = "";
        switch (valueType)
        {
            case "System.DateTime":
                DateTime nT=(System.DateTime)value;
                if(nT.Hour==0 && nT.Minute==0 && nT.Second==0)
                {
                    valueString = nT.ToString("yyyy-MM-dd");
                }
                else
                {
                    valueString = nT.ToString("yyyy-MM-dd HH:mm:ss");
                }
                break;
            case "System.DBNull":
                valueString = "";
                break;
            default:
                valueString = Convert.ToString(value);
                break;
        }
        switch (controlType)
        {
            case "System.Web.UI.WebControls.TextBox":
                ((System.Web.UI.WebControls.TextBox)webControl).Text = valueString;
                break;
            case "System.Web.UI.WebControls.ListBox":
                ((System.Web.UI.WebControls.ListBox)webControl).SelectedValue = valueString;
                break;
            case "System.Web.UI.WebControls.Label":
                ((System.Web.UI.WebControls.Label)webControl).Text = valueString;
                break;
            case "System.Web.UI.HtmlControls.HtmlInputHidden":
                ((System.Web.UI.HtmlControls.HtmlInputHidden)webControl).Value = valueString;
                break;
            case "System.Web.UI.WebControls.DropDownList":
                SetDropListSelectedValue(((System.Web.UI.WebControls.DropDownList)webControl), valueString);
                break;
            case "System.Web.UI.WebControls.CheckBox":
                ((System.Web.UI.WebControls.CheckBox)webControl).Checked = (valueString == "1") ? true : false;
                break;
            case  "System.Web.UI.WebControls.RadioButtonList":
                ((System.Web.UI.WebControls.RadioButtonList)webControl).SelectedValue = valueString;
                break;
            case "System.Web.UI.WebControls.CheckBoxList":

                string[] str = Convert.ToString(value).Split(',');
                CheckBoxList checkBoxList = (System.Web.UI.WebControls.CheckBoxList)webControl;
                for (int i = 0; i < str.Length; i++)
                {
                    for (int j = 0; j < checkBoxList.Items.Count; j++)
                    {
                        checkBoxList.Items[j].Selected = false;
                    }
                }
                for (int i = 0; i < str.Length; i++)
                {
                    for (int j = 0; j < checkBoxList.Items.Count; j++)
                    {
                        if (checkBoxList.Items[j].Value == str[i])
                        {
                            checkBoxList.Items[j].Selected = true;
                        }
                    }
                }
                break;
        }
    }
    #endregion

    #region 设置DropList选中的值，如没有有该值，则增加该值。
    public static void SetDropListSelectedValue(System.Web.UI.WebControls.DropDownList dropDownList, string newValue)
    {
        System.Web.UI.WebControls.ListItem listItem = dropDownList.Items.FindByValue(newValue);
        if (listItem == null)
        {
            dropDownList.Items.Insert(0, newValue);
            dropDownList.SelectedIndex = 0;
        }
        else
        {
            dropDownList.SelectedValue = newValue;
        }
    }
    #endregion
    /// <summary>
    /// 是否有权限编码为pcode的权限
    /// </summary>
    /// <param name="username">用户名</param>
    /// <param name="pcode">权限编码</param>
    /// <returns></returns>
    public static bool IsHavePrivate(string username,string pcode)
    {
        return DataFunction.HasRecord(string.Format(@"select p.* from t_sys_private p left join t_sys_r_groupprivate gp on gp.pcode = p.pcode left join t_Sys_r_Usergroup ug on ug.groupcode = gp.groupcode where ug.username = '{0}' and p.pcode = '{1}'",username,pcode));
    }


    public static void InsertLog(System.Web.UI.Page page, string pk_guid, string Title, string Memo)   //写日志文件
    {
        if (!string.IsNullOrEmpty(Memo) || Title.IndexOf("新增")>-1)
        {
            System.Guid guid = System.Guid.NewGuid();
            string ID = guid.ToString().Trim();
            string Ip = publ.GetClientIP();
            string UserName = "【" + page.Session["BranchName"].ToString().Trim() + "】" + page.Session["UserRealName"].ToString().Trim();
            ArrayList parameters = new ArrayList();
            parameters.Add(DataFunction.SetParameter(":ID", DbType.String, Guid.NewGuid().ToString()));
            parameters.Add(DataFunction.SetParameter(":Ip", DbType.String, Ip));
            parameters.Add(DataFunction.SetParameter(":UserName", DbType.String, UserName));
            parameters.Add(DataFunction.SetParameter(":Title", DbType.String, Title));
            parameters.Add(DataFunction.SetParameter(":Memo", DbType.String, Memo));
            parameters.Add(DataFunction.SetParameter(":USERDATETIME", DbType.DateTime, DateTime.Now.ToString()));
            parameters.Add(DataFunction.SetParameter(":pk_guid", DbType.String, pk_guid));
            DataFunction.InsertData(parameters, "T_SYS_LOG");
        }
    }

    #region 取首拼音字母
    public static string hz2py(string hz)  //获得汉字的区位码
    {
        byte[] sarr = System.Text.Encoding.Default.GetBytes(hz);
        int len = sarr.Length;
        if (len > 1)
        {
            byte[] array = new byte[2];
            array = System.Text.Encoding.Default.GetBytes(hz);
            int i1 = (short)(array[0] - '\0');
            int i2 = (short)(array[1] - '\0');
            int tmp = i1 * 256 + i2;
            string getpychar = "*";//找不到拼音码的用*补位
            if (tmp >= 45217 && tmp <= 45252) { getpychar = "A"; }
            else if (tmp >= 45253 && tmp <= 45760) { getpychar = "B"; }
            else if (tmp >= 45761 && tmp <= 46317) { getpychar = "C"; }
            else if (tmp >= 46318 && tmp <= 46825) { getpychar = "D"; }
            else if (tmp >= 46826 && tmp <= 47009) { getpychar = "E"; }
            else if (tmp >= 47010 && tmp <= 47296) { getpychar = "F"; }
            else if (tmp >= 47297 && tmp <= 47613) { getpychar = "G"; }
            else if (tmp >= 47614 && tmp <= 48118) { getpychar = "H"; }
            else if (tmp >= 48119 && tmp <= 49061) { getpychar = "J"; }
            else if (tmp >= 49062 && tmp <= 49323) { getpychar = "K"; }
            else if (tmp >= 49324 && tmp <= 49895) { getpychar = "L"; }
            else if (tmp >= 49896 && tmp <= 50370) { getpychar = "M"; }
            else if (tmp >= 50371 && tmp <= 50613) { getpychar = "N"; }
            else if (tmp >= 50614 && tmp <= 50621) { getpychar = "O"; }
            else if (tmp >= 50622 && tmp <= 50905) { getpychar = "P"; }
            else if (tmp >= 50906 && tmp <= 51386) { getpychar = "Q"; }
            else if (tmp >= 51387 && tmp <= 51445) { getpychar = "R"; }
            else if (tmp >= 51446 && tmp <= 52217) { getpychar = "S"; }
            else if (tmp >= 52218 && tmp <= 52697) { getpychar = "T"; }
            else if (tmp >= 52698 && tmp <= 52979) { getpychar = "W"; }
            else if (tmp >= 52980 && tmp <= 53690) { getpychar = "X"; }
            else if (tmp >= 53689 && tmp <= 54480) { getpychar = "Y"; }
            else if (tmp >= 54481 && tmp <= 55289) { getpychar = "Z"; }
            else { getpychar = ""; }
            return getpychar;
        }
        else
        {
            return hz;
        }
    }

    public static string transpy(string strhz)  //把汉字字符串转换成拼音码
    {
        string strtemp = "";
        int strlen = strhz.Length;
        for (int i = 0; i <= strlen - 1; i++)
        {
            strtemp += hz2py(strhz.Substring(i, 1));
        }
        return strtemp;
    }
   
    #endregion

    #region 绑定枚举类型的下拉列表
    public static void BindEnumDropList(DropDownList drp, string enum_sort)
    {
        DataSet ds = DataFunction.FillDataSet("select * from T_RES_SYS_ENUMDATA where enum_sort = '" + enum_sort + "' order by enum_name");
        drp.DataSource = ds;
        drp.DataTextField = "ENUM_NAME";
        drp.DataValueField = "ENUM_NAME";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("", ""));
        drp.SelectedIndex = 0;
    }
    #endregion

    #region 绑定枚举类型的下拉列表
    public static void BindEnumDropList(DropDownList drp, string enum_sort, string p_enum_name)
    {
        DataSet ds = DataFunction.FillDataSet("select * from T_RES_SYS_ENUMDATA where enum_sort = '" + enum_sort + "' and p_enum_name='"+p_enum_name+"' order by sequence");
        drp.DataSource = ds;
        drp.DataTextField = "ENUM_NAME";
        drp.DataValueField = "ENUM_NAME";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("", ""));
        drp.SelectedIndex = 0;
    }

    public static void BindEnumDropList(DropDownList drp, string enum_sort, string p_enum_name, bool bl)
    {
        string sql = "select * from T_RES_SYS_ENUMDATA where enum_sort = '" + enum_sort + "'";
        if (bl && p_enum_name != "")
        {
            sql += " and p_enum_name='" + p_enum_name + "'";
        }
        sql += " order by sequence";
        DataSet ds = DataFunction.FillDataSet(sql);
        drp.DataSource = ds;
        drp.DataTextField = "ENUM_NAME";
        drp.DataValueField = "ENUM_NAME";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("", ""));
        drp.SelectedIndex = 0;
    }
    #endregion

    #region 获取可访问区域权限
    public string GetfwqySql(Page page,string strFiled)
    {
        string strSql = "";
        if (page.Session["ISSUPER"].ToString() != "1")
        {
            if (page.Session["FWQY"] != null)
            {
                string sql1 = "";
                string[] fwqy = page.Session["FWQY"].ToString().Split(',');
                foreach (string fwqy1 in fwqy)
                {

                    if (sql1 != "")
                    {
                        sql1 += " or " + strFiled + " like '" + fwqy1 + "%'";
                    }
                    else
                    {
                        sql1 += strFiled + " like '" + fwqy1 + "%'";
                    }
                }
                strSql += " and (" + sql1 + ")";
            }
            else
            {
                strSql += " and 1<>1";
            }
        }
        return strSql;
    }
    #endregion


    /// <summary>
    /// 绑定grid,带分页控件
    /// </summary>
    /// <param name="girdView">列表</param>
    /// <param name="ds">数据源</param>
    /// <param name="AspNetPager1">分页控件id</param>
    public static void BindGridView(GridView gridView, DataSet ds, AspNetPager AspNetPager1)
    {
        bool newrow = false;
        if (ds.Tables[0].Rows.Count == 0)
        {
            newrow = true;
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
        }

        DataView dv = ds.Tables[0].DefaultView;

        PagedDataSource pds = new PagedDataSource();
        pds.DataSource = dv;//设置数据源(DataTable类型)
        pds.AllowPaging = true;

        //每页显示的行数
        AspNetPager1.PageSize = (AspNetPager1.PageSize == 10) ? 30 : AspNetPager1.PageSize;
        AspNetPager1.RecordCount = dv.Count;
        pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
        pds.PageSize = AspNetPager1.PageSize;

        gridView.AllowPaging = true;
        gridView.PageSize = AspNetPager1.PageSize;
        gridView.DataSource = pds;
        gridView.DataBind();


        if (newrow)
        {
            int columnCount = gridView.Columns.Count;
            gridView.Rows[0].Cells.Clear();
            //gridView.Rows[0].Cells.Add(new TableCell());
            //gridView.Rows[0].Cells[0].ColumnSpan = columnCount;
            //gridView.Rows[0].Cells[0].Width = Unit.Percentage(100);
            //gridView.Rows[0].Cells[0].Text = "无数据记录！";
            //gridView.Rows[0].Cells[0].Style.Add("text-align", "center");
        }
    }
}
