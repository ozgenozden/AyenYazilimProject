using ApiFunction;
using Autofac;

namespace AyenProjectApi
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<AyenApi>().As<AyenApi>();
 
        }
    }
}
