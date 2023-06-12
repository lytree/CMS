

namespace CMS.Web.Redis
{
	public static class RedisDependencyInjection
	{
		public static void AddRedisClient(this IServiceCollection services, ConfigurationSection section)
		{
			//redis缓存
			string _connectionString = section.GetSection("Connection").Value;//连接字符串
			string _instanceName = section.GetSection("InstanceName").Value; //实例名称
			int _defaultDB = int.Parse(section.GetSection("DefaultDB").Value ?? "0"); //默认数据库           
			_ = services.AddSingleton(new RedisHelper(_connectionString, _instanceName, _defaultDB));
		}
	}
}
