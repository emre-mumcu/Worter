using System;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Wörter.AppLib;

public static class PreloaderHtmlHelpers
{
	public static HtmlString Preloader(this IHtmlHelper htmlHelper)
	{
		StringBuilder htmlBuilder = new StringBuilder();

		htmlBuilder.Append("<div id=\"preloader\">");
		htmlBuilder.Append("  <div id=\"loader\"></div>");
		htmlBuilder.Append("</div>");

		HtmlString str = new HtmlString(htmlBuilder.ToString());

		return str;
	}
}
