using System;

namespace Richnova.CEMS.Framework.Web.Authentication
{
    public class CurrentUser
    {
        public Guid? Id { get; set; }
        public string User { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }
        public string Range { get; set; }
        public string AuthToken { get; set; }
    }
}