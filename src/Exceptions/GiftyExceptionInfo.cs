using gifty.Shared.Infrastructure;

namespace gifty.Shared.Exceptions
{
    public sealed class GiftyExceptionInfo : IInfo
    {
        public string Message { get; set; }
        public string ErrorType { get; set; }
        public string TargetType { get; set; }
    }
}