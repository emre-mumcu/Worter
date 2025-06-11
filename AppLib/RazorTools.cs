using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Wörter.AppLib;

public class RazorTools
{
	private readonly IHttpContextAccessor? _httpContextAccessor;
	private readonly IActionContextAccessor? _actionContextAccessor;
	private readonly LinkGenerator _generator;

	public RazorTools(IHttpContextAccessor httpContextAccessor, IActionContextAccessor actionContextAccessor, LinkGenerator generator)
	{
		_httpContextAccessor = httpContextAccessor;
		_actionContextAccessor = actionContextAccessor;
		_generator = generator;
	}

	public string GetActionDescriptor()
	{
		return JsonConvert.SerializeObject(_actionContextAccessor?.ActionContext?.ActionDescriptor.Id, Formatting.Indented, new JsonSerializerSettings()
		{
			ContractResolver = new CamelCasePropertyNamesContractResolver(),
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore
		});
	}

	public string GetControllerName()
	{
		if (_actionContextAccessor?.ActionContext is ControllerContext)
		{
			ControllerContext? controllerContext = _actionContextAccessor?.ActionContext as ControllerContext;
			return controllerContext?.ActionDescriptor.ControllerName ?? string.Empty;
		}

		return string.Empty;
	}

	public string GetActionName()
	{
		if (_actionContextAccessor?.ActionContext is ControllerContext)
		{
			ControllerContext? controllerContext = _actionContextAccessor?.ActionContext as ControllerContext;
			return controllerContext?.ActionDescriptor.ActionName ?? string.Empty;
		}

		return string.Empty;
	}

	public string GetSessionId()
	{
		return _httpContextAccessor?.HttpContext?.Session.Id ?? string.Empty;
	}

	public string GetCurrentUri()
	{
		var routeValues = _actionContextAccessor?.ActionContext?.ActionDescriptor.RouteValues;
		var scheme = _httpContextAccessor?.HttpContext?.Request.Scheme;
		HostString host = _httpContextAccessor?.HttpContext?.Request.Host ?? new HostString();

		return _generator.GetUriByAction(
					action: GetActionName(),
					controller: GetControllerName(),
					values: routeValues,
					scheme: scheme,
					host: host)
				?? string.Empty;
	}
}

/*
 *  In razor:
 *  ---------
 * 	var controller = ViewContext.RouteData.Values["Controller"]?.ToString()?.ToLower(new System.Globalization.CultureInfo("en-us"));
 * 	var action = ViewContext.RouteData.Values["Action"]?.ToString()?.ToLower(new System.Globalization.CultureInfo("en-us"));
 */