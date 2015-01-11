using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using Common.Logging;
using WuDada.Core.Auth;
using WuDada.Core.Auth.Domain;
using WuDada.Core.Auth.Service;
using WuDada.Core.SystemApplications.Domain;

/// <summary>
/// AuthModule 的摘要描述
/// </summary>
/// 
public class LogModule : IHttpModule, IRequiresSessionState
{
    ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public LogModule()
    {

    }
    #region Fields and Properties

    #endregion

    #region IHttpModule Members

    public void Init(HttpApplication application)
    {
        application.AcquireRequestState += new EventHandler(FillLog);

    }

    public void Dispose() { }

    #endregion

    public void FillLog(object sender, EventArgs e)
    {

        HttpApplication application = (HttpApplication)sender;

        string rawUrl = application.Request.RawUrl;

        rawUrl = rawUrl.ToLower();

        //只欄aspx
        if (rawUrl.IndexOf(".aspx") == -1)
        {
            return;
        }

        if (rawUrl.IndexOf("schema.aspx") != -1)
        {
            return;
        }


        //將function Name存入log                        

        if (!string.IsNullOrEmpty(rawUrl))
        {
            string path = rawUrl;

            string phypath = application.Request.ApplicationPath.ToLower();

            if (!string.IsNullOrEmpty(phypath) && !phypath.Equals("/"))
            {
                path = path.Replace(phypath, "");
            }

            if (path.StartsWith("/"))
            {
                path = path.Substring(1);
            }


            AuthFactory authFactory = new AuthFactory();
            IAuthService authService = authFactory.GetAuthService();

            IList<MenuFuncVO> menuFuncList = authService.GetMenuFuncList("as vo where vo.MainPath='" + path + "'", 0, 1);

            //checkMainPath
            if (menuFuncList != null && menuFuncList.Count > 0)
            {
                FillLog(menuFuncList[0]);
            }

            ////check functionPath                
            //IList<FunctionPathVO> fpaths = authService.DaoGetByWhere<FunctionPath>("as vo where vo.Path='" + path + "'", 0, 1);
            //if (fpaths != null && fpaths.Count > 0)
            //{
            //    FillLog(fpaths[0].BelongMenuFunc);
            //}


        }
    }

    private void FillLog(MenuFuncVO m)
    {
        AuthFactory authFactory = new AuthFactory();
        IAuthService authService = authFactory.GetAuthService();

        MenuFuncVO parentMenu = authService.GetMenuFuncById(m.ParentMenu.MenuFuncId);
        string functionName = parentMenu.MenuFuncName;
        string subfunctionName = m.MenuFuncName;
        m_Log.Debug("functionName:" + functionName + " >> " + m.MenuFuncName);
        SessionHelper sessionHelper = new SessionHelper();
        LogSystemVO logSystemVO = new LogSystemVO();
        logSystemVO.Fucntion = functionName;
        logSystemVO.SubFucntion = subfunctionName;
        sessionHelper.LogVO = logSystemVO;
    }
}
