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

public partial class Web_sysMenu_MenuList : System.Web.UI.Page
{
    public string strWhere = "", strSql = "", strLink = "", strMsg = "";
    string MenuCode = "";

    classMenu menu = new classMenu();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Button_Delete.OnClientClick = " return confirm('确认删除所选菜单吗');";

            ShowTree();
        }
    }

    public void ShowTree()
    {
        TreeView_Menu.Nodes.Clear();

        TreeNode node_0 = new TreeNode();
        node_0.ImageUrl = "Images/Small/calendar.gif";
        node_0.Text = Session["ClientName"].ToString().Trim() + "<a herf='#'></a>";
        node_0.Value = "0";

        node_0.Text += "&nbsp;&nbsp;<a target='_self' href='MenuInsert.aspx?code=" + publ.GetUrlToSend(node_0.Value.ToString().Trim()) + "'><font color='#0000FF'>增加子菜单</font></a>";

        node_0.SelectAction = TreeNodeSelectAction.Expand;
        node_0.Expanded = true;
        this.TreeView_Menu.Nodes.Add(node_0);

        strWhere = "";
        strWhere += " and 1 = 1 ";
        strWhere += " order by m.DisplayOrder asc ";

        this.ViewState["ds"] = DataFunction.FillDataSet(menu.GetQueryStr(strWhere));

        AddTree(node_0.Value, node_0);

    }

    public void AddTree(String ParentID, TreeNode pNode)
    {
        DataSet ds = (DataSet)this.ViewState["ds"];
        DataView dvTree = new DataView(ds.Tables[0]);
        dvTree.RowFilter = " PMenuCode = '" + ParentID + "'";

        if (dvTree.Table.Select(" PMenuCode = '" + ParentID + "'").Length > 0)
        {
            foreach (DataRowView Row in dvTree)
            {
                TreeNode Node = new TreeNode();


                Node.Value = Row["MenuCode"].ToString().Trim();
                Node.Text = Row["MenuName"].ToString().Trim() + "<a herf='#'></a>";
                Node.Text += " <font style='font-size: 11px' color='#808080'>[" + Node.Value.ToString().Trim() + "]</font> ";

                string qx = "";
                if (Row["FileName"].ToString().Trim().Length != 0)
                {
                    Node.Text += "<font style='font-size: 11px' color='#808080'>[" + Row["FileName"].ToString().Trim() + "]</font> ";
                    qx = "增加权限";
                }

                if (Row["IsVisible"].ToString().Trim() != "1")
                {
                    Node.Text += " <font color='#ff0000'>[隐藏]</font> ";
                }

                if (Row["IsUse"].ToString().Trim() != "1")
                {
                    Node.Text += " <font color='#ff0000'>[停用]</font> ";
                }
               
                Node.Text += "&nbsp;&nbsp;<a target='_self' href='MenuInsert.aspx?code=" + publ.GetUrlToSend(Node.Value.ToString().Trim()) + "'><font color='#0000FF'>增加子菜单</font></a>";

                Node.Text += "&nbsp;&nbsp;<a target='_self' href='MenuUpdate.aspx?code=" + publ.GetUrlToSend(Node.Value.ToString().Trim()) + "'><font color='#0000FF'>编辑</font></a>";
                if (!qx.Equals(""))
                {
                    Node.Text += "&nbsp;&nbsp;<a target='_self' href='MenuPrivate.aspx?code=" + publ.GetUrlToSend(Node.Value.ToString().Trim()) + "'><font color='#0000FF'>" + qx + "</font></a>";
                }

                pNode.ChildNodes.Add(Node);
                Node.Expanded = true;
                if (Row["Ico"].ToString().Trim() == "")
                {
                    Node.ImageUrl = "Images/Small/notes.gif";
                }
                else
                {
                    Node.ImageUrl = "Images/Small/" + Row["Ico"].ToString().Trim();
                }

                if (pNode != null)
                {
                    MenuCode = "'" + Row["MenuCode"].ToString().Trim() + "'";
                }

                AddTree(Row["MenuCode"].ToString(), Node);    //再次递归

            }
        }
        else
        {
            DataSet ds1 = menu.GetMenuPrivte(ParentID);
            foreach (DataRow dr in ds1.Tables[0].Rows)
            {
                TreeNode Node = new TreeNode();

                Node.Value = dr["PCode"].ToString().Trim();
                Node.Text = dr["PName"].ToString().Trim() + "<a herf='#'></a>";
                Node.Text += " <font style='font-size: 11px' color='#808080'>[" + Node.Value.ToString().Trim() + "]</font> ";
                Node.Text += "&nbsp;&nbsp;<a target='_self' href='MenuPrivate.aspx?pcode=" + publ.GetUrlToSend(Node.Value.ToString().Trim()) + "'><font color='#0000FF'>编辑</font></a>";
                Node.ImageUrl = "Images/Small/journal.gif";
                Node.ImageToolTip = "private";
                Node.ShowCheckBox = true;
                pNode.ChildNodes.Add(Node);
            }
        }
    }

    protected void Button_Delete_Click(object sender, EventArgs e)
    {
        string strmenu = "";
        string strprivate = "";
        if (TreeView_Menu.CheckedNodes.Count > 0)
        {
            foreach (TreeNode node in TreeView_Menu.CheckedNodes)
            {
                if (node.ImageToolTip.Equals("private"))
                {
                    if (!strprivate.Equals(""))
                    {
                        strprivate += ",";
                    }
                    strprivate += "'" + node.Value + "'";
                }
                else
                {
                    if (!strmenu.Equals(""))
                    {
                        strmenu += ",";
                    }
                    strmenu += "'" + node.Value + "'";
                }
            }
            menu.Delete(strmenu, strprivate);
            ShowTree();
            Session["Msg"] = "<script>alert('操作已提交。某些菜单若被角色使用，则不能被删除！');</script>";
        }
        else
        {
            Session["Msg"] = "<script>alert('未选择菜单，删除失败！');</script>";
        }
    }

    /* 得到所选择的节点=========================================================
     * 调用方法 string MenuCodeAll = GetTreeCheck(TreeView_Menu.Nodes[0]);
     * 
     */
    string FunctionString = "";
    public string GetTreeCheck(TreeNode tn)
    {
        foreach (TreeNode treenode in tn.ChildNodes)
        {
            if (treenode.Checked)
            {
                string va = treenode.Value;
                string vb = treenode.Text;
                FunctionString += ", " + va + "";
            }


            GetTreeCheck(treenode);//调用自身函数循环遍历TreeView

        }
        return FunctionString;//返回FunctionString字符串
    }
    //=============================================================================


}
