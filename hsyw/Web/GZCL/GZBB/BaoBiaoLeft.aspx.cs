using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Web_GZCL_GZBB_BaoBiaoLeft : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        string bbType = TreeView1.SelectedNode.Text;
        switch(bbType)
        {
            case "今日统计":
                iframes.Attributes["src"] = "JinRiTongJi.aspx";
                break;
            case "处理方法报表":
                iframes.Attributes["src"] = "ChuLiFangFaTongJi.aspx";
                break;
            case "故障类型报表":
                iframes.Attributes["src"] = "GuZhangLeiXing.aspx";
                break;
            case "故障来源报表":
                iframes.Attributes["src"] = "GuZhangLaiYuan.aspx";
                break;
            case "历史数据统计":
                iframes.Attributes["src"] = "LiShiShuJu.aspx";
                break;
            case "历史记录查询":
              iframes.Attributes["src"] = "LiShiJiLuChaXun.aspx";
                break;
            case "业务类型报表":
                iframes.Attributes["src"] = "GuZhangZhuanYe.aspx";
                break;
            case "故障数量统计":
                iframes.Attributes["src"] = "GuZhangShuLiang.aspx";
                break;
            case "行业分类报表":
                iframes.Attributes["src"] = "HangYeFenLei.aspx";
                break;
            case "故障统计明细":
                iframes.Attributes["src"] = "GuoDanTongJiMingXi.aspx";
                break;
            case "留单业务统计":
                iframes.Attributes["src"] = "LiuDanYeWuTongJi.aspx";
                break;
        }
    }
}
