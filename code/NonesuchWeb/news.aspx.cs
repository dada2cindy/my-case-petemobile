using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WuDada.Core.Post;
using WuDada.Core.Post.Service;
using WuDada.Core.Post.Domain;

public partial class news : System.Web.UI.Page
{
    private PostFactory m_PostFactory;
    private IPostService m_PostService;

    //臻美快訊 NodeId=4
    private int m_NodeId = 4;

    protected void Page_Load(object sender, EventArgs e)
    {
        m_PostFactory = new PostFactory();
        m_PostService = m_PostFactory.GetPostService();

        if (!IsPostBack)
        {
            FillNews();
        }
    }

    private void FillNews()
    {
        Dictionary<string, string> conditions = new Dictionary<string, string>();
        conditions.Add("NodeId", m_NodeId.ToString());
        conditions.Add("Flag", "1");

        ////分頁
        //AspNetPager1.RecordCount = m_PostService.GetPostCount(conditions);
        //int pageIndex = (AspNetPager1.CurrentPageIndex - 1);
        //int pageSize = AspNetPager1.PageSize;
        //conditions.Add("PageIndex", pageIndex.ToString());
        //conditions.Add("PageSize", pageSize.ToString());

        RepeaterNews.DataSource = m_PostService.GetPostList(conditions);
        RepeaterNews.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillNews();
    }
}