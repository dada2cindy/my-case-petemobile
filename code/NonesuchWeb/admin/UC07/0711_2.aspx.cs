using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Logging;
using WuDada.Core.SystemApplications.Domain;
using System.Collections.Generic;
using System.Data;
using WuDada.Core.Auth;
using WuDada.Core.Auth.Service;
using WuDada.Core.Auth.Domain;
using WuDada.Core.Member;
using WuDada.Core.Member.Service;
using WuDada.Core.Member.Domain;
using WuDada.Core.Post.Service;
using WuDada.Core.Post;
using WuDada.Core.Post.Domain;
using WuDada.Core.Accounting.Service;
using WuDada.Core.Accounting.Domain;

public partial class admin_UC07_0711_2 : System.Web.UI.Page
{
    private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private PostFactory m_PostFactory;
    private MemberFactory m_MemberFactory;
    private IMemberService m_MemberService;
    private AuthFactory m_AuthFactory;
    private IAuthService m_AuthService;
    private WebLogService m_WebLogService;
    private HttpHelper m_HttpHelper;
    private IPostService m_PostService;
    private SessionHelper m_SessionHelper;
    private AccountingFactory m_AccountingFactory;
    private IAccountingService m_AccountingService;

    private int m_Mode
    {
        get { if (ViewState["mode"] == null) { ViewState["mode"] = 0; } return int.Parse(ViewState["mode"].ToString()); }
        set { ViewState["mode"] = value; }
    }
    private string m_PicFileName
    {
        get { if (ViewState["picfilename"] == null) { ViewState["picfilename"] = string.Empty; } return ViewState["picfilename"].ToString(); }
        set { ViewState["picfilename"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        m_WebLogService = new WebLogService();
        m_PostFactory = new PostFactory();
        m_MemberFactory = new MemberFactory();
        m_AuthFactory = new AuthFactory();
        m_HttpHelper = new HttpHelper();
        m_SessionHelper = new SessionHelper();
        m_AccountingFactory = new AccountingFactory();
        m_AuthService = m_AuthFactory.GetAuthService();
        m_MemberService = m_MemberFactory.GetMemberService();
        m_PostService = m_PostFactory.GetPostService();
        m_AccountingService = m_AccountingFactory.GetAccountingService();

        if (!IsPostBack)
        {            
            ShowMode();
                       
            fillGridView();
        }
    }

    private void ShowMode()
    {
        if (!m_SessionHelper.IsAdmin)
        {
            
        }
    }

    private void fillGridView()
    {
        ////先更新到今天之前的結帳
        m_AccountingService.UpdateCash();
    }    

    

    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //GridView gv = (GridView)sender;
        //if (e.Row.RowIndex != -1)
        //{
        //    Control ctrl = e.Row;
        //    Label lblName = (Label)ctrl.FindControl("lblName");

        //    if (m_SessionHelper.IsAdmin && !"總合".Equals(lblName.Text))
        //    {
        //        UIHelper.SetContrlVisible(ref ctrl, "lblTarget", false);
        //        UIHelper.SetContrlVisible(ref ctrl, "txtTarget", true);
        //    }
        //    else
        //    {
        //        UIHelper.SetContrlVisible(ref ctrl, "lblTarget", true);
        //        UIHelper.SetContrlVisible(ref ctrl, "txtTarget", false);
        //    }


        //    Label lblTargetAchievementRates = (Label)ctrl.FindControl("lblTargetAchievementRates");
        //    double rates = double.Parse(lblTargetAchievementRates.Text.Trim());
        //    if (rates < 60)
        //    {
        //        lblTargetAchievementRates.ForeColor = System.Drawing.Color.Red;
        //    }            
        //}
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
}