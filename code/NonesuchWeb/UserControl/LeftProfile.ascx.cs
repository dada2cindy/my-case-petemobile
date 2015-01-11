using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WuDada.Core.Post;
using WuDada.Core.Post.Service;
using WuDada.Core.Post.Domain;

public partial class UserControl_LeftProfile : System.Web.UI.UserControl
{
    private PostFactory m_PostFactory;
    private IPostService m_PostService;

    //關於臻美 NodeId=2
    private int m_NodeId = 2;

    protected void Page_Load(object sender, EventArgs e)
    {
        m_PostFactory = new PostFactory();
        m_PostService = m_PostFactory.GetPostService();

        if (!IsPostBack)
        {
            FillMenu();
        }
    }

    private void FillMenu()
    {
        IList<PostVO> list = m_PostService.GetPostListByNodeId(m_NodeId, true, "SortNo", false);
        RepeaterMenu.DataSource = list;
        RepeaterMenu.DataBind();
    }
}