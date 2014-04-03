using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_SelectUsers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillCodes();
            ShowTree();
        }
    }
 
    public void ShowTree()
    {
        DataSet uds = DataFunction.FillDataSet("select * from T_SYS_USER order by displayorder");
        ViewState["uds"] = uds;
        DataSet ds = DataFunction.FillDataSet("select * from T_SYS_BRANCH order by displayorder");
        TreeView1.Nodes.Clear();

        TreeNode node_0 = new TreeNode();
        node_0.ImageUrl = "Images/Small/calendar.gif";
        DataRow[] dr = ds.Tables[0].Select("PBranchCode = '0'");
        node_0.Text = dr[0]["BranchName"].ToString().Trim();
        node_0.Value = dr[0]["BranchCode"].ToString().Trim();
        node_0.Expanded = true;
        this.TreeView1.Nodes.Add(node_0);
        if (BranchCodes.Text.IndexOf(node_0.Value) > -1)
        {
            AddUsers(node_0);
        }
        AddTree(node_0.Value, node_0,ds);

    }

    public void AddTree(String ParentID, TreeNode pNode,DataSet ds)
    {
        DataRow[] dr = ds.Tables[0].Select(" PBranchCode = '" + ParentID + "'");

        foreach (DataRow Row in dr)
        {
            TreeNode Node = new TreeNode();

            //添加当前节点的子节点
            Node.Value = Row["BranchCode"].ToString().Trim();
            Node.Text = Row["BranchName"].ToString().Trim();
            Node.ImageUrl = "Images/Small/branch.gif";
            pNode.ChildNodes.Add(Node);
            Node.Expanded = true;
            if (BranchCodes.Text.IndexOf(Node.Value) > -1)
            {
                AddUsers(Node);
            }
            AddTree(Row["BranchCode"].ToString(), Node,ds);    //再次递归

        }
    }
    private void AddUsers(TreeNode BNode)
    {
        DataSet ds = (DataSet)ViewState["uds"];
        DataRow[] dr = ds.Tables[0].Select("BranchCode = '"+BNode.Value+"'");
        foreach (DataRow Row in dr)
        {
            TreeNode Node = new TreeNode();
            //添加当前节点的子节点
            Node.Value = Row["UserName"].ToString().Trim();
            Node.Text = Row["UserRealName"].ToString().Trim();
            Node.ImageUrl = "Images/Small/0010.gif";
            BNode.ChildNodes.Add(Node);
            Node.ShowCheckBox = true;
            Node.Expanded = true;
            Node.SelectAction = TreeNodeSelectAction.None;
            if (UserNames.Text.IndexOf(Node.Value) > -1)
            {
                Node.Checked = true;
            }
        }
    }
    protected void BtnSure_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["IsDX"].Equals("1") && TreeView1.CheckedNodes.Count > 1)//是单选并且选择的节点数大于1
        {
            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('只能选择一个用户！');</script>");
            return;
        }
        string userrealname = "";
        string username = "";
        foreach (TreeNode node in TreeView1.CheckedNodes)
        {
            if (userrealname.Equals(""))
            {
                userrealname = node.Text;
            }
            else
            {
                userrealname += ","+node.Text;
            }
            if (username.Equals(""))
            {
                username = node.Value;
            }
            else
            {
                username += "," + node.Value;
            }
        }
        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>window.returnValue= new Array('"+userrealname+"','"+username+"');window.close();</script>");
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        TreeNode BNode = TreeView1.SelectedNode;
        BNode.SelectAction = TreeNodeSelectAction.Expand;
        AddUsers(BNode);
    }
    //给UserNames，BranchCodes赋值
    private void FillCodes()
    {
        string usernames = "''";
        string username = Request.QueryString["username"];
        if(!string.IsNullOrEmpty(username))
        {
            UserNames.Text = username;
            string[] un = username.Split(',');
            foreach(string s in un)
            {
                usernames += ",'"+s+"'";
            }
            DataSet ds = DataFunction.FillDataSet(string.Format("select distinct BRANCHCODE from T_SYS_USER where USERNAME in ({0})",usernames));
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                BranchCodes.Text += "," + dr["BRANCHCODE"];
            }
        }
    }
}
