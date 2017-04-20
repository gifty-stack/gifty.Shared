using System;
using System.Text;
using gifty.Shared.Exceptions;
using gifty.Shared.Infrastructure;
using Nancy;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace gifty.Shared.Nancy
{
    internal static class OnErrorHook
    {
        internal static dynamic OnError(NancyContext ctx, Exception ex)
        {
            var giftyException = ex as GiftyException;

            if(giftyException != null)
            {
                var info = ((IInformable<GiftyExceptionInfo>) giftyException).GetInfo();
                var infoJson = SerializeExceptionInfo(info);
                var infoJsonBytes = Encoding.UTF8.GetBytes(infoJson);

                return new Response
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Contents = stream => stream.Write(infoJsonBytes, 0, infoJsonBytes.Length)
                };
            } 

            throw ex;           
        }

        private static string SerializeExceptionInfo(GiftyExceptionInfo info)
            => JsonConvert.SerializeObject(info, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
    }
}