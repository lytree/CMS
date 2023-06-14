﻿using CMS.Common.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Encodings.Web;
using StatusCodes = CMS.Web.Auth.StatusCodes;
namespace CMS.Web.Auth
{

	/// <summary>
	/// 响应认证处理器
	/// </summary>
	public class ResponseAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
	{
		public ResponseAuthenticationHandler(
			IOptionsMonitor<AuthenticationSchemeOptions> options,
			ILoggerFactory logger,
			UrlEncoder encoder,
			ISystemClock clock
		) : base(options, logger, encoder, clock)
		{
		}

		protected override Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			throw new NotImplementedException();
		}

		protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
		{
			Response.ContentType = "application/json";
			Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized;
			await Response.WriteAsync(JsonConvert.SerializeObject(
				new ResponseStatusData
				{
					Code = StatusCodes.Status401Unauthorized,
					Msg = StatusCodes.Status401Unauthorized.ToDescription(),
					Success = false
				},
				new JsonSerializerSettings()
				{
					ContractResolver = new CamelCasePropertyNamesContractResolver()
				}
			));
		}

		protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
		{
			Response.ContentType = "application/json";
			Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden;
			await Response.WriteAsync(JsonConvert.SerializeObject(
				new ResponseStatusData
				{
					Code = StatusCodes.Status403Forbidden,
					Msg = StatusCodes.Status403Forbidden.ToDescription(),
					Success = false
				},
				new JsonSerializerSettings()
				{
					ContractResolver = new CamelCasePropertyNamesContractResolver()
				}
			));
		}
	}

	public class ResponseStatusData
	{
		public StatusCodes Code { get; set; } = StatusCodes.Status1Ok;
		public string Msg { get; set; }
		public bool Success { get; set; }

	}
}