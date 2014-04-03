using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_BranchTree : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            TXT_NAME.Text = Request.QueryString["NAME"];
            TXT_CODE.Text = Request.QueryString["CODE"];
            CreateTreeViewBranch();
            TreeViewBranch.Attributes.Add("onclick", " postBackByObject()");
            
            foreach (TreeNode node in TreeViewBranch.Nodes)
            {
                if (Session["FWQY"] != null)
                {
                    string[] fwqy = Session["FWQY"].ToString().Split(',');
                    if (fwqy.Contains(node.ToolTip))
                    {
                        node.ShowCheckBox = true;
                    }
                    else
                    {
                        node.ShowCheckBox = false;
                    }
                }
                else if (Session["ISSUPER"].ToString() == "")
                {
                    node.ShowCheckBox = false;
                }
                CheckFWQY(node);
            }
         }
        
    }
    private void CheckFWQY(TreeNode node)
    {
        foreach (TreeNode n in node.ChildNodes)
        {
            if (Session["FWQY"] != null)
            {
                string[] fwqy = Session["FWQY"].ToString().Split(',');
                if (fwqy.Contains(n.ToolTip))
                {
                    n.ShowCheckBox = true;
                }
                else
                {
                    n.ShowCheckBox = n.Parent.ShowCheckBox;
                }
            }
            else if (Session["ISSUPER"].ToString() == "")
            {
                n.ShowCheckBox = false;
            }
            CheckFWQY(n);
        }
    }
    private void CreateTreeViewBranch()
    {
        string sql = "select * from t_sys_branch b where b.pbranchcode='0' and ISUSE='1'  order by DISPLAYORDER";
        if (Request.QueryString["ISQY"] != null)
        {
            sql = "select * from t_sys_branch b where b.pbranchcode='0' and ISUSE='1' and ISQY='1' order by DISPLAYORDER";
        }
       
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach(DataRow dr in ds.Tables[0].Rows)
        {
            TreeNode node=new TreeNode();
            node.ToolTip = dr["BRANCHCODE"].ToString();
            node.Value = dr["BRANCHNAME"].ToString();
            node.Text = dr["BRANCHNAME"].ToString();
            if (dr["ISQY"] != DBNull.Value && dr["ISQY"].ToString() == "1")
            {
                node.ImageUrl = "../Images/Small/map.gif";
            }
            else
            {
                node.ImageUrl = "../Images/Small/branch.gif";
            }
            node.SelectAction = TreeNodeSelectAction.Expand;
            TreeViewBranch.Nodes.Add(node);
            CreateChildBranch(node);
        }
    }
    private void CreateChildBranch(TreeNode rootNode)
    {
        string sql = "select * from t_sys_branch b where b.pbranchcode='" + rootNode.ToolTip + "' and ISUSE='1' order by DISPLAYORDER";
        if (Request.QueryString["ISQY"] != null)
        {
            sql = "select * from t_sys_branch b where b.pbranchcode='" + rootNode.ToolTip + "' and ISUSE='1' and ISQY = '1' order by DISPLAYORDER";
        }
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            TreeNode node = new TreeNode();
            node.ToolTip = dr["BRANCHCODE"].ToString();
            node.Value = dr["BRANCHNAME"].ToString();
            node.Text = dr["BRANCHNAME"].ToString();
            if (dr["ISQY"] != DBNull.Value && dr["ISQY"].ToString() == "1")
            {
                node.ImageUrl = "../Images/Small/map.gif";
            }
            else
            {
                node.ImageUrl = "../Images/Small/branch.gif";
            }
            node.SelectAction = TreeNodeSelectAction.Expand;
            rootNode.ChildNodes.Add(node);
            CreateChildBranch(node);
        }
    }
    protected void TreeViewBranch_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        if (e.Node.Checked)
        {
            int n = TreeViewBranch.CheckedNodes.Count;
            for (int i = n - 1; i >= 0; i--)
            {
                if (TreeViewBranch.CheckedNodes[i].Value != e.Node.Value)
                {
                    TreeViewBranch.CheckedNodes[i].Checked = false;
                }
            }
          
        } 
        
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        string txtName = "";
        string txtCode = "";
        if (TreeViewBranch.CheckedNodes.Count > 0)
        {
            txtName= TreeViewBranch.CheckedNodes[0].ValuePath;
            txtCode=TreeViewBranch.CheckedNodes[0].ToolTip;
        }
        this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(),
           string.Format(@"<script> window.close();parent.WindowClose();
             parent.document.getElementById('{0}').value = '{1}'; parent.document.getElementById('{2}').value = '{3}'; </script>"
           , TXT_NAME.Text, txtName, TXT_CODE.Text, txtCode));
    }
}
