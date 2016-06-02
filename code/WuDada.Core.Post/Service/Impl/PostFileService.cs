using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Post.Domain;
using WuDada.Core.Post.Persistence;
using NHibernate;

namespace WuDada.Core.Post.Service.Impl
{
    public class PostFileService : IPostFileService
    {
        public IPostFileDao PostFileDao { get; set; }

        /// <summary>
        /// 新增File
        /// </summary>
        /// <param name="fileVO">File</param>
        /// <returns>新增後的File</returns>
        public FileVO CreateFile(FileVO fileVO)
        {            
            fileVO = PostFileDao.CreateFile(fileVO);
            return fileVO;
        }

        /// <summary>
        /// 取得File By FileId
        /// </summary>
        /// <param name="fileId">FileId</param>
        /// <returns>File</returns>
        public FileVO GetFileById(int fileId)
        {
            return PostFileDao.GetFileById(fileId);
        }

        /// <summary>
        /// 刪除File
        /// </summary>
        /// <param name="fileVO">被刪除的File</param>
        public void DeleteFile(FileVO fileVO)
        {
            PostFileDao.DeleteFile(fileVO);
        }

        /// <summary>
        /// 更新File
        /// </summary>
        /// <param name="fileVO">被更新的File</param>
        /// <returns>更新後的File</returns>
        public FileVO UpdateFile(FileVO fileVO)
        {
            return PostFileDao.UpdateFile(fileVO);
        }

        /// <summary>
        /// 取得File清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>File清單</returns>
        public IList<FileVO> GetFileList(IDictionary<string, string> conditions)
        {
            return PostFileDao.GetFileList(conditions);
        }

        /// <summary>
        /// 取得File數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int GetFileCount(IDictionary<string, string> conditions)
        {
            return PostFileDao.GetFileCount(conditions);
        }        
    }
}
