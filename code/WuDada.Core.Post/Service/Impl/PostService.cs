using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Post.Domain;
using WuDada.Core.Post.Persistence;
using NHibernate;

namespace WuDada.Core.Post.Service.Impl
{
    public class PostService : IPostService
    {
        public IPostDao PostDao { get; set; }

        public int RootNodeId { get; set; }

        /// <summary>
        /// 新增Node
        /// </summary>
        /// <param name="nodeVO">被新增的Node</param>
        /// <returns>新增後的Node</returns>
        public NodeVO CreateNode(NodeVO nodeVO)
        {
            //return PostDao.CreateNode(nodeVO);
            nodeVO = PostDao.CreateNode(nodeVO);
            if (nodeVO.SortNo == 0)
            {
                nodeVO.SortNo = nodeVO.NodeId;
                nodeVO = PostDao.UpdateNode(nodeVO);
            }
            return nodeVO;
        }

        /// <summary>
        /// 取得Node By NodeId
        /// </summary>
        /// <param name="nodeId">NodeId</param>
        /// <returns>Node</returns>
        public NodeVO GetNodeById(int nodeId)
        {
            return PostDao.GetNodeById(nodeId);
        }

        /// <summary>
        /// 取得Node By 父層Id
        /// </summary>
        /// <param name="parentId">父層Id</param>
        /// <returns>Node清單</returns>
        public IList<NodeVO> GetNodeListByParentId(int parentId)
        {
            return PostDao.GetNodeListByParentId(parentId);
        }

        /// <summary>
        /// 取得Node By 父層Name
        /// </summary>
        /// <param name="name">父層Name</param>
        /// <returns>Node清單</returns>
        public IList<NodeVO> GetNodeListByParentName(string name)
        {
            return PostDao.GetNodeListByParentName(name);
        }

        /// <summary>
        /// 取得Node By RootNode
        /// </summary>
        /// <returns>Node清單</returns>
        public IList<NodeVO> GetNodeListByRoot()
        {
            return PostDao.GetNodeListByParentId(this.RootNodeId);
        }

        /// <summary>
        /// 新增Post
        /// </summary>
        /// <param name="postVO">被新增的Post</param>
        /// <returns>新增後的Post</returns>
        public PostVO CreatePost(PostVO postVO)
        {
            postVO = PostDao.CreatePost(postVO);
            if (postVO.SortNo == 0)
            {
                postVO.SortNo = postVO.PostId;
                postVO = PostDao.UpdatePost(postVO);
            }
            return postVO;
        }

        /// <summary>
        /// 取得Post By PostId
        /// </summary>
        /// <param name="postId">PostId</param>
        /// <returns>Post</returns>
        public PostVO GetPostById(int postId)
        {
            return PostDao.GetPostById(postId);
        }

        /// <summary>
        /// 取得Post By NodeId
        /// </summary>
        /// <param name="nodeId">NodeId</param>
        /// <returns>Post清單</returns>
        public IList<PostVO> GetPostListByNodeId(int nodeId)
        {
            return PostDao.GetPostListByNodeId(nodeId);
        }

        /// <summary>
        /// 刪除Post
        /// </summary>
        /// <param name="postVO">被刪除的Post</param>
        public void DeletePost(PostVO postVO)
        {
            PostDao.DeletePost(postVO);
        }

