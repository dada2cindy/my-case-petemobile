using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Logging;
using System.Text;

/// <summary>
/// JavascriptUtil 的摘要描述
/// </summary>
public class JavascriptUtil
{
    private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public JavascriptUtil()
    {

    }

    /// <summary>
    /// 顯示訊息
    /// </summary>
    /// <param name="alertMsg"></param>
    /// <returns></returns>
    public static string AlertJS(string alertMsg)
    {
        string cont = string.Format("alert(\"{0}\");", alertMsg);

        return (WrapperScript(cont));
    }
    /// <summary>
    /// Confirm訊息
    /// </summary>
    /// <param name="alertMsg"></param>
    /// <returns></returns>
    public static string ConfirmJS(string alertMsg)
    {
        string cont = string.Format("confirm(\"{0}\");", alertMsg);

        return (WrapperScript(cont));
    }

    /// <summary>
    /// 顯示訊息後導至某頁
    /// </summary>
    /// <param name="alertMsg">要顯示的訊息</param>
    /// <param name="path">之後要去的位置</param>
    /// <returns></returns>
    public static string AlertJSAndRedirect(string alertMsg, string path)
    {
        string alertMsgJs = string.Format("alert('{0}');", alertMsg);

        return WrapperScript(alertMsgJs + Redirect(path));

    }

    /// <summary>
    /// 顯示訊息後導回上頁
    /// </summary>
    /// <param name="alertMsg">要顯示的訊息</param>        
    /// <returns></returns>
    public static string AlertJSAndGoBack(string alertMsg)
    {
        string alertMsgJs = string.Format(" alert('{0}'); ", alertMsg);

        return WrapperScript(alertMsgJs + " history.go(-1);  ");

    }

    public static string GoBack(int backPage)
    {

        return WrapperScript(" history.go(-" + backPage.ToString() + "); ");
    }

    public static string GoBack()
    {
        return WrapperScript(" history.go(-1); ");
    }

    /// <summary>
    /// 導畫面至某頁
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string Redirect(string path)
    {
        return (string.Format(" window.location.href='{0}';", path));
    }

    /// <summary>
    /// 開啟ModalDialog以傳遞資料
    /// </summary>
    /// <param name="path">網頁路徑</param>
    /// <param name="paramDic">傳遞的參數,其中key為ClientId為要傳回後的欄位ID</param>
    /// <param name="width">視窗寬度</param>
    /// <param name="height">視窗寬度</param>
    /// <returns></returns>
    public static string OpenDialog(string path, Dictionary<string, string> paramDic, int width, int height)
    {
        string paramStr = "";
        StringBuilder jsStr = new StringBuilder();

        if (paramDic.Count > 0)
        {
            List<string> tmpParamList = new List<string>();

            foreach (string key in paramDic.Keys)
            {
                tmpParamList.Add(string.Format("{0}={1}", key, paramDic[key]));
            }
            paramStr = "?" + string.Join("&", tmpParamList.ToArray());

        }

        jsStr.AppendLine(string.Format("var retuenValue = window.showModalDialog(\"{0}{1}\", \"MyDialog\", \"dialogWidth={2}px;dialogHeight={3}px\");", path, paramStr, width, height));

        return (WrapperScript(jsStr.ToString()));
    }

    /// <summary>
    /// 開啟視窗
    /// </summary>
    /// <param name="path"></param>
    /// <param name="paramDic"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public static string OpenWin(string path, Dictionary<string, string> paramDic, int width, int height)
    {
        string paramStr = "";
        StringBuilder jsStr = new StringBuilder();

        if (paramDic != null && paramDic.Count > 0)
        {
            List<string> tmpParamList = new List<string>();

            foreach (string key in paramDic.Keys)
            {
                tmpParamList.Add(string.Format("{0}={1}", key, paramDic[key]));
            }
            paramStr = "?" + string.Join("&", tmpParamList.ToArray());

        }

        paramStr = string.Format("window.open (\"{0}{1}\", \"openWin\", \"height={2}, width={3}\")", path, paramStr, height, width);
        return WrapperScript(paramStr);
    }

    /// <summary>
    /// 開啟視窗
    /// </summary>
    /// <param name="path"></param>
    /// <param name="paramDic"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public static string OpenWin(string path, Dictionary<string, string> paramDic, int width, int height, bool isScrollbars)
    {
        if (isScrollbars)
        {
            string paramStr = "";
            StringBuilder jsStr = new StringBuilder();

            if (paramDic != null && paramDic.Count > 0)
            {
                List<string> tmpParamList = new List<string>();

                foreach (string key in paramDic.Keys)
                {
                    tmpParamList.Add(string.Format("{0}={1}", key, paramDic[key]));
                }
                paramStr = "?" + string.Join("&", tmpParamList.ToArray());
            }

            paramStr = string.Format("window.open (\"{0}{1}\", \"openWin\", \"height={2}, width={3},scrollbars=1\")", path, paramStr, height, width);
            return WrapperScript(paramStr);
        }
        else
        {
            return (OpenWin(path, paramDic, width, height));
        }

    }


    /// <summary>
    /// 將字串以<script></script>包覆
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string WrapperScript(string str)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("<script>");
        sb.AppendLine(str);
        sb.AppendLine("</script>");
        return sb.ToString();
    }

}