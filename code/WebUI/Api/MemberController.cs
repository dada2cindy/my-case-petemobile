using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common.Logging;
using WebUI.Util;
using WuDada.Core.Auth;
using WuDada.Core.Auth.Service;
using WuDada.Core.Member;
using WuDada.Core.Member.Domain;
using WuDada.Core.Member.Dto;
using WuDada.Core.Member.Service;
using WuDada.Core.Post;
using WuDada.Core.Post.Service;

namespace WebUI.Api
{
    public class MemberController : ApiController
    {
        private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static ConfigHelper m_ConfigHelper = new ConfigHelper();
        private static PostFactory m_PostFactory = new PostFactory();
        private static AuthFactory m_AuthFactory = new AuthFactory();
        private static MemberFactory m_MemberFactory = new MemberFactory();       
        private static IMemberService m_MemberService = m_MemberFactory.GetMemberService();
        private static IAuthService m_AuthService = m_AuthFactory.GetAuthService();
        private static IPostService m_PostService = m_PostFactory.GetPostService();
        //private WebLogService m_WebLogService;
        //private HttpHelper m_HttpHelper;        

        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            IList<MemberDto> result = new List<MemberDto>();

            Dictionary<string, string> conditions = new Dictionary<string, string>();
            conditions.Add("Status", "1");
            conditions.Add("PageIndex", "0");
            conditions.Add("PageSize", "10");
            conditions.Add("Order", "order by m.ApplyDate desc, m.Name");

            IList<MemberVO> memberVOList = m_MemberService.GetMemberList(conditions);
            if (memberVOList != null && memberVOList.Count > 0)
            {
                foreach (MemberVO vo in memberVOList)
                {
                    result.Add(new MemberDto(vo));
                }
            }

            return Request.CreateResponse<IList<MemberDto>>(HttpStatusCode.OK, result);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public HttpResponseMessage Post(MemberDto memberDto)
        {
            if (memberDto != null)
            {
                try
                {
                    MemberVO memberVO = null;
                    //檢查是否有ServerId 有的話把狀態改成刪除, 重新建立一筆
                    if (memberDto.ServerId != 0)
                    {
                        MemberVO oldMemberVO = m_MemberService.GetMemberById(memberDto.ServerId);
                        if (oldMemberVO != null)
                        {
                            oldMemberVO.NeedUpdate = false;
                            oldMemberVO.Status = "0";
                            oldMemberVO.UpdateId = "系統API";
                            m_MemberService.UpdateMember(oldMemberVO);
                        }
                    }

                    memberVO = new MemberVO(memberDto);
                    memberVO.MemberId = 0;
                    memberVO.ServerId = 0;
                    memberVO.NeedUpdate = false;
                    memberVO.UpdateId = "系統API";
                    FixTimeZone(memberVO);
                    memberVO = m_MemberService.CreateMember(memberVO);
                    memberVO.ServerId = memberVO.MemberId;

                    return Request.CreateResponse<MemberDto>(HttpStatusCode.Created, new MemberDto(memberVO));
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent, ex.ToString());
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
        }

        private void FixTimeZone(MemberVO memberVO)
        {
            int addHours = m_ConfigHelper.AddHours;

            if (memberVO.ApplyDate.HasValue)
            {
                memberVO.ApplyDate = memberVO.ApplyDate.Value.AddHours(addHours);
            }
            if (memberVO.ApplyDate2.HasValue)
            {
                memberVO.ApplyDate2 = memberVO.ApplyDate2.Value.AddHours(addHours);
            }
            if (memberVO.DueDate.HasValue)
            {
                memberVO.DueDate = memberVO.DueDate.Value.AddHours(addHours);
            }
        }

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        [HttpDelete]
        public HttpResponseMessage Delete(string id)
        {
            int memberId;
            if (!string.IsNullOrWhiteSpace(id) && int.TryParse(id, out memberId))
            {
                try
                {
                    MemberVO oldMemberVO = m_MemberService.GetMemberById(memberId);
                    if (oldMemberVO != null)
                    {
                        oldMemberVO.NeedUpdate = false;
                        oldMemberVO.Status = "0";
                        oldMemberVO.UpdateId = "系統API";
                        m_MemberService.UpdateMember(oldMemberVO);

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.Gone);
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent, ex.ToString());
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
        }
    }
}