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

namespace WuDada.Core.Post.Persistence
{
    public class PostFileDao : AdoDao, IPostFileDao
    {
        public INHibernateDao NHibernateDao { get; set; }

        /// <summary>
        /// 新增File
        /// </summary>
        /// <param name="fileVO">File</param>
        /// <returns>新增後的File</returns>
        public FileVO CreateFile(FileVO fileVO)
        {
            NHibernateDao.Insert(fileVO);

            return fileVO;
        }

        /// <summary>
        /// 取得File By FileId
        /// </summary>
        /// <param name="fileId">FileId</param>
        /// <returns>File</returns>
        public FileVO GetFileById(int fileId)
        {
            return NHibernateDao.GetVOById<FileVO>(fileId);
        }

        /// <summary>
        /// 刪除File
        /// </summary>
        /// <param name="fileVO">被刪除的File</param>
        public void DeleteFile(FileVO fileVO)
        {
            NHibernateDao.Delete(fileVO);
        }

        /// <summary>
        /// 更新File
        /// </summary>
        /// <param name="fileVO">被更新的File</param>
        /// <returns>更新後的File</returns>
        public FileVO UpdateFile(FileVO fileVO)
        {
            NHibernateDao.Update(fileVO);

            return fileVO;
        }

        /// <summary>
        /// 取得File清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>File清單</returns>
        public IList<FileVO> GetFileList(IDictionary<string, string> conditions)
        {
            ArrayList param = new ArrayList();
            string fromScript = "select f from FileVO f ";
            StringBuilder whereScript = new StringBuilder();
            return this.QueryFile(param, fromScript, whereScript, conditions, true).OfType<FileVO>().ToList();
        }

        /// <summary>
        /// 取得File數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetFileCount(IDictionary<string, string> conditions)
        {
            int count = 0;
            ArrayList param = new ArrayList();
            string fromScript = "select count(f.FileId) from FileVO f ";
            StringBuilder whereScript = new StringBuilder();
            IList result = this.QueryFile(param, fromScript, whereScript, conditions, false);
            if (result.Count > 0)
            {
                count = Convert.ToInt32(result[0]);
            }
            return count;
        }        

        private IList QueryFile(ArrayList param, string fromScript, StringBuilder whereScript, IDictionary<string, string> conditions, bool useOrder)
        {
            AppendFileKeyWord(conditions, whereScript, param);           
            AppendFileFlag(conditions, whereScript, param);            
            AppendFileDate(conditions, whereScript, param);
            AppendFileType(conditions, whereScript, param);
            AppendFileNeedUpdate(conditions, whereScript, param);

            string hql = fromScript + "where 1=1 " + whereScript;
            if (useOrder)
            {
                hql += AppendFileOrder(conditions);
            }

            return NHibernateDao.Query(hql, param, conditions);
        }

        private void AppendFileNeedUpdate(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("NeedUpdate"))
            {
                whereScript.Append(" and f.NeedUpdate = ? ");
                param.Add(bool.Parse(conditions["NeedUpdate"]));
            }
        }        

        private void AppendFileType(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Type"))
            {
                whereScript.Append(" and f.Type = ? ");
                param.Add(conditions["Type"]);
            }
        }

        private void AppendFileDate(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("ShowDateStart"))
            {
                whereScript.Append(" and f.ShowDate >= ? ");
                param.Add(Convert.ToDateTime(conditions["ShowDateStart"]));
            }
            if (conditions.IsContainsValue("ShowDateEnd"))
            {
                whereScript.Append(" and f.ShowDate <= ? ");
                param.Add(Convert.ToDateTime(conditions["ShowDateEnd"]));
            }            
        }        

        private void AppendFileFlag(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("Flag"))
            {
                whereScript.Append(" and f.Flag = ? ");
                param.Add(conditions["Flag"]);
            }
        }

        private void AppendFileKeyWord(IDictionary<string, string> conditions, StringBuilder whereScript, ArrayList param)
        {
            if (conditions.IsContainsValue("KeyWord"))
            {
                whereScript.Append(" and (f.FileNo like ? ) ");
                param.Add("%" + conditions["KeyWord"] + "%");
            }
        }

        private string AppendFileOrder(IDictionary<string, string> conditions)
        {
            //// 排序條件
            string order = "order by f.ShowDate desc ";
            if (conditions.IsContainsValue("Order"))
            {
                order = conditions["Order"];
            }

            return order;
        }
    }
}
