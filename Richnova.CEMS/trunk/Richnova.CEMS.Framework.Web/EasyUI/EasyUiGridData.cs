using System;
using System.Collections.Generic;

namespace Richnova.CEMS.Framework.Web.EasyUI
{
    public class EasyUiGridData
    {
        /// <summary>
        /// 记录总数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 记录集合
        /// </summary>
        public IEnumerable<Object> rows { get; set; }
    }
  
}