using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Post.DTO;
using WuDada.Core.Post.Domain;
using Spring.Objects;

namespace WuDada.Core.Post.DTOConverter
{
    public class PostVOConverter
    {
        #region Constructor
        private PostVOConverter()
        {
        }
        #endregion

        #region ToDataTransferObject

        /// <summary>
        /// Post Domain轉DTO
        /// </summary>
        /// <param name="postList">PostDomain</param>
        /// <returns>ReportPostVO</returns>
        public static IList<ReportPostVO> ToDataTransferObjects(IList<PostVO> postList, int columnSize)
        {
            if (postList == null || columnSize < 1) return null;

            IList<ReportPostVO> result = new List<ReportPostVO>();

            ReportPostVO currentReportPostVO = new ReportPostVO();
            for (int i = 0; i < postList.Count; i++)
            {
                if (i == 0)
                {
                    currentReportPostVO = new ReportPostVO();
                    currentReportPostVO.Post1 = postList[i];
                }
                else
                {
                    //取餘數 
                    int mod = i % columnSize;

                    if (mod == 0)
                    {
                        result.Add(currentReportPostVO);
                        currentReportPostVO = new ReportPostVO();
                        currentReportPostVO.Post1 = postList[i];
                    }
                    else
                    {
                        ObjectWrapper ow = new ObjectWrapper(currentReportPostVO);
                        ow.SetPropertyValue(string.Format("Post{0}", mod + 1), postList[i]);
                    }
                }
            }

            //將最後的加入
            if (currentReportPostVO != null)
            {
                result.Add(currentReportPostVO);
            }

            return result;
        }

        #endregion
    }
}
