using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WuDada.Core.Post;
using WuDada.Core.Post.Service;
using WuDada.Core.Post.Domain;

public partial class profile : System.Web.UI.Page
{
    private PostFactory m_PostFactory;
    private IPostService m_PostService;
    private int m_PostId = 0;
    //關於臻美NodeId=2
    private int m_NodeId = 2;
    private string m_ListPath = "profile.aspx";

    protected void Page_Load(object sender, EventArgs e)
    {
        m_PostFactory = new PostFactory();
        m_PostService = m_PostFactory.GetPostService();
        m_PostId = QueryStringHelper.GetInteger("id", 0);

        if (!IsPostBack)
        {
            if (m_PostId == 0)
            {
                IList<PostVO> list = m_PostService.GetPostListByNodeId(m_NodeId, true, "SortNo", false);
                if (list != null && list.Count > 0)
                {
                    Response.Redirect(string.Format("{0}?id={1}", m_ListPath, list[0].PostId));
                }
            }

            PostVO postVO = m_PostService.GetPostByIdNoLazy(m_PostId);
            if (postVO != null)
            {
                ltlNodeName.Text = postVO.Title;
                ltlNodeName2.Text = postVO.Title;
                ltlTitle.Text = postVO.Title;
                ltlContent.Text = postVO.HtmlContent;
            }
        }
    }
}