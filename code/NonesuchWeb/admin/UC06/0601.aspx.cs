using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WuDada.Core.Post;
using WuDada.Core.Post.Service;
using WuDada.Core.Generic.Util;

public partial class admin_UC06_0601 : System.Web.UI.Page
{
    private PostFactory m_PostFactory;
    private IMessageService m_MessageService;

    protected void Page_Load(object sender, EventArgs e)
    {
        m_PostFactory = new PostFactory();
        m_MessageService = m_PostFactory.GetMessageService();

        if (!Page.IsPostBack)
        {
            fillGridView();
        }
    }

    protected void btnQuery_Click(object sender, ImageClickEventArgs e)
    {
        fillGridView();
    }

    private void fillGridView()
    {
        //搜尋條件
        DateTime? startDate = ConvertUtil.ToDateTimeMin(ConvertUtil.ToDateTime(txtDateFrom.Text));
        DateTime? endDate = ConvertUtil.ToDateTimeMax(ConvertUtil.ToDateTime(txtDateTo.Text));
        string sortField = "CreatedDate";
        bool sortDesc = true;

        //分頁
        AspNetPager1.RecordCount = m_MessageService.CountMessage(string.Empty, startDate, endDate);
        int pageIndex = (AspNetPager1.CurrentPageIndex - 1);
        int pageSize = AspNetPager1.PageSize;

        gvDetail.DataSource = m_MessageService.GetMessageList(string.Empty, startDate, endDate, pageIndex, pageSize, sortField, sortDesc);
        gvDetail.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        fillGridView();
    }


    protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //e.Row.Attributes.Add("onmouseover", "javascript:__doPostBack('ctl00$ContentPlaceHolder1$gvDetail','Select$" + e.Row.RowIndex + "')");

    }
}