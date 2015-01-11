﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WuDada.Core.Post;
using WuDada.Core.Post.Service;
using WuDada.Core.Post.Domain;
using WuDada.Core.Generic.Util;

public partial class news3 : System.Web.UI.Page
{
    private PostFactory m_PostFactory;
    private IPostService m_PostService;

    //療程分享 NodeId=6
    private int m_NodeId = 6;

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

        //分頁
        AspNetPager1.RecordCount = m_PostService.GetPostCount(conditions);
        int pageIndex = (AspNetPager1.CurrentPageIndex - 1);
        int pageSize = AspNetPager1.PageSize;
        conditions.Add("PageIndex", pageIndex.ToString());
        conditions.Add("PageSize", pageSize.ToString());

        RepeaterNews.DataSource = m_PostService.GetPostList(conditions);
        RepeaterNews.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        FillNews();
    }

    protected void RepeaterNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
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