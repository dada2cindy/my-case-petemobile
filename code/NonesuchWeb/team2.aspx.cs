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

public partial class team2 : System.Web.UI.Page
{
    private PostFactory m_PostFactory;
    private IPostService m_PostService;

    //儀器設備NodeId=9
    private int m_NodeId = 9;

    protected void Page_Load(object sender, EventArgs e)
    {
        m_PostFactory = new PostFactory();
        m_PostService = m_PostFactory.GetPostService();

        if (!IsPostBack)
        {
            FillService();
        }
    }

    private void FillService()
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
            int titleLength = 17;
            if (!string.IsNullOrEmpty(postVO.Title))
            {
                string title = postVO.Title;
                if (title.Length > titleLength)
                {
                    title = title.Substring(0, titleLength) + "...";
                }

                UIHelper.SetLiteralText(ctrl, "ltlTitle", title);
            }
        }
    }
}