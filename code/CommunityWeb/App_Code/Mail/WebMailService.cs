using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Logging;
using WuDada.Core.SystemApplications;
using WuDada.Core.SystemApplications.Service;
using WuDada.Core.SystemApplications.Domain;
using WuDada.Core.Generic.Mail;
using WuDada.Core.Post.Domain;
using System.Text;
using WuDada.Core.Member.Domain;

/// <summary>
/// WebMailService 的摘要描述
/// </summary>
public class WebMailService
{
    private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private MailService m_MailService;
    private SystemFactory m_SystemFactory;
    private ISystemService m_SystemService;
    private SystemParamVO m_MailVO;
    private ConfigHelper m_ConfigHelper;

	public WebMailService()
	{
        m_ConfigHelper = new ConfigHelper();
        m_SystemFactory = new SystemFactory();
        m_SystemService = m_SystemFactory.GetSystemService();

        m_MailVO = m_SystemService.GetSystemParamByRoot();

        bool enableSSL = m_MailVO.EnableSSL;
        int port = 25;

        if (m_MailVO.MailSmtp.IndexOf("gmail") != -1)
        {
            enableSSL = true;
            port = 587;
        }
        else if (!string.IsNullOrEmpty(m_MailVO.MailPort))
        {
            port = int.Parse(m_MailVO.MailPort);
        }

        m_MailService = new MailService(m_MailVO.MailSmtp, port, enableSSL, m_MailVO.Account, m_MailVO.Password);
	}

    /// <summary>
    /// 發信給設定的信箱 By 訊息
    /// </summary>
    /// <param name="messageVO"></param>
    public void SendMail_ToContactor_ByMessage(MessageVO messageVO)
    {
        try
        {
            string classify = "聯絡我們收件者";
            IList<ItemParamVO> contactorList = m_SystemService.GetAllItemParamByNoDel(classify);

            if (contactorList != null && contactorList.Count > 0)
            {
                SystemParamVO mailVO = m_SystemService.GetSystemParamByRoot();
                MailService mailService = new MailService(mailVO.MailSmtp, int.Parse(mailVO.MailPort), mailVO.EnableSSL, mailVO.Account, mailVO.Password);

                StringBuilder sbMailList = new StringBuilder();
                foreach (ItemParamVO contactor in contactorList)
                {
                    sbMailList.Append(string.Format("{0};", contactor.Value));
                }

                string mailTitle = string.Format("收到一封由【{0}】從網站發出的線上諮詢。", messageVO.CreateName);
                string mailContent = GenMailContent(messageVO);

                mailService.SendMail(mailVO.SendEmail, sbMailList.ToString(), mailTitle, mailContent);
            }
        }
        catch (Exception ex)
        {
            m_Log.Error(ex);
        }
    }

    private string GenMailContent(MessageVO messageVO)
    {
        StringBuilder sbContent = new StringBuilder();

        sbContent.Append(string.Format("建立時間：{0}<br />", messageVO.CreatedDate.Value.ToString()));
        sbContent.Append(string.Format("預約日期：{0}<br />", messageVO.ReservationDate.Value.ToString("yyyy/MM/dd")));
        sbContent.Append(string.Format("預約時段：{0}<br />", messageVO.ReservationPeriod));
        sbContent.Append(string.Format("姓　　名：{0}<br />", messageVO.CreateName));        
        sbContent.Append(string.Format("連絡電話：{0}<br />", messageVO.Phone));        
        sbContent.Append(string.Format("電子信箱：{0}<br />", messageVO.EMail));
        sbContent.Append(string.Format("預約內容：<br />{0}<br />", messageVO.Content.Replace("\n", "<br />")));

        return sbContent.ToString();
    }

    /// <summary>
    /// 發確認信給會員
    /// </summary>
    /// <param name="messageVO"></param>
    public void SendConfirmMail_ToMember(MemberVO memberVO)
    {
        try
        {
            SystemParamVO mailVO = m_SystemService.GetSystemParamByRoot();
            MailService mailService = new MailService(mailVO.MailSmtp, int.Parse(mailVO.MailPort), mailVO.EnableSSL, mailVO.Account, mailVO.Password);

            string mailTitle = "收到一封從網站的會員認證信。";
            string mailContent = GenMailContent(memberVO);

            mailService.SendMail(mailVO.SendEmail, memberVO.Email, mailTitle, mailContent);
        }
        catch (Exception ex)
        {
            m_Log.Error(ex);
        }
    }

    private string GenMailContent(MemberVO memberVO)
    {
        StringBuilder sbContent = new StringBuilder();

        sbContent.Append(string.Format("時　　間：{0}<br />", memberVO.CreatedDate.Value.ToString()));
        sbContent.Append(string.Format("姓　　名：{0}<br />", memberVO.Name));
        sbContent.Append(string.Format("電　　話：{0}<br />", memberVO.Phone));
        sbContent.Append(string.Format("性　　別：{0}<br />", memberVO.Sex));
        sbContent.Append(string.Format("電子信箱：{0}<br />", memberVO.Email));
        sbContent.Append(string.Format("<a href='{0}/memberConfirm.aspx?id={1}&token={2}'>請點此連結完成會員認證</a>"
            , m_ConfigHelper.Host, memberVO.MemberId, memberVO.Token));

        return sbContent.ToString();
    }
}