using System;
using System.Web;
using System.Web.UI;
using WuDada.Core.Generic.Util;

public partial class admin_top : System.Web.UI.Page
{
    private SessionHelper m_SessionHelper = new SessionHelper();
    ConfigHelper m_ConfigHelper = new ConfigHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (m_SessionHelper.LoginUser != null)
            {
                lblMsg.Text = m_SessionHelper.LoginUser.FullNameInChinese + "@" + HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] + " > 歡迎您再次登入！今天是民國" + (ConvertUtil.UtcDateTimeToTaiwanDateTime(DateTime.UtcNow).Year - 1911).ToString() + "年" + ConvertUtil.UtcDateTimeToTaiwanDateTime(DateTime.UtcNow).Month.ToString("00") + "月" + ConvertUtil.UtcDateTimeToTaiwanDateTime(DateTime.UtcNow).Day.ToString("00") + "日";
            }
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
