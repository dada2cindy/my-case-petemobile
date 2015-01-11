using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Post.Domain;
using WuDada.Core.Post.Persistence;

namespace WuDada.Core.Post.Service.Impl
{
    public class PostMessageService : IPostMessageService
    {
        public IPostMessageDao PostMessageDao { get; set; }

        /// <summary>
        /// 新增Post的留言
        /// </summary>
        /// <param name="postMessageVO">被新增的Post的留言</param>
        /// <returns>新增後的Post的留言</returns>
        public PostMessageVO CreatePostMessage(PostMessageVO postMessageVO)
        {
            return PostMessageDao.CreatePostMessage(postMessageVO);
        }

        /// <summary>
        /// 取得Post的留言清單
        /// </summary>
        /// <param name="postId">PostId</param>
        /// <param name="startDate">起始時間</param>
        /// <param name="endDate">結束時間</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <param name="sortField">排序欄位</param>
        /// <param name="sortDesc">升降冪排序</param>
        /// <returns>Post的留言清單</returns>
        public IList<PostMessageVO> GetPostMessageListByPostId(int postId, DateTime? startDate, DateTime? endDate
            , int pageIndex, int pageSize, string sortField, bool sortDesc)
        {
            return PostMessageDao.GetPostMessageListByPostId(postId, startDate, endDate, pageIndex, pageSize, sortField, sortDesc);
        }

        /// <summary>
        /// 取得Post的留言筆數
        /// </summary>
        /// <param name="postId">PostId</param>
        /// <param name="startDate">起始時間</param>
        /// <param name="endDate">結束時間</param>
        /// <returns>Post的留言筆數</returns>
        public int CountPostMessageByPostId(int postId, DateTime? startDate, DateTime? endDate)
        {
            return PostMessageDao.CountPostMessageByPostId(postId, startDate, endDate);
        }

        /// <summary>
        /// 刪除Post的留言
        /// </summary>
        /// <param name="postMessageVO">被刪除的Post的留言</param>
        public void DeletePostMessage(PostMessageVO postMessageVO)
        {
            PostMessageDao.DeletePostMessage(postMessageVO);
        }

        /// <summary>
        /// 取得Post的留言清單
        /// </summary>
        /// <param name="parentId">parentId</param>
        /// <param name="startDate">起始時間</param>
        /// <param name="endDate">結束時間</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <param name="sortField">排序欄位</param>
        /// <param name="sortDesc">升降冪排序</param>
        /// <returns>Post的留言清單</returns>
        public IList<PostMessageVO> GetPostMessageListByParentId(int parentId, DateTime? startDate, DateTime? endDate
            , int pageIndex, int pageSize, string sortField, bool sortDesc)
        {
            return PostMessageDao.GetPostMessageListByParentId(parentId, startDate, endDate, pageIndex, pageSize, sortField, sortDesc);
        }

        /// <summary>
        /// 取得Post的留言筆數
        /// </summary>
        /// <param name="parentId">parentId</param>
        /// <param name="startDate">起始時間</param>
        /// <param name="endDate">結束時間</param>
        /// <returns>Post的留言筆數</returns>
        public int CountPostMessageByParentId(int parentId, DateTime? startDate, DateTime? endDate)
        {
            return PostMessageDao.CountPostMessageByParentId(parentId, startDate, endDate);
        }
    }
}
