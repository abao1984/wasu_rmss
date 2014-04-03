using System;
using System.Collections;
using System.Configuration;
using System.Data;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;

using System.Data.Common;
//using yueue.ADOKeycap;

public partial class MainLeft3 : BasePage
{
    string MenuCode = "", strWhere = "";

  
    classMenu menu = new classMenu();

    protected void Page_Load(object sender, EventArgs e)
    {


        //用户是否登陆较验菜单
        //if (Session["UserName"] == null || Session["UserName"].ToString().Trim().Length == 0)
        //{
        //    TreeView1.Visible = false;
        //    return;
        //}
        if (!IsPostBack)
        {

             // UserMenuTreeShow();
        }

    }


 

    public void UserMenuTreeShow()
    {

        //TreeNode nodeRoot = new TreeNode();

        //nodeRoot.Text ="";
        //nodeRoot.Value ="01";// Session["ClientCode"].ToString().Trim();
        
        ////nodeRoot.Value = "0";

        //nodeRoot.SelectAction = TreeNodeSelectAction.Expand;
        //nodeRoot.Expanded = true;
        ////nodeRoot.ImageUrl = "Images/Small/calendar.gif";

        //this.TreeView1.Nodes.Add(nodeRoot);
        //this.TreeView1.Target = "tabWin";

        ////以下为子树，如果数据量过大，可以注释不用=============================================================================================================================

        strWhere = "";
        strWhere += " and m.IsUse = '1' and m.IsVisible = '1'  ";
        strWhere += " order by m.DisplayOrder asc ";

        this.ViewState["ds"] = DataFunction.FillDataSet(menu.GetQueryStr(strWhere));
        //AddTree(nodeRoot.Value.ToString().Trim(), nodeRoot);
        string pMenuCode = Request.QueryString["PMENUCODE"];
        if (string.IsNullOrEmpty(pMenuCode))
        {
            pMenuCode = "02";
        }


 DataSet ds = (DataSet)this.ViewState["ds"];
        DataView dvTree = new DataView(ds.Tables[0]);
        dvTree.RowFilter = " MenuCode = '" + pMenuCode + "'";
        LeftTop.Text = dvTree[0]["MENUNAME"].ToString();
        dvTree.RowFilter = " PMenuCode = '" + pMenuCode+ "'";

        foreach (DataRowView Row in dvTree)
        {
            TreeNode Node = new TreeNode();


            //添加当前节点的子节点
            Node.Value = Row["MenuCode"].ToString().Trim();
            Node.Text = Row["MenuName"].ToString().Trim();

            if (Row["Ico"].ToString().Trim() == "")
            {
                Node.ImageUrl = "Images/Small/notes.gif";
            }
            else
            {
                Node.ImageUrl = "Images/Small/" + Row["Ico"].ToString().Trim();
            }

            Node.SelectAction = TreeNodeSelectAction.Expand;

            //展开还是关闭==================================================================
            if (Row["IsExpand"].ToString().Trim() == "1")
            {
                Node.Expanded = true;
            }
            else
            {
                Node.Expanded = false;
            }
            //=============================================================================


            //所要执行的文件===============================================================
            if (Row["FileName"].ToString().Trim() == "")
            {
            }
            else
            {
                string url = Row["FileName"].ToString().Trim();
                if (url.IndexOf('?') > -1)
                {
                    url += "&SJGGG=" + Session["SJGGG"].ToString().Trim();
                }
                else
                {
                    url += "?SJGGG=" + Session["SJGGG"].ToString().Trim();
                }
                Node.NavigateUrl = url;
                Node.Target = "tabWin";
            }
            //=============================================================================



            MenuCode = "'" + Row["MenuCode"].ToString().Trim() + "'";

            if (Session["GroupMenu"].ToString().Trim().IndexOf(MenuCode) > -1 || Session["ISSUPER"].ToString()=="1")
            {
                TreeView1.Nodes.Add(Node);
            }

            AddTree(Row["MenuCode"].ToString(), Node);    //再次递归
        }
    }

    public string GetDBGZ(string MenuName)
    {
        if (MenuName == "待办故障")
        {
            string userID = Session["UserID"].ToString();
             
            string str = string.Format("select Count(*) from t_fau_zb t where (t.gzyyrid='{0}' or t.yjrcode like '%{0}%' or t.csrid like '%{0}%') and t.gzzt<>'结单' and t.fdzzt is null ", userID);
            //可访问区域
            if (Session["ISSUPER"].ToString() != "1")
            {
                if (Session["FWQY"] == null)
                {
                    str += " and 1<>1 ";
                }
                else
                {
                    string[] fwqy = Session["FWQY"].ToString().Split(',');
                    string strQy = "";
                    foreach (string qy in fwqy)
                    {
                        if (strQy != "")
                        {
                            strQy += " or ";
                        }
                        strQy += " t.KHQYID like '" + qy + "%' ";
                    }
                    if (!string.IsNullOrEmpty(strQy))
                    {
                        str += " and (" + strQy + ") ";
                    }
                }
            }
            return "【"+DataFunction.GetIntResult(str).ToString()+"】";
        }
        return "";
    }
    public void AddTree(String ParentID, TreeNode pNode)
    {
        DataSet ds = (DataSet)this.ViewState["ds"];
        DataView dvTree = new DataView(ds.Tables[0]);
        dvTree.RowFilter = " PMenuCode = '" + ParentID + "'";

        foreach (DataRowView Row in dvTree)
        {
            TreeNode Node = new TreeNode();


            //添加当前节点的子节点
            Node.Value = Row["MenuCode"].ToString().Trim();
            Node.Text = Row["MenuName"].ToString().Trim() + GetDBGZ(Row["MenuName"].ToString().Trim());

            if (Row["Ico"].ToString().Trim() == "")
            {
                Node.ImageUrl = "Images/Small/notes.gif";
            }
            else
            {
                Node.ImageUrl = "Images/Small/" + Row["Ico"].ToString().Trim();
            }

            Node.SelectAction = TreeNodeSelectAction.Expand;

            //展开还是关闭==================================================================
            if (Row["IsExpand"].ToString().Trim() == "1")
            {
                Node.Expanded = true;
            }
            else
            {
                Node.Expanded = false;
            }
            //=============================================================================


            //所要执行的文件===============================================================
            if (Row["FileName"].ToString().Trim() == "")
            {
            }
            else
            {
                string url = Row["FileName"].ToString().Trim();
                if (url.IndexOf('?') > -1)
                {
                    url += "&SJGGG=" + Session["SJGGG"].ToString().Trim();
                }
                else
                {
                    url += "?SJGGG="+Session["SJGGG"].ToString().Trim();
                }
                Node.NavigateUrl = url;
                Node.Target = "tabWin";
            }
            //=============================================================================



            MenuCode = "'" + Row["MenuCode"].ToString().Trim() + "'";

            if (Session["GroupMenu"].ToString().Trim().IndexOf(MenuCode) > -1 || Session["ISSUPER"].ToString() == "1")
            {
                pNode.ChildNodes.Add(Node);
            }
           

            if (pNode != null)
            {
                MenuCode = "'" + Row["MenuCode"].ToString().Trim() + "'";
            }

            AddTree(Row["MenuCode"].ToString(), Node);    //再次递归

        }
        

               
    }

}
