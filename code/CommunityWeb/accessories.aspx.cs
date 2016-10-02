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

public partial class accessories : System.Web.UI.Page
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
        LoadSuppliersTag();

        if (!string.IsNullOrEmpty(Request["keyword"]))
        {
            ltlKeyword.Text = string.Format("<div class='breadcrumb'>您搜尋的關鍵字為:【{0}】</div>"
                , Request["keyword"]);
        }
        else
        {
            ltlKeyword.Text = string.Empty;
        }
    }

    private void LoadSuppliersTag()
    {
        ltlSuppliersTag.Text = string.Empty;
        StringBuilder sb = new StringBuilder();

        //搜尋條件
        IList<NodeVO> list = m_PostService.GetNodeListByParentId(9);

        if (list != null && list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                NodeVO vo = list[i];
                if (HasProduct(vo))
                {
                    string lastCss = "";
                    if (i <= list.Count - 1)
                    {
                        lastCss = " store_item_a_last";
                    }
                    sb.AppendFormat("<a href='#pro_{0}'><li class='store_item_a store_item_logo border' style=\"background-image: url('upload/{1}');\"><div class='corner_bg corner_img {2}'></div></li></a>"
                        , vo.NodeId, vo.PicFileName, lastCss);
                }
            }
            ltlSuppliersTag.Text = sb.ToString();

            LoadSuppliers(list);
        }
    }

    private bool HasProduct(NodeVO vo)
    {
        StringBuilder sb = new StringBuilder();

        //搜尋條件
        string keyword = Request["keyword"];
        Dictionary<string, string> conditions = new Dictionary<string, string>();
        conditions.Add("NodeId", "4");
        conditions.Add("Flag", "1");
        conditions.Add("ProductKeyWord", keyword);
        conditions.Add("WarrantySuppliers", vo.Name);

        return m_PostService.GetPostCount(conditions) > 0;
    }

    private void LoadSuppliers(IList<NodeVO> SupplierList)
    {
        ltlProducts.Text = string.Empty;
        StringBuilder sb = new StringBuilder();

        foreach (NodeVO vo in SupplierList)
        {
            if (HasProduct(vo))
            {
                string supplierTable = GetSupplierTable(vo);
                string productList = GetProductList(vo);
                sb.AppendFormat(supplierTable, productList);
            }
        }

        ltlProducts.Text = sb.ToString();
    }

    private string GetProductList(NodeVO vo)
    {
        StringBuilder sb = new StringBuilder();

        //搜尋條件
        string keyword = Request["keyword"];
        Dictionary<string, string> conditions = new Dictionary<string, string>();
        conditions.Add("NodeId", "4");
        conditions.Add("Flag", "1");
        conditions.Add("ProductKeyWord", keyword);
        conditions.Add("WarrantySuppliers", vo.Name);
        conditions.Add("Order", string.Format("order by {0}", "p.WarrantySuppliers, p.SortNo, p.PostId"));

        IList<PostVO> list = m_PostService.GetPostList(conditions);

        if (list != null && list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                PostVO post = list[i];
                sb.AppendFormat("<tr onclick=\"window.location='product.aspx?id={0}';\"><td class='product_ov_type'>{1}</td><td class='product_ov_price'>${2}</td></tr>"
                    , post.PostId, post.Title, post.SellPrice);
            }
        }

        return sb.ToString();
    }

    private string GetSupplierTable(NodeVO vo)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("<div class='product_ov_br' id='pro_{0}'>", vo.NodeId);
        sb.AppendFormat("<div class='product_ovlogo' style=\"background-image:url('upload/{0}')\"></div>"
            , vo.PicFileName);
        sb.Append("<table class='product_ov'><tr><th class='product_ov_thtype'></th><th class='product_ov_thprice'></th></tr>");
        sb.Append("{0}");
        sb.Append("</table>");
        sb.Append("</div>");
        return sb.ToString();
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