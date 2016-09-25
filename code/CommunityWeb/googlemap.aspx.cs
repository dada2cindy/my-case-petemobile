using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Logging;
using WuDada.Core.Post;
using WuDada.Core.Post.Domain;
using WuDada.Core.Post.Service;

public partial class googlemap : System.Web.UI.Page
{
    private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private PostFactory m_PostFactory;
    private IPostService m_PostService;

    protected void Page_Load(object sender, EventArgs e)
    {
        m_PostFactory = new PostFactory();
        m_PostService = m_PostFactory.GetPostService();

        if (!IsPostBack)
        {
            LoadUI();
        }
    }

    private void LoadUI()
    {
        if (!string.IsNullOrEmpty(Request["id"]))
        {
            string id = Request["id"];
            PostVO postVO = m_PostService.GetPostById(int.Parse(id));
            if (postVO != null && !string.IsNullOrEmpty(postVO.HtmlContent))
            {
                ltlMap.Text = string.Format("<iframe src=\"{0}\" width='800' height='800' frameborder='0' style='border:0' allowfullscreen></iframe>", postVO.HtmlContent);
            }
        }
    }
}