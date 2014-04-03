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

public partial class MenuLeft : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        //用户是否登陆较验菜单
        if (Session["UserName"] == null || Session["UserName"].ToString().Trim().Length == 0)
        {
            TreeView_Menu.Visible = false;
            return;
        }

        //UserMenu = Session["GroupMenu"].ToString().Trim() + ", " + Session["UserMenu"].ToString().Trim();

        if (!IsPostBack)
        {
            
            AdminMenuTreeShow();

        }

    }
 

    public void AdminMenuTreeShow()
    {

        TreeNode nodeRoot = new TreeNode();
        nodeRoot.ImageUrl = "Images/Small/outbox.gif";
        nodeRoot.Text = "系统环境设置";
        nodeRoot.SelectAction = TreeNodeSelectAction.Expand;
        this.TreeView_Menu.Nodes.Add(nodeRoot);
        this.TreeView_Menu.Target = "MainFrame";
        nodeRoot.Expanded = true;

        if (Session["UserGroup"].ToString().Trim() == "0")
        {

            TreeNode nodeMenu = new TreeNode();
            nodeMenu.ImageUrl = "Images/Small/phiz.gif";
            nodeMenu.Text = "菜单管理";
            nodeMenu.SelectAction = TreeNodeSelectAction.Expand;
            nodeMenu.NavigateUrl = "MenuList.aspx"+"?SJGGG="+Session["SJGGG"].ToString().Trim();
            nodeRoot.ChildNodes.Add(nodeMenu);

            TreeNode nodeGroup = new TreeNode();
            nodeGroup.ImageUrl = "Images/Small/phiz.gif";
            nodeGroup.Text = "角色管理";
            nodeGroup.SelectAction = TreeNodeSelectAction.Expand;
            nodeGroup.NavigateUrl = "GroupList.aspx" + "?SJGGG=" + Session["SJGGG"].ToString().Trim();
            nodeRoot.ChildNodes.Add(nodeGroup);

            TreeNode nodeAdmin = new TreeNode();
            nodeAdmin.ImageUrl = "Images/Small/phiz.gif";
            nodeAdmin.Text = "管 理 员";
            nodeAdmin.SelectAction = TreeNodeSelectAction.Expand;
            nodeAdmin.NavigateUrl = "AdminList.aspx" + "?SJGGG=" + Session["SJGGG"].ToString().Trim();
            nodeRoot.ChildNodes.Add(nodeAdmin);

            TreeNode nodeData = new TreeNode();
            nodeData.ImageUrl = "Images/Small/phiz.gif";
            nodeData.Text = "数据字典";
            nodeData.SelectAction = TreeNodeSelectAction.Expand;
            nodeData.NavigateUrl = "DataList.aspx" + "?SJGGG=" + Session["SJGGG"].ToString().Trim();
            nodeRoot.ChildNodes.Add(nodeData);


            TreeNode nodeLog = new TreeNode();
            nodeLog.ImageUrl = "Images/Small/phiz.gif";
            nodeLog.Text = "系统日志";
            nodeLog.SelectAction = TreeNodeSelectAction.Expand;
            nodeLog.NavigateUrl = "LogList.aspx" + "?SJGGG=" + Session["SJGGG"].ToString().Trim();
            nodeRoot.ChildNodes.Add(nodeLog);
        }

        TreeNode nodeBranch = new TreeNode();
        nodeBranch.ImageUrl = "Images/Small/phiz.gif";
        nodeBranch.Text = "机构管理";
        nodeBranch.SelectAction = TreeNodeSelectAction.Expand;
        nodeBranch.NavigateUrl = "BranchList.aspx" + "?SJGGG=" + Session["SJGGG"].ToString().Trim();
        nodeRoot.ChildNodes.Add(nodeBranch);

        TreeNode nodeUser = new TreeNode();
        nodeUser.ImageUrl = "Images/Small/phiz.gif";
        nodeUser.Text = "用户管理";
        nodeUser.SelectAction = TreeNodeSelectAction.Expand;
        nodeUser.NavigateUrl = "UserList.aspx" + "?SJGGG=" + Session["SJGGG"].ToString().Trim();
        nodeRoot.ChildNodes.Add(nodeUser);

        TreeNode nodeDataBak = new TreeNode();
        nodeDataBak.ImageUrl = "Images/Small/phiz.gif";
        nodeDataBak.Text = "数据备份";
        nodeDataBak.SelectAction = TreeNodeSelectAction.Expand;
        nodeDataBak.NavigateUrl = "DataBak.aspx" + "?SJGGG=" + Session["SJGGG"].ToString().Trim();
        nodeRoot.ChildNodes.Add(nodeDataBak);

    }
}
