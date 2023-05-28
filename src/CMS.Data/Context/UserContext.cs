using CMS.Data.Models;
using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data
{
	public class UserContext : DbContext
	{
		private readonly IFreeSql _freeSql;

		public DbSet<User> Users { get; set; }


		public UserContext(IFreeSql free)
		{
			this._freeSql = free;
		}
		protected override void OnConfiguring(DbContextOptionsBuilder builder)
		{
			builder.UseFreeSql(_freeSql);
			//这里直接指定一个静态的 IFreeSql 对象即可，切勿重新 Build()
		}
		//每个 DbContext 只触发一次
		protected override void OnModelCreating(ICodeFirst codefirst)
		{

			codefirst.Entity<User>(e =>
			{
				e.HasData(new[]{
					new User(){
						Name="admin",
						Password = "test"
						},
					new User(){
						Name="test",
						Password = "test"
						},
					new User(){
						Name="test1",
						Password = "test"
						}
					});
			});
			codefirst.SyncStructure<User>();
		}
	}
}
