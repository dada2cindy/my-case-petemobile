using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using Common.Logging;
using WuDada.Core.Auth;
using WuDada.Core.Auth.Container;
using WuDada.Core.Auth.Domain;
using WuDada.Core.Auth.Service;

/// <summary>
/// AuthModule 的摘要描述
/// </summary>
/// 
public class AuthModule : IHttpModule, IRequiresSessionState
{
    ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private readonly string MANAGE_LOGIN_PATH = "~/admin/login/Login.aspx";
    private readonly string MANAGE_NOAUTH_PATH = "~/admin/login/NoAuth.aspx";
    private readonly string MANAGE_ERROR = "~/admin/login/Error.aspx";
    public AuthModule()
    {

    }
    #region Fields and Properties

    #endregion

    #region IHttpModule Members

    public void Init(HttpApplication application)
    {
        application.AcquireRequestState += new EventHandler(Authorization);

    }

    public void Dispose() { }

    #endregion

    public void Authorization(object sender, EventArgs e)
    {
        HttpApplication application = (HttpApplication)sender;
        Uri uri = application.Request.Url;

        string rawUrl = application.Request.RawUrl;

        //只欄aspx
        if (rawUrl.IndexOf("aspx") == -1)
        {
            return;
        }


        //登入頁面不檢查權限
        if (rawUrl.IndexOf("Login.aspx") != -1 || rawUrl.IndexOf("NoAuth.aspx") != -1 || rawUrl.IndexOf("leftmenu.aspx") != -1 || rawUrl.IndexOf("welcome.aspx") != -1 || rawUrl.IndexOf("Error.aspx") != -1 || rawUrl.IndexOf("toggle.aspx") != -1 || rawUrl.IndexOf("top.aspx") != -1 || rawUrl.IndexOf("connector.aspx") != -1)
        {
            return;
        }

        m_Log.Debug("Uri=" + uri.ToString());


        CheckAuth(application, uri, rawUrl);
    }

    /// <summary>
    /// 檢查權限
    /// </summary>
    /// <param name="application"></param>
    /// <param name="uri"></param>
    /// <param name="rawUrl"></param>
    private void CheckAuth(HttpApplication application, Uri uri, string rawUrl)
    {
        SessionHelper sHelper = new SessionHelper();

        LoginUserVO loginUser = sHelper.LoginUser;

        string applicationPath = application.Request.ApplicationPath;

        string mamagePath = String.IsNullOrEmpty(applicationPath) ? "/admin" : applicationPath + "/admin";
        mamagePath = mamagePath.Replace("//", "/");

        if (rawUrl.StartsWith(mamagePath) == true)
        {
            AuthFactory authFactory = new AuthFactory();
            IAuthService authService = authFactory.GetAuthService();

            if (loginUser == null)
            {
                toLoginPage(application.Response);
                return;
            }

            string userId = loginUser.UserId;

            //判斷只有主路徑是否有權限
            //if (!PathHasRight(UserMenuFuncContainer.GetInstance().GetUser(userId), uri, UserMenuFuncContainer.GetInstance().PathFunc))
            //{
            //    toLoginNoAuthPage(application.Response);
            //}


            //判斷所有路徑是否有權限                          
            if (!authService.PathHasAuth(UserMenuFuncContainer.GetInstance().GetUser(userId), uri))
            {
                toLoginNoAuthPage(application.Response);
            }
        }
    }


    private bool PathHasRight(LoginUserVO loginUser, Uri uri, Dictionary<string, List<int>> pathFunc)
    {
        string url = uri.ToString();

        foreach (string path in pathFunc.Keys)
        {
            if (url.IndexOf(path) != -1)
            {
                m_Log.Fatal(path.IndexOf(url));

                IList<int> funIdList = pathFunc[path];

                //判斷是否有此功能權限

                if (loginUser.LoginRoleList != null && loginUser.LoginRoleList.Count > 0)
                {
                    foreach (LoginRoleVO role in loginUser.LoginRoleList)
                    {
                        if (role.MenuFuncList != null && role.MenuFuncList.Count > 0)
                        {
                            foreach (MenuFuncVO roleMenuFunc in role.MenuFuncList)
                            {
                                foreach (int id in funIdList)
                                {
                                    if (id == roleMenuFunc.MenuFuncId)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }

                    //若未有權限 則丟回false
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }

        return true;

    }

    private void toLoginPage(HttpResponse httpResponse)
    {
        httpResponse.Redirect(MANAGE_ERROR, false);
        return;
    }
    private void toLoginNoAuthPage(HttpResponse httpResponse)
    {
        httpResponse.Redirect(MANAGE_NOAUTH_PATH, false);
        return;
    }

}