using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Logging;
using WuDada.Core.Auth;
using WuDada.Core.Auth.Domain;
using WuDada.Core.Auth.Service;
using WuDada.Core.SystemApplications.Domain;
using WuDada.Core.Generic.Util;

public partial class adm_UC14_UC14_1_RoleAdd : System.Web.UI.Page
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

        if (!IsPostBack)
        {
            LoadDataToUI();
        }
    }

    private void LoadDataToUI()
    {
        GridView1.DataSource = m_AuthService.GetAllLoginRoleList();
        GridView1.DataBind();
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        lblMsg.Text = "";

        string roleName = txtRoleName.Text;
        if (String.IsNullOrEmpty(txtRoleName.Text))
        {
            return;
        }

        LoginRoleVO loginRole = new LoginRoleVO();

        loginRole.RoleName = roleName;

        m_AuthService.CreateLoginRole(loginRole);
        m_WebLogService.AddSystemLog(MsgVO.Action.新增, loginRole);

        lblMsg.Text = MsgVO.INSERT_OK;

        LoadDataToUI();
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        lblMsg.Text = "";
        clearInput();
    }

    private void clearInput()
    {
        txtRoleName.Text = "";
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridView gv = (GridView)sender;

        string roleId = e.Keys["RoleId"].ToString();
        m_Log.Debug("delete roleId=" + roleId);

        LoginRoleVO loginRole = m_AuthService.GetLoginRoleByIdNoLazy(int.Parse(roleId));


        if (loginRole.LoginUserList != null && loginRole.LoginUserList.Count > 0)
        {

            lblMsg.Text = MsgVO.USER_EXIST_ROLE;
            e.Cancel = true;
        }
        if (loginRole.MenuFuncList != null && loginRole.MenuFuncList.Count > 0)
        {
            lblMsg.Text = MsgVO.MENU_EXIST_ROLE;
            e.Cancel = true;
        }


    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string cmdName = e.CommandName;
        object argobj = e.CommandArgument;
        string roleId = "";

        if (argobj != null)
        {
            roleId = argobj.ToString();
        }
        switch (cmdName)
        {
            case "MyUpdate":

                foreach (GridViewRow row in GridView1.Rows)
                {
                    Control ctrl = row;
                    string id = UIHelper.FindHiddenValue(ref ctrl, "hdnRoleId");
                    if (roleId.Equals(id))
                    {
                        UIHelper.SetContrlVisible(ref ctrl, "lblRoleName", false);
                        UIHelper.SetContrlVisible(ref ctrl, "txtRoleName", true);
                        UIHelper.SetContrlVisible(ref ctrl, "imgUpdate", false);
                        UIHelper.SetContrlVisible(ref ctrl, "imgCancel", true);
                        UIHelper.SetContrlVisible(ref ctrl, "imgUpdateSure", true);
                    }
                    else
                    {
                        UIHelper.SetContrlVisible(ref ctrl, "lblRoleName", true);
                        UIHelper.SetContrlVisible(ref ctrl, "txtRoleName", false);
                        UIHelper.SetContrlVisible(ref ctrl, "imgUpdate", true);
                        UIHelper.SetContrlVisible(ref ctrl, "imgCancel", false);
                        UIHelper.SetContrlVisible(ref ctrl, "imgUpdateSure", false);
                    }
                }
                break;

            case "MyCancel":

                ResetGridViewUI();
                break;

            case "MyUpdateSure":
                foreach (GridViewRow row in GridView1.Rows)
                {
                    Control ctrl = row;
                    string id = UIHelper.FindHiddenValue(ref ctrl, "hdnRoleId");
                    if (roleId.Equals(id))
                    {
                        string updateName = UIHelper.FindTextBoxText(ref ctrl, "txtRoleName");

                        LoginRoleVO role = m_AuthService.GetLoginRoleById(ConvertUtil.ToInt32(roleId));

                        role.RoleName = updateName.Trim();

                        m_AuthService.UpdateLoginRole(role);
                        m_WebLogService.AddSystemLog(MsgVO.Action.修改, role);

                        UIHelper.SetLabelText(ref ctrl, "lblRoleName", role.RoleName);
                        lblMsg.Text = MsgVO.UPDATE_OK;
                        break;
                    }
                }
                ResetGridViewUI();
                break;


            case "MyDelete":
                m_Log.Debug("delete roleId=" + roleId);

                LoginRoleVO loginRole = m_AuthService.GetLoginRoleByIdNoLazy(int.Parse(roleId));
                if (loginRole.LoginUserList != null && loginRole.LoginUserList.Count > 0)
                {
                    lblMsg.Text = MsgVO.USER_EXIST_ROLE;
                    return;
                }
                if (loginRole.MenuFuncList != null && loginRole.MenuFuncList.Count > 0)
                {
                    lblMsg.Text = MsgVO.MENU_EXIST_ROLE;
                    return;
                }
                m_AuthService.DeleteLoginRole(loginRole);
                m_WebLogService.AddSystemLog(MsgVO.Action.刪除, loginRole);
                lblMsg.Text = MsgVO.DELETE_OK;
                LoadDataToUI();
                break;
        }
    }

    private void ResetGridViewUI()
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            Control ctrl = row;

            UIHelper.SetContrlVisible(ref ctrl, "lblRoleName", true);
            UIHelper.SetContrlVisible(ref ctrl, "txtRoleName", false);
            UIHelper.SetContrlVisible(ref ctrl, "imgUpdate", true);
            UIHelper.SetContrlVisible(ref ctrl, "imgCancel", false);
            UIHelper.SetContrlVisible(ref ctrl, "imgUpdateSure", false);

        }
    }
}
