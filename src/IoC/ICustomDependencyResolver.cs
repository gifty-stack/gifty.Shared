namespace gifty.Shared.IoC
{
    public interface ICustomDependencyResolver
    {
         TResolved Resolve<TResolved>();
    }
}