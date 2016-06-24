using System;
using NHibernate.Engine;
using NHibernate.Id;

namespace Richnova.CEMS.Framework.Data
{
    public class IdGenerator : IIdentifierGenerator
    {
        public object Generate(ISessionImplementor session, object obj)
        {
            return Guid.NewGuid().ToString();
        }
    }
}
