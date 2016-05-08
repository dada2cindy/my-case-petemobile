using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common.Logging;
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
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}