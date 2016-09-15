using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Logging;
using WuDada.Core.Auth;
using WuDada.Core.Auth.Service;
using WuDada.Core.Auth.Domain;
using WuDada.Core.Auth.Container;
using WuDada.Core.SystemApplications.Domain;

public partial class adm_UC14_UC14_3_UserRoleSet : System.Web.UI.Page
{
    private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private AuthFactory m_AuthFactory;
    private IAuthService m_AuthService;
    private Hashtable m_ToBeApplyRole = new Hashtable();
    private WebLogService m_WebLogService;

    protected void Page_Load(object sender, EventArgs e)
    {
        m_WebLogService = new WebLogService();
        m_AuthFactory = new AuthFactory();
        m_AuthService = m_AuthFactory.GetAuthService();


        string userId = Request.QueryString["UserId"];
        if (!Page.IsPostBack) 
        {
            initData(userId);
        }
    }

    
    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        string userId = ddlUser.SelectedValue;
        initData(userId);
    }
   
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        int selectedIndex = lbxHadRole.SelectedIndex;
        m_Log.Debug("selected user = " + selectedIndex.ToString());

        if (selectedIndex != -1)
        {

            ListItem selectedItem = lbxHadRole.SelectedItem;
            lbxHadRole.Items.Remove(selectedItem);
            lblxToBeRole.Items.Add(selectedItem);
        }
        lbxHadRole.DataBind();
        lblxToBeRole.DataBind();

    }

    private void initData(string userId)
    {
        lblMsg.Text = "";
        ddlUser.Items.Clear();
        lblxToBeRole.Items.Clear();
        lbxHadRole.Items.Clear();

        initDDlUser(userId);
        m_Log.Debug("original selected user = " + ddlUser.SelectedValue);
        string selectedUserId = ddlUser.SelectedValue;
        initRight(selectedUserId);
        initLeft(selectedUserId);

   }

    /// <summary>
    /// 初始化右邊視窗
    /// </summary>
    /// <param name="selectedUserId"></param>
    private void initLeft(string selectedUserId)
    {
        IList<LoginRoleVO> allRoleList = m_AuthService.GetAllLoginRoleList();

        LoginUserVO loginUser = m_AuthService.GetLoginUserByIdNoLazy(selectedUserId);

        foreach (LoginRoleVO role in allRoleList)
        {
            if (loginUser.LoginRoleList == null || !loginUser.LoginRoleList.Contains(role))
            {
                ListItem item = new ListItem(role.RoleName, role.RoleId.ToString());
                lblxToBeRole.Items.Add(item);
            }
        }

        lblxToBeRole.DataBind();
    }


    /// <summary>
    /// 初始化左邊視窗
    /// </summary>
    /// <param name="selectedUserId"></param>
    private void initRight(string selectedUserId)
    {
        LoginUserVO loginUser = m_AuthService.GetLoginUserByIdNoLazy(selectedUserId);
        IList<LoginRoleVO> roleList = loginUser.LoginRoleList;
        if (roleList != null)
        {
            foreach (LoginRoleVO role in roleList)
            {
                ListItem item = new ListItem(role.RoleName, role.RoleId.ToString());
                lbxHadRole.Items.Add(item);
            }
        }

        lbxHadRole.DataBind();

    }

    /// <summary>
    /// 初始化User下拉
    /// </summary>
    /// <param name="userId"></param>
    private void initDDlUser(string userId)
    {
        IList<LoginUserVO> users = m_AuthService.GetAllLoginUserList();
        foreach (LoginUserVO user in users) 
        {
            ListItem item = new ListItem(user.FullNameInChinese, user.UserId);
            ddlUser.Items.Add(item);
          
        }

        if (string.IsNullOrEmpty(userId))
        {
            ddlUser.SelectedIndex = 0;
        }
        else
        {
            ddlUser.SelectedValue = userId;
        }
        
        ddlUser.DataBind();
    }

    protected void lblxToBeRole_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void lbxHadRole_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        int selectedIndex = lblxToBeRole.SelectedIndex;
        m_Log.Debug("selected user = " + selectedIndex.ToString());

        if (selectedIndex != -1) 
        {

            ListItem selectedItem = lblxToBeRole.SelectedItem;
            lblxToBeRole.Items.Remove(selectedItem);
            lbxHadRole.Items.Add(selectedItem);
        }  
        lbxHadRole.DataBind();
        lblxToBeRole.DataBind();
    }
    protected void Button3_Click(object sender, ImageClickEventArgs e)
    {
        string userId = ddlUser.SelectedValue;
        LoginUserVO user = m_AuthService.GetLoginUserByIdNoLazy(userId);
        List<LoginRoleVO> loginRoleList = new List<LoginRoleVO>();

        foreach (ListItem item in lbxHadRole.Items)
        {
            loginRoleList.Add(m_AuthService.GetLoginRoleById(int.Parse(item.Value)));
        }

        user.LoginRoleList = loginRoleList;

        m_AuthService.UpdateLoginUser(user);
        m_WebLogService.AddSystemLog(MsgVO.Action.修改, user);

        //更新快取
        UserMenuFuncContainer.GetInstance().ResetAll();

        lblMsg.Text = MsgVO.UPDATE_OK;
    }
}
