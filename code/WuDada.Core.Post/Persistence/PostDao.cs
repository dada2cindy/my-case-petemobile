using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Post.Domain;
using WuDada.Core.Persistence.Ado;
using WuDada.Core.Persistence;
using NHibernate.Criterion;
using System.Collections;
using WuDada.Core.Generic.Extension;
using Common.Logging;

namespace WuDada.Core.Post.Persistence
{
    public class PostDao : AdoDao, IPostDao
    {
        public INHibernateDao NHibernateDao { get; set; }        

        /// <summary>
        /// 新增Node
        /// </summary>
        /// <param name="nodeVO">被新增的Node</param>
        /// <returns>新增後的Node</returns>
        public NodeVO CreateNode(NodeVO nodeVO)
        {
            NHibernateDao.Insert(nodeVO);

            return nodeVO;
        }

        /// <summary>
        /// 取得Node By NodeId
        /// </summary>
        /// <param name="nodeId">NodeId</param>
        /// <returns>Node</returns>
        public NodeVO GetNodeById(int nodeId)
        {
            return NHibernateDao.GetVOById<NodeVO>(nodeId);
        }

        /// <summary>
        /// 取得Node By RootNode
        /// </summary>
        /// <returns>Node清單</returns>
        public IList<NodeVO> GetNodeListByParentId(int parentId)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<NodeVO>();
            dCriteria.CreateCriteria("ParentNode").Add(Expression.Eq("NodeId", parentId));
            dCriteria.AddOrder(Order.Asc("SortNo"));
            dCriteria.AddOrder(Order.Asc("Name"));

            int count = NHibernateDao.CountByDetachedCriteria(dCriteria);

            if (count == 0)
            {
                return null;
            }

            return NHibernateDao.SearchByDetachedCriteria<NodeVO>(dCriteria);
        }

        /// <summary>
        /// 取得Node By 父層Name
        /// </summary>
        /// <param name="name">父層Name</param>
        /// <returns>Node清單</returns>
        public IList<NodeVO> GetNodeListByParentName(string name)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<NodeVO>();
            dCriteria.CreateCriteria("ParentNode").Add(Expression.Eq("Name", name));
            dCriteria.AddOrder(Order.Asc("SortNo"));
            dCriteria.AddOrder(Order.Asc("Name"));

            int count = NHibernateDao.CountByDetachedCriteria(dCriteria);

            if (count == 0)
            {
                return null;
            }

