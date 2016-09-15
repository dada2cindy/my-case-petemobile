using System.Linq; 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Logging;
using WuDada.Core.Auth;
using WuDada.Core.Auth.Container;
using WuDada.Core.Auth.Domain;
using WuDada.Core.Auth.Service;
using WuDada.Core.SystemApplications.Domain;

public partial class adm_UC14_UC14_4_RoleFuncSet : System.Web.UI.Page
{    
    ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    AuthFactory m_AuthFactory;
    IAuthService m_AuthService;
    private WebLogService m_WebLogService;

    protected void Page_Load(object sender, EventArgs e)
    {
        m_WebLogService = new WebLogService();
        m_AuthFactory = new AuthFactory();
        m_AuthService = m_AuthFactory.GetAuthService();

        if (!Page.IsPostBack)
        {
            initData();

        }
    }

    private void initData()
    {
        initDDLRole();
        initDDLMenuFunc();
        initGridView();
    }



    private void initDDLMenuFunc()
    {
        IList<MenuFuncVO> MenuFuncList = m_AuthService.GetTopMenuFunc();


        foreach (MenuFuncVO menu in MenuFuncList)
        {
            ListItem item = new ListItem(menu.MenuFuncName, menu.MenuFuncId.ToString());
            ddlMenuFunc.Items.Add(item);
        }
        ddlMenuFunc.Items.Add(new ListItem("全部", "0"));
        ddlMenuFunc.DataBind();


    }

    private void initDDLRole()
    {
        IList<LoginRoleVO> roleList = m_AuthService.GetAllLoginRoleList();

        foreach (LoginRoleVO role in roleList)
        {
            ListItem item = new ListItem(role.RoleName, role.RoleId.ToString());
            ddlRole.Items.Add(item);
        }

        ddlRole.DataBind();
    }

    private void initGridView()
    {
        btnSearch_Click(null, null);
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        string selectedRole = ddlRole.SelectedValue;
        string selectedMenuFunc = ddlMenuFunc.SelectedValue;
        IList<RoleMenuShowVO> showVOList = new List<RoleMenuShowVO>();

        m_Log.Debug("selectedRole=" + selectedRole);
        m_Log.Debug("selectedMenuFunc=" + selectedMenuFunc);

        LoginRoleVO role = m_AuthService.GetLoginRoleByIdNoLazy(int.Parse(selectedRole));

        IList<MenuFuncVO> selectedMenuFuncList;


        if (selectedMenuFunc.Equals("0"))
        {
            selectedMenuFuncList = m_AuthService.GetNotTopMenuFunc();
        }
        else
        {
            selectedMenuFuncList = m_AuthService.GetMenuFuncByParentId(int.Parse(selectedMenuFunc));
        }

        foreach (MenuFuncVO menu in selectedMenuFuncList)
        {
            RoleMenuShowVO showVO = new RoleMenuShowVO();

            showVO.Id = menu.MenuFuncId;
            showVO.Name = menu.MenuFuncName;
            showVO.No = menu.Note;
            showVO.IsAuth = m_AuthService.RoleHasMenuRight(role, menu);

            showVOList.Add(showVO);
        }

        gvAuth.DataSource = showVOList;
        gvAuth.DataBind();
    }

    protected void ddlMenuFunc_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSearch_Click(null, null);
    }
    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSearch_Click(null, null);
    }

    protected void ckAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ckAll = (CheckBox)sender;

        if (ckAll.ID.Equals("ckAll"))
        {
            foreach (GridViewRow row in gvAuth.Rows)
            {
                Control ctrl = row;

                UIHelper.SetCheckBoxChecked(ref ctrl, "ckIsAuth", ckAll.Checked);
            }
        }
    }

    protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        string selectedRole = ddlRole.SelectedValue;
        LoginRoleVO role = m_AuthService.GetLoginRoleByIdNoLazy(int.Parse(selectedRole));

        foreach (GridViewRow row in gvAuth.Rows)
        {
            CheckBox ckAuth = (CheckBox)row.FindControl("ckIsAuth");
            HiddenField hdnId = (HiddenField)row.FindControl("hdnId");

            if (ckAuth.Checked == true)
            {
                if (role.MenuFuncList == null)
                {
                    role.MenuFuncList = new List<MenuFuncVO>();
                }

                MenuFuncVO theFunc = m_AuthService.GetMenuFuncById(int.Parse(hdnId.Value));

                if (!m_AuthService.RoleHasMenuRight(role, theFunc))
                {
                    role.MenuFuncList.Add(theFunc);
                }
            }
            else 
            {
                MenuFuncVO theFunc = m_AuthService.GetMenuFuncById(int.Parse(hdnId.Value));

                if (m_AuthService.RoleHasMenuRight(role, theFunc))
                {
                    //role.MenuFuncList.Remove(theFunc);
                    role.MenuFuncList.Remove(role.MenuFuncList.Where(p => p.MenuFuncId == int.Parse(hdnId.Value)).ToList()[0]);
                }
            }
        }

        m_AuthService.UpdateLoginRole(role);
        m_WebLogService.AddSystemLog(MsgVO.Action.修改, role);

        UserMenuFuncContainer.GetInstance().ResetAll() ;

        lblMsg.Text = MsgVO.UPDATE_OK;

    }
}

