using IntegrationTests.Domains;
using Xunit;
using RVTR.Lodging.WebApi;
[CollectionDefinition("Client collection")]
public class ClientCollection : ICollectionFixture<CustomWebApplicationFactoryInMemDB<Startup>>
{
  // This class has no code, and is never created. Its purpose is simply
  // to be the place to apply [CollectionDefinition] and all the
  // ICollectionFixture<> interfaces.
}
