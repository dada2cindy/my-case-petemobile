using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Logging;
using WuDada.Core.Auth.Domain;
using WuDada.Core.SystemApplications.Domain;
using WuDada.Core.Post;
using WuDada.Core.Post.Service;
using WuDada.Core.Post.Domain;
using System.Text;
using Spring.Objects;

/// <summary>
/// WebPageHelper 的摘要描述
/// </summary>
public class WebPageHelper : System.Web.UI.Page
{
    private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private PostFactory m_PostFactory;
    private IPostService m_PostService;
    private SessionHelper m_SessionHelper = new SessionHelper();

    //網站預設Meta資訊 m_postId=1
    private int m_DefaultMetaPostId = 1;

    public WebPageHelper()
    {
        m_PostFactory = new PostFactory();
        m_PostService = m_PostFactory.GetPostService();
    }

    public string GetDefaultHeadMeta(PostVO postVO)
    {
        StringBuilder sb = new StringBuilder();

        //設定預設
        PostVO defaultMetaVO = m_PostService.GetPostById(m_DefaultMetaPostId);
        string title = defaultMetaVO.PageTitle;
        string description = defaultMetaVO.PageDescription;
        string keyWord = defaultMetaVO.PageKeyWord;

        //傳入的Post，有值的話就替換
        if (postVO != null)
        {
            if (!string.IsNullOrEmpty(postVO.PageTitle))
            {
                title = postVO.PageTitle;
            }
            else
            {
                title = string.Format("{0} - {1}", GetContent(postVO, "Title"), defaultMetaVO.PageTitle);
            }

            if (!string.IsNullOrEmpty(postVO.PageDescription))
            {
                description = postVO.PageDescription;
            }

            if (!string.IsNullOrEmpty(postVO.PageKeyWord))
            {
                keyWord = postVO.PageKeyWord;
            }
        }

        if (!string.IsNullOrEmpty(title))
        {
            sb.Append(string.Format("<title>{0}</title>", title));
        }

        if (!string.IsNullOrEmpty(description))
        {
            sb.Append(string.Format("<META NAME=\"Description\" CONTENT=\"{0}\">", description));
        }

        if (!string.IsNullOrEmpty(keyWord))
        {
            sb.Append(string.Format("<META NAME=\"KeyWords\" CONTENT=\"{0}\">", keyWord));
        }

        return sb.ToString();
    }

    /// <summary>
    /// 取得該語言的內容
    /// </summary>
    /// <param name="object">抓取的物件</param>
    /// <param name="fieldNamePrefix">抓取欄位的名稱(不包含Language的字,預設中文是沒有lng的字眼)</param>
    /// <returns></returns>
    public string GetContent(object obj, string fieldNamePrefix)
    {
        string content = string.Empty;
        string fieldName = fieldNamePrefix;
        if (!WebLanguageUtil.Language.Cht.ToString().Equals(m_SessionHelper.FrontLanguage))
        {
            fieldName += m_SessionHelper.FrontLanguage.ToUpper();
        }
        m_Log.Info("取得內容的欄位:" + fieldName);

        try
        {
            ObjectWrapper ow = new ObjectWrapper(obj);
            object value = ow.GetPropertyValue(fieldName);
            if (value != null)
            {
                content = value.ToString();
            }
        }
        catch (Exception ex)
        {
            m_Log.Debug(ex);
        }

        return content;
    }

