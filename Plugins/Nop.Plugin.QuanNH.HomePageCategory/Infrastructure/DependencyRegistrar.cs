using Autofac;
using Autofac.Core;
using Nop.Core.Caching;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Plugin.Widgets.HomePageCategory.Controllers;
using Nop.Plugin.Widgets.HomePageCategory.Services;

namespace Nop.Plugin.Widgets.HomePageCategory.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            //we cache presentation models between requests
            builder.RegisterType<WidgetsHomePageCategoryController>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));

            //services
            builder.RegisterType<CateService>().As<ICateService>().InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 2; }
        }
    }
}
