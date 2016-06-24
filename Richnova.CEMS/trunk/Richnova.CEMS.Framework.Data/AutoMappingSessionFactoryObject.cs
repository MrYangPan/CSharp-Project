using System.IO;
using System.Reflection;
using NHibernate.Cfg;
using NHibernate.Mapping.Attributes;
using Spring.Data.NHibernate;

namespace Richnova.CEMS.Framework.Data
{
    public class AutoMappingSessionFactoryObject : LocalSessionFactoryObject
    {
        public string[] ModelAssemblyName { get; set; }

        protected override void PostProcessConfiguration(Configuration config)
        {
            base.PostProcessConfiguration(config);

            foreach (var modelAssemblyName in ModelAssemblyName)
            {
                if (string.IsNullOrEmpty(modelAssemblyName) || string.IsNullOrWhiteSpace(modelAssemblyName)) continue;
                HbmSerializer.Default.Validate = true;
                using (var stream = new MemoryStream())
                {
                    var assembly = Assembly.Load(modelAssemblyName);
                    if (assembly == null) continue;
                    HbmSerializer.Default.Serialize(stream, assembly);
                    stream.Position = 0;
                    config.AddInputStream(stream);
                }
            }
        }
    }
}
