using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Logging;
using WuDada.Core.Auth.Domain;
using WuDada.Core.SystemApplications.Domain;

/// <summary>
/// SessionHelper 的摘要描述
/// </summary>
public class SessionHelper : System.Web.UI.Page
{
    private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private readonly string LOGIN_USER = "LOGIN_USER";
    private readonly string MENU_STATUS_OPEN = "MENU_STATUS_OPEN";
    private readonly string LOGVO = "LOG_VO";
    private readonly string TMP_SS = "TMP_SESSION";
    private readonly string MENU_FUNCTION_NAME = "MENU_FUNCTION_NAME";
    private readonly string LOGIN_USER_BELONGTOBRANCH = "LOGIN_USER_BELONGTOBRANCH";
    private readonly string MEMBER = "MEMBER";
    private readonly string FRONT_LANGUAGE = "FRONT_LANGUAGE";
    private readonly string IS_AMDIN = "IS_AMDIN";

    /// <summary>
    ///  目前的後台清單是否開啟
    /// </summary>
    public bool MenuStatusOpen
    {
        get { return ((Session[MENU_STATUS_OPEN] == null || String.IsNullOrEmpty(Session[MENU_STATUS_OPEN].ToString())) ? true : (bool)Session[MENU_STATUS_OPEN]); }

        set { Session[MENU_STATUS_OPEN] = value; }
    }

    /// <summary>
    /// 使用者的資訊
    /// </summary>
    public LoginUserVO LoginUser
    {
        get
        {
            try
            {
                return (Session[LOGIN_USER] == null ? null : (LoginUserVO)Session[LOGIN_USER]);
            }
            catch
            {
                // log.Error(ex);
                return null;
            }
        }

        set { Session[LOGIN_USER] = value; }
    }

    //public MemberVO Member
    //{
    //    get
    //    {
    //        try
    //        {
    //            return (Session[MEMBER] == null ? null : (MemberVO)Session[MEMBER]);
    //        }
    //        catch
    //        {
    //            return null;
    //        }
    //    }
    //    set { Session[MEMBER] = value; }
    //}

    /// <summary>
    /// for 一般暫存或傳值用
    /// </summary>
    public string MenuFuncName
    {
        get { return (Session[MENU_FUNCTION_NAME] == null ? "" : Session[MENU_FUNCTION_NAME].ToString()); }

        set { Session[MENU_FUNCTION_NAME] = value; }
    }

    /// <summary>
    /// 前台語言 //預設英文版
    /// </summary>
    public string FrontLanguage
    {
        get { return (Session[FRONT_LANGUAGE] == null ? WebLanguageUtil.Language.Eng.ToString() : Session[FRONT_LANGUAGE].ToString()); }

        set { Session[FRONT_LANGUAGE] = value; }
    }

    /// <summary>
    /// for 一般暫存或傳值用
    /// </summary>
    public object TmpSS
    {
        get { return (Session[TMP_SS] == null ? null : Session[TMP_SS]); }

        set { Session[TMP_SS] = value; }
    }


    /// <summary>
    /// 異動檔Log
    /// </summary>
    public LogSystemVO LogVO
    {
        get
        {
            if (Session[LOGVO] == null)
            {
                return new LogSystemVO();
            }
            else
            {
                try
                {
                    return (LogSystemVO)Session[LOGVO];
                }
                catch
                {
                    return new LogSystemVO();
                }
            }
        }
        set { Session[LOGVO] = value; }
    }

    /// <summary>
    ///  登入使用者的BranchNo
    /// </summary>
    public string LoginUserBelongToBranchNo
    {
        get { return (Session[LOGIN_USER_BELONGTOBRANCH] == null ? "" : Session[LOGIN_USER_BELONGTOBRANCH].ToString()); }

        set { Session[LOGIN_USER_BELONGTOBRANCH] = value; }
    }

    /// <summary>
    ///  判斷是否是admin
    /// </summary>
    public bool IsAdmin
    {
        get { return ((Session[IS_AMDIN] == null || String.IsNullOrEmpty(Session[IS_AMDIN].ToString())) ? true : (bool)Session[IS_AMDIN]); }

        set { Session[IS_AMDIN] = value; }
    }
}