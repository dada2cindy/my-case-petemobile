using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WuDada.Core.Post;
using WuDada.Core.Post.Service;
using WuDada.Core.Post.Domain;

public partial class team_in : System.Web.UI.Page
{
    private PostFactory m_PostFactory;
    private IPostService m_PostService;
    private int m_PostId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        m_PostFactory = new PostFactory();
        m_PostService = m_PostFactory.GetPostService();
        m_PostId = QueryStringHelper.GetInteger("id", 0);

        if (!IsPostBack)
        {
            PostVO postVO = m_PostService.GetPostByIdNoLazy(m_PostId);
            if (postVO != null)
            {
                ltlNodeName.Text = postVO.Node.Name;
                ltlNodeName2.Text = postVO.Node.Name;
                ltlTitle.Text = postVO.Title;
                ltlCustomField1.Text = postVO.CustomField1;
                ltlCustomField2.Text = postVO.CustomField2;
                ltlContent.Text = postVO.HtmlContent.Replace(Environment.NewLine, "<br/>");
                imgPicFileName.ImageUrl = string.Format("upload/{0}", postVO.PicFileName);
            }
        }
    }
}