using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Post.Domain;
using WuDada.Core.Persistence;
using NHibernate.Criterion;
using WuDada.Core.Persistence.Ado;

namespace WuDada.Core.Post.Persistence
{
    public class PostMessageDao : AdoDao, IPostMessageDao
    {
        public INHibernateDao NHibernateDao { get; set; }

        /// <summary>
        /// 新增Post的留言
        /// </summary>
        /// <param name="postMessageVO">被新增的Post的留言</param>
        /// <returns>新增後的Post的留言</returns>
        public PostMessageVO CreatePostMessage(Domain.PostMessageVO postMessageVO)
        {
            NHibernateDao.Insert(postMessageVO);

            return postMessageVO;
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
            DetachedCriteria dCriteria = DetachedCriteria.For<PostMessageVO>();

            dCriteria.CreateCriteria("Post").Add(Expression.Eq("PostId", postId));
            

            if (startDate != null)
            {
                dCriteria.Add(Expression.Ge("CreatedDate", startDate));
            }
            if (endDate != null)
            {
                dCriteria.Add(Expression.Le("CreatedDate", endDate));
            }

            if (!string.IsNullOrEmpty(sortField))
            {
                if (sortDesc)
                {
                    dCriteria.AddOrder(Order.Desc(sortField));
                }
                else
                {
                    dCriteria.AddOrder(Order.Asc(sortField));
                }
            }

            int count = NHibernateDao.CountByDetachedCriteria(dCriteria);

            if (count == 0)
            {
                return null;
            }

            return NHibernateDao.SearchByDetachedCriteria<PostMessageVO>(dCriteria, pageIndex, pageSize);
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
            DetachedCriteria dCriteria = DetachedCriteria.For<PostMessageVO>();

            dCriteria.CreateCriteria("Post").Add(Expression.Eq("PostId", postId));

            if (startDate != null)
            {
                dCriteria.Add(Expression.Ge("CreatedDate", startDate));
            }
            if (endDate != null)
            {
                dCriteria.Add(Expression.Le("CreatedDate", endDate));
            }

            return NHibernateDao.CountByDetachedCriteria(dCriteria);
        }

        /// <summary>
        /// 刪除Post的留言
        /// </summary>
        /// <param name="postMessageVO">被刪除的Post的留言</param>
        public void DeletePostMessage(PostMessageVO postMessageVO)
        {
            NHibernateDao.Delete(postMessageVO);
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
            DetachedCriteria dCriteria = DetachedCriteria.For<PostMessageVO>();

            dCriteria.CreateCriteria("ParentPostMessage").Add(Expression.Eq("PostMessageId", parentId));


            if (startDate != null)
            {
                dCriteria.Add(Expression.Ge("CreatedDate", startDate));
            }
            if (endDate != null)
            {
                dCriteria.Add(Expression.Le("CreatedDate", endDate));
            }

            if (!string.IsNullOrEmpty(sortField))
            {
                if (sortDesc)
                {
                    dCriteria.AddOrder(Order.Desc(sortField));
                }
                else
                {
                    dCriteria.AddOrder(Order.Asc(sortField));
                }
            }

            int count = NHibernateDao.CountByDetachedCriteria(dCriteria);

            if (count == 0)
            {
                return null;
            }

            return NHibernateDao.SearchByDetachedCriteria<PostMessageVO>(dCriteria, pageIndex, pageSize);
        }

        /// <summary>
        /// 取得Post的留言筆數
        /// </summary>
        /// <param name="postId">parentId</param>
        /// <param name="startDate">起始時間</param>
        /// <param name="endDate">結束時間</param>
        /// <returns>Post的留言筆數</returns>
        public int CountPostMessageByParentId(int parentId, DateTime? startDate, DateTime? endDate)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<PostMessageVO>();

            dCriteria.CreateCriteria("ParentPostMessage").Add(Expression.Eq("PostMessageId", parentId));

            if (startDate != null)
            {
                dCriteria.Add(Expression.Ge("CreatedDate", startDate));
            }
            if (endDate != null)
            {
                dCriteria.Add(Expression.Le("CreatedDate", endDate));
            }

            return NHibernateDao.CountByDetachedCriteria(dCriteria);
        }
    }
}
