namespace src.IoC
{
    public interface ICustomDependencyResolver
    {
         TResolved Resolve<TResolved>();
    }
}