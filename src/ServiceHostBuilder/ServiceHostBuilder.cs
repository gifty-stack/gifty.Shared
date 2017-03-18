using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using src.IoC;

namespace src.ServiceHostBuilder
{
    public class ServiceHostBuilder
    {
        private IWebHostBuilder _webHostBuilder;
        private ICustomDependencyResolver _customDependencyResolver;

        public ServiceHostBuilder CreateDefault<TStartup>() where TStartup : class
        {
            _webHostBuilder = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<TStartup>();

            return this;
        }

        public ServiceHostBuilder WithPort(int port)
        {
            _webHostBuilder.UseUrls($"http://*:{port}");
            return this;
        }

        public ServiceHostBuilder RegisterAutofacDependencies(Func<ICustomDependencyResolver> autofacRegistration)
        {
            var customDependencyResolver = autofacRegistration();
            return this;
        }
        public class AutofacBuilder
        {
            
        }        

        public class RabbitMqBuilder
        {

        }
    }
}