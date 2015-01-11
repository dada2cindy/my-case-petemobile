using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WuDada.Core.Post.DTO;
using WuDada.Core.Post.Domain;
using Spring.Objects;

namespace WuDada.Core.Post.DTOConverter
{
    public class NodeVOConverter
    {
        #region Constructor
        private NodeVOConverter()
        {
        }
        #endregion

        #region ToDataTransferObject

        /// <summary>
        /// Node Domain轉DTO
        /// </summary>
        /// <param name="nodeList">NodeDomain</param>
        /// <returns>ReportNode DTO</returns>
        public static IList<ReportNodeVO> ToDataTransferObjects(IList<NodeVO> nodeList, int columnSize)
        {
            if (nodeList == null || columnSize < 1) return null;

            IList<ReportNodeVO> result = new List<ReportNodeVO>();

            ReportNodeVO currentReportNodeVO = new ReportNodeVO();
            for (int i = 0; i < nodeList.Count; i++)
            {
                if (i == 0)
                {
                    currentReportNodeVO = new ReportNodeVO();
                    currentReportNodeVO.Node1 = nodeList[i];
                }
                else
                {
                    //取餘數 
                    int mod = i % columnSize;

                    if (mod == 0)
                    {
                        result.Add(currentReportNodeVO);
                        currentReportNodeVO = new ReportNodeVO();
                        currentReportNodeVO.Node1 = nodeList[i];
                    }
                    else
                    {
                        ObjectWrapper ow = new ObjectWrapper(currentReportNodeVO);
                        ow.SetPropertyValue(string.Format("Node{0}", mod + 1), nodeList[i]);
                    }
                }
            }

            //將最後的加入
            if (currentReportNodeVO != null)
            {
                result.Add(currentReportNodeVO);
            }

            return result;
        }

        #endregion
    }
}
