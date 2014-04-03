using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_FDZ_DiaoDuFaDan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            ZBGUID.Text = Request.QueryString["ZBGUID"];
            InitialControl();
        }
    }

    private void InitialControl()
    {
        string lb = Request.QueryString["lb"];
        if (lb == "DDFD")//调度发单
        {
            this.Title = "调度发单";
            tr_clsm.Style.Add("display","none");
            BtnCL.Style.Add("display", "none");//隐藏返回网管中心按钮
            CreateTreeViewBranch();
        }
        else if(lb == "FHWG")//返回网管中心
        {
            this.Title = "返回网管中心";
            BtnFD.Style.Add("display", "none");
            tr_tree.Style.Add("display", "none");//隐藏树
        }
    }
   
    /// <summary>
    ///  绑定人员
    /// </summary>
    private void CreateTreeViewBranch()
    {
        string sql = "select * from t_sys_branch b where b.branchname='客户维护部' order by DISPLAYORDER";
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            TreeNode node = new TreeNode();
            node.ToolTip = dr["BRANCHCODE"].ToString();
            node.Value = dr["BRANCHNAME"].ToString();
            node.Text = dr["BRANCHNAME"].ToString();
            node.ImageUrl = "../../Images/Small/map.gif";
            node.SelectAction = TreeNodeSelectAction.Expand;
            TreeViewBranch.Nodes.Add(node);
            CreateChildBranch(node);
        }
    }
    private void CreateChildBranch(TreeNode rootNode)
    {
        string sql = "select * from t_sys_branch b where b.pbranchcode='" + rootNode.ToolTip + "' and ISUSE='1' order by DISPLAYORDER";
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            TreeNode node = new TreeNode();
            node.ToolTip = dr["BRANCHCODE"].ToString();
            node.Value = dr["BRANCHNAME"].ToString();
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
        string sql = string.Format("select t.userrealname,t.id from t_sys_user t where t.branchcode ='{0}'", rootNode.ToolTip);
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            TreeNode node = new TreeNode();
            node.ToolTip = "用户";
            node.Value = dr["id"].ToString();
            node.Text = dr["userrealname"].ToString();
            node.ImageUrl = "../../Images/Small/emoney.gif";
            node.SelectAction = TreeNodeSelectAction.Expand;
            rootNode.ChildNodes.Add(node);
        }
    }

    protected void BtnQX_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>window.close();</script>");
    }
    protected void BtnCL_Click(object sender, EventArgs e)
    {
        string sql = "select * from T_FAU_ZB where zbguid='" + ZBGUID.Text + "'";
        DataRow dataRow = DataFunction.GetSingleRow(sql);
        dataRow["FDZZT"] = DBNull.Value;
        dataRow["GZYYR"] = DBNull.Value;
        dataRow["CSRID"] = DBNull.Value;
        //dataRow["yjrcode"] = getUsers("1001010107");
        dataRow["SFSD"] = "1";
        dataRow["SDRY"] = DBNull.Value;
        dataRow["YJBMCODE"] = Session["BranchCode"].ToString();
        DataFunction.SaveData(dataRow.Table.DataSet, "T_FAU_ZB");

        string guid = Guid.NewGuid().ToString();
        sql = "select t.* from t_fau_cllc t where t.guid='" + guid + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow dr = ds.Tables[0].NewRow();

        string UserId = Session["UserID"].ToString();
        string bm = Session["BranchName"].ToString();
        string clr = Session["UserRealName"].ToString();
        dr["GUID"] = guid;
        dr["ZBGUID"] = ZBGUID.Text;
        dr["CLSJ"] = DateTime.Now;
        dr["CLBM"] = bm;
        dr["CLRYID"] = UserId;
        dr["CLRY"] = clr;
        dr["GZZT"] = "处理中";
        dr["LCCZ"] = "返回网管中心";
        dr["CLSM"] = CLSM.Text;
        ds.Tables[0].Rows.Add(dr);
        DataFunction.SaveData(ds, "t_fau_cllc");
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('故障单已返回网管中心！'); window.returnValue='true';window.close();</script>");
    }

    private string getUsers(string branchcode)
    {
        string sql = string.Format("select t.id from t_sys_user t where t.branchcode like '{0}%'",branchcode);
        DataSet ds = DataFunction.FillDataSet(sql);
        string users = "";
        foreach(DataRow dr in ds.Tables[0].Rows)
        {
            if (dr["id"] != DBNull.Value && dr["id"].ToString()!="")
            {
                users += dr["id"].ToString() + ",";
            }
        }
        return users.Substring(0,users.Length-1);
    }

    protected void BtnFD_Click(object sender, EventArgs e)
    {
        string sql = "select * from T_FAU_ZB where zbguid='" + ZBGUID.Text + "'";
        DataRow dataRow = DataFunction.GetSingleRow(sql);
        dataRow["SFSD"] = "1";
        dataRow["SDRY"] = DBNull.Value;
        dataRow["GZYYR"] = DBNull.Value;
        dataRow["CSRID"] = DBNull.Value;
        dataRow["FDZZT"] = "维修返单";
        dataRow["DDFDR"] = getUsersName();
        dataRow["DDFDSJ"] = DateTime.Now;
        dataRow["yjrcode"] = getUsers("10010103");
        DataFunction.SaveData(dataRow.Table.DataSet, "T_FAU_ZB");

        string guid = Guid.NewGuid().ToString();
        sql = "select t.* from t_fau_cllc t where t.guid='" + guid + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow dr = ds.Tables[0].NewRow();

        string UserId = Session["UserID"].ToString();
        string bm = Session["BranchName"].ToString();
        string clr = Session["UserRealName"].ToString();
        dr["GUID"] = guid;
        dr["ZBGUID"] = ZBGUID.Text;
        dr["CLSJ"] = DateTime.Now;
        dr["CLBM"] = bm;
        dr["CLRYID"] = UserId;
        dr["CLRY"] = clr;
        dr["GZZT"] = "维修返单";
        dr["LCCZ"] = "发单";
        dr["SJCLRY"] = getUsersName();
        ds.Tables[0].Rows.Add(dr);
        DataFunction.SaveData(ds, "t_fau_cllc");
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>window.returnValue='true';window.close();</script>");
    }

    private string getUsersName()
    {
        string names = "";
        for (int i = 0; i < TreeViewBranch.CheckedNodes.Count; i++)
        {
            if (TreeViewBranch.CheckedNodes[i].ToolTip == "用户")
            {
                names += TreeViewBranch.CheckedNodes[i].Text + ",";
            }
        }
        names = names.Substring(0, names.Length - 1);
        return names;
    }

    protected void TreeViewBranch_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {

        CreateChildCheck(e.Node);
        if (e.Node.Parent != null)
        {
            SelNodeCheck(e.Node);
        }
    }

    private void SelNodeCheck(TreeNode nodes)
    {
        bool bl = true;
        foreach (TreeNode node in nodes.Parent.ChildNodes)
        {
            if (!node.Checked)
            {
                bl = false;
                break;
            }
            //nodes.Parent.ChildNodes
        }
        nodes.Parent.Checked = bl;
        if (nodes.Parent.Parent != null)
        {
            SelNodeCheck(nodes.Parent);
        }
        //return bl;
    }

    private void CreateChildCheck(TreeNode nodes)
    {
        foreach (TreeNode node in nodes.ChildNodes)
        {
            node.Checked = nodes.Checked;
            CreateChildCheck(node);
        }

    }
}
