using CMS.Web;
using Microsoft.AspNetCore.Builder;
using System;
using System.IO;

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
builder.CreateWebApplication();
// Add services to the container.

var app = builder.Build();

app.ConfigureApp();


app.Run();
