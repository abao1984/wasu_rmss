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

public partial class Web_sysUser_UserGroupUpdateAll : System.Web.UI.Page
{
    public string UserGroup = "", GroupCode = "";
    public string strWhere = "", strSessCode = "", strSql = "", strLink = "", strMsg = "";
    public int n = 0;
    classGroup group = new classGroup();
    classUser user = new classUser();
    classLog log = new classLog();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            UserGroup = user.GetGroupCode(strSessCode, out strMsg);
            ShowTree();
        }

        TreeView_Group.Attributes.Add("onclick", "postBackByObject()");

    }

    public void ShowTree()
    {
        TreeView_Group.Nodes.Clear();

        TreeNode node_0 = new TreeNode();
        node_0.ImageUrl = "Images/Small/calendar.gif";
        node_0.Text = Session["ClientName"].ToString().Trim() + "<a herf='#'></a>";
        //node_0.Text = user.GetUserRealName(strSessCode, out strMsg).ToString().Trim() + "[" + strSessCode + "]";
        node_0.Value = "0";

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

            Node.ImageUrl = "Images/Small/group.gif";

            if (Row["IsVisible"].ToString().Trim() != "1")
            {
                Node.Text += " <font color='#ff0000'>[隐藏]</font> ";
            }

            if (Row["IsUse"].ToString().Trim() != "1")
            {
                Node.Text += " <font color='#ff0000'>[停用]</font> ";
            }


            pNode.ChildNodes.Add(Node);

            Node.Expanded = true;

            GroupCode = "'" + Row["GroupCode"].ToString().Trim() + "'";
            if (UserGroup.IndexOf(GroupCode) > -1)
            {
                Node.Checked = true;
            }

            if (pNode != null)
            {
                GroupCode = "'" + Row["GroupCode"].ToString().Trim() + "'";

            }

            AddTree(Row["GroupCode"].ToString(), Node);    //再次递归

        }


    }



    string FunctionString = "";
    public string GetTreeCheck(TreeNode tn)
    {
        foreach (TreeNode treenode in tn.ChildNodes)
        {
            if (treenode.Checked)
            {
                string va = treenode.Value;
                string vb = treenode.Text;

                FunctionString += "," + va + "";
            }
            else
            {
            }
            GetTreeCheck(treenode);//调用自身函数循环遍历TreeView

        }

        return FunctionString;//返回FunctionString字符串

    }

    protected void Button_Update_Group_Click(object sender, EventArgs e)
    {

        string tmpUserName = publ.GetUrlToReceive(Request.QueryString["code"].Trim());
        string tmpUserGroup = GetTreeCheck(TreeView_Group.Nodes[0]);

        string[] paraUserName = tmpUserName.Split(',');
        string[] paraUserGroup = tmpUserGroup.Split(',');

        for (int i = 0; i < paraUserName.Length; i++)
        {
            if (paraUserName[i].ToString().Trim() != "")
            {

                //删除关系表中的该角色记录
                n = user.DelUserGroup(paraUserName[i].ToString().Trim(), out strMsg);

                for (int l = 0; l < paraUserGroup.Length; l++)
                {

                    if (paraUserGroup[l].ToString().Trim() != "")
                    {
                        n = user.InsUserGroup(paraUserName[i].ToString().Trim(), paraUserGroup[l].ToString().Trim(), out strMsg);
                    }
                }
            }
        }

        //写日志文件开始====================================================================
        if (Session["BoolLog"].ToString().Trim() == "1")
        {
            string LogStrMsg = "";
            //LogUserName-人员 LogTitle-标题  LogMemo-内容
            string LogUserName = Session["UserRealName"].ToString().Trim() + "[" + Session["UserName"].ToString().Trim() + "]";
            LogUserName += "  " + Session["BranchName"].ToString().Trim() + "[" + Session["BranchCode"].ToString().Trim() + "]";
            string LogTitle = "批量设置用户对应的角色";
            string LogMemo = "";
            log.Insert(LogUserName, LogTitle, LogMemo, out LogStrMsg);

        }
        //写日志文件结束====================================================================

        Session["Msg"] = "<script>alert('设置成功！');location.replace('UserList.aspx');</script>";
        return;

    }

    //当点击选择框时执行的事件
    protected void TreeView_Group_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        SetChildChecked(e.Node);
        SetParentChecked(e.Node);
    }

    //选择所有的父目录
    private void SetChildChecked(TreeNode parentNode)
    {
        foreach (TreeNode node in parentNode.ChildNodes)
        {
            node.Checked = parentNode.Checked;

            if (node.ChildNodes.Count > 0)
            {
                SetChildChecked(node);
            }
        }
    }

    //选择所有的子目录
    private void SetParentChecked(TreeNode childNode)
    {
        if (childNode.Parent != null)
        {
            if (childNode.Checked)
            {
                childNode.Parent.Checked = true;
            }
            SetParentChecked(childNode.Parent);
        }


    }

}
