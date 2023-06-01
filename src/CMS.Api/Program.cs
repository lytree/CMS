using CMS.Api;
using CMS.Api.Extensions;
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
var app = builder.Build();
app.ConfigureApp();
app.Run(singleton: true);
