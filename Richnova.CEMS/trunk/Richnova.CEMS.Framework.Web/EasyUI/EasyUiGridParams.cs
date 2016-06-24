namespace Richnova.CEMS.Framework.MVC.EasyUI
{
    /// <summary>
    /// EasyUI Grid Query Parames
    /// </summary>
    public class EasyUiGridParams
    {
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int rows { get; set; }

        /// <summary>
        /// 第几页
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string sort { get; set; }

        /// <summary>
        /// 排序的方向（升序 or 降序）
        /// </summary>
        public string order { get; set; }

        /// <summary>
        /// 过滤条件
        /// </summary>
        public dynamic formParamets { get; set; }
    }
}