using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WuDada.Core.Post;
using WuDada.Core.Post.Service;
using WuDada.Core.Post.Domain;
using WuDada.Core.Generic.Util;

public partial class service : System.Web.UI.Page
{
    private PostFactory m_PostFactory;
    private IPostService m_PostService;

    //服務項目主分類 m_ParentNodeId=14
    private int m_ParentNodeId = 14;

    //服務項目子分類 NodeId=0
    private int m_NodeId = 0;

    private string m_ListPath = "service.aspx";

    protected void Page_Load(object sender, EventArgs e)
    {
        m_PostFactory = new PostFactory();
        m_PostService = m_PostFactory.GetPostService();
        m_NodeId = QueryStringHelper.GetInteger("node", 0);

        if (!IsPostBack)
        {
            if (m_NodeId == 0)
            {
                IList<NodeVO> subNodeList = m_PostService.GetNodeListByParentId(m_ParentNodeId);
                if (subNodeList != null && subNodeList.Count > 0)
                {
                    Response.Redirect(string.Format("{0}?node={1}", m_ListPath, subNodeList[0].NodeId));
                }
            }

            NodeVO nodeVO = m_PostService.GetNodeById(m_NodeId);
            if (nodeVO != null)
            {
                ltlNodeName.Text = nodeVO.Name;
                ltlNodeName2.Text = nodeVO.Name;
                FillService();
            }
        }
    }

    private void FillService()
    {
        Dictionary<string, string> conditions = new Dictionary<string, string>();
        conditions.Add("NodeId", m_NodeId.ToString());
        conditions.Add("Flag", "1");

        //分頁
        AspNetPager1.RecordCount = m_PostService.GetPostCount(conditions);
        int pageIndex = (AspNetPager1.CurrentPageIndex - 1);
        int pageSize = AspNetPager1.PageSize;
        conditions.Add("PageIndex", pageIndex.ToString());
        conditions.Add("PageSize", pageSize.ToString());

        RepeaterService.DataSource = m_PostService.GetPostList(conditions);
        RepeaterService.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillService();
    }

    protected void RepeaterService_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            Control ctrl = e.Item;
            PostVO postVO = (PostVO)e.Item.DataItem;

            //標題
            int titleLength = 11;
            if (!string.IsNullOrEmpty(postVO.Title))
            {
                string title = postVO.Title;
                if (title.Length > titleLength)
                {
                    title = title.Substring(0, titleLength) + "...";
                }

                UIHelper.SetLiteralText(ctrl, "ltlTitle", title);
            }

            //內容
            int contentLength = 40;
            string content = ConvertUtil.FilterHtml(postVO.HtmlContent);
            if (!string.IsNullOrEmpty(content))
            {
                if (content.Length > contentLength)
                {
                    content = content.Substring(0, contentLength) + "...";
                }

                UIHelper.SetLiteralText(ctrl, "ltlContent", content);
            }
        }
    }
}