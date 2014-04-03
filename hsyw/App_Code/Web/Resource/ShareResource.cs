using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;



/// <summary>
///ShareResource 的摘要说明
/// </summary>
public class ShareResource
{
    public string comp_house_unit_id = "d86fbb8d-87c4-44f8-abfd-8ca14744299d";
    public string house_unit_id = "8e1a1563-2e5c-46ae-965c-d779b66de353";
    public string cupboard_unit_id = "ee01eccb-7c95-4b96-b122-ed9913026f24";
    public string equ_unit_id = "3518e4d8-2188-4aa9-8d43-4b541dfa0f90";
    public string groove_unit_id = "00c3a457-24eb-4c00-a7e0-5e7f0116fe68";
    public string port_unit_id = "bfc13d2d-eab8-4784-a96a-b8ffc21b4e88";
    public string core_unit_id = "3b5bfe00-e5dd-4f02-bf41-b347ca9c7624";
    public string board_unit_id = "f45de5f7-24bc-4b5a-b0f9-5e576c97aef3";
    public string light_core_unit_id = "53d347a2-5be1-4b85-af53-ffd35b3ccfc7";
	public ShareResource()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
    }

    #region 创建资源表单界面  
    /// <summary>
    /// 创建资源表单界面
    /// </summary>
    /// <param name="Td_Page">装载表单的容器</param>
    /// <param name="Unit_Id">资源单元ID</param>
    /// <param name="isQuery">是否为查询界面</param>
    public void CreateResourceTable(HtmlTableCell Td_Resource_Page, string Unit_Id, string pageType)
    {      
        DataSet ds = GetPropertyData(Unit_Id, pageType);
        if (ds.Tables[0].Rows.Count > 0)
        {           
            HtmlTable tb = new HtmlTable();
            tb.Border = 1;
            tb.Style.Add("BORDER-COLLAPSE", "collapse");
            tb.BorderColor = "#5b9ed1";
            tb.Width = "100%";
            tb.CellPadding =1;
            tb.CellSpacing = 0;
            HtmlTableRow tr = null;
            int ColCount = GetColCount(Unit_Id, pageType);
            int colWidth = 100 / ColCount;
            int col = 0;
            if (ds.Tables[0].Rows.Count > 0)
            { 
                tr = new HtmlTableRow();
                tb.Rows.Add(tr);
                foreach (DataRow DR in ds.Tables[0].Rows)
                {
                    string propery_Name=  DR["PROPERY_NAME"].ToString();
                    if (DR["UNIT_ID"].ToString() == light_core_unit_id && pageType == "QUERY")
                    {
                        if (DR["FILED_NAME"].ToString() == "ZZD")
                        {
                            continue;
                        }
                        if (DR["FILED_NAME"].ToString() == "QSD")
                        {
                            propery_Name = "机房名称";
                        }
                    }
                    int lable_num = 1;
                    if (!string.IsNullOrEmpty(DR["COL_LABLE_NUM"].ToString()))
                    {
                      lable_num= Convert.ToInt32(DR["COL_LABLE_NUM"]);
                    }
                    int text_num =1;
                    if (!string.IsNullOrEmpty(DR["COL_TEXT_NUM"].ToString()))
                    {
                        text_num = Convert.ToInt32(DR["COL_TEXT_NUM"]);
                    }
                    if (pageType=="QUERY")
                    {
                        text_num = 1;
                    }
                    int n = col + lable_num + text_num;
                    if (n > ColCount)
                    {
                        CreateEmptyCell(tr, ColCount, col, colWidth);
                        col = lable_num + text_num;
                        tr = new HtmlTableRow();
                        tb.Rows.Add(tr);
                    }
                    else
                    {
                        col = n;
                    }
                    HtmlTableCell td1 = new HtmlTableCell(); 
                    td1.InnerText = propery_Name;
                    if (!string.IsNullOrEmpty(DR["FORMULA"].ToString()) || !string.IsNullOrEmpty(DR["TSGS"].ToString()))
                    {
                        CheckBox ch = new CheckBox();
                        ch.ID = DR["FILED_NAME"].ToString() + "_CHECK";
                        td1.Controls.Add(ch);
                    }
                    td1.Attributes.Add("class", "tdBak");
                    td1.Align = "center";
                    td1.Width = lable_num * colWidth + "%";
                    td1.ColSpan = lable_num;
                    tr.Cells.Add(td1);
                    HtmlTableCell td2 = new HtmlTableCell();
                    td2.Width = text_num * colWidth + "%";
                    CreateControls(DR, td2, pageType);
                    td2.ColSpan = text_num;
                    tr.Cells.Add(td2);
                }
            CreateEmptyCell(tr, ColCount, col, colWidth);       
            }

            Td_Resource_Page.Controls.Add(tb);
        }       
    }

    #region 
    private void CreateEmptyCell(HtmlTableRow tr,int ColCount, int col, int colWidth)
    {
        if (ColCount - col > 0)//计算剩余行用空填充
        {
            HtmlTableCell td = new HtmlTableCell();
            td.ColSpan = ColCount - col;
            td.Width = (ColCount - col) * colWidth + "%";
            td.BgColor = "Silver";
            tr.Cells.Add(td);
        }
    }
    private DataSet GetPropertyData(string Unit_Id, string pageType)
    {
        string sql = "select * from T_RES_SYS_PROPERTY t where t.unit_id='" + Unit_Id + "'";
        switch (pageType)
        {
            case "QUERY":
                sql += " and isquery=1";
                break;
            case "PAGE":
                break;
            default:
                sql += " and FILED_NAME<>'" + pageType + "'";
                break;

        }
        sql += " order by t.sequence";
        return DataFunction.FillDataSet(sql);
    }
    private int GetColCount(string Unit_Id, string pageType)
    {
        string str = "COLCOUNT";
        if (pageType == "QUERY")
        {
            str = "Q_COLCOUNT";
        }
        string sql = "select "+str+" from T_RES_SYS_UNIT t where t.unit_id='" + Unit_Id + "' ";
        return DataFunction.GetIntResult(sql);
    }
    #endregion

    #region 创建界面控件

    private void CreateControls(DataRow DR, HtmlTableCell TD, string pageType)
        {
            switch (DR["DATA_TYPE"].ToString())
            {
                case "枚举":
                    CreateDropDown(DR, TD, pageType);
                    break;
                case "复选":
                    CheckBox ch = new CheckBox();                   
                    ch.ID = DR["FILED_NAME"].ToString();                  
                    TD.Controls.Add(ch);
                    break;
                case "组织机构":
                    CreateBranch(DR, TD);
                    break;
                case "VLAN资源":
                case "IP资源":
                case "资源选择":
                    CreatePhyResource(DR, TD, pageType, DR["DATA_TYPE"].ToString());
                    break;
                case "日期":
                    CreateTextBox(TD, DR,DR["FILED_NAME"].ToString(), false, false, "setDay(this);");           
                    break;
                case "日期时间":
                    CreateTextBox(TD, DR, DR["FILED_NAME"].ToString(), false, false, "setDayHM(this);"); 
                    break;
                case "数字":
                    if (pageType != "QUERY" && DR["ISEDIT"].ToString() == "0")
                    {
                        CreateTextBox(TD, DR, DR["FILED_NAME"].ToString(), true, false, "NUM");
                    }
                    else
                    {
                        CreateTextBox(TD, DR, DR["FILED_NAME"].ToString(), false, false, "NUM");
                    }
                    break;
                default:
                    if (pageType != "QUERY" && DR["ISEDIT"].ToString() == "0")
                    {
                        CreateTextBox(TD, DR, DR["FILED_NAME"].ToString(), true, false, null);
                    }
                    else
                    {
                        CreateTextBox(TD, DR, DR["FILED_NAME"].ToString(), false, false, null);
                    }
                    break;
            }
        }
        #region 创建文本控件
        private TextBox CreateTextBox(HtmlTableCell TD, DataRow DR,string filed_name,bool isReadonly,bool isHiden,string strDay)
        {
            TextBox tex = new TextBox();
            tex.ID = filed_name; 
            tex.BorderStyle = BorderStyle.None;
            tex.BorderWidth = Unit.Pixel(0);
            tex.Width = Unit.Percentage(99);
            if (isReadonly )
            {
                tex.Attributes.Add("readonly", "true");
                tex.BackColor = Color.WhiteSmoke;
                TD.BgColor = "WhiteSmoke";
            }
            if (isHiden)
            {
                tex.Style.Add("display", "none");
            }
            if (!string.IsNullOrEmpty(strDay))
            {
                if (strDay == "NUM")
                {
                    tex.Attributes.Add("onchange", "checkTxt(this);");
                    tex.Attributes.Add("onKeyPress", "return limitNum(this);");
                }
                else
                {
                    tex.Attributes.Add("onfocus", strDay);
                }
            }

            TD.Controls.Add(tex);
            return tex;
        }
        #endregion

       

        #region 创建下拉控件
        private DropDownList CreateDropDownList(HtmlTableCell TD, string Filed_Name, string Enum_Sort)
        {
            DropDownList drt = new DropDownList();
            drt.Items.Clear();
            drt.DataTextField = "ENUM_NAME";
            drt.DataValueField = "ENUM_NAME";           
            drt.DataSource = GetEnumData(Enum_Sort);
            drt.DataBind();
            drt.Items.Insert(0, "");
            drt.ID = Filed_Name;
            drt.Width = Unit.Percentage(99);
            TD.Controls.Add(drt);
            return drt;
        }
        #endregion
        #region 创建表单
        private HtmlTable CreateHtmlTable()
        {
            HtmlTable tb = new HtmlTable();
            tb.Border = 0;
            tb.Width = "99%";
            tb.CellPadding = 0;
            tb.CellSpacing = 0;
            return tb;
        }
        #endregion

        #region 创建组织机构控件
        private void CreateBranch(DataRow DR, HtmlTableCell TD)
        {
            HtmlTable tb = CreateHtmlTable();
            HtmlTableRow tr = new HtmlTableRow();
            tb.Rows.Add(tr);
            string name= DR["FILED_NAME"].ToString();;
            string code=DR["FILED_NAME"].ToString()+"_CODE";
            HtmlTableCell td1 = new HtmlTableCell();
            CreateTextBox(td1,DR, name,true,false,null);
            CreateTextBox(td1,DR,code, false, true, null);  
            td1.Width = "100%";
            tr.Cells.Add(td1);           
            HtmlTableCell td2 = new HtmlTableCell();
            td2.InnerHtml = "<img align='right' src='../Images/Small/bb_table.gif' onclick=\"windowOpenBranchTree('"+name+"','"+code+"')\"  />";
            tr.Cells.Add(td2);
            TD.BgColor = "WhiteSmoke";
            TD.Controls.Add(tb);
        }
        #endregion

        #region 创建枚举类型下拉控件
        private void CreateDropDown(DataRow DR, HtmlTableCell TD, string pageType)
        {
            HtmlTable tb = CreateHtmlTable();
            HtmlTableRow tr = new HtmlTableRow();
            tb.Rows.Add(tr);
            HtmlTableCell td1 = new HtmlTableCell();
            DropDownList drt=CreateDropDownList(td1, DR["FILED_NAME"].ToString(), DR["ENUM_SORT"].ToString());
            if(!string.IsNullOrEmpty( DR["LINKAGE_CODE"].ToString()))
            {
                drt.Attributes.Add("onfocus", "LoadEnumData('" + DR["FILED_NAME"].ToString() + "','" + DR["ENUM_SORT"] + "','" + DR["LINKAGE_CODE"] + "')");
            }
            if (DR["ISENUMSHORT"].ToString() == "1")
            {
              CreateTextBox(td1, DR, DR["FILED_NAME"].ToString()+"_SHORT", true, true, null);             
              drt.Attributes.Add("onchange", "changeEnumData('" + DR["FILED_NAME"].ToString() + "','" + DR["ENUM_SORT"] + "','" + DR["LINKAGE_CODE"] + "')");
            }          
            td1.Width = "100%";
            tr.Cells.Add(td1);
            if (pageType !="QUERY")
            {
                HtmlTableCell td2 = new HtmlTableCell();
                td2.InnerHtml = "<img align='right' src='../Images/Small/bb_table.gif' onclick=\"windowOpenEnumDataPage('" + DR["ENUM_SORT"]+ "','" + DR["LINKAGE_CODE"] + "')\"  />";
                tr.Cells.Add(td2);
            }
            TD.Controls.Add(tb);
        }
        private DataSet GetEnumData(string Enum_Sort)
        {
            string sql = "select * from T_RES_SYS_ENUMDATA where ENUM_SORT='" + Enum_Sort + "' order by SEQUENCE";
            return DataFunction.FillDataSet(sql);
        }
        #endregion

        #region 创建物理资源控件
        private void CreatePhyResource(DataRow DR, HtmlTableCell TD, string pageType,string dataType)
        {
            string name = DR["FILED_NAME"].ToString();
            string guid = DR["FILED_NAME"].ToString() + "_GUID";
            string code = DR["FILED_NAME"].ToString() + "_CODE";
            string unit_name = DR["ENUM_SORT"].ToString();
            string linkage_code = DR["LINKAGE_CODE"].ToString();
          
            HtmlTable tb = CreateHtmlTable();
            HtmlTableRow tr = new HtmlTableRow();
            tb.Rows.Add(tr);
          
            HtmlTableCell td1 = new HtmlTableCell();
            if (DR["ISEQUCODE"].ToString() == "1")
            {
                HtmlTableCell td3 = new HtmlTableCell();
                CreateTextBox(td3, DR, code, true, false, null);
                td3.Width = "48%";
                tr.Cells.Add(td3);
                HtmlTableCell td4 = new HtmlTableCell();
                td4.Attributes.Add("class", "tdBak");
                td4.InnerText = "—";
                td4.Width = "4%";
                tr.Cells.Add(td4);
                td1.Width = "48%";
            }
            else
            { 
                td1.Width = "100%";
            }          
            CreateTextBox(td1,DR, name, true, false, null);
            CreateTextBox(td1, DR, guid, false, true, null); 
            tr.Cells.Add(td1);
            HtmlTableCell td2 = new HtmlTableCell();
            switch(dataType)
            {
                case "IP资源":
                    td2.InnerHtml = "<img align='right' src='../Images/Small/bb_table.gif' onclick=\"windowOpenLogicResourceIpSelect('" + name + "','" + linkage_code + "')\"  />";
                    break;
                default:
                    td2.InnerHtml = "<img align='right' src='../Images/Small/bb_table.gif' onclick=\"windowOpenPhyResourceSelect('" + DR["PROPERY_ID"].ToString() + "','" + name + "','" + code + "','" + guid + "','" + linkage_code + "','" + DR["ISEQUCODE"] + "')\"  />";
                 break;    
             }
            tr.Cells.Add(td2);
            TD.Controls.Add(tb);
            TD.BgColor = "WhiteSmoke";
        }
        #endregion

        #endregion

    #endregion

    #region 获取数据   
    public DataRow GetResourceDataRow(string tableName, string guid)
    {
       
        string sql = "select * from " + tableName+" where GUID='"+guid+"'";
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow DR = null;
        if (ds.Tables[0].Rows.Count == 0)
        {
            DR = ds.Tables[0].NewRow();
            DR["GUID"] = guid;
            DR["CREATEDATETIME"] = DateTime.Now;
            ds.Tables[0].Rows.Add(DR);
        }
        else
        {
            DR = ds.Tables[0].Rows[0];
        }      
        return DR;
    }
    public DataRow GetResourceDataRowLS(string tableName, string guid)
    {

        string sql = "select * from " + tableName + "_LS where GUID='" + guid + "' and updatedatetime>sysdate-1";
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow DR = null;
        if (ds.Tables[0].Rows.Count == 0)
        {
            DR = ds.Tables[0].NewRow();
            DR["PK_GUID"] = Guid.NewGuid().ToString();
            DR["GUID"] = guid;
            DR["CREATEDATETIME"] = DateTime.Now;
            ds.Tables[0].Rows.Add(DR);
        }
        else
        {
            DR = ds.Tables[0].Rows[0];
        }
        return DR;
    }
    public string GetTableName(string Unit_Id)
    {
        string sql = "select t.table_name from T_RES_SYS_UNIT t where t.unit_id='" + Unit_Id + "'";
        return DataFunction.GetStringResult(sql);
    }
   
    #endregion

    public DataRow GetResUnitData(string Unit_Id)
    {
        string sql = "select t.* from T_RES_SYS_UNIT t where t.unit_id='" + Unit_Id + "'";
        return DataFunction.FillDataSet(sql).Tables[0].Rows[0];
    }

    public class CheckBoxTemplate : System.Web.UI.ITemplate
    {
        public CheckBoxTemplate()
        {
        }
        public void InstantiateIn(System.Web.UI.Control container)
        {
            CheckBox ch = new CheckBox();
            ch.ID = "XZ";
            container.Controls.Add(ch);
        }
    }
     

    #region 创建列表界面
    public void CreateResourceGrid(GridView ResourceGrid, string Unit_Id,bool isSelect)
    {
        ResourceGrid.Attributes.Add("BorderColor", "#5B9ED1");
        DataSet ds = GetPropertyData(Unit_Id);
        ResourceGrid.Columns.Clear();
        if (isSelect)
        {
            CreateGridColum(ResourceGrid, "TemplateField", 5, null, "选择",null);           
        }
        else
        {
            CreateGridColum(ResourceGrid, "TemplateField", 5, null, "选择", null);  
            CreateGridColum(ResourceGrid, "BoundField", 5, null, "序号","居中");           
        }
        DataSet eDs = GetEnumFiledData(Unit_Id);
        eDs.Tables[0].PrimaryKey = new DataColumn[] { eDs.Tables[0].Columns["filed_name"] };
        foreach (DataRow DR in ds.Tables[0].Rows)
        {
            DataRow eDr = eDs.Tables[0].Rows.Find(DR["FILED_NAME"].ToString());
            int intWidth = 10;
            if (!string.IsNullOrEmpty(DR["GRIDWIDTH"].ToString()))
            {
                intWidth = Convert.ToInt32(DR["GRIDWIDTH"]);
            }
            if (eDr == null)
            {

                CreateGridColum(ResourceGrid, "BoundField", intWidth, DR["FILED_NAME"].ToString(), DR["PROPERY_NAME"].ToString(), DR["ALIGN_TYPE"].ToString());
            }
            else
            {
                CreateGridColum(ResourceGrid, "ImageField", intWidth, DR["FILED_NAME"].ToString(), DR["PROPERY_NAME"].ToString(), DR["ALIGN_TYPE"].ToString());
            }
        }
        string sql = "select GRID_MODE from t_res_sys_unit t where  t.unit_id='" + Unit_Id + "'";
        switch (DataFunction.GetStringResult(sql))
        {
            case "含下级分类名称":
                CreateGridColum(ResourceGrid, "BoundField", 10, "UNIT_NAME", "下属资源分类", "居中");
                CreateGridColum(ResourceGrid, "BoundField", 10, "EQU_NAME", "下属资源名称", "居中");
                break;
            case "含下级所有属性":
                sql = string.Format(@"select p.* from t_res_sys_unit_relation r,T_RES_SYS_PROPERTY p where p.isgridshow='1' and
r.father_unit_id='{0}' and r.child_unit_id=p.unit_id order by p.sequence",Unit_Id);
               DataSet dsP= DataFunction.FillDataSet(sql);
               foreach (DataRow drP in dsP.Tables[0].Rows)
               {
                   int intWidthP = 10;
                   if (!string.IsNullOrEmpty(drP["GRIDWIDTH"].ToString()))
                   {
                       intWidthP = Convert.ToInt32(drP["GRIDWIDTH"]);
                   }
                   CreateGridColum(ResourceGrid, "BoundField", intWidthP, "CHILD_"+drP["FILED_NAME"].ToString(), drP["PROPERY_NAME"].ToString(), drP["ALIGN_TYPE"].ToString());
               }
                break;
        }
        if (!isSelect)
        {
            CreateGridColum(ResourceGrid, "BoundField", 5, null, "详细", "居中");          
        }
    }

    private void CreateGridProperty(GridView ResourceGrid, DataSet eDs,DataSet ds)
    {
       
    }
    private void CreateGridColum(GridView ResourceGrid, string type, int intWidth, string filed_name, string propery_name, string align_type)
    {
        switch (type)
        {
            case "BoundField":
                BoundField bfd = new BoundField();
                bfd.HeaderText = propery_name;                
                if (!string.IsNullOrEmpty(filed_name))
                {
                    bfd.DataField = filed_name;
                    bfd.SortExpression = filed_name;
                }
                bfd.ItemStyle.BorderColor = Color.FromName("#5B9ED1");
                bfd.HeaderStyle.BorderColor = Color.FromName("#5B9ED1");
                bfd.ItemStyle.BorderWidth = Unit.Pixel(1);
                bfd.HeaderStyle.BorderWidth = Unit.Pixel(1);
                bfd.ItemStyle.Width = Unit.Percentage(intWidth);
                bfd.HeaderStyle.Width = Unit.Percentage(intWidth);
                switch (align_type)
                {
                    case "居中":
                        bfd.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        break;
                    case "居右":
                        bfd.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                        break;
                    default:
                        bfd.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                        break;
                }
                ResourceGrid.Columns.Add(bfd);
                break;
            case "TemplateField":
                TemplateField tc = new TemplateField();
                tc.HeaderText = propery_name;
                tc.ItemStyle.BorderColor = Color.FromName("#5B9ED1");
                tc.HeaderStyle.BorderColor = Color.FromName("#5B9ED1");
                tc.ItemStyle.BorderWidth = Unit.Pixel(1);
                tc.HeaderStyle.BorderWidth = Unit.Pixel(1);
                tc.ItemStyle.Width = Unit.Percentage(intWidth);
                tc.HeaderStyle.Width = Unit.Percentage(intWidth);
                tc.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                tc.ItemTemplate = new CheckBoxTemplate();
                ResourceGrid.Columns.Add(tc);
                break;
            case "ImageField":
                ImageField bf = new ImageField();
                bf.DataImageUrlField = filed_name + "_URL";
                bf.SortExpression = filed_name;
                bf.DataAlternateTextField = filed_name;
                bf.HeaderText = propery_name;
                bf.ItemStyle.BorderColor = Color.FromName("#5B9ED1");
                bf.HeaderStyle.BorderColor = Color.FromName("#5B9ED1");
                bf.ItemStyle.BorderWidth = Unit.Pixel(1);
                bf.HeaderStyle.BorderWidth = Unit.Pixel(1);               
                bf.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                bf.ItemStyle.Width = Unit.Percentage(intWidth);
                bf.HeaderStyle.Width = Unit.Percentage(intWidth);
                ResourceGrid.Columns.Add(bf);
                break;
        }
    }
    public int BindGrid(System.Web.UI.Page page,GridView ResourceGrid, string Unit_Id)
    {
        int Count = 0;
        DataSet ds = GetResourceUnitData(page,Unit_Id, GetQueryStr(page, Unit_Id), ref Count);
        ResourceGrid.DataSource = ds;
        ResourceGrid.DataBind();   
        if (ds.Tables[0].Rows[0]["UPDATEDATETIME"].ToString() == "")
        {
            return 0;
        }
        else
        {
            return Count;
        }
      
    }
    public string GetChildQueryStr(string unit_id,  string guid)
    {
        string sql = "select * from T_RES_SYS_PROPERTY t where t.unit_id='" + unit_id + "' and t.data_type='资源选择'";
        DataSet ds = DataFunction.FillDataSet(sql);
        string QueryStr = "";
        foreach (DataRow DR in ds.Tables[0].Rows)
        {
            if (!string.IsNullOrEmpty(QueryStr))
            {
                QueryStr += " or ";
            }
            QueryStr += "t." + DR["FILED_NAME"].ToString() + "_GUID='" + guid + "'";
        }
        if (!string.IsNullOrEmpty(QueryStr))
        {
            QueryStr = " and (" + QueryStr + ")";
        }
        return QueryStr;
    }
    public string GetQueryStr(System.Web.UI.Page page, string Unit_Id)
    {
        string QueryStr = "";
        DataSet ds = GetPropertyData(Unit_Id, "QUERY");
        foreach (DataRow DR in ds.Tables[0].Rows)
        {
            string columnName = DR["FILED_NAME"].ToString();
            if (DR["DATA_TYPE"].ToString() == "组织机构")
            {
                columnName = columnName+"_CODE";
            }
            if (DR["DATA_TYPE"].ToString() == "资源选择" || DR["DATA_TYPE"].ToString() == "IP资源" || DR["DATA_TYPE"].ToString() == "VLAN资源")
            {
                columnName = columnName + "_GUID";
            }
            object webControl = ((System.Web.UI.Control)page).FindControl(columnName);
            if (webControl == null)
            {
                continue;
            }
           string value= ShareFunction.GetControlValue(webControl).ToString();
           if (!string.IsNullOrEmpty(value))
           {
               switch(DR["DATA_TYPE"].ToString())
               {
                   case "组织机构":
                       QueryStr += " and t." + columnName + " like '" + value + "%' ";
                       break;
                   case "资源选择":
                       if (Unit_Id == port_unit_id)
                       {
                           QueryStr += string.Format(@" and t.{0}  in  ( select b1.guid from t_res_child_board b1 where b1.groove_name_guid in (select g1.guid from t_res_child_groove g1 where g1.equ_name_guid='{1}')
union  select '{1}' as guid from dual  union
select b3.guid from t_res_child_board b3 where b3.groove_name_guid in (select g3.guid from t_res_child_groove g3 where g3.equ_name_guid  in 
(select b2.guid from t_res_child_board b2 where b2.groove_name_guid in (select g2.guid from t_res_child_groove g2 where g2.equ_name_guid='{1}'))))", columnName, value);
                       }
                       else
                       {

                           if (DR["UNIT_ID"].ToString() == light_core_unit_id)
                           {
                               if (DR["FILED_NAME"].ToString() == "QSD")
                               {
                                   QueryStr += " and (t.QSD_GUID = '" + value + "' or t.ZZD_GUID='"+value+"')";
                               }
                               else if (DR["FILED_NAME"].ToString() == "ZZD")
                               {

                               }
                               else
                               {
                                   QueryStr += " and t." + columnName + " = '" + value + "' ";
                               }
                           }
                           else
                           {
                               QueryStr += " and t." + columnName + " = '" + value + "' ";
                           }
                       }
                       break;
                   case "枚举":
                   case "VLAN资源":
                   case "IP资源":
                   case "数字":
                       QueryStr += " and t." + columnName + " = '" + value + "' ";
                       break;
                   case "日期":
                       QueryStr += " and t." + columnName + " =to_date( '" + value + "','yyyy-mm-dd') ";
                       break;
                   default:
                       QueryStr += " and t." + columnName + " like '%" + value + "%' ";
                       break;
               }
           }
           else
            {
                if (DR["DATA_TYPE"].ToString() == "组织机构")
                {
                    if (page.Session["ISSUPER"].ToString() != "1")
                    {
                        if (page.Session["FWQY"]==null)
                        {
                            QueryStr += " and 1<>1 ";
                        }
                        else
                        {
                            string[] fwqy = page.Session["FWQY"].ToString().Split(',');
                            string strQy = "";
                            foreach (string qy in fwqy)
                            {
                                if (strQy != "")
                                {
                                    strQy += " or ";
                                }
                                strQy += " t." + columnName + " like '" + qy + "%' ";
                            }
                            if (!string.IsNullOrEmpty(strQy))
                            {
                                QueryStr += " and (" + strQy + ") ";
                            }
                        }
                    }
                }
            }
        }
       
        return QueryStr;
    }
    private DataSet GetPropertyData(string Unit_Id)
    {
        string sql = "select t.* from T_RES_SYS_PROPERTY t where t.unit_id='" + Unit_Id + "' and ISGRIDSHOW='1' order by t.sequence";
        return DataFunction.FillDataSet(sql);
    }

    private DataSet GetEnumFiledData(string Unit_Id)
    {
        string sql = string.Format(@"select distinct t.filed_name, t.enum_sort from T_RES_SYS_PROPERTY t,T_RES_SYS_ENUMDATA d 
where t.unit_id='{0}' and t.enum_sort=d.enum_sort and d.image_url is not null", Unit_Id);
        return DataFunction.FillDataSet(sql);
    }

    public DataSet GetResourceUnitData(System.Web.UI.Page page, string Unit_Id, string strQuery, ref int count)
    {
        string sql = "select * from T_RES_SYS_UNIT t where t.unit_id='" + Unit_Id + "'";
        DataSet dst = DataFunction.FillDataSet(sql);
        string tableName = "T_RES_HOUSE_COMPUTER";
        string SequenceFiled = "";
        string strEqu = "";
        string strEquColum = "";
        if (dst.Tables[0].Rows.Count > 0)
        {
            DataRow drt = dst.Tables[0].Rows[0];
            tableName = drt["TABLE_NAME"].ToString();
            SequenceFiled = drt["SEQUENCE_FILED"].ToString();
            switch (drt["GRID_MODE"].ToString())
            {
                case "含下级分类名称":
                    strEquColum = "u.unit_id,u.unit_name,u.name_filed,a.equ_name,a.equ_guid,";
                     strEqu = @" left join t_res_equment_relation r on t.guid=r.father_equ_guid left join 
         t_res_equment_all a on r.child_equ_guid=a.equ_guid left join t_res_sys_unit u on a.unit_id=u.unit_id ";
                break;
                case "含下级所有属性":
                sql = "select t.child_unit_id from t_res_sys_unit_relation t where t.father_unit_id='"+Unit_Id+"'";
                string child_unit_id = DataFunction.GetStringResult(sql);
                sql = string.Format(@"select u.unit_name,u.name_filed,u.table_name,p.filed_name from t_res_sys_property_unit up,T_RES_SYS_PROPERTY p,t_res_sys_unit u 
where up.propery_id=p.propery_id and up.unit_id='{0}' and p.unit_id='{1}' and p.unit_id=u.unit_id", Unit_Id, child_unit_id);
                DataSet dsE = DataFunction.FillDataSet(sql);
                if (dsE.Tables[0].Rows.Count > 0)
                {
                    DataRow drE = dsE.Tables[0].Rows[0];
                    sql = string.Format(@"select p.* from t_res_sys_unit_relation r,T_RES_SYS_PROPERTY p where p.isgridshow='1' and
r.father_unit_id='{0}' and r.child_unit_id=p.unit_id order by p.sequence", Unit_Id);
                    DataSet dsP = DataFunction.FillDataSet(sql);
                    foreach (DataRow drP in dsP.Tables[0].Rows)
                    {
                        strEquColum +="e." + drP["FILED_NAME"].ToString() +"  as CHILD_" + drP["FILED_NAME"].ToString() + ",";
                        if (drE["name_filed"].ToString() == drP["FILED_NAME"].ToString())
                        {
                            strEquColum += "e." + drP["FILED_NAME"].ToString() + "  as equ_name,";
                        }
                    }
                    strEquColum += "e.GUID as equ_guid,'" + child_unit_id + "' as unit_id,'" + drE["unit_name"].ToString() + "' as unit_name,'" + drE["name_filed"] + "' as name_filed,";
                   
                    strEqu = " left join " + drE["table_name"].ToString() + " e on t.guid=" + "e." + drE["FILED_NAME"].ToString() + "_GUID  ";
                }
                break;
            }
        }
        DataSet eDs = GetEnumFiledData(Unit_Id);
        string strColum = "";
        string strEnum = "";
       
        int i = 0;
        foreach(DataRow eDr in eDs.Tables[0].Rows)
        {
            string tb = "E" + i.ToString();
            strColum +=  tb + ".IMAGE_URL  as " + eDr["filed_name"].ToString() + "_URL,";
            strEnum += "  left join T_RES_SYS_ENUMDATA " + tb + " on t." + eDr["filed_name"].ToString() + "=" + tb + ".ENUM_NAME and " + tb + ".ENUM_SORT='" + eDr["enum_sort"].ToString() + "'";
            i++;
        }
        sql = "select " + strColum +strEquColum+ "t.* from " + tableName + " t " + strEnum + strEqu + "  where 1=1 " + strQuery + " " + SequenceFiled;
        //DataSet ds = DataFunction.FillDataSet(sql);

        int pageSize =Convert.ToInt32( ((DropDownList)page.FindControl("PageSize")).SelectedValue);
        int index = ((DropDownList)page.FindControl("GridPageList")).SelectedIndex;
        count = DataFunction.GetIntResult(string.Format("select count(*) from ({0})", sql));
        int n = 0;
        if (index > -1)
        {
            n = index;
        }
        int s = n * pageSize;
        int e = s + pageSize;
        string strSql = string.Format("select * from  (select rownum as rn,a.* from ({0}) a ) where rn>{1} and rn<={2}", sql, s, e);
        //按修改时间、创建时间排序
        //strSql += "  order by updatedatetime desc,createdatetime desc";
        DataSet ds = DataFunction.FillDataSet(strSql);
        if (ds.Tables[0].Rows.Count == 0)
        {
            DataRow DR = ds.Tables[0].NewRow();
            DR["GUID"] = Guid.NewGuid().ToString();
            ds.Tables[0].Rows.Add(DR);
        }
        return ds;
    }

    public DataSet GetResourceUnitData(System.Web.UI.Page page, string Unit_Id, string strQuery)
    {
        string sql = "select * from T_RES_SYS_UNIT t where t.unit_id='" + Unit_Id + "'";
        DataSet dst = DataFunction.FillDataSet(sql);
        string tableName = "T_RES_HOUSE_COMPUTER";
        string SequenceFiled = "";
        string strEqu = "";
        string strEquColum = "";
        if (dst.Tables[0].Rows.Count > 0)
        {
            DataRow drt = dst.Tables[0].Rows[0];
            tableName = drt["TABLE_NAME"].ToString();
            SequenceFiled = drt["SEQUENCE_FILED"].ToString();
            switch (drt["GRID_MODE"].ToString())
            {
                case "含下级分类名称":
                    strEquColum = "u.unit_id,u.unit_name,u.name_filed,a.equ_name,a.equ_guid,";
                    strEqu = @" left join t_res_equment_relation r on t.guid=r.father_equ_guid left join 
         t_res_equment_all a on r.child_equ_guid=a.equ_guid left join t_res_sys_unit u on a.unit_id=u.unit_id ";
                    break;
                case "含下级所有属性":
                    sql = "select t.child_unit_id from t_res_sys_unit_relation t where t.father_unit_id='" + Unit_Id + "'";
                    string child_unit_id = DataFunction.GetStringResult(sql);
                    sql = string.Format(@"select u.unit_name,u.name_filed,u.table_name,p.filed_name from t_res_sys_property_unit up,T_RES_SYS_PROPERTY p,t_res_sys_unit u 
where up.propery_id=p.propery_id and up.unit_id='{0}' and p.unit_id='{1}' and p.unit_id=u.unit_id", Unit_Id, child_unit_id);
                    DataSet dsE = DataFunction.FillDataSet(sql);
                    if (dsE.Tables[0].Rows.Count > 0)
                    {
                        DataRow drE = dsE.Tables[0].Rows[0];
                        sql = string.Format(@"select p.* from t_res_sys_unit_relation r,T_RES_SYS_PROPERTY p where p.isgridshow='1' and
r.father_unit_id='{0}' and r.child_unit_id=p.unit_id order by p.sequence", Unit_Id);
                        DataSet dsP = DataFunction.FillDataSet(sql);
                        foreach (DataRow drP in dsP.Tables[0].Rows)
                        {
                            strEquColum += "e." + drP["FILED_NAME"].ToString() + "  as CHILD_" + drP["FILED_NAME"].ToString() + ",";
                            if (drE["name_filed"].ToString() == drP["FILED_NAME"].ToString())
                            {
                                strEquColum += "e." + drP["FILED_NAME"].ToString() + "  as equ_name,";
                            }
                        }
                        strEquColum += "e.GUID as equ_guid,'" + child_unit_id + "' as unit_id,'" + drE["unit_name"].ToString() + "' as unit_name,'" + drE["name_filed"] + "' as name_filed,";

                        strEqu = " left join " + drE["table_name"].ToString() + " e on t.guid=" + "e." + drE["FILED_NAME"].ToString() + "_GUID  ";
                    }
                    break;
            }
        }
        DataSet eDs = GetEnumFiledData(Unit_Id);
        string strColum = "";
        string strEnum = "";

        int i = 0;
        foreach (DataRow eDr in eDs.Tables[0].Rows)
        {
            string tb = "E" + i.ToString();
            strColum += tb + ".IMAGE_URL  as " + eDr["filed_name"].ToString() + "_URL,";
            strEnum += "  left join T_RES_SYS_ENUMDATA " + tb + " on t." + eDr["filed_name"].ToString() + "=" + tb + ".ENUM_NAME and " + tb + ".ENUM_SORT='" + eDr["enum_sort"].ToString() + "'";
            i++;
        }
        sql = "select " + strColum + strEquColum + "t.* from " + tableName + " t " + strEnum + strEqu + "  where 1=1 " + strQuery + " " + SequenceFiled;
        DataSet ds = DataFunction.FillDataSet(sql);
        return ds;
    }
    #endregion

    #region 因为光设备里要加光缆段，资源类别里又没有的。所以要手动来添加
    public void CreateResourceGldGrid(GridView ResourceGrid)
    {
        ResourceGrid.Attributes.Add("BorderColor", "#5B9ED1");
        ResourceGrid.Columns.Clear();
        
         CreateGridColum(ResourceGrid, "TemplateField", 5, null, "选择", null);
         CreateGridColum(ResourceGrid, "BoundField", 5, "GLDXH", "光缆段序号", "居中");
         CreateGridColum(ResourceGrid, "BoundField", 5, "GLDCODE", "光缆段编码", "居中");
         CreateGridColum(ResourceGrid, "BoundField", 5, "GLDMC", "光缆段名称", "居中");
         CreateGridColum(ResourceGrid, "BoundField", 5, "GXHCODE", "光缆段纤芯号", "居中");
   
        
        
    }
    #endregion

   
    #region 保存设备总库
    public void SaveT_RES_EQUMENT_ALL(string guid,string name,string unit_id)
    {
        string sql = "delete from T_RES_EQUMENT_ALL where EQU_GUID='"+guid+"'";
        DataFunction.ExecuteNonQuery(sql);
        sql =string.Format( "insert into T_RES_EQUMENT_ALL (EQU_GUID,EQU_NAME,UNIT_ID) values ('{0}','{1}','{2}')",guid,name,unit_id);
        DataFunction.ExecuteNonQuery(sql);
    }
    #endregion

    #region 获取关联字段名称
    public string GetRelationNameFild(string p_unit_id,string unit_id)
    {
        string sql =string.Format( @"select p.filed_name from t_res_sys_property_unit t,T_RES_SYS_PROPERTY p where
 t.unit_id='{0}' and t.propery_id=p.propery_id and p.unit_id='{1}'",p_unit_id, unit_id);
        return DataFunction.GetStringResult(sql);
    }
    #endregion

    #region 保存设备关联
    public void SaveT_RES_EQUMENT_RELATION(System.Web.UI.Page page,string unit_id,string guid)
    {
        string sql = "select * from t_res_sys_property t where t.unit_id='" + unit_id + "' and t.data_type='资源选择'";
        DataSet ds = DataFunction.FillDataSet(sql);
        if (ds.Tables[0].Rows.Count > 0)
        {
            sql = "delete  from t_res_equment_relation t where t.CHILD_EQU_GUID='" + guid + "'";
            DataFunction.ExecuteNonQuery(sql);
            sql = "select * from t_res_equment_relation t where t.CHILD_EQU_GUID='" + guid + "'";
            DataSet dst = DataFunction.FillDataSet(sql);


            foreach (DataRow DR in ds.Tables[0].Rows)
            {
                TextBox txt = (TextBox)page.FindControl(DR["FILED_NAME"].ToString() + "_GUID");
                string[] str = txt.Text.Split(',');
                foreach (string s in str)
                {
                    DataRow drt = dst.Tables[0].NewRow();
                    drt["GUID"] = Guid.NewGuid().ToString();
                    drt["FATHER_EQU_GUID"] =s ;
                    drt["CHILD_EQU_GUID"] = guid;
                    drt["CHILD_EQU_FILED"] = DR["FILED_NAME"].ToString();
                    dst.Tables[0].Rows.Add(drt);
                }
            }
            DataFunction.SaveData(dst, "T_RES_EQUMENT_RELATION");
        }
    }
    #endregion

    public DataSet GetSessionUnitData(string unit_id)
    {
        string sql = "select up.unit_id,p.filed_name from t_res_sys_property p,t_res_sys_property_unit up where p.propery_id=up.propery_id and p.unit_id='" + unit_id + "' and up.unit_id<>'"+unit_id+"'";
        return DataFunction.FillDataSet(sql);
    }

    public DataSet GetListUnitData()
    {
        string sql = "select * from t_res_sys_unit t where t.is_create_table=1 order by t.sequence";
        return DataFunction.FillDataSet(sql);
    }


    [Ajax.AjaxMethod]
    public DataSet getEnumData(string enumSort, string pEnumName)
    {
        string sql = "select * from T_RES_SYS_ENUMDATA where ENUM_SORT='" + enumSort + "' ";
        if (!string.IsNullOrEmpty(pEnumName))
        {
            sql += " and P_ENUM_NAME='" + pEnumName + "'";
        }
        sql += " order by SEQUENCE";
        return DataFunction.FillDataSet(sql);
    }

    [Ajax.AjaxMethod()]
    public string checkSbMc(string sbname, string sbcode, string sblx)
    {
        string TableName = "";
        if (sblx == "网络设备")
        {
            TableName = "T_RES_EQU_NET";
        }
        else if (sblx == "传输设备")
        {
            TableName = "T_RES_EQU_TRANSFERS";
        }
        else if (sblx == "光设备")
        {
            TableName = "T_RES_EQU_LIGHT";
        }
        if (TableName.IsNullOrEmpty())
        {
            return "找不到表";
        }
        int idx = 0;
        idx = DataFunction.GetIntResult("select count(*) from " + TableName + " where equ_code='" + sbcode.Trim() + "'");
        if (idx > 0)
        {
            return "设备编码有重复，请重新输入";
        }
        //idx = DataFunction.GetIntResult("select count(*) from " + TableName + " where equ_name='" + sbname.Trim() + "'");
        //if (idx > 0)
        //{
        //    return "设备名称有重复，请重新输入";
        //}
        return "";

    }

    [Ajax.AjaxMethod]
    public string GetEnumDataShort(string enumSort,string EnumName)
    {
        string sql = "select ENUM_SHORT from T_RES_SYS_ENUMDATA where ENUM_SORT='" + enumSort + "' and ENUM_NAME='" + EnumName + "'";
        return DataFunction.GetStringResult(sql);
    }

    public void SetResourcePort(string strPortGuid)
    {
        string[] strPort = strPortGuid.Split(',');
        foreach (string str in strPort)
        {
            if (!string.IsNullOrEmpty(str))
            {
                string sql =string.Format( "select * from v_business t where t.jdsbdk_guid='{0}' or t.ydsbdk_guid='{0}'",str);
                if (DataFunction.HasRecord(sql))
                {
                    sql =string.Format( "update t_res_child_port t set t.dkzt='启用' where t.guid='{0}'",str);
                }
                else
                {
                    //dsh 9.21
                    //sql = string.Format("update t_res_child_port t set t.dkzt='未启用' where t.guid='{0}' and (t.dkzt<>'损坏' or t.dkzt is null)", str);
                    sql = string.Format("update t_res_child_port t set t.dkzt='未启用' where t.guid='{0}'", str);
                }
                DataFunction.ExecuteNonQuery(sql);
            }
        }
    }

    public void updateIP()
    {
        DataFunction.ExecuteNonQuery(@"update t_logic_equ_ip r set r.ipfpzt=
(select  case when a.ipdz2-a.ipdz1-b.cn<=0 then '已分配' when b.cn>0 then '部分分配' else '未分配' end from 
t_logic_equ_ip a,
(select t.guid,nvl(sum(p.ipdz2-p.ipdz1),0) as cn from t_logic_equ_ip t left join (select distinct ipdz1,ipdz2 from t_logic_equ_ip_pz) p on t.ipdz1<=p.ipdz1 and t.ipdz2>=p.ipdz2  group by t.guid) b
where a.guid=b.guid and a.guid=r.guid)");
    }

}
