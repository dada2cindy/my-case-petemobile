using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Logging;
using WuDada.Core.Auth.Service;
using WuDada.Core.Auth;
using WuDada.Core.Auth.Domain;
using WuDada.Core.Auth.Container;

public partial class admin_leftmenu : System.Web.UI.Page
{
    private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private SessionHelper m_SessionHelper = new SessionHelper();
    private AuthFactory m_AuthFactory;
    private IAuthService m_AuthService;

    protected void Page_Load(object sender, EventArgs e)
    {
        m_AuthFactory = new AuthFactory();
        m_AuthService = m_AuthFactory.GetAuthService();

        if (!Page.IsPostBack)
        {
            initMenu();

            new TreeViewState().RestoreTreeView(tvMenu, this.GetType().ToString());

            //初始選單下方左邊資料
            initLeftData();

        }
    }

    private void initMenu()
    {
        LoginUserVO user = m_SessionHelper.LoginUser;

        //快取載入
        UserMenuFuncContainer userContainer = UserMenuFuncContainer.GetInstance();

        if (user == null)
        {
            Response.Redirect(UIHelper.LOGIN_PAGE_MANAGER, false);
            return;
        }
        user = userContainer.GetUser(user.UserId);

        //TreeveiwService tvService = new TreeveiwService();

        IList<MenuFuncVO> menuFuncList = m_AuthService.GetTopMenuFunc(user, userContainer.AllMenu, userContainer.RoleDic);

        foreach (MenuFuncVO menu in menuFuncList)
        {
            TreeNode treeNode = new TreeNode(menu.MenuFuncName, menu.MenuFuncId.ToString(), "", "welcome.aspx", "mainfrm");

            if (menu.SubFuncs.Count > 0)
            {
                foreach (MenuFuncVO subMenu in menu.SubFuncs)
                {
                    if (string.IsNullOrEmpty(subMenu.Note) || subMenu.Note.ToLower().IndexOf("_sub") == -1)
                    {
                        TreeNode subTreeNode = new TreeNode(subMenu.MenuFuncName, null, null, "~/" + subMenu.MainPath, "mainfrm");
                        treeNode.ChildNodes.Add(subTreeNode);
                    }
                }
            }

            tvMenu.Nodes.Add(treeNode);
          
        }

        if (tvMenu.Nodes != null && tvMenu.Nodes.Count > 0)
        {
            tvMenu.Nodes[0].Expand();
            tvMenu.DataBind();
        }
    }
    protected void tvMenu_SelectedNodeChanged(object sender, EventArgs e)
    {
        m_Log.Debug("tvMenu.SelectedNode.Value=" + tvMenu.SelectedNode.Value);
        if (tvMenu.SelectedNode.Value != string.Empty)
        {

            Response.Redirect("~/" + tvMenu.SelectedNode.Value, false);
        }
        else 
        {
            tvMenu.SelectedNode.ExpandAll();
        }
    }
    protected void tvMenu_Unload(object sender, EventArgs e)
    {
        //  log.Debug("enter tvMenu_Unload");
        new TreeViewState().SaveTreeView(tvMenu, this.GetType().ToString());
    }


    private void initLeftData()
    {
        LoginUserVO user = m_SessionHelper.LoginUser;

        if (user != null)
        {
       //     lblUserId.Text = user.UserId;
            //快取載入
             user = UserMenuFuncContainer.GetInstance().GetUser(user.UserId);

            //user = m_AuthService.GetLoginUserByIdNoLazy(user.UserId);

            IList<LoginRoleVO> roleList = user.LoginRoleList;

            List<string> roleStr = new List<string>();

            if (roleList != null && roleList.Count > 0)
            {
                foreach (LoginRoleVO role in roleList)
                {
                    roleStr.Add(role.RoleName);
                }
            }

       //     lblRole.Text = String.Join(",", roleStr.ToArray());
        }
        else
        {
            Response.Redirect(UIHelper.LOGIN_PAGE_MANAGER, false);
            return;
        }
    }
    protected void btnLogout_Click(object sender, ImageClickEventArgs e)
    {
        m_SessionHelper.LoginUser = null;

        Response.Redirect(UIHelper.LOGIN_PAGE_MANAGER, false);
        return;
    }
    protected void btnLogout_Click1(object sender, EventArgs e)
    {
    
        logout();
    }
    protected void btnLogout_Click1(object sender, ImageClickEventArgs e)
    {
        logout();
    }

    private void logout()
    {
        //sHelper.LoginUser = null;
        //Session.Clear();
        //string url = string.Format("top.document.location.href='http://{0}';", configHelper.Host);
        //ScriptManager.RegisterClientScriptBlock(lblGoto, lblGoto.GetType(), "gotoJs", JavascriptUtil.WrapperScript(url), false);
        //return;
    }

}
