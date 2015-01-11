using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WuDada.Core.Post.Domain
{
    public interface IPageTagObject
    {
        /// <summary>
        /// PageTitle
        /// </summary>
        string PageTitle { get; set; }

        /// <summary>
        /// KeyWord
        /// </summary>
        string PageKeyWord { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        string PageDescription { get; set; }        

    }
}
