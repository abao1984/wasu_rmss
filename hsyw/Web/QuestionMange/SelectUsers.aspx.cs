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
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        if (!Page.IsPostBack)
        {
            FillCodes();
            ShowTree();
            InitPrivate();
        }
    }

    private void InitPrivate()
    {
        //因为在新建时是发到问题指派的。所以要在选人的保存的时候把下一步给置上 罗耀斌
        string ztxz = Convert.ToString(Request.QueryString["ztxz"]);
        switch(ztxz)
        {
            case "新建":
                TxtZtXz.Text = "问题指派";
                break;
            case "问题指派":
                TxtZtXz.Text = "问题处理";
                break;
            case "问题处理":
                TxtZtXz.Text = "问题评审";
                break;
        }
    }
    public void ShowTree()
    {
        DataSet uds = DataFunction.FillDataSet("select * from T_SYS_USER order by displayorder");
        ViewState["uds"] = uds;
        DataSet ds = DataFunction.FillDataSet("select * from T_SYS_BRANCH order by displayorder");
        TreeView1.Nodes.Clear();

        TreeNode node_0 = new TreeNode();
        node_0.ImageUrl = "../Images/Small/calendar.gif";
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
            Node.ImageUrl = "../Images/Small/branch.gif";
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
            Node.ImageUrl = "../Images/Small/0010.gif";
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
        if (TreeView1.CheckedNodes.Count == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('请选择一个用户！');</script>");
            return;
        }
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
        string quesid = Request.QueryString["QUESID"];
        if (!string.IsNullOrEmpty(quesid))
        {
            string sql1 = string.Format("update T_QUES_CLR set ISDQ = '0' where QUESID = '{0}'", quesid);
            string lx = "";
            switch (TxtZtXz.Text)
            {
                case "问题指派":
                    lx = "ZPR";
                    break;
                case "问题处理":
                    lx = "CLR";
                    break;
                default:
                    lx = "CLR";
                    break;
            }
            string sql2 = string.Format("insert into T_QUES_CLR (id,quesid,sj,urealname,uname,lx,isdq,bz) values ('{0}','{1}',to_date('{2}','YYYY-MM-DD HH24:MI:SS'),'{3}','{4}','{5}','1','{6}')", Guid.NewGuid().ToString(), quesid, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), userrealname, username, lx,TxtZtXz.Text);
            string sql3 = string.Format("update t_ques_info set ztxz='" + TxtZtXz.Text + "' where id='{0}'", quesid);

            //当类型是问题指派时，把指派人给置到数据库中，因为前面给文本赋值时已经把流程推到下一步了，所以要判断是否是问题处理，其它这步就是问题指派 罗耀斌
            if (TxtZtXz.Text == "问题处理")
            {
                DataFunction.ExecuteNonQuery("update t_ques_info set ZPR='" + Convert.ToString(Session["username"]) + "' where id='" + quesid + "'");
            }
            string[] sql = new string[] { sql1, sql2,sql3 };
            DataFunction.ExecuteTransaction(sql);
            //这个是新建用户时转交时用的 罗耀斌
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), @"<script> alert('保存成功....');window.close();parent.WindowClose();parent.parent.WindowClose();</script>");

        }
        else
        {
            //这个是在我的问题时点击处理时选择用户用的 罗耀斌
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(),
            string.Format(@"<script> window.close();parent.WindowClose();
        parent.document.getElementById('{0}').value = '{1}'; parent.document.getElementById('{2}').value = '{3}';parent.parent.WindowClose(); </script>"
            , Request.QueryString["realname"], userrealname, Request.QueryString["username"], username));
        }

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
        string usernames1 = Request.QueryString["usernames"];
        if(!string.IsNullOrEmpty(usernames1))
        {
            UserNames.Text = usernames1;
            string[] un = usernames1.Split(',');
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
