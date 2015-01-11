using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WuDada.Core.Post;
using WuDada.Core.Post.Service;
using WuDada.Core.Post.Domain;

public partial class UserControl_LeftService : System.Web.UI.UserControl
{
    private PostFactory m_PostFactory;
    private IPostService m_PostService;

    //服務項目 NodeId=14
    private int m_NodeId = 14;

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
        IList<NodeVO> subNodeList = m_PostService.GetNodeListByParentId(m_NodeId);
        RepeaterMenu.DataSource = subNodeList;
        RepeaterMenu.DataBind();
    }
}