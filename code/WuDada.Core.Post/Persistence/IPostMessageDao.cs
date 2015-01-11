using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Post.Domain;

namespace WuDada.Core.Post.Persistence
{
    public interface IPostMessageDao
    {
        /// <summary>
        /// 新增Post的留言
        /// </summary>
        /// <param name="postPostMessageVO">被新增的Post的留言</param>
        /// <returns>新增後的Post的留言</returns>
        PostMessageVO CreatePostMessage(PostMessageVO postPostMessageVO);

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
        IList<PostMessageVO> GetPostMessageListByPostId(int postId, DateTime? startDate, DateTime? endDate
            , int pageIndex, int pageSize, string sortField, bool sortDesc);

        /// <summary>
        /// 取得Post的留言筆數
        /// </summary>
        /// <param name="postId">PostId</param>
        /// <param name="startDate">起始時間</param>
        /// <param name="endDate">結束時間</param>
        /// <returns>Post的留言筆數</returns>
        int CountPostMessageByPostId(int postId, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// 刪除Post的留言
        /// </summary>
        /// <param name="postPostMessageVO">被刪除的Post的留言</param>
        void DeletePostMessage(PostMessageVO postPostMessageVO);

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
        IList<PostMessageVO> GetPostMessageListByParentId(int parentId, DateTime? startDate, DateTime? endDate
            , int pageIndex, int pageSize, string sortField, bool sortDesc);

        /// <summary>
        /// 取得Post的留言筆數
        /// </summary>
        /// <param name="parentId">parentId</param>
        /// <param name="startDate">起始時間</param>
        /// <param name="endDate">結束時間</param>
        /// <returns>Post的留言筆數</returns>
        int CountPostMessageByParentId(int parentId, DateTime? startDate, DateTime? endDate);
    }
}
