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

public partial class discount : System.Web.UI.Page
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
        LoadSuppliersTag();
    }

    private void LoadSuppliersTag()
    {
        ltlSuppliersTag.Text = string.Empty;
        StringBuilder sb = new StringBuilder();

        //搜尋條件
        Dictionary<string, string> conditions = new Dictionary<string, string>();
        conditions.Add("NodeId", "21");
        conditions.Add("Order", string.Format("order by {0}", "p.SortNo"));
        
        IList<NodeVO> list = m_PostService.GetNodeListByParentId(21);

        if (list != null && list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                NodeVO vo = list[i];
                string lastCss = "";
                if (i <= list.Count - 1)
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

    private void LoadSuppliers(IList<NodeVO> SupplierList)
    {
        ltlDiscounts.Text = string.Empty;
        StringBuilder sb = new StringBuilder();

        foreach (NodeVO vo in SupplierList)
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
                    ,lastCss, post.Title, post.CustomField1, post.CustomField2,post.Summary,post.HtmlContent);
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
}