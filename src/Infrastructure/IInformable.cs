namespace gifty.Shared.Infrastructure
{
    public interface IInformable<out TInfo>  where TInfo : class, IInfo
    {
         TInfo GetInfo();
    }
}