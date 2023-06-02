using CMS.Data.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data;

public static class ConfigureFreeSql
{
    public static IServiceCollection AddFreeSQLDbContext(this IServiceCollection services, string connectionString)
    {
        var freeSql = FreeSqlFactory.Create(connectionString);
        services.AddSingleton(freeSql);
        services.AddFreeDbContext<PostContext>(options => options.UseFreeSql(freeSql));
        services.AddFreeDbContext<CategoryContext>(options => options.UseFreeSql(freeSql));
        services.AddFreeDbContext<TagContext>(options => options.UseFreeSql(freeSql));
        services.AddFreeDbContext<LinkContext>(options => options.UseFreeSql(freeSql));
        services.AddFreeDbContext<UserContext>(options => options.UseFreeSql(freeSql));
        services.AddFreeDbContext<CommentContext>(options => options.UseFreeSql(freeSql));
        services.AddFreeDbContext<ConfigContext>(options => options.UseFreeSql(freeSql));
        return services;
    }
}
