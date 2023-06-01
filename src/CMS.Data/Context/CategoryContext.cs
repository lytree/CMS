﻿using CMS.Data.Models;
using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Context
{
	public class CategoryContext : DbContext
	{
		public DbSet<Category> Categories { get; set; }

		public DbSet<Post> Posts { get; set; }

		protected override void OnModelCreating(ICodeFirst codeFirst)
		{

		}
	}
}