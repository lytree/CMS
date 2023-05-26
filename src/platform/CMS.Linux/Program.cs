using CMS.Web;

var contentRoot = Path.GetDirectoryName(Environment.ProcessPath);
if (string.IsNullOrEmpty(contentRoot) == false)
{
	Environment.CurrentDirectory = contentRoot;
}
var options = new WebApplicationOptions
{
	Args = args,
	ContentRootPath = contentRoot
};

var builder = WebApplication.CreateBuilder(options);

Startup.ConfigureServices(builder);
var app = builder.Build();

Startup.ConfigureApp(app);

app.Run();
