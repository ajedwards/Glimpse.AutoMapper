using System.Linq;

using AutoMapper;

using Glimpse.Core.Extensibility;
using Glimpse.Core.Tab.Assist;

namespace Glimpse.AutoMapper
{
    public class AutoMapperTab : TabBase<IConfigurationProvider>
    {
        private static readonly string[] Headers = { "Profile", "Type Map" };

        public override string Name
        {
            get
            {
                return "AutoMapper";
            }
        }

        public override object GetData(ITabContext context)
        {
            var plugin = Plugin.Create(Headers);

            if (context != null)
            {
                var configuration = context.GetRequestContext<IConfigurationProvider>();
                if (configuration != null)
                {
                    TypeMap[] typeMaps = configuration.GetAllTypeMaps();

                    foreach (var profileName in typeMaps.Select(map => map.Profile).Distinct())
                    {
                        var typeMapSection = new TypeMapTabSection(configuration, profileName);

                        plugin.AddRow().Column(profileName).Column(typeMapSection);
                    }
                }
            }

            return plugin;
        }
    }
}