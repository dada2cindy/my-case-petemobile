using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Logging;
using WuDada.Core.Post;
using WuDada.Core.Post.Domain;
using WuDada.Core.Post.Service;

public partial class product : System.Web.UI.Page
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
        LoadBannerRight();
        LoadProduct();
    }

    private void LoadProduct()
    {
        int id;
        if (!string.IsNullOrEmpty(Request["id"]) && int.TryParse(Request["id"], out id))
        {
            PostVO post = m_PostService.GetPostById(id);
            if (post != null && post.Flag == 1)
            {
                ltlProductPic.Text = string.Format("<div class='product_item_img' style=\"background-image:url('upload/{0}')\"></div>", post.PicFileName);
                ltlTitle.Text = post.Title;
                ltlPrice.Text = post.Price.ToString();
                ltlSellPrice.Text = post.SellPrice.ToString();
                ltlSummary.Text = string.Empty;
                ltlHtmlContent.Text = post.HtmlContent;

                if (!string.IsNullOrEmpty(post.Summary))
                {
                    StringBuilder sb = new StringBuilder();
                    string[] summarys = post.Summary.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    foreach (string s in summarys)
                    {
                        sb.AppendFormat("<li>{0}</li>", s);
                    }
                    ltlSummary.Text = sb.ToString();
                }
            }
        }
    }

    private void LoadBannerRight()
    {
        ltlRight.Text = string.Empty;
        StringBuilder sb = new StringBuilder();

        //搜尋條件
        Dictionary<string, string> conditions = new Dictionary<string, string>();
        conditions.Add("NodeId", "7");
        conditions.Add("PageIndex", "0");
        conditions.Add("PageSize", "9");
        conditions.Add("Flag", "1");
        conditions.Add("Order", string.Format("order by {0}", "p.SortNo, p.PostId"));

        IList<PostVO> list = m_PostService.GetPostList(conditions);
        if (list != null && list.Count > 0)
        {
            foreach (PostVO post in list)
            {
                if (string.IsNullOrEmpty(post.LinkUrl))
                {
                    sb.AppendFormat("<div style=\"background-image: url('upload/{0}')\" class=\"contant_right_ad\" ></div>", post.PicFileName);
                }
                else
                {
                    sb.AppendFormat("<div style=\"background-image: url('upload/{0}'); cursor: pointer;\" class='contant_right_ad' onclick=\"window.location='{1}';\" ></div>", post.PicFileName, post.LinkUrl);
                }
            }

            ltlRight.Text = sb.ToString();
        }
    }
}