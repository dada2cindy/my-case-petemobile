using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using WuDada.Core.Auth;
using WuDada.Core.Auth.Domain;
using WuDada.Core.Auth.Service;
using WuDada.Core.SystemApplications;
using WuDada.Core.SystemApplications.Domain;
using WuDada.Core.SystemApplications.Service;
using WuDada.Core.Generic.Util;

public partial class adm_UC14_UC14_5_QueryLog : System.Web.UI.Page
{
    private SystemFactory m_SystemFactory;
    private AuthFactory m_AuthFactory;
    private ILogService m_LogService;
    private IAuthService m_AuthSevice;

    protected void Page_Load(object sender, EventArgs e)
    {
        m_SystemFactory = new SystemFactory();
        m_AuthFactory = new AuthFactory();
        m_LogService = m_SystemFactory.GetLogService();
        m_AuthSevice = m_AuthFactory.GetAuthService();

        if (!Page.IsPostBack)
        {
            InitData();
            fillGridView();
        }
    }

    private void InitData()
    {
        ddlClassify.Items.Add(new ListItem(""));

        ddlClassify.Items.Add(new ListItem(MsgVO.LogTitleName.登入記錄.ToString()));

        IList<MenuFuncVO> menuList = m_AuthSevice.GetTopMenuFunc();

        foreach (MenuFuncVO menu in menuList)
        {
            ListItem item = new ListItem(menu.MenuFuncName);
            ddlClassify.Items.Add(item);
        }

        ddlClassify.SelectedIndex = 0;
    }

    protected void btnQuery_Click(object sender, ImageClickEventArgs e)
    {
        fillGridView();
    }

    private void fillGridView()
    {
        //搜尋條件
        string function = ddlClassify.SelectedValue;
        DateTime? startDate = ConvertUtil.ToDateTimeMin(ConvertUtil.ToDateTime(txtDateFrom.Text));
        DateTime? endDate = ConvertUtil.ToDateTimeMax(ConvertUtil.ToDateTime(txtDateTo.Text));
        string sortField = "UpdateDate";
        bool sortDesc = true;

        //分頁
        AspNetPager1.RecordCount = m_LogService.CountLogSystem(function, startDate, endDate);
        int pageIndex = (AspNetPager1.CurrentPageIndex - 1);
        int pageSize = AspNetPager1.PageSize;        

        gvDetail.DataSource = m_LogService.GetLogSystemList(function, startDate, endDate, pageIndex, pageSize, sortField, sortDesc);
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
