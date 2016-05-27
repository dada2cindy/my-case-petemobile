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
}