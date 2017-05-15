namespace gifty.Shared.Neo4j
{
    public interface INeo4jSessionProvider
    {
         INeo4jSession Create();
    }
}