    /// <summary>
    /// 取得該語言的內容
    /// </summary>
    /// <param name="object">抓取的物件</param>
    /// <param name="fieldNamePrefix">抓取欄位的名稱(不包含Language的字,預設中文是沒有lng的字眼)</param>
    /// <param name="dateFormate">日期格式</param>
    /// <returns></returns>
    public string GetDateContent(object obj, string fieldName, string dateFormate)
    {
        string content = string.Empty;
        m_Log.Info("取得內容的欄位:" + fieldName);

        try
        {
            ObjectWrapper ow = new ObjectWrapper(obj);
            object value = ow.GetPropertyValue(fieldName);
            //預設日期格式
            if (string.IsNullOrEmpty(dateFormate))
            {
                dateFormate = "MMMM dd, yyyy";
            }

            if (value != null)
            {
                //只有日期欄位才做
                if (ow.GetPropertyType(fieldName) == typeof(DateTime) || ow.GetPropertyType(fieldName) == typeof(DateTime?))
                {
                    //中文格式一種，其他語言一種，目前是兩個都設定一樣
                    if (WebLanguageUtil.Language.Cht.ToString().Equals(m_SessionHelper.FrontLanguage))
                    {
                        DateTime dateTime = (DateTime)value;
                        content = dateTime.ToString(dateFormate, new System.Globalization.CultureInfo("en-us"));
                        //content = dateTime.ToString(dateFormate, new System.Globalization.CultureInfo("zh-tw"));
                    }
                    else
                    {
                        DateTime dateTime = (DateTime)value;
                        content = dateTime.ToString(dateFormate, new System.Globalization.CultureInfo("en-us"));
                    }
                }
                //else if (ow.GetPropertyType(fieldName) == typeof(DateTime?))
                //{
                //}
            }
        }
        catch (Exception ex)
        {
            m_Log.Debug(ex);
        }

        return content;
    }

    public string GetPaget_PrevPageText()
    {
        //預設
        string content = "<aa>Previous</aa> | ";

        //中文格式一種，其他語言一種
        if (WebLanguageUtil.Language.Cht.ToString().Equals(m_SessionHelper.FrontLanguage))
        {
            content = "<aa>上一頁</aa> | ";
        }
        //else
        //{
        //    content = "Previous | ";
        //}

        return content;
    }

    public string GetPaget_NextPageText()
    {
        //預設
        string content = "| <aa>Next</aa>";

        //中文格式一種，其他語言一種
        if (WebLanguageUtil.Language.Cht.ToString().Equals(m_SessionHelper.FrontLanguage))
        {
            content = "| <aa>下一頁</aa>";
        }
        //else
        //{
        //    content = "| Next";
        //}

        return content;
    }

    public string GetArrowImg()
    {
        return "<img src='image/breadcrumb-arrowa.gif' width='12' height='6' border='0' /> ";
    }

    public string GetIndexTopTabContent(int tabNum, string tabContent)
    {
        //預設都關閉除了tabNum=1
        string displayNone = "style='display:none;'";
        if (tabNum == 1)
        {
            displayNone = string.Empty;
        }

        StringBuilder sbContent = new StringBuilder();

        //sbContent.Append("<tr id='ajaxTab{0}' class='ajaxTab' {1}><td>");
        sbContent.Append("<div id='ajaxTab{0}' class='ajaxTab' {1}>");
        sbContent.Append("<table border='0' align='center' cellpadding='0' cellspacing='0' class='index-box{0}'><tr><td valign='top'>");
        sbContent.Append("{2}</td></tr></table>");
        sbContent.Append("</div>");
        //sbContent.Append("</td></tr>");

        return string.Format(sbContent.ToString(), tabNum, displayNone, tabContent);
    }

    public string[] GetSearchColumn()
    {
        //中文格式一種，其他語言一種
        if (WebLanguageUtil.Language.Cht.ToString().Equals(m_SessionHelper.FrontLanguage))
        {
            return new string[] { "Title", "HtmlContent", "HtmlContent2" };
        }
        else
        {
            return new string[] { "TitleENG", "HtmlContentENG", "HtmlContent2ENG" };
        }
    }

    public string GetTopPicHiddenContent(int topPicId, string picName)
    {
        return string.Format("<span id='hdnPic{0}' style='display:none;'>upload/template/{1}</span>", topPicId, picName);
    }

    public string SubStringWord(string content, int wordLength, int wordLengthCht, string endWord)
    {
        string result = content;
        if (WebLanguageUtil.Language.Cht.ToString().Equals(m_SessionHelper.FrontLanguage))
        {
            if (content.Length > wordLengthCht)
            {
                result = string.Empty;
                result = content.Substring(0, wordLengthCht) + endWord;
            }
        }
        else
        {
            string[] strArray = content.Split(' ');
            if (strArray.Count() > wordLength)
            {
                result = string.Empty;
                int count = 0;
                foreach (string str in strArray)
                {
                    if(!string.IsNullOrEmpty(str))
                    {
                        if (count >= wordLength)
                        {
                            break;
                        }

                        result += (str + " ");
                        count++;
                    }
                }
                
                result += endWord;
            }
        }

        return result;
    }
}