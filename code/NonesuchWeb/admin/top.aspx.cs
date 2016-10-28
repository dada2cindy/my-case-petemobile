using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using WuDada.Core.Generic.Util;
using WuDada.Core.Post;
using WuDada.Core.Post.Domain;
using WuDada.Core.Post.Service;

public partial class admin_top : System.Web.UI.Page
{
    private SessionHelper m_SessionHelper = new SessionHelper();
    ConfigHelper m_ConfigHelper = new ConfigHelper();
    private PostFactory m_PostFactory;
    private IPostService m_PostService;

    protected void Page_Load(object sender, EventArgs e)
    {
        m_PostFactory = new PostFactory();
        m_PostService = m_PostFactory.GetPostService();

        if (!Page.IsPostBack)
        {
            if (m_SessionHelper.LoginUser != null)
            {
                lblMsg.Text = m_SessionHelper.LoginUser.FullNameInChinese + "@" + HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] + " > 歡迎您再次登入！今天是民國" + (ConvertUtil.UtcDateTimeToTaiwanDateTime(DateTime.UtcNow).Year - 1911).ToString() + "年" + ConvertUtil.UtcDateTimeToTaiwanDateTime(DateTime.UtcNow).Month.ToString("00") + "月" + ConvertUtil.UtcDateTimeToTaiwanDateTime(DateTime.UtcNow).Day.ToString("00") + "日";
            }
            IList<NodeVO> storeList = m_PostService.GetNodeListByParentName("店家");
            ltlTitle.Text = string.Format("<title> 品讚行動通訊聯合系統-{0}</title>", storeList[0].Name);
            ltlTitle2.Text = string.Format("<品讚行動通訊聯合系統-{0}", storeList[0].Name);
        }
    }
    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        logout();
    }
    private void logout()
    {
        m_SessionHelper.LoginUser = null;
        Session.Clear();
        string url = string.Format("top.document.location.href='../admin/Login/Login.aspx';", m_ConfigHelper.Host);
        //string url = string.Format("top.document.location.href='http://{0}';", m_ConfigHelper.Host);
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "gotoJs", JavascriptUtil.WrapperScript(url), false);
        return;
    }
  
}
