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

public partial class Web_sysGroup_GroupMenu : System.Web.UI.Page
{
    public string MenuCode = "";
    public string strWhere = "", strSessCode = "", strSql = "", strLink = "", strMsg = "";
    public int n = 0;

    classGroup group = new classGroup();
    classMenu menu = new classMenu();
    classLog log = new classLog();

    protected void Page_Load(object sender, EventArgs e)
    {

        strSessCode = publ.GetUrlToReceive(Request.QueryString["code"].ToString().Trim());

        if (!IsPostBack)
        {         
            ShowTree();
        }

        TreeView_Menu.Attributes.Add("onclick", "postBackByObject()");

    }

    public void ShowTree()
    {
        TreeView_Menu.Nodes.Clear();

        TreeNode node_0 = new TreeNode();
        node_0.ImageUrl = "Images/Small/calendar.gif";
        //node_0.Text = Session["ClientName"].ToString().Trim() + "<a herf='#'></a>";
        node_0.Text = group.GetGroupName(strSessCode, out strMsg).ToString().Trim();
        node_0.Value = "0";

        node_0.SelectAction = TreeNodeSelectAction.Expand;
        node_0.Expanded = true;
        this.TreeView_Menu.Nodes.Add(node_0);

        strWhere = "";
        strWhere += " and 1 = 1 ";
        strWhere += " order by m.DisplayOrder asc ";

        this.ViewState["ds"] = DataFunction.FillDataSet(menu.GetQueryStr(strWhere));
        this.ViewState["ds1"] = DataFunction.FillDataSet("select * from T_SYS_PRIVATE order by PCODE");
        AddTree(node_0.Value, node_0);

    }

    public void AddTree(String ParentID, TreeNode pNode)
    {
        DataSet ds = (DataSet)this.ViewState["ds"];
        DataView dvTree = new DataView(ds.Tables[0]);
        dvTree.RowFilter = " PMenuCode = '" + ParentID + "'";

        foreach (DataRowView Row in dvTree)
        {
            TreeNode Node = new TreeNode();
            //添加当前节点的子节点
            Node.Value = Row["MenuCode"].ToString().Trim();
            Node.Text = Row["MenuName"].ToString().Trim() + "<a herf='#'></a>";
            Node.Text += " <font style='font-size: 11px' color='#808080'>[" + Node.Value.ToString().Trim() + "]</font> ";


            if (Row["FileName"].ToString().Trim().Length != 0)
            {
                Node.Text += "<font style='font-size: 11px' color='#0000ff'>" + Row["FileName"].ToString().Trim() + "</font> ";
            }

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
            if (Row["Ico"].ToString().Trim() == "")
            {
                Node.ImageUrl = "Images/Small/notes.gif";
            }
            else
            {
                Node.ImageUrl = "Images/Small/" + Row["Ico"].ToString().Trim();
            }


            MenuCode = "'" + Row["MenuCode"].ToString().Trim() + "'";
            if (group.IsHaveMenu(strSessCode, Row["MenuCode"].ToString()))
            {
                Node.Checked = true;
            }

            if (pNode != null)
            {
                MenuCode = "'" + Row["MenuCode"].ToString().Trim() + "'";

            }
            AddPrivate(Row["MenuCode"].ToString(), Node);

            AddTree(Row["MenuCode"].ToString(), Node);    //再次递归

        }


    }
    //添加权限节点
    private void AddPrivate(string menucode,TreeNode menunode)
    {
        DataView dv = (this.ViewState["ds1"] as DataSet).Tables[0].DefaultView;
        dv.RowFilter = "MENUCODE = '" + menucode + "'";
        foreach (DataRowView row in dv)
        {
            TreeNode node = new TreeNode();
            node.Value = row["PCODE"].ToString();
            node.Text = row["PNAME"].ToString() + "<a herf='#'></a>";
            node.Text += " <font style='font-size: 11px' color='#808080'>[" + node.Value.ToString().Trim() + "]</font> ";
            if (group.IsHavePrivate(strSessCode, row["PCode"].ToString()))
            {
                node.Checked = true;
            }
            menunode.ChildNodes.Add(node);
            node.Expanded = true;
            node.ImageUrl = "Images/Small/journal.gif";
            node.ImageToolTip = "private";
            node.ShowCheckBox = true;
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

    protected void Button_Update_Menu_Click(object sender, EventArgs e)
    {
        string tmpGroupCode = strSessCode;

        //删除关系表中的该角色记录
        n = group.DelGroupMenu(tmpGroupCode, out strMsg);
        group.DelGroupPrivate(tmpGroupCode);
        DataSet mds = DataFunction.FillDataSet(string.Format("select * from T_SYS_R_GROUPMENU where GROUPCODE = '{0}'", tmpGroupCode));
        DataSet pds = DataFunction.FillDataSet(string.Format("select * from T_SYS_R_GROUPPRIVATE where GROUPCODE = '{0}'", tmpGroupCode));
        foreach (TreeNode node in TreeView_Menu.CheckedNodes)
        {
            if (node.ImageToolTip.Equals("private"))
            {
                DataRow dr = pds.Tables[0].NewRow();
                dr["ID"] = Guid.NewGuid().ToString();
                dr["GROUPCODE"] = tmpGroupCode;
                dr["PCODE"] = node.Value;
                pds.Tables[0].Rows.Add(dr);
            }
            else if(!node.Value.Equals("0"))
            {
                DataRow dr = mds.Tables[0].NewRow();
                dr["ID"] = Guid.NewGuid().ToString();
                dr["GROUPCODE"] = tmpGroupCode;
                dr["MENUCODE"] = node.Value;
                mds.Tables[0].Rows.Add(dr);
            }
        }
        if (pds.Tables[0].Rows.Count > 0)
        {
            DataFunction.SaveData(pds, "T_SYS_R_GROUPPRIVATE");
        }
        if (mds.Tables[0].Rows.Count > 0)
        {
            DataFunction.SaveData(mds, "T_SYS_R_GROUPMENU");
        }
        //写日志文件开始====================================================================
        if (Session["BoolLog"].ToString().Trim() == "1")
        {
            string LogStrMsg = "";
            //LogUserName-人员 LogTitle-标题  LogMemo-内容
            string LogUserName = Session["UserRealName"].ToString().Trim() + "[" + Session["UserName"].ToString().Trim() + "]";
            LogUserName += "  " + Session["BranchName"].ToString().Trim() + "[" + Session["BranchCode"].ToString().Trim() + "]";
            string LogTitle = "设置角色对应的菜单";
            string LogMemo = "";
            log.Insert(LogUserName, LogTitle, LogMemo, out LogStrMsg);

        }
        //写日志文件结束====================================================================


        Session["Msg"] = "<script>alert('设置成功！');location.replace('GroupList.aspx');</script>";
        return;

    }

    //当点击选择框时执行的事件
    protected void TreeView_Menu_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
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


    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
        strLink = "GroupList.aspx";
        Response.Redirect(strLink, false);
    }
}
