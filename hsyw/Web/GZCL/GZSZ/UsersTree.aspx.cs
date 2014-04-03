using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_GZSZ_UsersTree : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            
            Codes();
            TXT_NAME.Text = Request.QueryString["NAME"];
            TXT_CODE.Text = Request.QueryString["CODE"];
            CreateTreeViewBranch();
            TreeViewBranch.Attributes.Add("onclick", " postBackByObject()");
        }
    }

    private void CreateTreeViewBranch()
    {
        string sql = string.Format(@"select t.userrealname,h.branchname,t.id from t_sys_user t left join T_SYS_BRANCH h on h.branchcode=t.branchcode where
        t.branchcode in ({0}) order by t.branchcode",bmCode.Text);
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            TreeNode node = new TreeNode();
            node.ToolTip = dr["id"].ToString();
            node.Value = dr["userrealname"].ToString();
            node.Text = dr["userrealname"].ToString()+"      "+dr["branchname"].ToString();
            node.ImageUrl = "../../Images/Small/emoney.gif";
            node.SelectAction = TreeNodeSelectAction.Expand;
            TreeViewBranch.Nodes.Add(node);
        }
    }
    //private void CreateChildBranch(TreeNode rootNode)
    //{
    //    string sql = string.Format(@"select * from t_sys_branch b where b.pbranchcode='{0}' and ISUSE='1' and b.branchcode in ({1}) and ISQY=0 order by DISPLAYORDER", rootNode.ToolTip,bmCode.Text);
    //    DataSet ds = DataFunction.FillDataSet(sql);
    //    foreach (DataRow dr in ds.Tables[0].Rows)
    //    {
    //        TreeNode node = new TreeNode();
    //        node.ToolTip = dr["BRANCHCODE"].ToString();
    //        node.Value = dr["BRANCHNAME"].ToString();
    //        node.Text = dr["BRANCHNAME"].ToString();
    //        node.ImageUrl = "../../Images/Small/emoney.gif";
    //        node.SelectAction = TreeNodeSelectAction.Expand;
    //        rootNode.ChildNodes.Add(node);
    //        CreateChildUser(node);
    //        CreateChildBranch(node);
    //    }
    //}

    //private void CreateChildUser(TreeNode rootNode)
    //{
    //    string sql = string.Format("select t.userrealname,t.id from t_sys_user t where t.branchcode ='{0}'",rootNode.ToolTip);
    //    DataSet ds = DataFunction.FillDataSet(sql);
    //    foreach (DataRow dr in ds.Tables[0].Rows)
    //    {
    //        TreeNode node = new TreeNode();
    //        node.ToolTip = dr["id"].ToString();
    //        node.Value = dr["userrealname"].ToString();
    //        node.Text = dr["userrealname"].ToString();
    //        node.ImageUrl = "../../Images/Small/emoney.gif";
    //        node.SelectAction = TreeNodeSelectAction.Expand;
    //        rootNode.ChildNodes.Add(node);
    //    }
    //}
    protected void TreeViewBranch_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        string dx=Request.QueryString["DX"];
        if(dx=="1")
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
        
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        string txtName = "";
        string txtCode = "";
        for (int i = 0; i < TreeViewBranch.CheckedNodes.Count; i++)
        {
            txtName += "" + TreeViewBranch.CheckedNodes[i].ValuePath + ",";
            txtCode += "" + TreeViewBranch.CheckedNodes[i].ToolTip + ",";
        }
        txtName = txtName.Substring(0, txtName.Length - 1);
        txtCode = txtCode.Substring(0, txtCode.Length - 1);
        this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(),
           string.Format(@"<script> parent.WindowClose();
             parent.document.getElementById('{0}').value = '{1}'; parent.document.getElementById('{2}').value = '{3}'; </script>"
           , TXT_NAME.Text, txtName, TXT_CODE.Text, txtCode));
    }

    private void Codes()
    {
        bmCode.Text = Request.QueryString["bmCode"];
        string codes = "";
        foreach(string code in bmCode.Text.Split(','))
        {
            codes += "'"+code+"',";
        }
        bmCode.Text = codes.Substring(0, codes.Length - 1);
    }
}
