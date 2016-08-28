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
    public class PostFileController : ApiController
    {
        private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static ConfigHelper m_ConfigHelper = new ConfigHelper();
        private static PostFactory m_PostFactory = new PostFactory();
        private static AuthFactory m_AuthFactory = new AuthFactory();
        private static MemberFactory m_MemberFactory = new MemberFactory();
        private static IPostFileService m_PostFileService = m_PostFactory.GetPostFileService();
        private static IAuthService m_AuthService = m_AuthFactory.GetAuthService();
        private static IPostService m_PostService = m_PostFactory.GetPostService();
        //private WebLogService m_WebLogService;
        //private HttpHelper m_HttpHelper;        

        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            IList<FileDto> result = new List<FileDto>();

            Dictionary<string, string> conditions = new Dictionary<string, string>();
            conditions.Add("Flag", "1");
            conditions.Add("PageIndex", "0");
            conditions.Add("PageSize", "10");
            //conditions.Add("Order", "order by f.FileId");

            IList<FileVO> fileVOList = m_PostFileService.GetFileList(conditions);
            if (fileVOList != null && fileVOList.Count > 0)
            {
                foreach (FileVO vo in fileVOList)
                {
                    result.Add(new FileDto(vo));
                }
            }

            return Request.CreateResponse<IList<FileDto>>(HttpStatusCode.OK, result);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public HttpResponseMessage Post(FileDto fileDto)
        {
            if (fileDto != null)
            {
                try
                {
                    FileVO fileVO = null;
                    //檢查是否有ServerId 有的話把狀態改成刪除, 重新建立一筆
                    if (fileDto.ServerId != 0)
                    {
                        FileVO oldFileVO = m_PostFileService.GetFileById(fileDto.ServerId);
                        if (oldFileVO != null)
                        {
                            oldFileVO.NeedUpdate = false;
                            oldFileVO.Flag = 0;
                            oldFileVO.UpdateId = "系統API";
                            m_PostFileService.UpdateFile(oldFileVO);
                        }
                    }

                    fileVO = new FileVO(fileDto);
                    fileVO.FileId = 0;
                    fileVO.ServerId = 0;
                    fileVO.NeedUpdate = false;
                    fileVO.UpdateId = "系統API";
                    FixTimeZone(fileVO);
                    fileVO = m_PostFileService.UpdateFile(fileVO);
                    fileVO.ServerId = fileVO.FileId;

                    return Request.CreateResponse<FileDto>(HttpStatusCode.Created, new FileDto(fileVO));
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

        private void FixTimeZone(FileVO fileVO)
        {
            int addHours = m_ConfigHelper.AddHours;

            if (fileVO.ShowDate.HasValue)
            {
                fileVO.ShowDate = fileVO.ShowDate.Value.AddHours(addHours);
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
            int fileId;
            if (!string.IsNullOrWhiteSpace(id) && int.TryParse(id, out fileId))
            {
                try
                {
                    FileVO oldFileVO = m_PostFileService.GetFileById(fileId);
                    if (oldFileVO != null)
                    {
                        oldFileVO.NeedUpdate = false;
                        oldFileVO.Flag = 0;
                        oldFileVO.UpdateId = "系統API";
                        m_PostFileService.UpdateFile(oldFileVO);

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