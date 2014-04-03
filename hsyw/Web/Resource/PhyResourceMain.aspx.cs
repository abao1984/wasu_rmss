using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_PhyResourceMain : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {
            CreateTreeRoot();
        }
    }

    #region 绑定树结构
    private void CreateTreeRoot()
    {
        TreeView_Unit.Nodes.Clear();
        string sql = "select * from T_RES_SYS_UNIT t where t.parent_unit_id is null and isshow='1' order by SEQUENCE";
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow DR in ds.Tables[0].Rows)
        {
            TreeNode nodeRoot = new TreeNode();
            nodeRoot.ImageUrl = "../Images/Small/outbox.gif";
            nodeRoot.Value = DR["UNIT_ID"].ToString();
            nodeRoot.Text = DR["UNIT_NAME"].ToString();
      
            // nodeRoot.SelectAction = TreeNodeSelectAction.Expand;
            if (DR["IS_CREATE_TABLE"].ToString() == "1")
            {
                nodeRoot.NavigateUrl = "PhyResourceList.aspx?UNIT_ID=" + nodeRoot.Value + "&UNIT_NAME=" + HttpUtility.UrlEncode(nodeRoot.Text) + "&NAME_FILED=" + DR["NAME_FILED"].ToString()+"&SJGGG="+Session["SJGGG"].ToString().Trim();
                nodeRoot.Target = "PhyPage";
            }
            nodeRoot.Expanded = true;
            this.TreeView_Unit.Nodes.Add(nodeRoot);
            CreateTreeChild(nodeRoot);
        }
        //if (!string.IsNullOrEmpty(UNIT_ID.Text))
        //{
        //    UnitPage.Attributes.Add("src", "ResourceUnit.aspx?UNIT_ID=" + UNIT_ID.Text);
        //}
    }
    private void CreateTreeChild(TreeNode nodeRoot)
    {
        string sql = "select * from T_RES_SYS_UNIT t where t.parent_unit_id='" + nodeRoot.Value + "'  and isshow='1' order by SEQUENCE";
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow DR in ds.Tables[0].Rows)
        {
            TreeNode nodeChild = new TreeNode();
            nodeChild.ImageUrl = "../Images/Small/phiz.gif";
            nodeChild.Value = DR["UNIT_ID"].ToString();
            nodeChild.Text = DR["UNIT_NAME"].ToString();
            nodeChild.NavigateUrl = DR["NAME_FILED"].ToString();
            // nodeChild.SelectAction = TreeNodeSelectAction.Expand;
            if (DR["IS_CREATE_TABLE"].ToString() == "1")
            {
                nodeChild.NavigateUrl = "PhyResourceList.aspx?UNIT_ID=" + nodeChild.Value + "&UNIT_NAME=" + HttpUtility.UrlEncode(nodeChild.Text) + "&NAME_FILED=" + DR["NAME_FILED"].ToString() + "&SJGGG=" + Session["SJGGG"].ToString().Trim(); 
                nodeChild.Target = "PhyPage";
            }
            nodeChild.Expanded = true;
            nodeRoot.ChildNodes.Add(nodeChild);
            CreateTreeChild(nodeChild);
        }
    }
    #endregion
}
