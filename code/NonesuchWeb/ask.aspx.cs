using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Logging;
using WuDada.Core.Post.Service;
using WuDada.Core.Post;
using WuDada.Core.Post.Domain;
using WuDada.Core.SystemApplications.Domain;

public partial class ask : System.Web.UI.Page
{
    private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private PostFactory m_PostFactory;
    private IMessageService m_MessageService;
    private HttpHelper m_HttpHelper;
    private WebMailService m_WebMailService;

    protected void Page_Load(object sender, EventArgs e)
    {
        m_PostFactory = new PostFactory();
        m_MessageService = m_PostFactory.GetMessageService();
        m_HttpHelper = new HttpHelper();
        m_WebMailService = new WebMailService();

        if (!IsPostBack)
        {
            FillDate();
            ClearUI();
        }
    }

    private void FillDate()
    {
        ddlMonth.Items.Clear();
        ddlMonth.Items.Add(new ListItem("請選擇月", string.Empty));
        for (int i = 1; i <= 12; i++)
        {
            ddlMonth.Items.Add(new ListItem(string.Format("{0}月", i), i.ToString()));
        }

        ddlDay.Items.Clear();
        ddlDay.Items.Add(new ListItem("請選擇日", string.Empty));
        for (int i = 1; i <= 31; i++)
        {
            ddlDay.Items.Add(new ListItem(string.Format("{0}日", i), i.ToString()));
        }
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        string confirmationCode = txtConfirmationCode.Text.Trim();
        if (string.IsNullOrEmpty(confirmationCode))
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "js", JavascriptUtil.AlertJS(MsgVO.SECURCODE_EMPTY), false);
            return;
        }

        try
        {
            DateTime date = new DateTime(DateTime.Today.Year, int.Parse(ddlMonth.SelectedValue), int.Parse(ddlDay.SelectedValue));
            if (date < DateTime.Now)
            {
                date = date.AddYears(1);
            }

            if (confirmationCode.ToUpper().Equals(Session["Captcha"].ToString().ToUpper()))
            {
                MessageVO messageVO = new MessageVO();
                UIHelper.FillVO(PanelUI, messageVO);
                messageVO.ReservationDate = date;
                messageVO.CreatedDate = DateTime.Now;
                messageVO.CreateIP = m_HttpHelper.GetUserIp(Context);
                messageVO = m_MessageService.CreateMessage(messageVO);

                //發信
                m_WebMailService.SendMail_ToContactor_ByMessage(messageVO);

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "js", JavascriptUtil.AlertJS("預約已送出。"), false);
                ClearUI();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "js", JavascriptUtil.AlertJS(MsgVO.SECURCODE_ERROR), false);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "js", JavascriptUtil.AlertJS("請選擇正確的預約日期"), false);
            return;
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearUI();
    }

    private void ClearUI()
    {
        UIHelper.ClearUI(PanelUI);
        ddlMonth.SelectedValue = string.Empty;
        ddlDay.SelectedValue = string.Empty;
    }
}