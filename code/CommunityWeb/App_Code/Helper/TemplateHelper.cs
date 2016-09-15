using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WuDada.Core.SystemApplications.Service;
using WuDada.Core.SystemApplications;
using System.Web.UI;
using WuDada.Core.SystemApplications.Domain;
using Common.Logging;
using Spring.Objects;
using System.IO;
using FredCK.FCKeditorV2;
using System.Collections;
using Spring.Core;
using WuDada.Core.Post;
using WuDada.Core.Post.Service;
using WuDada.Core.Post.Domain;

/// <summary>
/// TemplateHelper 的摘要描述
/// </summary>
public class TemplateHelper 
{
    public TemplateHelper()
    {

    }

    /// <summary>
    /// 取得主視覺的內容
    /// </summary>
    /// <param name="server"></param>
    /// <param name="mainAdvNodeId">主視覺設定檔代碼</param>
    /// <returns>主視覺的內容</returns>
    public static string GetMainAdvContent(HttpServerUtility server, int mainAdvNodeId)
    {
        string content = string.Empty;

        PostFactory postFactory = new PostFactory();
        IPostService postService = postFactory.GetPostService();

        NodeVO nodeVO = postService.GetNodeById(mainAdvNodeId);
        if (nodeVO != null)
        {
            if (nodeVO.UType == NodeVO.UnitType.Flash)
            {                
                string advFile = server.MapPath("~/template/") + "main-adv-flash01.txt";
                string fileContent = File.ReadAllText(advFile, System.Text.Encoding.UTF8);

                content = fileContent.Replace("(#FILENAME)", nodeVO.PicFileName);
            }
            //暫時不用圖片，僅flash
            //else if (nodeVO.UType == NodeVO.UnitType.Pic)
            //{
            //    string advFile = server.MapPath("~/template/") + "main-adv-pic01.txt";
            //    string fileContent = File.ReadAllText(advFile, System.Text.Encoding.UTF8);

            //    content = fileContent.Replace("(#FILENAME)", nodeVO.PicFileName);
            //}
        }

        return content;
    }

    public static string GetMainTopAdvPic(HttpServerUtility server, int m_PostId1)
    {
        string content = string.Empty;

        WebPageHelper webPageHelper = new WebPageHelper();
        PostFactory postFactory = new PostFactory();
        IPostService postService = postFactory.GetPostService();

        PostVO podeVO = postService.GetPostById(m_PostId1);
        if (podeVO != null)
        {
            string advFile = server.MapPath("~/template/") + "main-adv-pic01.txt";
            string fileContent = File.ReadAllText(advFile, System.Text.Encoding.UTF8);

            content = fileContent.Replace("(#FILENAME)", webPageHelper.GetContent(podeVO, "PicFileName"));
        }

        return content;
    }
}