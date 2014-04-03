using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_FWQYtree : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CreateTreeViewBranch();
            TreeViewBranch.Attributes.Add("onclick", " postBackByObject()");
            foreach (TreeNode node in TreeViewBranch.Nodes)
            {
                if (Session["FWQY"] != null)
                {
                    string[] fwqy = Session["FWQY"].ToString().Split(',');
                    if (fwqy.Contains(node.Value))
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
    private void CreateTreeViewBranch()
    {
        string[] fwqy = publ.GetUrlToReceive(Request.QueryString["fwqy"]).Split(',');
        string sql = "select * from t_sys_branch b where b.pbranchcode='0' and ISUSE='1' and ISQY='1' order by DISPLAYORDER";     
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            TreeNode node = new TreeNode();
            node.Value = dr["BRANCHCODE"].ToString();
            if (fwqy.Contains(node.Value))
            {
                node.Checked = true;
            }
            node.Text = dr["BRANCHNAME"].ToString();
            node.ImageUrl = "../web/Images/Small/map.gif";
            node.SelectAction = TreeNodeSelectAction.Expand;
            TreeViewBranch.Nodes.Add(node);
            CreateChildBranch(node,fwqy);
        }
    }
    private void CreateChildBranch(TreeNode rootNode,string[] fwqy)
    {
        string sql = "select * from t_sys_branch b where b.pbranchcode='" + rootNode.Value + "' and ISUSE='1' and ISQY = '1' order by DISPLAYORDER";
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            TreeNode node = new TreeNode();
            node.Value = dr["BRANCHCODE"].ToString();
            if (fwqy.Contains(node.Value))
            {
                node.Checked = true;
            }
            node.Text = dr["BRANCHNAME"].ToString();
            node.ImageUrl = "../web/Images/Small/map.gif";
            rootNode.ChildNodes.Add(node);
            rootNode.SelectAction = TreeNodeSelectAction.Expand;
            CreateChildBranch(node,fwqy);
        }
    }
    //确定
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        string fwqy = "";
        foreach (TreeNode node in TreeViewBranch.CheckedNodes)
        {
            if (!string.IsNullOrEmpty(fwqy))
            {
                fwqy += ",";
            }
            fwqy +=  node.Value;
        }       
        DataFunction.ExecuteNonQuery(string.Format("update t_sys_User set FWQY = '{0}' where USERNAME in( {1})",fwqy,publ.GetUrlToReceive(Request.QueryString["code"])));
        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('操作成功！');</script>");
    }
   
    protected void TreeViewBranch_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        if (e.Node.Checked)
        {
            uncheckparent(e.Node);
            uncheckchild(e.Node);
        }
    }
    private void uncheckparent(TreeNode node)
    {
        if (node.Parent != null)
        {
            node.Parent.Checked = false;
            uncheckparent(node.Parent);
        }
        else
        {
            return;
        }
    }
    private void uncheckchild(TreeNode node)
    { 
        foreach(TreeNode n in node.ChildNodes)
        {
            n.Checked = false;
            uncheckchild(n);
        }
    }

    private void CheckFWQY(TreeNode node)
    {
        foreach (TreeNode n in node.ChildNodes)
        {
            if (Session["FWQY"] != null)
            {
                string[] fwqy = Session["FWQY"].ToString().Split(',');
                if (fwqy.Contains(n.Value))
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

}
