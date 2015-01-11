using System;
using System.Web.UI;
using WuDada.Core.Auth;
using WuDada.Core.Auth.Domain;
using WuDada.Core.Auth.Service;
using WuDada.Core.SystemApplications.Domain;

public partial class admin_UC30_3001Personal : System.Web.UI.Page
{
    private SessionHelper m_SessionHelper = new SessionHelper();
    private AuthFactory m_AuthFactory;
    private IAuthService m_AuthService;
    private WebLogService m_WebLogService;

    protected void Page_Load(object sender, EventArgs e)
    {
        m_WebLogService = new WebLogService();
        m_AuthFactory = new AuthFactory();
        m_AuthService = m_AuthFactory.GetAuthService();
    }

    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        LoginUserVO user = m_AuthService.GetLoginUserById(m_SessionHelper.LoginUser.UserId);
        if (txtOldPassword.Text != user.Password)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "js", JavascriptUtil.AlertJS(MsgVO.PASSWORD_WRONG), false);
            return;
        }
        else//txtOldPassword.Text == sHelper.LoginUser.Password
        {
            user.Password = txtNewPassword.Text;
            m_AuthService.UpdateLoginUser(user);
            m_WebLogService.AddSystemLog(MsgVO.Action.修改, user);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "js", JavascriptUtil.AlertJS(MsgVO.UPDATE_OK), false);
        }
    }
}
