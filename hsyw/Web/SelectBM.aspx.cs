using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_SelectBM : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["BCODES"] != null)
            {
                BranchCodes.Text = Request.QueryString["BCODES"];
            }
            ShowTree();
        }
    }
    public void ShowTree()
    {
        DataSet ds = DataFunction.FillDataSet("select * from T_SYS_BRANCH order by displayorder");
        TreeView1.Nodes.Clear();

        TreeNode node_0 = new TreeNode();
        node_0.ImageUrl = "Images/Small/calendar.gif";
        DataRow[] dr = ds.Tables[0].Select("PBranchCode = '0'");
        node_0.Text = dr[0]["BranchName"].ToString().Trim();
        node_0.Value = dr[0]["BranchCode"].ToString().Trim();
        node_0.Expanded = true;
        node_0.ShowCheckBox = true;
        if (BranchCodes.Text.IndexOf(node_0.Value) > -1)
        {
            node_0.Checked = true;
        }
        this.TreeView1.Nodes.Add(node_0);
        AddTree(node_0.Value, node_0, ds);

    }

    public void AddTree(String ParentID, TreeNode pNode, DataSet ds)
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
            Node.ShowCheckBox = true;
            if (BranchCodes.Text.IndexOf(Node.Value) > -1)
            {
                Node.Checked = true;
            }
            AddTree(Row["BranchCode"].ToString(), Node, ds);    //再次递归

        }
    }
    protected void BtnSure_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["IsDX"].Equals("1") && TreeView1.CheckedNodes.Count > 1)//是单选并且选择的节点数大于1
        {
            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('只能选择一个部门！');</script>");
            return;
        }
        string bmname = "";
        string bmcode = "";
        foreach (TreeNode node in TreeView1.CheckedNodes)
        {
            if (bmname.Equals(""))
            {
                bmname = node.Text;
            }
            else
            {
                bmname += "," + node.Text;
            }
            if (bmcode.Equals(""))
            {
                bmcode = node.Value;
            }
            else
            {
                bmcode += "," + node.Value;
            }
        }
        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>window.returnValue= new Array('" + bmname + "','" + bmcode + "');window.close();</script>");
    }
}
