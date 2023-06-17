using Autofac;
using CMS.Common.Attributes;
using CMS.Common.Helpers;
using CMS.Web.Config;
using System.Linq;
using System.Reflection;
using Module = Autofac.Module;

namespace CMS.Web.RegisterModules;

public class SingleInstanceModule : Module
{
    private readonly AppConfig _appConfig;

    /// <summary>
    /// 单例注入
    /// </summary>
    /// <param name="appConfig">AppConfig</param>
    public SingleInstanceModule(AppConfig appConfig)
    {
        _appConfig = appConfig;
    }

    protected override void Load(ContainerBuilder builder)
    {
        if(_appConfig.AssemblyNames?.Length > 0)
        {
            // 获得要注入的程序集
            Assembly[] assemblies = AssemblyHelper.GetAssemblyList(_appConfig.AssemblyNames);

            //无接口注入单例
            builder.RegisterAssemblyTypes(assemblies)
            .Where(t => t.GetCustomAttribute<SingleInstanceAttribute>() != null)
            .SingleInstance()
            .PropertiesAutowired();

            //有接口注入单例
            builder.RegisterAssemblyTypes(assemblies)
            .Where(t => t.GetCustomAttribute<SingleInstanceAttribute>() != null)
            .AsImplementedInterfaces()
            .SingleInstance()
            .PropertiesAutowired();
        }
    }
}
