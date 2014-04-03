using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_ResourceMain : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        if (!this.IsPostBack)
        {
            CreateTreeRoot();
        }
    }

    private void CreateTreeRoot()
    {
        TreeView_Unit.Nodes.Clear();
        string sql = "select * from T_RES_SYS_UNIT t where t.parent_unit_id is null order by SEQUENCE";
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow DR in ds.Tables[0].Rows)
        {
            TreeNode nodeRoot = new TreeNode();
            nodeRoot.ImageUrl = "../Images/Small/outbox.gif";
            nodeRoot.Value = DR["UNIT_ID"].ToString();
            nodeRoot.Text = DR["UNIT_NAME"].ToString();
           // nodeRoot.SelectAction = TreeNodeSelectAction.Expand;
            nodeRoot.NavigateUrl = "ResourceUnit.aspx?UNIT_ID=" + nodeRoot.Value;
            nodeRoot.Target = "UnitPage";
            nodeRoot.Expanded = true;
            this.TreeView_Unit.Nodes.Add(nodeRoot);
            CreateTreeChild(nodeRoot);
        }
        if (!string.IsNullOrEmpty(UNIT_ID.Text))
        {
            UnitPage.Attributes.Add("src", "ResourceUnit.aspx?UNIT_ID="+UNIT_ID.Text);
        }
    }
    private void CreateTreeChild(TreeNode nodeRoot)
    {
        string sql = "select * from T_RES_SYS_UNIT t where t.parent_unit_id='" + nodeRoot.Value + "' order by SEQUENCE";
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow DR in ds.Tables[0].Rows)
        {
            TreeNode nodeChild = new TreeNode();
            nodeChild.ImageUrl = "../Images/Small/phiz.gif";
            nodeChild.Value = DR["UNIT_ID"].ToString();
            nodeChild.Text = DR["UNIT_NAME"].ToString();
           // nodeChild.SelectAction = TreeNodeSelectAction.Expand;
            nodeChild.NavigateUrl = "ResourceUnit.aspx?UNIT_ID=" + nodeChild.Value;
            nodeChild.Target = "UnitPage";
            nodeChild.Expanded = true;
            nodeRoot.ChildNodes.Add(nodeChild);
            CreateTreeChild(nodeChild);
        }
    }
    protected void Btn_Click(object sender, EventArgs e)
    {
        CreateTreeRoot();
    }
}
