namespace src.Builders
{
    public interface IServiceBuilder
    {
         IAutofacServiceBuilder WithPort(int port);
         void Run();
    }
}