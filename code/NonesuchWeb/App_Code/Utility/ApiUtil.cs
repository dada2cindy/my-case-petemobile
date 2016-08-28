using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using WuDada.Core.Member;
using WuDada.Core.Member.Service;
using WuDada.Core.Member.Domain;
using WuDada.Core.Member.Dto;
using System.Collections.Specialized;
using WuDada.Core.Post;
using WuDada.Core.Post.Service;
using WuDada.Core.Post.Domain;

/// <summary>
/// ApiUtil 的摘要描述
/// </summary>
public static class ApiUtil
{
    public static WebRequest Post<T>(string url, string method, T obj)
    {
        string jsonData = JsonConvert.SerializeObject(obj);
        return Post(url, method, jsonData);
    }

    public static WebRequest Post(string url, string method, string jsonData)
    {
        try
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = method.ToUpper();

            if (!string.IsNullOrEmpty(jsonData))
            {
                request.ContentType = "application/json";
                byte[] bts = Encoding.UTF8.GetBytes(jsonData);
                request.ContentLength = bts.Length;

                if (method != "GET")
                {
                    using (Stream st = request.GetRequestStream())
                    {
                        st.Write(bts, 0, bts.Length);
                        st.Close();
                    }
                }
            }

            return request;
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
        }

        return null;
    }

    /// <summary>
    /// 同步到Server
    /// </summary>
    public static void UpdateFileToServer(string filePath)
    {
        PostFactory m_PostFactory = new PostFactory();
        IPostFileService m_PostFileService = m_PostFactory.GetPostFileService();
        ConfigHelper m_ConfigHelper = new ConfigHelper();
        WebUtility m_WebUtility = new WebUtility();

        if (string.IsNullOrEmpty(m_ConfigHelper.MemberApiUrl))
        {
            return;
        }

        Dictionary<string, string> conditions = new Dictionary<string, string>();
        conditions.Add("NeedUpdate", "true");
        IList<FileVO> list = m_PostFileService.GetFileList(conditions);

        if (list != null && list.Count > 0)
        {
            foreach (FileVO vo in list)
            {
                try
                {
                    FileVO fileVO = m_PostFileService.GetFileById(vo.FileId);
                    if (fileVO.IsUpdatingToServer)
                    {
                        continue;
                    }

                    FileDto dto = new FileDto(vo);

                    //狀態為刪除
                    if (dto.Flag == 0)
                    {
                        vo.IsUpdatingToServer = true;
                        m_PostFileService.UpdateFile(vo);

                        if (dto.ServerId > 0)
                        {
                            //有serverId就去server刪除
                            string url = m_ConfigHelper.MemberApiUrl + "/" + dto.ServerId.ToString();
                            WebRequest request = ApiUtil.Post(url, "DELETE", "");

                            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                            {
                                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Gone || response.StatusCode == HttpStatusCode.NoContent)
                                {
                                    vo.NeedUpdate = false;
                                    vo.IsUpdatingToServer = false;
                                    m_PostFileService.UpdateFile(vo);
                                }
                            }
                        }
                        else
                        {
                            //沒有serverId就直接標記已更新
                            vo.NeedUpdate = false;
                            vo.IsUpdatingToServer = false;
                            m_PostFileService.UpdateFile(vo);
                        }
                    }
                    else
                    {
                        vo.IsUpdatingToServer = true;
                        m_PostFileService.UpdateFile(vo);

                        WebRequest request = ApiUtil.Post<FileDto>(m_ConfigHelper.MemberApiUrl, "POST", dto);

                        string responseInfo = string.Empty;
                        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                        {
                            if (response.StatusCode == HttpStatusCode.Created)
                            {
                                using (Stream stream = response.GetResponseStream())
                                {
                                    responseInfo = (new StreamReader(stream)).ReadToEnd().Trim();

                                    MemberDto newMemberDto = JsonConvert.DeserializeObject<MemberDto>(responseInfo);

                                    vo.IsUpdatingToServer = false;
                                    vo.NeedUpdate = false;
                                    vo.ServerId = newMemberDto.MemberId;
                                    m_PostFileService.UpdateFile(vo);

                                    //成功的話在ftp檔案
                                    m_WebUtility.UploadFileToFTP(Path.Combine(filePath,vo.FileName));
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    vo.IsUpdatingToServer = false;
                    m_PostFileService.UpdateFile(vo);
                    string error = ex.ToString();
                }
            }
        }
    }

    /// <summary>
    /// 同步到Server
    /// </summary>
    public static void UpdateMemberToServer()
    {
        MemberFactory m_MemberFactory = new MemberFactory();
        IMemberService m_MemberService = m_MemberFactory.GetMemberService();
        ConfigHelper m_ConfigHelper = new ConfigHelper();

        if (string.IsNullOrEmpty(m_ConfigHelper.MemberApiUrl))
        {
            return;
        }

        Dictionary<string, string> conditions = new Dictionary<string, string>();
        conditions.Add("NeedUpdate", "true");
        IList<MemberVO> list = m_MemberService.GetMemberList(conditions);

        if (list != null && list.Count > 0)
        {
            foreach (MemberVO vo in list)
            {
                try
                {
                    MemberVO memberVO = m_MemberService.GetMemberById(vo.MemberId);
                    if (memberVO.IsUpdatingToServer)
                    {
                        continue;
                    }

                    MemberDto dto = new MemberDto(vo);

                    //狀態為刪除
                    if (dto.Status == "0")
                    {
                        vo.IsUpdatingToServer = true;
                        m_MemberService.UpdateMember(vo);

                        if (dto.ServerId > 0)
                        {
                            //有serverId就去server刪除
                            string url = m_ConfigHelper.MemberApiUrl + "/" + dto.ServerId.ToString();
                            WebRequest request = ApiUtil.Post(url, "DELETE", "");

                            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                            {
                                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Gone || response.StatusCode == HttpStatusCode.NoContent)
                                {
                                    vo.NeedUpdate = false;
                                    vo.IsUpdatingToServer = false;
                                    m_MemberService.UpdateMember(vo);
                                }
                            }
                        }
                        else
                        {
                            //沒有serverId就直接標記已更新
                            vo.NeedUpdate = false;
                            vo.IsUpdatingToServer = false;
                            m_MemberService.UpdateMember(vo);
                        }
                    }
                    else
                    {
                        vo.IsUpdatingToServer = true;
                        m_MemberService.UpdateMember(vo);

                        WebRequest request = ApiUtil.Post<MemberDto>(m_ConfigHelper.MemberApiUrl, "POST", dto);

                        string responseInfo = string.Empty;
                        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                        {
                            if (response.StatusCode == HttpStatusCode.Created)
                            {
                                using (Stream stream = response.GetResponseStream())
                                {
                                    responseInfo = (new StreamReader(stream)).ReadToEnd().Trim();

                                    MemberDto newMemberDto = JsonConvert.DeserializeObject<MemberDto>(responseInfo);

                                    vo.IsUpdatingToServer = false;
                                    vo.NeedUpdate = false;
                                    vo.ServerId = newMemberDto.MemberId;
                                    m_MemberService.UpdateMember(vo);
                                }
                            }
                        }
                    }                    
                }
                catch (Exception ex)
                {
                    vo.IsUpdatingToServer = false;
                    m_MemberService.UpdateMember(vo);
                    string error = ex.ToString();
                }
            }
        }
    }

    /// <summary>
    /// 上傳檔案至 Server 透過HttpWebRequest
    /// </summary>
    /// <param name="url">上傳網址</param>
    /// <param name="file">檔案位置</param>
    /// <param name="paramName">該control name</param>
    /// <param name="contentType">image type </param>
    /// <param name="nvc">其他參數</param>
    /// <returns></returns>
    public static string UploadFileByHttpWebRequest(string url, string file, string paramName, string contentType, NameValueCollection nvc)
    {

        // Debug.WriteLine(string.Format("上傳至 {0} to {1}", file, url));

        string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
        byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

        HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
        wr.ContentType = "multipart/form-data; boundary=" + boundary;
        wr.Method = "POST";
        wr.KeepAlive = true;
        wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

        Stream rs = wr.GetRequestStream();

        if (nvc != null)
        {
            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);
        }

        string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
        string header = string.Format(headerTemplate, paramName, file, contentType);
        byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
        rs.Write(headerbytes, 0, headerbytes.Length);

        FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
        byte[] buffer = new byte[4096];
        int bytesRead = 0;
        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
        {
            rs.Write(buffer, 0, bytesRead);
        }
        fileStream.Close();

        byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
        rs.Write(trailer, 0, trailer.Length);
        rs.Close();

        WebResponse wresp = null;
        var result = "";
        try
        {
            wresp = wr.GetResponse();
            Stream stream2 = wresp.GetResponseStream();
            StreamReader reader2 = new StreamReader(stream2);
            //成功回傳結果
            result = reader2.ReadToEnd();

            //File.WriteAllText(Server.MapPath("log.txt"), string.Format("Server 回應{0}", reader2.ReadToEnd()));
        }
        catch (Exception ex)
        {

            // File.WriteAllText(Server.MapPath("log.txt"), string.Format("上傳失敗，錯誤訊息: {0}", ex.Message));
            if (wresp != null)
            {
                wresp.Close();

            }

        }

        wr = null;
        return result;

    }
}