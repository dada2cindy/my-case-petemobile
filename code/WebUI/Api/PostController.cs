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
using WuDada.Core.Post.Domain;
using WuDada.Core.Post.Service;

namespace WebUI.Api
{
    public class PostController : ApiController
    {
        private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static ConfigHelper m_ConfigHelper = new ConfigHelper();
        private static PostFactory m_PostFactory = new PostFactory();
        private static AuthFactory m_AuthFactory = new AuthFactory();
        private static MemberFactory m_MemberFactory = new MemberFactory();
        private static IMemberService m_MemberService = m_MemberFactory.GetMemberService();
        private static IPostFileService m_PostFileService = m_PostFactory.GetPostFileService();
        private static IAuthService m_AuthService = m_AuthFactory.GetAuthService();
        private static IPostService m_PostService = m_PostFactory.GetPostService();
        //private WebLogService m_WebLogService;
        //private HttpHelper m_HttpHelper;        

        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            IList<PostDto> result = new List<PostDto>();

            Dictionary<string, string> conditions = new Dictionary<string, string>();
            conditions.Add("Flag", "1");
            conditions.Add("NodeId", "2");
            conditions.Add("PageIndex", "0");
            conditions.Add("PageSize", "10");
            //conditions.Add("Order", "order by f.FileId");

            IList<PostVO> postVOList = m_PostService.GetPostList(conditions);
            if (postVOList != null && postVOList.Count > 0)
            {
                foreach (PostVO vo in postVOList)
                {
                    result.Add(new PostDto(vo));
                }
            }

            return Request.CreateResponse<IList<PostDto>>(HttpStatusCode.OK, result);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public HttpResponseMessage Post(PostDto postDto)
        {
            if (postDto != null)
            {
                try
                {
                    PostVO postVO = null;
                    //檢查是否有ServerId 有的話把狀態改成刪除, 重新建立一筆
                    if (postDto.ServerId != 0)
                    {
                        PostVO oldPostVO = m_PostService.GetPostById(postDto.ServerId);
                        if (oldPostVO != null)
                        {
                            oldPostVO.NeedUpdate = false;
                            oldPostVO.Flag = 0;
                            oldPostVO.UpdateId = "系統API";
                            m_PostService.UpdatePost(oldPostVO);
                        }
                    }

                    postVO = new PostVO(postDto);
                    postVO.PostId = 0;
                    postVO.ServerId = 0;
                    postVO.NeedUpdate = false;
                    postVO.UpdateId = "系統API";
                    FixTimeZone(postVO);

                    //有memberId 就要用Ser去查Server的Member
                    if (!string.IsNullOrWhiteSpace(postDto.MemberId) && !string.IsNullOrWhiteSpace(postDto.ProductSer))
                    {
                        Dictionary<string, string> conditions = new Dictionary<string, string>();
                        conditions.Add("Status", "1");
                        conditions.Add("KeyWord", postDto.ProductSer);
                        conditions.Add("Store", postDto.Store);

                        IList<MemberVO> memberList = m_MemberService.GetMemberList(conditions);
                        if (memberList != null && memberList.Count > 0 && postDto.ProductSer.Equals(memberList[0].PhoneSer))
                        {
                            postVO.MemberId = memberList[0].MemberId.ToString();
                        }
                    }

                    postVO = m_PostService.CreatePost(postVO);
                    postVO.ServerId = postVO.PostId;                    

                    return Request.CreateResponse<PostDto>(HttpStatusCode.Created, new PostDto(postVO));
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

        private void FixTimeZone(PostVO postVO)
        {
            int addHours = m_ConfigHelper.AddHours;

            if (postVO.ShowDate.HasValue)
            {
                postVO.ShowDate = postVO.ShowDate.Value.AddHours(addHours);
            }

            if (postVO.CloseDate.HasValue)
            {
                postVO.CloseDate = postVO.CloseDate.Value.AddHours(addHours);
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
            int postId;
            if (!string.IsNullOrWhiteSpace(id) && int.TryParse(id, out postId))
            {
                try
                {
                    PostVO postVO = m_PostService.GetPostById(postId);
                    if (postVO != null)
                    {
                        postVO.NeedUpdate = false;
                        postVO.Flag = 0;
                        postVO.UpdateId = "系統API";
                        m_PostService.UpdatePost(postVO);

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