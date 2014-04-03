using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_ComputerHouseMain : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
           // BindTreeView_Computer();
            CreateTreeRoot();
        }

    }

    #region 创建机房结构树
    //创建根节点
    private void BindTreeView_Computer()
    {
        TreeNode nodeRoot = new TreeNode();
        nodeRoot.ImageUrl = "../Images/Small/outbox.gif";
        nodeRoot.Value = "c7cea0e2-5628-4fc7-86db-acb8ef9d9738";
        nodeRoot.Text ="机房";
        nodeRoot.NavigateUrl = "ComputerHouseList.aspx?UNIT_ID=" + nodeRoot.Value;
        nodeRoot.Target = "UnitPage";
        nodeRoot.Expanded = true;
        this.TreeView_Computer.Nodes.Add(nodeRoot);
        CreateSubComputer(nodeRoot);
    }
    //创建机房分类
    private void CreateSubComputer(TreeNode nodeRoot)
    {
        string sql = "select * from T_RES_SYS_UNIT t where t.parent_unit_id='" + nodeRoot.Value + "' order by  t.sequence";
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow DR in ds.Tables[0].Rows)
        {
            TreeNode nodeChild = new TreeNode();
            nodeChild.ImageUrl = "../Images/Small/home1.gif";
            nodeChild.Value = DR["UNIT_ID"].ToString();
            nodeChild.Text = DR["UNIT_NAME"].ToString();
            
            // nodeChild.SelectAction = TreeNodeSelectAction.Expand;
            nodeChild.NavigateUrl = "ComputerHouseList.aspx?UNIT_ID=" + nodeRoot.Value + "&SUB_UNIT_ID=" + nodeChild.Value;
            nodeChild.Target = "UnitPage";
            nodeChild.Expanded = true;
            nodeRoot.ChildNodes.Add(nodeChild);
            CreateComputerHouse(nodeChild);
        }
    }

    //加载机房
    private void CreateComputerHouse(TreeNode nodeRoot)
    {
        string sql = "select t.table_name from T_RES_SYS_UNIT t where t.unit_id='" + nodeRoot.Value + "'";
        string table_name = DataFunction.GetStringResult(sql);
        sql = "select * from "+table_name;
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow DR in ds.Tables[0].Rows)
        {
            TreeNode nodeChild = new TreeNode();
            nodeChild.ImageUrl = "../Images/Small/inter.gif";
            nodeChild.Value = DR["GUID"].ToString();
            nodeChild.Text = DR["HOUSE_NAME"].ToString();

            // nodeChild.SelectAction = TreeNodeSelectAction.Expand;
            nodeChild.NavigateUrl = "ComputerHouseList.aspx?UNIT_ID=7aed4be6-d929-43b8-87ed-75085df73786" + "&SUB_UNIT_ID=" + nodeRoot.Value; 
            nodeChild.Target = "UnitPage";
            nodeChild.Expanded = false;
            nodeRoot.ChildNodes.Add(nodeChild);
            CreateSubCoumputerCup(nodeChild);
        }
    }
    //加载机柜分类
    private void CreateSubCoumputerCup(TreeNode nodeRoot)
    {
        string sql = "select * from T_RES_SYS_UNIT t where t.parent_unit_id='7aed4be6-d929-43b8-87ed-75085df73786' order by  t.sequence";
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow DR in ds.Tables[0].Rows)
        {
            TreeNode nodeChild = new TreeNode();
            nodeChild.ImageUrl = "../Images/Small/folders.gif";
            nodeChild.Value = DR["UNIT_ID"].ToString();
            nodeChild.Text = DR["UNIT_NAME"].ToString();

            // nodeChild.SelectAction = TreeNodeSelectAction.Expand;
            nodeChild.NavigateUrl = "ComputerHouseList.aspx?UNIT_ID=7aed4be6-d929-43b8-87ed-75085df73786";
            nodeChild.Target = "UnitPage";
            nodeChild.Expanded = false;
            nodeRoot.ChildNodes.Add(nodeChild);
           CreateComputerCup(nodeChild);
        }
    }
    //加载机柜
    private void CreateComputerCup(TreeNode nodeRoot)
    {
        string sql = "select t.table_name from T_RES_SYS_UNIT t where t.unit_id='" + nodeRoot.Value + "'";
        string table_name = DataFunction.GetStringResult(sql);
        sql = "select * from " + table_name;
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow DR in ds.Tables[0].Rows)
        {
            TreeNode nodeChild = new TreeNode();
            nodeChild.ImageUrl = "../Images/Small/layout_content.gif";
            nodeChild.Value = DR["GUID"].ToString();
            nodeChild.Text = DR["HOUSE_NAME"].ToString();

            // nodeChild.SelectAction = TreeNodeSelectAction.Expand;
            nodeChild.NavigateUrl = "ComputerHouseList.aspx?UNIT_ID=3518e4d8-2188-4aa9-8d43-4b541dfa0f90" + "&SUB_UNIT_ID=" + nodeRoot.Value; 
            nodeChild.Target = "UnitPage";
            nodeChild.Expanded = false;
            nodeRoot.ChildNodes.Add(nodeChild);
            CreateSubEquipment(nodeChild);
        }
    }

    //加载设备分类
    private void CreateSubEquipment(TreeNode nodeRoot)
    {
        string sql = "select * from T_RES_SYS_UNIT t where t.parent_unit_id='3518e4d8-2188-4aa9-8d43-4b541dfa0f90' order by  t.sequence";
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow DR in ds.Tables[0].Rows)
        {
            TreeNode nodeChild = new TreeNode();
            nodeChild.ImageUrl = "../Images/Small/report_disk.gif";
            nodeChild.Value = DR["UNIT_ID"].ToString();
            nodeChild.Text = DR["UNIT_NAME"].ToString();

            // nodeChild.SelectAction = TreeNodeSelectAction.Expand;
            nodeChild.NavigateUrl = "ComputerHouseList.aspx?UNIT_ID=3518e4d8-2188-4aa9-8d43-4b541dfa0f90";
            nodeChild.Target = "UnitPage";
            nodeChild.Expanded = false;
            nodeRoot.ChildNodes.Add(nodeChild);
            CreateEquipment(nodeChild);
        }
    }
    //加载设备
    private void CreateEquipment(TreeNode nodeRoot)
    {
        string sql = "select t.table_name from T_RES_SYS_UNIT t where t.unit_id='" + nodeRoot.Value + "'";
        string table_name = DataFunction.GetStringResult(sql);
        sql = "select * from " + table_name;
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow DR in ds.Tables[0].Rows)
        {
            TreeNode nodeChild = new TreeNode();
            nodeChild.ImageUrl = "../Images/Small/tv.gif";
            nodeChild.Value = DR["GUID"].ToString();
            nodeChild.Text = DR["HOUSE_NAME"].ToString();

            // nodeChild.SelectAction = TreeNodeSelectAction.Expand;
            nodeChild.NavigateUrl = "ComputerHouseList.aspx?UNIT_ID=3518e4d8-2188-4aa9-8d43-4b541dfa0f90" + "&SUB_UNIT_ID=" + nodeRoot.Value; 
            nodeChild.Target = "UnitPage";
            nodeChild.Expanded = false;
            nodeRoot.ChildNodes.Add(nodeChild);
            //  CreateSubCoumputerCup(nodeChild);
        }
    }
    #endregion


    private void CreateTreeRoot()
    {
        TreeView_Computer.Nodes.Clear();
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
            this.TreeView_Computer.Nodes.Add(nodeRoot);
            CreateTreeChild(nodeRoot);
        }
       
    }
    private void CreateTreeChild(TreeNode nodeRoot)
    {
        string sql = "select * from T_RES_SYS_UNIT t where t.parent_unit_id='" + nodeRoot.Value + "' order by SEQUENCE";
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow DR in ds.Tables[0].Rows)
        {
            TreeNode nodeChild = new TreeNode();
            nodeChild.ImageUrl = "../Images/Small/home1.gif";
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
}
