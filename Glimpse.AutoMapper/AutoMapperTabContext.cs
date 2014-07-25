using System.Collections;

using AutoMapper;

using Glimpse.Core.Extensibility;

namespace Glimpse.AutoMapper
{
    public class AutoMapperTabContext : TabContext
    {
        public AutoMapperTabContext(
            IConfigurationProvider configuration)
            : this(configuration, new DictionaryDataStoreAdapter(new Hashtable()))
        {
        }

        public AutoMapperTabContext(
            IConfigurationProvider configuration,
            IDataStore dataStore)
            : this(configuration, dataStore, new NullLogger())
        {
        }

        public AutoMapperTabContext(
            IConfigurationProvider configuration,
            IDataStore dataStore,
            ILogger logger)
            : this(configuration, dataStore, logger, new MessageBroker(logger))
        {
        }

        public AutoMapperTabContext(
            IConfigurationProvider configuration,
            IDataStore dataStore,
            ILogger logger,
            IMessageBroker messageBroker)
            : base(configuration, dataStore, logger, messageBroker)
        {
        }
    }
}
