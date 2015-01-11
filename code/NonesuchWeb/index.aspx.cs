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
using WuDada.Core.Post.DTO;
using WuDada.Core.Post.DTOConverter;

public partial class index : System.Web.UI.Page
{
    private PostFactory m_PostFactory;
    private IPostService m_PostService;

    protected void Page_Load(object sender, EventArgs e)
    {        
        m_PostFactory = new PostFactory();
        m_PostService = m_PostFactory.GetPostService();

        if (!IsPostBack)
        {
            FillAdv();
            FillNews();
            FillPrety();
            FillService();
        }
    }

    private void FillService()
    {
        IList<NodeVO> nodeList = m_PostService.GetNodeListByParentId(14);
        IList<ReportNodeVO> reportList = NodeVOConverter.ToDataTransferObjects(nodeList, 2);
        RepeaterService.DataSource = reportList;
        RepeaterService.DataBind();
    }

    protected void RepeaterService_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            Control ctrl = e.Item;
            ReportNodeVO reportNodeVO = (ReportNodeVO)e.Item.DataItem;
            int contentLength = 40;

            if (reportNodeVO != null)
            {
                if (reportNodeVO.Node1 != null)
                {
                    //標題
                    string title1 = reportNodeVO.Node1.Name;
                    UIHelper.SetLiteralText(ctrl, "ltlName", title1);

                    //內容
                    string content1 = ConvertUtil.FilterHtml(reportNodeVO.Node1.HtmlContent).Trim();
                    if (content1.Length > contentLength)
                    {
                        content1 = content1.Substring(0, contentLength) + "...";
                    }
                    UIHelper.SetLiteralText(ctrl, "ltlContent", content1);
                }

                if (reportNodeVO.Node2 != null)
                {
                    //標題
                    string title2 = reportNodeVO.Node2.Name;
                    UIHelper.SetLiteralText(ctrl, "ltlName2", title2);

                    //內容                    
                    string content2 = ConvertUtil.FilterHtml(reportNodeVO.Node2.HtmlContent).Trim();
                    if (content2.Length > contentLength)
                    {
                        content2 = content2.Substring(0, contentLength) + "...";
                    }
                    UIHelper.SetLiteralText(ctrl, "ltlContent2", content2);
                }
                else
                {
                    UIHelper.SetContrlVisible(ref ctrl, "imgMore2", false);
                }
            }
        }
    }

    private void FillPrety()
    {
        Dictionary<string, string> conditions = new Dictionary<string, string>();
        conditions.Add("ParentNodeId", "10");
        conditions.Add("Flag", "1");
        conditions.Add("PageIndex", "0");
        conditions.Add("PageSize", "5");

        RepeaterPrety.DataSource = m_PostService.GetPostList(conditions);
        RepeaterPrety.DataBind();
    }

    private void FillAdv()
    {
        //搜尋條件
        Dictionary<string, string> conditions = new Dictionary<string, string>();
        conditions.Add("NodeId", "20");        
        conditions.Add("Flag", "1");
        conditions.Add("PageIndex", "0");
        conditions.Add("PageSize", "5");

        RepeaterAdv.DataSource = m_PostService.GetPostList(conditions);
        RepeaterAdv.DataBind();
    }

    private void FillNews()
    {
        Dictionary<string, string> conditions = new Dictionary<string, string>();
        conditions.Add("NodeId", "4");
        conditions.Add("Flag", "1");
        conditions.Add("PageIndex", "0");
        conditions.Add("PageSize", "5");

        RepeaterNews.DataSource = m_PostService.GetPostList(conditions);
        RepeaterNews.DataBind();
    }

    protected void RepeaterNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            Control ctrl = e.Item;
            PostVO postVO = (PostVO)e.Item.DataItem;

            //標題
            int titleLength = 10;
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