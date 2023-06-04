using CMS.Model.Entity;
using FreeSql;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Repository
{
	public sealed class PostRepository : BaseRepository<Post, int>
	{
		private readonly ILogger<PostRepository> _logger;


		public PostRepository(ILoggerFactory loggerFactory, IFreeSql fsql, Expression<Func<Post, bool>> filter, Func<string, string> asTable = null) : base(fsql, filter, asTable)
		{
			_logger = loggerFactory == null ? NullLogger<PostRepository>.Instance : loggerFactory.CreateLogger<PostRepository>();
		}

		public void SavePost(Post post)
		{
			SaveMany(post, nameof(Post.Contexts));
			SaveMany(post, nameof(Post.Comments));
			SaveMany(post, nameof(Post.Categories));
			SaveMany(post, nameof(Post.Tags));
		}
		public IEnumerable<PostContext> GetPostContext(int postId)
		{
			return Select.IncludeMany(a => a.Contexts.Where(b => b.PostId == a.Id)).Where(c => c.Id == postId).ToOne().Contexts;
		}
		public Post GetPostDesc(int postId)
		{
			return Select.Where(p => p.Id == postId).ToOne<Post>();
		}
	}
}
