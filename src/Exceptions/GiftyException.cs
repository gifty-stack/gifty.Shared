using System;
using gifty.Shared.Infrastructure;

namespace gifty.Shared.Exceptions
{
    public sealed class GiftyException : Exception, IInformable<GiftyExceptionInfo>
    {
        public ErrorType ErrorType { get; }
        public Type TargetType { get; }

        public GiftyException(ErrorType errorType = ErrorType.Unknown, string message = "")
            :base(message)
        {
            ErrorType = errorType;
        }

        public GiftyException(Type targetType, ErrorType errorType = ErrorType.Unknown, string message = "")
            :this(errorType, message)
        {
            TargetType = targetType;
        }

        public GiftyException(Exception innerException, string message = "")
            :base(message, innerException)
        {
            ErrorType = ErrorType.Unknown;
        }

        GiftyExceptionInfo IInformable<GiftyExceptionInfo>.GetInfo()
            => new GiftyExceptionInfo
            {
                Message = Message,
                ErrorType = ErrorType.ToString(),
                TargetType = TargetType?.ToString()
            };
    }
}