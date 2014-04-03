using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.MobileControls;

public partial class Web_GZCL_GZSZ_UserList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Codes();
            TXT_RYBM.Text = Request.QueryString["RYBM"];
            TXT_NAME.Text = Request.QueryString["NAME"];
            TXT_CODE.Text = Request.QueryString["CODE"];
            TXT_Branch.Text = Request.QueryString["Branch"];
            TXT_BMNAME.Text = Request.QueryString["BMNAME"];
            TXT_TYPE.Text = Request.QueryString["type"];
            CreateTreeViewBranch();
            TreeViewBranch.Attributes.Add("onclick", " postBackByObject()");
        }
    }

    private void CreateTreeViewBranch()
    {
        //bool bl = false;
        string strSql=string.Format("select * from t_sys_branch t where t.branchcode like '10010103%' and t.branchcode='{0}'", Session["BranchCode"].ToString());
       
        string sql = "select * from t_sys_branch b where b.pbranchcode='0' and ISUSE='1'  order by DISPLAYORDER";
        if(DataFunction.HasRecord(strSql))
        {
            sql = "select * from t_sys_branch b where b.branchcode = '10010103' and ISUSE='1'  order by DISPLAYORDER";
           // bl = true;
        }
        if (!string.IsNullOrEmpty(Request.QueryString["BranchCode"] ))
        {
            sql = string.Format("select * from t_sys_branch t where t.branchcode = '{0}' and ISUSE='1' order by DISPLAYORDER", Request.QueryString["BranchCode"]);
        }
        //string sql = "select * from t_sys_branch b where b.pbranchcode='0' and ISUSE='1'  order by DISPLAYORDER";
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            //if (dr["BRANCHNAME"].ToString())
            //{
            //}
            TreeNode node = new TreeNode();
            node.ToolTip = "部门";
            node.Value = dr["BRANCHCODE"].ToString();
            node.Text = dr["BRANCHNAME"].ToString();
            node.ImageUrl = "../../Images/Small/map.gif";
            node.SelectAction = TreeNodeSelectAction.Expand;
            TreeViewBranch.Nodes.Add(node);
            CreateChildUser(node);
            CreateChildBranch(node);

        }
    }
    private void CreateChildBranch(TreeNode rootNode)
    {
        string sql = "select * from t_sys_branch b where b.pbranchcode='" + rootNode.Value + "' and ISUSE='1' order by DISPLAYORDER";
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if(rootNode.Text=="客户维护部"&&Request.QueryString["khwhxs"]!="1")
            {
                continue;
            }
            TreeNode node = new TreeNode();
            node.ToolTip = "部门";
            node.Value = dr["BRANCHCODE"].ToString();
            node.Text = dr["BRANCHNAME"].ToString();
            node.SelectAction = TreeNodeSelectAction.Expand;
            node.ImageUrl = "../../Images/Small/branch.gif";
            rootNode.ChildNodes.Add(node);
            CreateChildUser(node);
            CreateChildBranch(node);
        }
    }

    private void CreateChildUser(TreeNode rootNode)
    {
        string sql = string.Format("select t.userrealname,t.id from t_sys_user t where t.branchcode ='{0}'", rootNode.Value);
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (dr["id"].ToString() == "82568c7c-6771-4a32-88ed-4398486f11ba")//超级管理员
            {
                continue;
            }
            if (rootNode.Text == "客户维护部" && Request.QueryString["khwhxs"] != "1")
            {
                continue;
            }
            TreeNode node = new TreeNode();
            node.ToolTip = "用户";
            node.Value = dr["id"].ToString();
            node.Text = dr["userrealname"].ToString();
            node.ImageUrl = "../../Images/Small/emoney.gif";
            node.SelectAction = TreeNodeSelectAction.Expand;
            rootNode.ChildNodes.Add(node);
        }
    }
    protected void TreeViewBranch_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        CreateChildCheck(e.Node);
        if (e.Node.Parent!=null)
        {
            int parentChildCount = e.Node.ChildNodes.Count;
            if (parentChildCount > 1)
            {
                SelNodeCheck(e.Node);
            }
        }
        int i = TreeViewBranch.CheckedNodes.Count;
        if (Request.QueryString["DX"] == "1" && i > 1)
        {
            ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('只能选择一个用户，请重新选择！')</script>");
            return;
        }
        
    }

    private void SelNodeCheck(TreeNode nodes)
    {
        bool bl = true;
        foreach (TreeNode node in nodes.Parent.ChildNodes)
        {
            if(!node.Checked)
            {
                bl = false;
                break;
            }
            //nodes.Parent.ChildNodes
        }
        nodes.Parent.Checked = bl;
        if(nodes.Parent.Parent != null)
        {
            SelNodeCheck(nodes.Parent);
        }
        //return bl;
      
    }

    private void CreateChildCheck(TreeNode nodes)
    {
        foreach(TreeNode node in  nodes.ChildNodes)
        {
            node.Checked = nodes.Checked;
            CreateChildCheck(node);
        }
        
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        string txtName = "";
        string txtCode = "";
        List branch = new List();
        string branchID = "";
        string branchCode = "";
        string branchName = "";
        int Yhnum = 0;
        for (int i = 0; i < TreeViewBranch.CheckedNodes.Count; i++)
        {
            if (Request.QueryString["DX"] == "1" && i > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('只能选择一个用户，请重新选择！')</script>");
                return;
            }
            TreeNode node=TreeViewBranch.CheckedNodes[i];
            if (node.Parent!=null&&node.Parent.Checked)
            {
                continue;
            }
            if (node.ToolTip == "用户" )
            {
                txtName += "" + TreeViewBranch.CheckedNodes[i].Text + ",";
                txtCode += "" + TreeViewBranch.CheckedNodes[i].Value + ",";
                branchID = node.Parent.Value;
                if (!(branchName.IndexOf(node.Text) > -1) && TXT_TYPE.Text != "yj")
                {
                    branchName += node.Parent.Text + ",";
                }
                Yhnum++;
            }
            else
            {
                branchID = node.Value;
                if (!(branchName.IndexOf(node.Text)>-1))
                {
                    branchName += node.Text + ",";
                }
            }
            AddBranch(branchID, branch);
            branchCode = GetBranch(branch);
            if (node.Text=="客户维护部")
            {
                txtName = "客户维护部,";
                txtCode = "客户维护部,";
                break;
            }
        }



        if (txtName.Length > 0)
        {
            txtName = txtName.Substring(0, txtName.Length - 1);
            txtCode = txtCode.Substring(0, txtCode.Length - 1);
            //branchName = branchName.Substring(0, branchName.Length - 1);
        }
        
        if (branchName.Length > 0)
        {
            branchName = branchName.Substring(0, branchName.Length - 1);
        }
        
        if (Request.QueryString["DX"] == "1")
        {
            branchCode = TreeViewBranch.CheckedNodes[0].Parent.Text;
        }
        string str=  string.Format(@"<script> parent.WindowClose();
             parent.document.getElementById('{0}').value = '{1}'; parent.document.getElementById('{2}').value = '{3}'; "
           , TXT_NAME.Text, txtName, TXT_CODE.Text, txtCode);
        if (branchCode != "")
        {
            str += "parent.document.getElementById('" + TXT_Branch.Text + "').value = '" + branchCode + "';";
        }
        if (branchName != "" && TXT_TYPE.Text!="yj")
        {
            str += "parent.document.getElementById('" + TXT_BMNAME.Text + "').value = '" + branchName + "';";
        }
        string rybm = "";
        if (txtName!="")
        {
            rybm = "人员:" + txtName;
        }
        if (txtName!=""&&branchName != "")
        {
            rybm += ";部门:";
        }
        rybm += branchName;
        if(TXT_RYBM.Text!="")
        {
            str += "parent.document.getElementById('" + TXT_RYBM.Text + "').value = '" + rybm + "';";
        }
        str += "</script>";
        this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(),str);
    }

    private void AddBranch(string branchID,List branch)
    {
        foreach (MobileListItem item in branch.Items)
        {
            if (branchID == item.Text)
            {
                break;
            }
        }
        branch.Items.Add(branchID);
    }

    private string GetBranch(List branch)
    {
        string branchs = "";
        foreach (MobileListItem item in branch.Items)
        {
            branchs += "," + item.Text ;
        }
        return branchs;
    }
}
