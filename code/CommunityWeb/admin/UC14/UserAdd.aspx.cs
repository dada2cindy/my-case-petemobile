using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHibernate;
using Common.Logging;
using WuDada.Core.Auth;
using WuDada.Core.Auth.Service;
using WuDada.Core.Auth.Domain;
using WuDada.Core.SystemApplications.Domain;

public partial class adm_UC14_UC14_1_UserAdd : System.Web.UI.Page
{
    private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private AuthFactory m_AuthFactory;
    private IAuthService m_AuthService;
    private WebLogService m_WebLogService;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        m_WebLogService = new WebLogService();
        m_AuthFactory = new AuthFactory();
        m_AuthService = m_AuthFactory.GetAuthService();

        //載入清單
        LoadDataToUI();

        //新增模式
        ToInsertMode();
    }

    private void LoadDataToUI()
    {
        GridView1.DataSource = m_AuthService.GetLoginUserList("1=1 ORDER BY ArrivedDate");
        GridView1.DataBind();
    }

    private void ToInsertMode()
    {
        btnUpdate.Visible = false;
        btnAdd.Visible = true;
        txtId.ReadOnly = false;
    }

    private void ToUpdateMode()
    {
        btnUpdate.Visible = true;
        btnAdd.Visible = false;
        txtId.ReadOnly = true;
    }

    protected void btnReset_Click(object sender, ImageClickEventArgs e)
    {
        clearInput();
    }
    protected void btnAdd_Click(object sender, ImageClickEventArgs e)
    {
        lblMsg.Text = string.Empty;
        //轉成小寫
        string id = txtId.Text.Trim().ToLower();
        string nameInChinese = txtFullNameInChinese.Text.Trim();
        string nameInEnglish = txtFullNameInEnglish.Text.Trim();
        int IsAlive = int.Parse(rdbIsValidAccount.SelectedValue);
        int ShowInSalesStatistics = int.Parse(rdbShowInSalesStatistics.SelectedValue);

        string mobie = txtMobile.Text.Trim();
        string ssid = txtSSID.Text.Trim();
        string email = txtEmail.Text.Trim();
        string address = txtAddress.Text.Trim();

        LoginUserVO user = m_AuthService.GetLoginUserById(id);

        if (user != null)
        {
            lblMsg.Text = MsgVO.USER_ALREADY_EXIST;
            return;
        }
        else
        {
            LoginUserVO newUser = new LoginUserVO();
            newUser.UserId = id;
            newUser.FullNameInChinese = nameInChinese;
            newUser.FullNameInEnglish = nameInEnglish;
            newUser.IsAlive = IsAlive;
            newUser.ShowInSalesStatistics = ShowInSalesStatistics;
            newUser.Mobile = mobie;
            newUser.SSID = ssid;
            newUser.Email = email;
            newUser.ContactAddress = address;
            newUser.Password = "1234";
            newUser.CreateDate = DateTime.Now;
            m_AuthService.CreateLoginUser(newUser);
            m_WebLogService.AddSystemLog(MsgVO.Action.新增, newUser);
            lblMsg.Text = MsgVO.INSERT_OK;
            clearInput();
            LoadDataToUI();
        }

    }

    private void clearInput()
    {
        txtId.Text = string.Empty;
        txtFullNameInChinese.Text = string.Empty;
        txtFullNameInEnglish.Text = string.Empty;
        txtMobile.Text = string.Empty;
        txtSSID.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtAddress.Text = string.Empty;
        hdnVersion.Value = string.Empty;
        rdbIsValidAccount.SelectedValue = "1";
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        string id = txtId.Text;
        string nameInChinese = txtFullNameInChinese.Text.Trim();
        string nameInEnglish = txtFullNameInEnglish.Text.Trim();
        string version = hdnVersion.Value;
        int isAlive = int.Parse(rdbIsValidAccount.SelectedValue);
        int showInSalesStatistics = int.Parse(rdbShowInSalesStatistics.SelectedValue);
        string mobie = txtMobile.Text.Trim();
        string ssid = txtSSID.Text.Trim();
        string email = txtEmail.Text.Trim();
        string address = txtAddress.Text.Trim();

        LoginUserVO user = m_AuthService.GetLoginUserById(id);
        user.FullNameInChinese = nameInChinese;
        user.FullNameInEnglish = nameInEnglish;
        user.Version = Int32.Parse(version);
        user.IsAlive = isAlive;
        user.ShowInSalesStatistics = showInSalesStatistics;
        user.Mobile = mobie;
        user.SSID = ssid;
        user.Email = email;

        try
        {
            m_AuthService.UpdateLoginUser(user);
            m_WebLogService.AddSystemLog(MsgVO.Action.修改, user);

            lblMsg.Text = MsgVO.UPDATE_OK;

            clearInput();
            ToInsertMode();
            GridView1.DataBind();
        }
        catch (StaleObjectStateException ex)
        {
            m_Log.Info(ex);
            lblMsg.Text = MsgVO.STALE_EXCEPTION_MSG;
            clearInput();
        }

    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = string.Empty;
        string userId = string.Empty;

        switch (e.CommandName)
        {
            case "MyEdit":
                userId = e.CommandArgument.ToString();
                m_Log.Debug("get UserId=" + userId);
                LoadVoToUI(userId);
                ToUpdateMode();
                break;

            case "MyDelete":
                userId = e.CommandArgument.ToString();
                m_Log.Debug("get UserId=" + userId);
                LoginUserVO user = m_AuthService.GetLoginUserById(userId);
                m_AuthService.DeleteLoginUser(user);
                m_WebLogService.AddSystemLog(MsgVO.Action.刪除, user);
                lblMsg.Text = MsgVO.DELETE_OK;
                LoadDataToUI();
                break;

            case "MySelect":
                Response.Redirect("~/admin/UC14/UserRoleSet.aspx?UserId=" + e.CommandArgument.ToString());
                break;
        }
    }


    private void LoadVoToUI(string userId)
    {
        LoginUserVO user = m_AuthService.GetLoginUserById(userId);
        txtId.Text = user.UserId;
        txtFullNameInChinese.Text = user.FullNameInChinese;
        txtFullNameInEnglish.Text = user.FullNameInEnglish;
        hdnVersion.Value = user.Version.ToString();

        rdbIsValidAccount.SelectedValue = user.IsAlive.ToString();
        rdbShowInSalesStatistics.SelectedValue = user.ShowInSalesStatistics.ToString();

        txtMobile.Text = user.Mobile;
        txtSSID.Text = user.SSID;
        txtEmail.Text = user.Email;
        txtAddress.Text = user.ContactAddress;

    }
}
