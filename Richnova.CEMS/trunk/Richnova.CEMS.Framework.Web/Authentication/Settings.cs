namespace Richnova.CEMS.Framework.Web.Authentication
{
    public class Settings
    {
        /// <summary>
        /// 皮肤
        /// </summary>
        public virtual string Theme { get; set; }

        /// <summary>
        /// 菜单现实方式
        /// </summary>
        public virtual string Navigation { get; set; }

        /// <summary>
        /// Grid的每页行数
        /// </summary>
        public virtual int GridRows { get; set; }

        /// <summary>
        /// 最多可打开Tab的个数
        /// </summary>
        public virtual int MaxTabCount { get; set; }
    }
}
