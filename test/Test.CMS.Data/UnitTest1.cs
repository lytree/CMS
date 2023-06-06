using CMS.Data;
using CMS.Data.Repository;
using CMS.Model.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace Test.CMS.Data
{
	public class UnitTest1
	{
		[Fact]
		public void Test1()
		{
			var budile = new ServiceCollection();
			budile.AddDataRepository("host=127.0.0.1;uid=root;password=test;database=admin;pooling=false;charset=utf8mb4;Pooling=true;Min Pool Size=5;Max Pool Size=50;ConnectionLifeTime=14400;SslMode=None;");
			var service = budile.BuildServiceProvider();
			var re = service.GetService<PostRepository>();
			re.SavePost(new Post()
			{
				Title = "test",
				Contexts = new PostContext[] { new() { Context = "test", Version = DateTime.Now } }
				Comments = new Comment[] { new() { Username = "", Content = "test" } }
			});
		}
	}
}