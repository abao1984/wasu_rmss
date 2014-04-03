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

public partial class Web_sysBranch_BranchList : System.Web.UI.Page
{
    public string strWhere = "", strSql = "", strLink = "", strMsg = "";
    string BranchCode = "";

    classBranch branch = new classBranch();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Button_Delete.OnClientClick = " return confirm('确认删除所选机构吗');";
            ShowTree();
        }
    }

    public void ShowTree()
    {
        TreeView_Branch.Nodes.Clear();

        DataSet ds = branch.GetRootNodes();
        if (ds.Tables[0].Rows.Count > 0)
        {
            strWhere = "";
            strWhere += " and 1 = 1 ";
            strWhere += " order by b.DisplayOrder asc ";

            this.ViewState["ds"] = DataFunction.FillDataSet(branch.GetQueryStr(strWhere));
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                TreeNode node_0 = new TreeNode();
                this.TreeView_Branch.Nodes.Add(node_0);

                node_0.ImageUrl = "Images/Small/branch.gif";
                node_0.ImageToolTip = "部门";
                node_0.Text = dr["BranchName"].ToString().Trim();
                node_0.Value = dr["BranchName"].ToString().Trim();
                string branchcode = dr["BranchCode"].ToString().Trim();
                node_0.ToolTip = branchcode;
                node_0.SelectAction = TreeNodeSelectAction.Expand;
               // node_0.Expanded = true;           
                string path = node_0.ValuePath;
                path = System.Web.HttpUtility.UrlEncode(path, System.Text.Encoding.GetEncoding("GB2312"));
                node_0.Text += "&nbsp;&nbsp;<a target='_self' href='BranchInsert.aspx?code=" + publ.GetUrlToSend(branchcode) + "&path=" + publ.GetUrlToSend(path) + "'><font color='#0000FF'>增加机构</font></a>";
                
                node_0.Text += "&nbsp;&nbsp;<a target='_self' href='BranchUpdate.aspx?code=" + publ.GetUrlToSend(branchcode) + "&path=" + publ.GetUrlToSend(path) + "'><font color='#0000FF'>编辑</font></a>";
                if (dr["ISQY"] != DBNull.Value && dr["ISQY"].ToString() == "1")
                {
                    node_0.Text += "&nbsp;&nbsp;<font color='brown'>区域</font>";
                    node_0.ImageUrl = "Images/Small/map.gif";
                    node_0.ImageToolTip = "区域";
                }
                else
                {
                    node_0.Text += "&nbsp;&nbsp;<font color='brown'>部门</font>";
                }

                AddTree(branchcode, node_0);          
            }
        }
    }

    public void AddTree(String ParentID, TreeNode pNode)
    {
        DataSet ds = (DataSet)this.ViewState["ds"];
        DataView dvTree = new DataView(ds.Tables[0]);
        dvTree.RowFilter = " PBranchCode = '" + ParentID + "'";

        foreach (DataRowView Row in dvTree)
        {
            TreeNode Node = new TreeNode();
            pNode.ChildNodes.Add(Node);

            //添加当前节点的子节点
            Node.Value = Row["BranchName"].ToString().Trim();
            Node.Text = Row["BranchName"].ToString().Trim() + "<a herf='#'></a>";
            Node.Text += " <font style='font-size: 11px' color='#808080'>[" + Row["BranchCode"].ToString().Trim() + "]</font> ";
            Node.ImageUrl = "Images/Small/branch.gif";
            Node.ImageToolTip = "部门";
            Node.ToolTip = Row["BranchCode"].ToString().Trim();

            string path = Node.ValuePath;
            path = System.Web.HttpUtility.UrlEncode(path, System.Text.Encoding.GetEncoding("GB2312"));
            Node.Text += "&nbsp;&nbsp;<a target='_self' href='BranchInsert.aspx?code=" + publ.GetUrlToSend(Row["BranchCode"].ToString().Trim()) + "&path=" +  publ.GetUrlToSend(path) + "'><font color='#0000FF'>增加子机构</font></a>";

            Node.Text += "&nbsp;&nbsp;<a target='_self' href='BranchUpdate.aspx?code=" + publ.GetUrlToSend(Row["BranchCode"].ToString().Trim()) + "&path=" +  publ.GetUrlToSend(path) + "'><font color='#0000FF'>编辑</font></a>";
            if (Row["IsVisible"].ToString().Trim() != "1")
            {
                Node.Text += " <font color='#ff0000'>[隐藏]</font> ";
            }

            if (Row["IsUse"].ToString().Trim() != "1")
            {
                Node.Text += " <font color='#ff0000'>[停用]</font> ";
            }

          
            if (Row["ISQY"] != DBNull.Value && Row["ISQY"].ToString() == "1")
            {
                Node.Text += "&nbsp;&nbsp;<font color='brown'>区域</font>";
                Node.ImageUrl = "Images/Small/map.gif";
                Node.ImageToolTip = "区域";
            }
            else
            {
                Node.Text += "&nbsp;&nbsp;<font color='brown'>部门</font>";
            }
           // Node.Expanded = true;
            AddTree(Row["BranchCode"].ToString(), Node);    //再次递归

        }
    }
    protected void Button_Delete_Click(object sender, EventArgs e)
    {
        string str = IsHaveCZY();
        if (str != "")
        {
            ClientScript.RegisterStartupScript(this.GetType(),Guid.NewGuid().ToString(), "<script>alert('"+str+"');</script>");
        }
        else
        {
            string MenuCodeAll = GetTreeCheck();
            string[] ss = MenuCodeAll.Split(',');

            if (MenuCodeAll == "")
            {
                Session["Msg"] = "<script>alert('未选择机构，删除失败！');</script>";
                return;
            }
            else
            {
                for (int i = 1; i < ss.Length; i++)
                {
                    branch.Delete(ss[i].ToString().Trim(), out strMsg);
                }
            }

            ShowTree();
            Session["Msg"] = "<script>alert('操作成功！(若某些机构下有人员,则不能删除)');</script>";
            return;
        }
    }

    /* 得到所选择的节点
     * 调用方法 string MenuCodeAll = GetTreeCheck(TreeView_Branch.Nodes[0]);
     * 
     */
    string FunctionString = "";
    public string GetTreeCheck()
    {
        foreach (TreeNode treenode in TreeView_Branch.CheckedNodes)
        {
            string code = treenode.ToolTip;
            
            FunctionString += ", " + code + "";
        }
        return FunctionString;//返回FunctionString字符串
    }
    //=============================================================================


    protected void BtnNewRoot_Click(object sender, EventArgs e)
    {
        Response.Redirect("BranchInsert.aspx?code="+publ.GetUrlToSend("root"), true);
    }
    //是否有操作员，返回有用户的部门的警告语句
    private string IsHaveCZY()
    {
        string str = "";
        string sql1 = "";
        if (Session["FWQY"] != null)
        {
            string[] fwqy = Session["FWQY"].ToString().Split(',');
            foreach (string fwqy1 in fwqy)
            {

                if (sql1 != "")
                {
                    sql1 += " or BRANCHCODE like '" + fwqy1 + "%'";
                }
                else
                {
                    sql1 += "BRANCHCODE like '" + fwqy1 + "%'";
                }
            }
        }
        else
        {
            sql1 = " 1=1";
        }
        foreach (TreeNode node in TreeView_Branch.CheckedNodes)
        {
            if (!DataFunction.HasRecord(string.Format("select ID from t_sys_branch where BRANCHCODE = '{0}' and ({1})", node.ToolTip,sql1)))
            {
                str += node.Value + "不是访问区域不能删除\\n";
            }
            if (DataFunction.HasRecord(string.Format("select ID from t_sys_user where BRANCHCODE = '{0}'", node.ToolTip)))
            {
                str += node.Value+"下有用户不能删除\\n";
            }
            
        }
        return str;
    }
}