        /// <summary>
        /// 更新Post
        /// </summary>
        /// <param name="postVO">被更新的Post</param>
        /// <returns>更新後的Post</returns>
        public PostVO UpdatePost(PostVO postVO)
        {
            return PostDao.UpdatePost(postVO);
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
            return PostDao.GetPostListByNodeId(nodeId, onlyShow, sortField, sortDesc);
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
        public IList<PostVO> GetPostListByNodeId(int nodeId, bool onlyShow, DateTime? startShowDate
            , int pageIndex, int pageSize, string sortField, bool sortDesc)
        {
            return PostDao.GetPostListByNodeId(nodeId, onlyShow, startShowDate, pageIndex, pageSize, sortField, sortDesc);
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
            return PostDao.CountPostListByNodeId(nodeId, onlyShow, startShowDate);
        }

        /// <summary>
        /// 刪除Node
        /// </summary>
        /// <param name="nodeVO">被刪除的Node</param>
        public void DeleteNode(NodeVO nodeVO)
        {
            PostDao.DeleteNode(nodeVO);
        }

        /// <summary>
        /// 更新Node
        /// </summary>
        /// <param name="nodeVO">被更新的Node</param>
        /// <returns>更新後的Node</returns>
        public NodeVO UpdateNode(NodeVO nodeVO)
        {
            return PostDao.UpdateNode(nodeVO);
        }

        /// <summary>
        /// 取得Post By 父層PostId
        /// </summary>
        /// <param name="parentPostId">父層PostId</param>
        /// <returns>Post清單</returns>
        public IList<PostVO> GetPostListByParentPostId(int parentPostId)
        {
            return PostDao.GetPostListByParentPostId(parentPostId);
        }

        /// <summary>
        /// 取得Post By 父層ParentPostId
        /// </summary>
        /// <param name="parentPostId">父層PostId</param>
        /// <param name="onlyShow">僅抓取上架</param>
        /// <param name="showDate">目前顯示的日期</param>
        /// <param name="pageIndex">分頁索引</param>
        /// <param name="pageSize">分頁大小</param>
        /// <param name="sortField">排序欄位</param>
        /// <param name="sortDesc">升降冪排序</param>
        /// <returns>Post清單</returns>
        public IList<PostVO> GetPostListByParentPostId(int parentPostId, bool onlyShow, DateTime? showDate, int pageIndex, int pageSize, string sortField, bool sortDesc)
        {
            return PostDao.GetPostListByParentPostId(parentPostId, onlyShow, showDate, pageIndex, pageSize, sortField, sortDesc);
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
            return PostDao.CountPostListByParentPostId(parentPostId, onlyShow, showDate);
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
            return PostDao.GetPostListByNodeId(nodeId, onlyShow, showDate, sortField, sortDesc);
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
            return PostDao.GetPostListByParentPostId(parentPostId, onlyShow, showDate, sortField, sortDesc);
        }

        /// <summary>
        /// 動態查詢Post
        /// </summary>
        /// <param name="where">搜尋語法，用Where...order by ...</param>
        /// <returns>搜尋結果</returns>
        public IList<PostVO> SearchPostByWhere(string where)
        {
            return PostDao.SearchPostByWhere(where);
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
            return PostDao.SearchPostByWhere(where, pageIndex, pageSize);
        }

        /// <summary>
        /// 動態查詢筆數Post
        /// </summary>
        /// <param name="where">搜尋語法，用Where...order by ...</param>
        /// <returns>筆數</returns>
        public int CountPostByWhere(string where)
        {
            return PostDao.CountPostByWhere(where);
        }

        /// <summary>
        /// 取得Post By PostId No Lazy
        /// </summary>
        /// <param name="postId">PostId</param>
        /// <returns>Post</returns>
        public PostVO GetPostByIdNoLazy(int postId)
        {
            PostVO postVO = PostDao.GetPostById(postId);

            if (postVO != null && postVO.ParentPost != null)
            {
                NHibernateUtil.Initialize(postVO.ParentPost);

                if (postVO.ParentPost.ParentPost != null)
                {
                    NHibernateUtil.Initialize(postVO.ParentPost.ParentPost);
                }                
            }

            if (postVO.Node != null)
            {
                NHibernateUtil.Initialize(postVO.Node);
            }

            return postVO;
        }

        /// <summary>
        /// 取得Post清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>電子Post清單</returns>
        public IList<PostVO> GetPostList(IDictionary<string, string> conditions)
        {
            return PostDao.GetPostList(conditions);
        }

        /// <summary>
        /// 取得Post數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetPostCount(IDictionary<string, string> conditions)
        {
            return PostDao.GetPostCount(conditions);
        }

        /// <summary>
        /// 取得數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetTotalQuantity(IDictionary<string, string> conditions)
        {
            return PostDao.GetTotalQuantity(conditions);
        }

        /// <summary>
        /// 取得Node By Name
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Node</returns>
        public NodeVO GetNodeByName(string name)
        {
            return PostDao.GetNodeByName(name);
        }

        /// <summary>
        /// 抓出推薦的手機折扣
        /// <param name="count">數量</param>
        /// <returns>搜尋結果</returns>
        public IList<PromoteVO> GetPromoteList(int count)
        {
            IList<PromoteVO> result = new List<PromoteVO>();

            //50強手機
            Dictionary<string, string> conditionsPhone = new Dictionary<string, string>();
            conditionsPhone.Add("NodeId", "3");
            conditionsPhone.Add("PageIndex", "0");
            conditionsPhone.Add("PageSize", count.ToString());
            conditionsPhone.Add("Order", string.Format("order by {0}", "p.WarrantySuppliers, p.SortNo, p.Title"));

            IList<PostVO> phoneList = PostDao.GetPostList(conditionsPhone);

            if (phoneList != null && phoneList.Count > 0)
            {
                foreach (PostVO vo in phoneList)
                {
                    PromoteVO promoteVO = new PromoteVO();
                    promoteVO.WarrantySuppliers = vo.WarrantySuppliers;
                    promoteVO.SortNo = vo.SortNo;
                    promoteVO.Title = vo.Title;
                    promoteVO.SellPrice = vo.SellPrice.ToString();
                    promoteVO.SellPriceW1 = GetPromoteSellPrice(vo.SellPrice, "中華電信");
                    promoteVO.SellPriceW2 = GetPromoteSellPrice(vo.SellPrice, "遠傳");
                    promoteVO.SellPriceW3 = GetPromoteSellPrice(vo.SellPrice, "台哥大");
                    promoteVO.SellPriceW4 = GetPromoteSellPrice(vo.SellPrice, "亞太電信");
                    promoteVO.SellPriceW5 = GetPromoteSellPrice(vo.SellPrice, "台灣之星");

                    result.Add(promoteVO);
                }
            }


            return result;
        }

        private string GetPromoteSellPrice(double? sellPrice, string warrantySuppliers)
        {
            string result = "特價中";

            //搜尋條件
            Dictionary<string, string> conditions = new Dictionary<string, string>();
            conditions.Add("NodeId", "5");
            conditions.Add("ProductKeyWord", warrantySuppliers);
            conditions.Add("PageIndex", "0");
            conditions.Add("PageSize", "1");
            conditions.Add("IsPromote", "true");
            conditions.Add("Order", string.Format("order by {0}", "p.WarrantySuppliers, p.Type, p.SortNo, p.PostId"));

            IList<PostVO> discountList = PostDao.GetPostList(conditions);

            if (discountList != null && discountList.Count > 0)
            {
                PostVO discount = discountList[0];
                double discountPrice = 0;

                if (sellPrice.HasValue && double.TryParse(discount.CustomField1, out discountPrice))
                {
                    double finalPrice = sellPrice.Value - discountPrice;

                    if (finalPrice > 0)
                    {
                        result = finalPrice.ToString();
                    }
                    else
                    {
                        result = "0";
                    }
                }
            }

            return result;
        }
    }
}
