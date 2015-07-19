<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // 應用程式啟動時執行的程式碼

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  應用程式關閉時執行的程式碼

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // 發生未處理錯誤時執行的程式碼

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // 啟動新工作階段時執行的程式碼
        Common.Logging.ILog log = Common.Logging.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        log.Info("Session_Start");

        object loginUser = Session["LOGIN_USER"];
        SessionHelper sessionHelper = new SessionHelper();
        WuDada.Core.Auth.Domain.LoginUserVO loginUser2 = sessionHelper.LoginUser;
        if (loginUser2 == null)
        {
            log.Info("Session_End logout2");
            sessionHelper.LoginUser = null;
        }

    }

    void Session_End(object sender, EventArgs e) 
    {
        Common.Logging.ILog log = Common.Logging.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        log.Info("Session_End");
            
        //object loginUser = Session["LOGIN_USER"];
        SessionHelper sessionHelper = new SessionHelper();
        WuDada.Core.Auth.Domain.LoginUserVO loginUser2 = sessionHelper.LoginUser;        
        //if (loginUser == null && loginUser2 == null)
        if (loginUser2 == null)
        {
            log.Info("Session_End logout");
            sessionHelper.LoginUser = null;            
        }
    }
       
</script>
