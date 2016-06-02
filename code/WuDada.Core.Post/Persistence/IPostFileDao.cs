using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Post.Domain;

namespace WuDada.Core.Post.Persistence
{
    public interface IPostFileDao
    {
        /// <summary>
        /// 新增File
        /// </summary>
        /// <param name="fileVO">File</param>
        /// <returns>新增後的File</returns>
        FileVO CreateFile(FileVO fileVO);

        /// <summary>
        /// 取得File By FileId
        /// </summary>
        /// <param name="fileId">FileId</param>
        /// <returns>File</returns>
        FileVO GetFileById(int fileId);

        /// <summary>
        /// 刪除File
        /// </summary>
        /// <param name="fileVO">被刪除的File</param>
        void DeleteFile(FileVO fileVO);

        /// <summary>
        /// 更新File
        /// </summary>
        /// <param name="fileVO">被更新的File</param>
        /// <returns>更新後的File</returns>
        FileVO UpdateFile(FileVO fileVO);

        /// <summary>
        /// 取得File清單
        /// </summary>
        /// <param name="conditions">搜尋條件</param>
        /// <returns>File清單</returns>
        IList<FileVO> GetFileList(IDictionary<string, string> conditions);

        /// <summary>
        /// 取得File數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int GetFileCount(IDictionary<string, string> conditions);
    }
}
