namespace gifty.Shared.Builders
{
    public interface INeo4jServiceBuilder
    {
         IRabbitMqServiceBuilder WithNeo4j(string boltEndpoint, string login, string password);
         IRabbitMqServiceBuilder WithNoNeo4j();
    }
}