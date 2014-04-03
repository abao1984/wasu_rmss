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

public partial class Web_sysGroup_GroupList : System.Web.UI.Page
{
    public string strWhere = "", strSql = "", strLink = "", strMsg = "";
    string GroupCode = "";

    classGroup group = new classGroup();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowTree();
        }
    }

    public void ShowTree()
    {
        TreeView_Group.Nodes.Clear();

        TreeNode node_0 = new TreeNode();
        node_0.ImageUrl = "Images/Small/calendar.gif";
        node_0.Text = Session["ClientName"].ToString().Trim() + "<a herf='#'></a>";
        //node_0.Value = Session["ClientCode"].ToString().Trim();
        node_0.Value = "0";

        node_0.Text += "&nbsp;&nbsp;<a target='_self' href='GroupInsert.aspx?code=" + publ.GetUrlToSend(node_0.Value.ToString().Trim()) + "'><font color='#0000FF'>增加角色</font></a>";

        node_0.SelectAction = TreeNodeSelectAction.Expand;
        node_0.Expanded = true;
        this.TreeView_Group.Nodes.Add(node_0);

        strWhere = "";
        strWhere += " and 1 = 1 ";
        strWhere += " order by g.DisplayOrder asc ";

        this.ViewState["ds"] = DataFunction.FillDataSet(group.GetQueryStr(strWhere));

        AddTree(node_0.Value, node_0);

    }

    public void AddTree(String ParentID, TreeNode pNode)
    {
        DataSet ds = (DataSet)this.ViewState["ds"];
        DataView dvTree = new DataView(ds.Tables[0]);
        dvTree.RowFilter = " PGroupCode = '" + ParentID + "'";

        foreach (DataRowView Row in dvTree)
        {
            TreeNode Node = new TreeNode();

            //添加当前节点的子节点
            Node.Value = Row["GroupCode"].ToString().Trim();
            Node.Text = Row["GroupName"].ToString().Trim() + "<a herf='#'></a>";
            Node.Text += " <font style='font-size: 11px' color='#808080'>[" + Node.Value.ToString().Trim() + "]</font> ";

            if (Row["IsVisible"].ToString().Trim() != "1")
            {
                Node.Text += " <font color='#ff0000'>[隐藏]</font> ";
            }

            if (Row["IsUse"].ToString().Trim() != "1")
            {
                Node.Text += " <font color='#ff0000'>[停用]</font> ";
            }

            Node.ImageUrl = "Images/Small/group.gif";

            Node.Text += "&nbsp;&nbsp;<a target='_self' href='GroupInsert.aspx?code=" + publ.GetUrlToSend(Node.Value.ToString().Trim()) + "'><font color='#0000FF'>增加子角色</font></a>";
            Node.Text += "&nbsp;&nbsp;<a target='_self' href='GroupUpdate.aspx?code=" + publ.GetUrlToSend(Node.Value.ToString().Trim()) + "'><font color='#0000FF'>编辑</font></a>";
            Node.Text += "&nbsp;&nbsp;<a target='_self' href='GroupMenu.aspx?code=" + publ.GetUrlToSend(Node.Value.ToString().Trim()) + "'><font color='#0000FF'>菜单</font></a>";
            Node.Text += "&nbsp;&nbsp;<a href='javascript:OpenUsers(\"" + Row["GroupCode"].ToString().Trim() + "\")'><font color='#0000FF'>用户</font></a>";
            if (Row["GROUPMS"] != DBNull.Value)
            {
                Node.Text += "&nbsp;&nbsp;描述：" + Row["GROUPMS"].ToString();
            }
            pNode.ChildNodes.Add(Node);
            Node.Expanded = true;


            if (pNode != null)
            {
                GroupCode = "'" + Row["GroupCode"].ToString().Trim() + "'";

            }

            AddTree(Row["GroupCode"].ToString(), Node);    //再次递归

        }
    }
    protected void Button_Delete_Click(object sender, EventArgs e)
    {
        string MenuCodeAll = GetTreeCheck(TreeView_Group.Nodes[0]);
        string[] ss = MenuCodeAll.Split(',');
        for (int i = 1; i < ss.Length; i++)
        {
            group.Delete(ss[i].ToString().Trim());
        }
        ShowTree();
        ClientScript.RegisterStartupScript(this.GetType(),Guid.NewGuid().ToString(), "<script>alert('操作成功！(某些角色若被用户使用,则无法删除!)');</script>");
        
    }

    /* 得到所选择的节点
     * 调用方法 string MenuCodeAll = GetTreeCheck(TreeView_Group.Nodes[0]);
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
            else
            {
            }

            GetTreeCheck(treenode);//调用自身函数循环遍历TreeView

        }
        return FunctionString;//返回FunctionString字符串
    }
    //=============================================================================


}
