using CMS.Data;
using CMS.Data.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Test.CMS.Data
{
	public class UnitTest1
	{
		[Fact]
		public void Test1()
		{
			var fsql = FreeSqlFactory.Create("Data Source=db.db");
			ServiceCollection service = new();
			service.AddSingleton<IFreeSql>(fsql);
			service.AddFreeDbContext<UserContext>(options => options.UseFreeSql(fsql));

			var dbContext = service.BuildServiceProvider().GetService<UserContext>();
			dbContext.SaveChanges();
		}
	}
}