using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WuDada.Provider.ResourceHandle.Domain
{
    /// <summary>
    /// 資料夾類型列舉
    /// </summary>
    public enum FolderType
    {
        /// <summary>
        /// Storage path root
        /// </summary>
        ROOT,

        /// <summary>
        /// 公開的資料夾
        /// </summary>
        PUBLIC_FOLDER,

        /// <summary>
        /// 暫存的資料夾
        /// </summary>
        TEMPORARY_FOLDER,

        /// <summary>
        /// 上傳檔案暫存的資料夾
        /// </summary>
        UPLOAD_FOLDER
    }
}
