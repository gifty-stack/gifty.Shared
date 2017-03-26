namespace gifty.Shared.Builders
{
    public interface IServiceBuilder
    {
         IAutofacServiceBuilder WithPort(int port);
         void Run();
    }
}