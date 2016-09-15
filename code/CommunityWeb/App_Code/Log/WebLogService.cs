using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Logging;
using WuDada.Core.SystemApplications.Domain;
using WuDada.Core.Auth.Domain;
using WuDada.Core.SystemApplications.Service;
using WuDada.Core.SystemApplications;

/// <summary>
/// WebLogService 的摘要描述
/// </summary>
public class WebLogService
{
    private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private HttpHelper m_HttpHelper = new HttpHelper();
    private SystemFactory m_SystemFactory;
    private ILogService m_LogService;
    
    public WebLogService()
    {
        m_SystemFactory = new SystemFactory();
        m_LogService = m_SystemFactory.GetLogService();
    }

    /// <summary>
    /// 傳回whomak
    /// </summary>
    /// <returns></returns>
    public static string GetWhoMake()
    {
        SessionHelper shelper = new SessionHelper();

        LoginUserVO user = shelper.LoginUser;

        if (user == null)
        {
            return "";
        }
        else
        {
            if (user.UserId.Length > 10)
            {
                return user.UserId.Substring(0, 10);
            }
            else
            {
                return user.UserId;
            }
        }
    }

    /// <summary>
    /// 檢查密碼是否正確
    /// </summary>
    /// <param name="pw"></param>
    /// <returns></returns>
    public static bool CheckPassoword(string pw)
    {
        SessionHelper shelper = new SessionHelper();

        LoginUserVO user = shelper.LoginUser;

        if (user == null)
        {
            return false;
        }
        else
        {
            return user.Password.Equals(pw);
        }
    }


    public void AddSystemLogLogin(string UserId)
    {
        LogSystemVO logVO = new LogSystemVO();
        logVO.Action = MsgVO.LogTitleName.登入記錄.ToString();
        logVO.Fucntion = MsgVO.LogTitleName.登入記錄.ToString();
        logVO.SubFucntion = MsgVO.LogTitleName.登入記錄.ToString();
        logVO.IpAddress = m_HttpHelper.GetUserIp(HttpContext.Current);
        logVO.UpdateId = UserId;
        logVO.UpdateDate = DateTime.Now;

        m_LogService.CreateLogSystem(logVO);
    }

    /// <summary>
    /// 加入 system log
    /// </summary>
    /// <param name="action">異動行為</param>
    public void AddSystemLog(MsgVO.Action action, object obj)
    {
        SessionHelper sHelper = new SessionHelper();

        LoginUserVO userVO = sHelper.LoginUser;
        LogSystemVO logVO = sHelper.LogVO;

        if (!String.IsNullOrEmpty(logVO.Fucntion))
        {

            if (userVO != null)
            {
                logVO.UpdateId = userVO.UserId;
            }

            logVO.UpdateDate = DateTime.Now;
            logVO.Action = action.ToString();
            logVO.UpdateClassName = obj.GetType().ToString();
            logVO.IpAddress = m_HttpHelper.GetUserIp(HttpContext.Current);

            m_LogService.CreateLogSystem(logVO);

        }
        else
        {
            // log.Debug("logVO.Function is null ,updateClassName "+obj.ToString());            
        }
    }

    /// <summary>
    /// 加入 system log
    /// </summary>
    /// <param name="action">異動行為</param>
    public void AddSystemLog(MsgVO.Action action, object obj, string function, string note)
    {
        SessionHelper sHelper = new SessionHelper();

        LoginUserVO userVO = sHelper.LoginUser;

        LogSystemVO logVO = new LogSystemVO();

        if (userVO != null)
        {
            logVO.UpdateId = userVO.UserId;
        }

        logVO.UpdateDate = DateTime.Now;
        logVO.Action = action.ToString();
        logVO.UpdateClassName = obj.GetType().ToString();
        logVO.Fucntion = function;
        logVO.SubFucntion = logVO.SubFucntion;
        logVO.Note = note;
        logVO.IpAddress = m_HttpHelper.GetUserIp(HttpContext.Current);
        m_LogService.CreateLogSystem(logVO);
    }
}