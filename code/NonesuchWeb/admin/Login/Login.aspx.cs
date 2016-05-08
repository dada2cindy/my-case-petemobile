﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Logging;
using WuDada.Core.Auth.Service;
using WuDada.Core.Auth;
using WuDada.Core.Auth.Domain;
using WuDada.Core.Auth.Container;
using WuDada.Core.SystemApplications.Domain;
using System.Net;
using System.Text;
using System.IO;
using WuDada.Core.Member.Dto;
using Newtonsoft.Json;

public partial class admin_Login_Login : System.Web.UI.Page
{
    private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    AuthFactory m_AuthFactory;
    IAuthService m_AuthService;
    WebLogService webLogService = new WebLogService();

    protected void Page_Load(object sender, EventArgs e)
    {
        m_AuthFactory = new AuthFactory();
        m_AuthService = m_AuthFactory.GetAuthService();
        testAPI();
    }

    private void testAPI()
    {
        string responseInfo = string.Empty;
        try
        {
            string jsonData = "";
            string method = "Get";
            string url = "http://test.xinmingeyes.com/api/member";
            WebRequest request = WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "Get";
            byte[] bts = Encoding.UTF8.GetBytes(jsonData);
            request.ContentLength = bts.Length;

            if (method != "Get")
            {
                using (Stream st = request.GetRequestStream())
                {
                    st.Write(bts, 0, bts.Length);
                    st.Close();
                }
            }

            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    responseInfo = (new StreamReader(stream)).ReadToEnd().Trim();
                }
            }

            IList<MemberDto> list = JsonConvert.DeserializeObject<IList<MemberDto>>(responseInfo);
            string a = "";
        }
        catch (Exception e)
        {
            string error = e.ToString();
        }
    }


    private void doLogin(string id, string pw)
    {
        //帳號皆改為小寫
        if (!string.IsNullOrEmpty(id))
        {
            id = id.ToLower();
        }

        LoginUserVO loginUser = m_AuthService.Login(id, pw);

        if (loginUser != null)
        {
            SessionHelper sHelper = new SessionHelper();

            sHelper.LoginUser = loginUser;
            sHelper.IsAdmin = m_AuthService.IsAdmin(loginUser);
            //sHelper.LoginUserBelongToBranchNo = loginUser.BelongToBranch[0].BranchNo;
            //加入log
            webLogService.AddSystemLogLogin(loginUser.UserId);

            //NHibernateUtil.Initialize(loginUser.BelongRoles);

            //清除快取
            UserMenuFuncContainer.GetInstance().ReloadAllMenu();

            //HttpHelper httpHelper = new HttpHelper();
            //string referer = httpHelper.GetReferer(HttpContext.Current);

            Response.Redirect("~/admin/index.aspx", false);

            return;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "js", JavascriptUtil.AlertJSAndRedirect(MsgVO.LOGIN_ERROR, "Login.aspx"), false);
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string id = txtId.Text.Trim();
        string pw = txtPw.Text.Trim();
        string confirmationCode = txtConfirmationCode.Text.Trim();
        if (string.IsNullOrEmpty(confirmationCode))
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "js", JavascriptUtil.AlertJS(MsgVO.SECURCODE_EMPTY), false);
            return;
        }


        if (confirmationCode.ToUpper().Equals(Session["Captcha"].ToString().ToUpper()))
        {
            doLogin(id, pw);            
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "js", JavascriptUtil.AlertJSAndRedirect(MsgVO.SECURCODE_ERROR, "Login.aspx"), false);
        }


    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtConfirmationCode.Text = string.Empty;
        txtId.Text = string.Empty;
        txtPw.Text = string.Empty;
    }

    protected void ddlTestAccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTestAccount.SelectedValue != "-1")
        {
            doLogin(ddlTestAccount.SelectedValue, "1234");
        }
    }
}