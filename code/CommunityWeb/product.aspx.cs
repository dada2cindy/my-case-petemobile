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
        LoadSuppliersTag();
        LoadPromoteList();
    }

    private void LoadPromoteList()
    {
        IList<PromoteVO> list = m_PostService.GetPromoteList(50);

        LoadSuppliersTag2(list);        
    }

    private void LoadSuppliersTag2(IList<PromoteVO> promoteList)
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
                if (promoteList.Count(p => p.WarrantySuppliers.Equals(vo.Name)) > 0)
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
            ltlSuppliersTag2.Text = sb.ToString();

            LoadSuppliers2(list, promoteList);
        }
    }

    private void LoadSuppliers2(IList<NodeVO> supplierList, IList<PromoteVO> promoteList)
    {
        ltlProducts2.Text = string.Empty;
        StringBuilder sb = new StringBuilder();

        foreach (NodeVO vo in supplierList)
        {
            if (promoteList.Count(p => p.WarrantySuppliers.Equals(vo.Name)) > 0)
            {
                string supplierTable = GetSupplierTable2(vo);
                string productList = GetProductList2(vo, promoteList);
                sb.AppendFormat(supplierTable, productList);
            }
        }

        ltlProducts2.Text = sb.ToString();
    }

    private string GetProductList2(NodeVO vo, IList<PromoteVO> promoteList)
    {
        StringBuilder sb = new StringBuilder();

        IList<PromoteVO> list = promoteList.Where(p => p.WarrantySuppliers.Equals(vo.Name)).ToList();

        if (list != null && list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                PromoteVO promoteVO = list[i];
                sb.AppendFormat("<tr><td class='product_ov_type'>{0}</td><td class='product_ov_price'>${1}</td><td class='product_ov_price'>${2}</td><td class='product_ov_price'>${3}</td><td class='product_ov_price'>${4}</td><td class='product_ov_price'>${5}</td><td class='product_ov_price'>${6}</td></tr>"
                    , promoteVO.Title, promoteVO.SellPrice
                    , promoteVO.SellPriceW1
                    , promoteVO.SellPriceW2
                    , promoteVO.SellPriceW3
                    , promoteVO.SellPriceW4
                    , promoteVO.SellPriceW5);
            }
        }

        return sb.ToString();
    }

    private string GetSupplierTable2(NodeVO vo)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("<div class='product_ov_br' id='pro_{0}'>", vo.NodeId);
        sb.Append("<table class='product_ov' style='width: 100%;'><tr>");
        sb.AppendFormat("<th><div style='position: relative; height: 25px;'><img src='upload/{0}' width='94' height='42' style='position: absolute;' /></div></th>"
            , vo.PicFileName);
        sb.Append("<th class='promote_title'>單機價</th>");
        sb.AppendFormat("<th class='promote_title'>{0}</th>", GetPromoteTitle("中華電信"));
        sb.AppendFormat("<th class='promote_title'>{0}</th>", GetPromoteTitle("遠傳"));
        sb.AppendFormat("<th class='promote_title'>{0}</th>", GetPromoteTitle("台哥大"));
        sb.AppendFormat("<th class='promote_title'>{0}</th>", GetPromoteTitle("亞太電信"));
        sb.AppendFormat("<th class='promote_title'>{0}</th></tr>", GetPromoteTitle("台灣之星"));
        sb.Append("{0}");
        sb.Append("</table>");
        sb.Append("</div>");
        return sb.ToString();
    }

    private string GetPromoteTitle(string warrantySuppliers)
    {
        string result = "無";

        //搜尋條件
        Dictionary<string, string> conditions = new Dictionary<string, string>();
        conditions.Add("NodeId", "5");
        conditions.Add("ProductKeyWord", warrantySuppliers);
        conditions.Add("PageIndex", "0");
        conditions.Add("PageSize", "1");
        conditions.Add("IsPromote", "true");
        conditions.Add("Order", string.Format("order by {0}", "p.WarrantySuppliers, p.Type, p.SortNo, p.PostId"));

        IList<PostVO> discountList = m_PostService.GetPostList(conditions);
        if (discountList != null && discountList.Count > 0)
        {
            string title = discountList[0].Title;
            if (title.IndexOf("(") != -1)
            {
                title = title.Replace("(", "<div style='height:5px;'></div>(");
            }
            result = title;
        }

        return result;
    }

    private void LoadSuppliersTag()
    {
        ltlSuppliersTag.Text = string.Empty;
        StringBuilder sb = new StringBuilder();

        //搜尋條件
        IList<NodeVO> list = m_PostService.GetNodeListByParentId(21);

        if (list != null && list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                NodeVO vo = list[i];
                string lastCss = "";
                if (i == list.Count - 1)
                {
                    lastCss = " store_item_a_last";
                }
                sb.AppendFormat("<a href='#pro_{0}'><li class='store_item_a store_item_logo border' style=\"background-image: url('upload/{1}');\"><div class='corner_bg corner_img {2}'></div></li></a>"
                    , vo.NodeId, vo.PicFileName, lastCss);
            }
            ltlSuppliersTag.Text = sb.ToString();

            LoadSuppliers(list);
        }
    }

    private void LoadSuppliers(IList<NodeVO> supplierList)
    {
        ltlDiscounts.Text = string.Empty;
        StringBuilder sb = new StringBuilder();

        foreach (NodeVO vo in supplierList)
        {
            string supplierTable = GetSupplierTable(vo);
            string threePart = Get3gPart(vo);
            string fourPart = Get4gPart(vo);
            sb.AppendFormat(supplierTable, threePart, fourPart);
        }

        ltlDiscounts.Text = sb.ToString();
    }

    private string Get3gPart(NodeVO vo)
    {
        StringBuilder sb = new StringBuilder();

        //搜尋條件
        Dictionary<string, string> conditions = new Dictionary<string, string>();
        conditions.Add("NodeId", "5");
        conditions.Add("WarrantySuppliers", vo.Name);
        conditions.Add("Type", "0");
        conditions.Add("Flag", "1");
        conditions.Add("Order", string.Format("order by {0}", "p.WarrantySuppliers, p.Type, p.SortNo, p.PostId"));

        IList<PostVO> list = m_PostService.GetPostList(conditions);

        if (list != null && list.Count > 0)
        {
            sb.AppendFormat("<tr class='discount_list_contant'><td rowspan='{0}' class='discount_list_type discount_list_type_3G' >3G</td></tr>"
                , (list.Count + 1));

            for (int i = 0; i < list.Count; i++)
            {
                PostVO post = list[i];
                string lastCss = "";
                if (i <= list.Count - 1)
                {
                    lastCss = " discount_list_contant-last";
                }
                sb.AppendFormat("<tr class='discount_list_contant {0}'><td class='discount_list_pro'>{1}</td><td class='discount_list_dis'>{2}</td><td class='discount_list_pre'>{3}</td><td class='discount_list_tran'>{4}</td><td class='discount_list_con'>{5}</td></tr>"
                    , lastCss, post.Title, post.CustomField1, post.CustomField2, post.Summary, post.HtmlContent);
            }
        }

        return sb.ToString();
    }

    private string Get4gPart(NodeVO vo)
    {
        StringBuilder sb = new StringBuilder();

        //搜尋條件
        Dictionary<string, string> conditions = new Dictionary<string, string>();
        conditions.Add("NodeId", "5");
        conditions.Add("WarrantySuppliers", vo.Name);
        conditions.Add("Type", "1");
        conditions.Add("Flag", "1");
        conditions.Add("Order", string.Format("order by {0}", "p.WarrantySuppliers, p.Type, p.SortNo, p.PostId"));

        IList<PostVO> list = m_PostService.GetPostList(conditions);

        if (list != null && list.Count > 0)
        {
            sb.AppendFormat("<tr class='discount_list_contant'><td rowspan='{0}' class='discount_list_type discount_list_type_4G' >4G</td></tr>"
                , (list.Count + 1));

            for (int i = 0; i < list.Count; i++)
            {
                PostVO post = list[i];
                string lastCss = "";
                if (i <= list.Count - 1)
                {
                    lastCss = " discount_list_contant-last";
                }
                sb.AppendFormat("<tr class='discount_list_contant {0}'><td class='discount_list_pro'>{1}</td><td class='discount_list_dis'>{2}</td><td class='discount_list_pre'>{3}</td><td class='discount_list_tran'>{4}</td><td class='discount_list_con'>{5}</td></tr>"
                    , lastCss, post.Title, post.CustomField1, post.CustomField2, post.Summary, post.HtmlContent);
            }
        }

        return sb.ToString();
    }

    private string GetSupplierTable(NodeVO vo)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<table class='discount_list border'>");
        sb.Append("<tr class='discount_list_title border'>");
        sb.AppendFormat("<th class='discount_list_img' style=\"background-image:url('upload/{1}')\"><a name='pro_{0}' id='pro_{0}'></a></th>"
            , vo.NodeId, vo.PicFileName);
        sb.Append("<th class='discount_list_pro bg_disfrom_pro'></th>");
        sb.Append("<th class='discount_list_dis bg_disfrom_dis'></th>");
        sb.Append("<th class='discount_list_pre bg_disfrom_pre'></th>");
        sb.Append("<th class='discount_list_tran bg_disfrom_tran'></th>");
        sb.Append("<th class='discount_list_con bg_disfrom_con'></th>");
        sb.Append("</tr>");
        sb.Append("{0}");
        sb.Append("{1}");
        sb.Append("</table>");
        return sb.ToString();
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
                ltlFbComments.Text = string.Format("<div class='fb-comments' data-href=\"http://p-like.com.tw/product.aspx?id={0}\" data-width='1080' data-include-parent='false' data-numposts='5'></div>", id);

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