            return NHibernateDao.SearchByDetachedCriteria<NodeVO>(dCriteria);
        }

        /// <summary>
        /// 新增Post
        /// </summary>
        /// <param name="postVO">被新增的Post</param>
        /// <returns>新增後的Post</returns>
        public PostVO CreatePost(PostVO postVO)
        {
            NHibernateDao.Insert(postVO);

            return postVO;
        }

        /// <summary>
        /// 取得Post By PostId
        /// </summary>
        /// <param name="postId">PostId</param>
        /// <returns>Post</returns>
        public PostVO GetPostById(int postId)
        {
            return NHibernateDao.GetVOById<PostVO>(postId);
        }

        /// <summary>
        /// 取得Post By NodeId
        /// </summary>
        /// <param name="nodeId">NodeId</param>
        /// <returns>Post清單</returns>
        public IList<PostVO> GetPostListByNodeId(int nodeId)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<PostVO>();
            dCriteria.CreateCriteria("Node").Add(Expression.Eq("NodeId", nodeId));
            dCriteria.Add(Expression.Eq("IsTemp", false));
            dCriteria.AddOrder(Order.Asc("SortNo"));
            dCriteria.AddOrder(Order.Desc("UpdatedDate"));

            int count = NHibernateDao.CountByDetachedCriteria(dCriteria);

            if (count == 0)
            {
                return null;
            }

            return NHibernateDao.SearchByDetachedCriteria<PostVO>(dCriteria);
        }

        /// <summary>
        /// 刪除Post
        /// </summary>
        /// <param name="postVO">被刪除的Post</param>
        public void DeletePost(PostVO postVO)
        {
            NHibernateDao.Delete(postVO);
        }

        /// <summary>
        /// 更新Post
        /// </summary>
        /// <param name="postVO">被更新的Post</param>
        /// <returns>更新後的Post</returns>
        public PostVO UpdatePost(PostVO postVO)
        {
            NHibernateDao.Update(postVO);

            return postVO;
        }

        /// <summary>
        /// 取得Post By NodeId
        /// </summary>
        /// <param name="nodeId">NodeId</param>
        /// <param name="onlyShow">僅抓取上架</param>
        /// <param name="sortField">排序欄位</param>
        /// <param name="sortDesc">升降冪排序</param>
        /// <returns>Post清單</returns>
        public IList<PostVO> GetPostListByNodeId(int nodeId, bool onlyShow, string sortField, bool sortDesc)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<PostVO>();
            dCriteria.CreateCriteria("Node").Add(Expression.Eq("NodeId", nodeId));
            dCriteria.Add(Expression.Eq("IsTemp", false));
            
            if (onlyShow)
            {
                dCriteria.Add(Expression.Eq("Flag", 1));                
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

            return NHibernateDao.SearchByDetachedCriteria<PostVO>(dCriteria);
        }

        /// <summary>
        /// 取得Post By NodeId
        /// </summary>
        /// <param name="nodeId">NodeId</param>
        /// <param name="onlyShow">僅抓取上架</param>
        /// <param name="startDate">起始的上架日期</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <param name="sortField">排序欄位</param>
        /// <param name="sortDesc">升降冪排序</param>
        /// <returns>Post清單</returns>
        public IList<PostVO> GetPostListByNodeId(int nodeId, bool onlyShow, DateTime? startShowDate, int pageIndex, int pageSize, string sortField, bool sortDesc)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<PostVO>();
            dCriteria.CreateCriteria("Node").Add(Expression.Eq("NodeId", nodeId));
            dCriteria.Add(Expression.Eq("IsTemp", false));
            
            if (onlyShow)
            {
                dCriteria.Add(Expression.Eq("Flag", 1));
            }

            if (startShowDate != null)
            {
                dCriteria.Add(Expression.Le("ShowDate", startShowDate));
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

            return NHibernateDao.SearchByDetachedCriteria<PostVO>(dCriteria, pageIndex, pageSize);
        }

        /// <summary>
        /// 取得Post筆數 By NodeId
        /// </summary>
        /// <param name="nodeId">NodeId</param>
        /// <param name="onlyShow">僅抓取上架</param>
        /// <param name="startDate">起始的上架日期</param>
        /// <returns>Post清單筆數</returns>
        public int CountPostListByNodeId(int nodeId, bool onlyShow, DateTime? startShowDate)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<PostVO>();
            dCriteria.CreateCriteria("Node").Add(Expression.Eq("NodeId", nodeId));
            dCriteria.Add(Expression.Eq("IsTemp", false));

            if (onlyShow)
            {
                dCriteria.Add(Expression.Eq("Flag", 1));
            }

            if (startShowDate != null)
            {
                dCriteria.Add(Expression.Le("ShowDate", startShowDate));
            }

            return NHibernateDao.CountByDetachedCriteria(dCriteria);
        }

        /// <summary>
        /// 刪除Node
        /// </summary>
        /// <param name="nodeVO">被刪除的Node</param>
        public void DeleteNode(NodeVO nodeVO)
        {
            NHibernateDao.Delete(nodeVO);
        }

        /// <summary>
        /// 更新Node
        /// </summary>
        /// <param name="nodeVO">被更新的Node</param>
        /// <returns>更新後的Node</returns>
        public NodeVO UpdateNode(NodeVO nodeVO)
        {
            NHibernateDao.Update(nodeVO);

            return nodeVO;
        }

        /// <summary>
        /// 取得Post By 父層PostId
        /// </summary>
        /// <param name="parentPostId">父層PostId</param>
        /// <returns>Post清單</returns>
        public IList<PostVO> GetPostListByParentPostId(int parentPostId)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<PostVO>();
            dCriteria.CreateCriteria("ParentPost").Add(Expression.Eq("PostId", parentPostId));
            dCriteria.Add(Expression.Eq("IsTemp", false));
            dCriteria.AddOrder(Order.Asc("SortNo"));

            int count = NHibernateDao.CountByDetachedCriteria(dCriteria);

            if (count == 0)
            {
                return null;
            }

            return NHibernateDao.SearchByDetachedCriteria<PostVO>(dCriteria);
        }

        /// <summary>
        /// 取得Post By 父層PostId
        /// </summary>
        /// <param name="parentPostId">父層PostId</param>
        /// <param name="onlyShow">僅抓取上架</param>
        /// <param name="startDate">目前顯示的日期</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <param name="sortField">排序欄位</param>
        /// <param name="sortDesc">升降冪排序</param>
        /// <returns>Post清單</returns>
        public IList<PostVO> GetPostListByParentPostId(int parentPostId, bool onlyShow, DateTime? showDate, int pageIndex, int pageSize, string sortField, bool sortDesc)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<PostVO>();
            dCriteria.CreateCriteria("ParentPost").Add(Expression.Eq("PostId", parentPostId));
            dCriteria.Add(Expression.Eq("IsTemp", false));

            if (onlyShow)
            {
                dCriteria.Add(Expression.Eq("Flag", 1));
            }

            if (showDate != null)
            {
                dCriteria.Add(Expression.Le("ShowDate", showDate.Value.Date));
                dCriteria.Add(Expression.Ge("CloseDate", showDate));
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

            return NHibernateDao.SearchByDetachedCriteria<PostVO>(dCriteria, pageIndex, pageSize);
        }

        /// <summary>
        /// 取得Post筆數 By ParentPostId
        /// </summary>
        /// <param name="parentPostId">父層PostId</param>
        /// <param name="onlyShow">僅抓取上架</param>
        /// <param name="showDate">目前顯示的日期</param>
        /// <returns>Post清單筆數</returns>
        public int CountPostListByParentPostId(int parentPostId, bool onlyShow, DateTime? showDate)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<PostVO>();
            dCriteria.CreateCriteria("ParentPost").Add(Expression.Eq("PostId", parentPostId));
            dCriteria.Add(Expression.Eq("IsTemp", false));

            if (onlyShow)
            {
                dCriteria.Add(Expression.Eq("Flag", 1));
            }

            if (showDate != null)
            {
                dCriteria.Add(Expression.Le("ShowDate", showDate.Value.Date));
                dCriteria.Add(Expression.Ge("CloseDate", showDate));
            }

            return NHibernateDao.CountByDetachedCriteria(dCriteria);
        }

        /// <summary>
        /// 取得Post By NodeId
        /// </summary>
        /// <param name="nodeId">NodeId</param>
        /// <param name="onlyShow">僅抓取上架</param>
        /// <param name="showDate">目前顯示的日期</param>
        /// <param name="sortField">排序欄位</param>
        /// <param name="sortDesc">升降冪排序</param>
        /// <returns>Post清單</returns>
        public IList<PostVO> GetPostListByNodeId(int nodeId, bool onlyShow, DateTime? showDate, string sortField, bool sortDesc)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<PostVO>();
            dCriteria.CreateCriteria("Node").Add(Expression.Eq("NodeId", nodeId));
            dCriteria.Add(Expression.Eq("IsTemp", false));

            if (onlyShow)
            {
                dCriteria.Add(Expression.Eq("Flag", 1));
            }

            if (showDate != null)
            {
                dCriteria.Add(Expression.Le("ShowDate", showDate.Value.Date));
                dCriteria.Add(Expression.Ge("CloseDate", showDate));
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

            return NHibernateDao.SearchByDetachedCriteria<PostVO>(dCriteria);
        }

        /// <summary>
        /// 取得Post By 父層ParentPostId
        /// </summary>
        /// <param name="parentPostId">父層PostId</param>
        /// <param name="onlyShow">僅抓取上架</param>
        /// <param name="showDate">目前顯示的日期</param>
        /// <param name="sortField">排序欄位</param>
        /// <param name="sortDesc">升降冪排序</param>
        /// <returns>Post清單</returns>
        public IList<PostVO> GetPostListByParentPostId(int parentPostId, bool onlyShow, DateTime? showDate, string sortField, bool sortDesc)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<PostVO>();
            dCriteria.CreateCriteria("ParentPost").Add(Expression.Eq("PostId", parentPostId));
            dCriteria.Add(Expression.Eq("IsTemp", false));

            if (onlyShow)
            {
                dCriteria.Add(Expression.Eq("Flag", 1));
            }

            if (showDate != null)
            {
                dCriteria.Add(Expression.Le("ShowDate", showDate.Value.Date));
                dCriteria.Add(Expression.Ge("CloseDate", showDate));
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

            return NHibernateDao.SearchByDetachedCriteria<PostVO>(dCriteria);
        }

        /// <summary>
        /// 動態查詢Post
        /// </summary>
        /// <param name="where">搜尋語法，用Where...order by ...</param>
        /// <returns>搜尋結果</returns>
        public IList<PostVO> SearchPostByWhere(string where)
        {
            return NHibernateDao.SearchByWhere<PostVO>(where);
        }

        /// <summary>
        /// 動態查詢Post
        /// </summary>
        /// <param name="where">搜尋語法，用Where...order by ...</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <returns>搜尋結果</returns>
        public IList<PostVO> SearchPostByWhere(string where, int pageIndex, int pageSize)
        {
            return NHibernateDao.SearchByWhere<PostVO>(where, pageIndex, pageSize);
        }

        /// <summary>
        /// 動態查詢筆數Post
        /// </summary>
        /// <param name="where">搜尋語法，用Where...order by ...</param>
        /// <returns>筆數</returns>
        public int CountPostByWhere(string where)
        {
            return NHibernateDao.CountByWhere<PostVO>(where);
        }

        /// <summary>
        /// 取得Post清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>電子Post清單</returns>
        public IList<PostVO> GetPostList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select p from PostVO p ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryPost(param, fromScript, whereScript, conditions, true).OfType<PostVO>().ToList();
        }

        /// <summary>
        /// 取得Post數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetPostCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(p.PostId) from PostVO p ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryPost(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        /// <summary>
        /// 取得數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetTotalQuantity(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select sum(p.Quantity) from PostVO p ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryPost(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }

        private IList QueryPost(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendPostKeyWord(conditions, whereScript, param);
            AppendPostNode(conditions, whereScript, param);
            AppendPostParentPost(conditions, whereScript, param);
            AppendPostFlag(conditions, whereScript, param);
            AppendPostIsRecommend(conditions, whereScript, param);
            AppendPostDate(conditions, whereScript, param);
            AppendPostType(conditions, whereScript, param);
            AppendPostCustomField1(conditions, whereScript, param);
            AppendPostEqualTitle(conditions, whereScript, param);
            AppendPostCustomField2(conditions, whereScript, param);
            AppendPostProductSer(conditions, whereScript, param);
            AppendPostWithOutMemberId(conditions, whereScript, param);
            AppendPostMemberId(conditions, whereScript, param);
            AppendPostNeedUpdate(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendPostOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private void AppendPostNeedUpdate(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("NeedUpdate"))
            {
                whereScript.Append(" and p.NeedUpdate = ? ");
                param.Add(bool.Parse(conditions["NeedUpdate"]));
            }
        }

        private void AppendPostMemberId(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("MemberId"))
            {
                whereScript.Append(" and p.MemberId = ? ");
                param.Add(conditions["MemberId"]);
            }
        }

        private void AppendPostWithOutMemberId(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("WithOutMemberId"))
            {
                whereScript.Append(" and (p.MemberId is null or  p.MemberId = '')");                
            }
        }

        private void AppendPostProductSer(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("ProductSer"))
            {
                whereScript.Append(" and p.ProductSer = ? ");
                param.Add(conditions["ProductSer"]);
            }
        }

        private void AppendPostCustomField2(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("CustomField2"))
            {
                whereScript.Append(" and p.CustomField2 = ? ");
                param.Add(conditions["CustomField2"]);
            }
        }

        private void AppendPostEqualTitle(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("EqualTitle"))
            {
                whereScript.Append(" and p.Title = ? ");
                param.Add(conditions["EqualTitle"]);
            }
        }

        private void AppendPostCustomField1(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("CustomField1"))
            {
                whereScript.Append(" and p.CustomField1 = ? ");
                param.Add(conditions["CustomField1"]);
            }
        }

        private void AppendPostType(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Type"))
            {
                whereScript.Append(" and p.Type = ? ");
                param.Add(conditions["Type"]);
            }
        }

        private void AppendPostDate(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            if (conditions.IsContainsValue("ShowDateStart"))
            {
                whereScript.Append(" and p.ShowDate >= ? ");
                param.Add(Convert.ToDateTime(conditions["ShowDateStart"]));
            }
            if (conditions.IsContainsValue("ShowDateEnd"))
            {
                whereScript.Append(" and p.ShowDate <= ? ");
                m_Log.Debug("p.ShowDate <=" + (Convert.ToDateTime(conditions["ShowDateEnd"]).AddDays(1).AddMinutes(-1)).ToString());
                param.Add(Convert.ToDateTime(conditions["ShowDateEnd"]).AddDays(1).AddMinutes(-1));
            }
            if (conditions.IsContainsValue("CloseDateStart"))
            {
                whereScript.Append(" and p.CloseDate >= ? ");
                param.Add(Convert.ToDateTime(conditions["CloseDateStart"]));
            }
            if (conditions.IsContainsValue("CloseDateEnd"))
            {
                whereScript.Append(" and p.CloseDate <= ? ");
                m_Log.Debug("p.CloseDateEnd <=" + (Convert.ToDateTime(conditions["CloseDateEnd"]).AddDays(1).AddMinutes(-1)).ToString());
                param.Add(Convert.ToDateTime(conditions["CloseDateEnd"]).AddDays(1).AddMinutes(-1));
            }

            if (conditions.IsContainsValue("CloseDate"))
            {
                whereScript.Append(" and p.CloseDate = ? ");
                param.Add(Convert.ToDateTime(conditions["CloseDate"]));
            }

            if (conditions.IsContainsValue("ShowDate"))
            {
                whereScript.Append(" and p.ShowDate = ? ");
                param.Add(Convert.ToDateTime(conditions["ShowDate"]));
            }
        }

        private void AppendPostIsRecommend(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("IsRecommend"))
            {
                whereScript.Append(" and p.IsRecommend = ? ");
                param.Add(bool.Parse(conditions["IsRecommend"]));
            }
        }

        private void AppendPostFlag(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Flag"))
            {
                whereScript.Append(" and p.Flag = ? ");
                param.Add(conditions["Flag"]);
            }
        }

        private void AppendPostParentPost(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("ParentPostId"))
            {
                whereScript.Append(" and p.ParentPost.PostId = ? ");
                param.Add(conditions["ParentPostId"]);
            }
        }

        private void AppendPostNode(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("ParentNodeId"))
            {
                whereScript.Append(" and p.Node.ParentNode.NodeId = ? ");
                param.Add(conditions["ParentNodeId"]);
            }
            else
            {
                if (conditions.IsContainsValue("NodeId"))
                {
                    whereScript.Append(" and p.Node.NodeId = ? ");
                    param.Add(conditions["NodeId"]);
                }
            }
        }

        private void AppendPostKeyWord(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("KeyWord"))
            {
                whereScript.Append(" and (p.Title like ? or p.HtmlContent like ? or p.CustomField1 like ? or p.MemberName like ? or p.MemberPhone like ? or p.ProductSer like ? ) ");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
                param.Add("%" + conditions["KeyWord"] + "%");
            }
        }

        private string AppendPostOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by p.SortNo, p.ShowDate desc ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }

        /// <summary>
        /// 取得Node By Name
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Node</returns>
        public NodeVO GetNodeByName(string name)
        {
            DetachedCriteria dCriteria = DetachedCriteria.For<NodeVO>();
            dCriteria.Add(Expression.Eq("Name", name));

            int count = NHibernateDao.CountByDetachedCriteria(dCriteria);

            if (count == 0)
            {
                return null;
            }

            return NHibernateDao.SearchByDetachedCriteria<NodeVO>(dCriteria).FirstOrDefault();
        }
    }
}
