using Infra.Data.Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Ioc
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {

            services.AddScoped<AplicationDBContext>();

            //Core Services
            //services.AddScoped<ISMSNotification, SMSNotification>();

            //Application Layer.
            ApplicationDependencycontainer.RegisterServices(services);

            // Infra.Data Layer
            InfraDependencycontainer.RegisterServices(services);

            // Infra.Data Layer
            CoreServiceContainer.RegisterServices(services);


        }
    }
}
