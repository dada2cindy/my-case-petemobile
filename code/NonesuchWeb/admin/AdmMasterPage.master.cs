using System;
using System.Web.UI;
using Common.Logging;
using WuDada.Core.Auth.Service;
using WuDada.Core.Auth;

public partial class adm_AdmMasterPage : System.Web.UI.MasterPage
{
    private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    SessionHelper m_SessionHelper = new SessionHelper();
    AuthFactory m_AuthFactory;
    IAuthService m_AuthService;

    protected void Page_Load(object sender, EventArgs e)
    {
        m_AuthFactory = new AuthFactory();
        m_AuthService = m_AuthFactory.GetAuthService();

        if (!Page.IsPostBack)
        {
           // setPrgName();
        }
    }

    //private void setPrgName()
    //{
    //    if (!string.IsNullOrEmpty(sHelper.LogVO.Fucntion))
    //    {
    //        lblPrgName.Text = sHelper.LogVO.Fucntion;
    //        lblPrgSunName.Text = sHelper.LogVO.SubFucntion;
    //        lblTo.Visible = true;
    //    }
    //    else
    //    {
    //        lblTo.Visible = false;
    //    }
    //}

}