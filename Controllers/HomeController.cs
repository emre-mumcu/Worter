using System.Globalization;
using System.Net;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wörter.AppData;
using Wörter.AppData.Entities;
using Wörter.ViewModels;
using static Wörter.AppData.AppTypes;

namespace Wörter.Controllers
{
	public class HomeController : Controller
	{
		public async Task<IActionResult> Index() => await Task.Run(() => View());
	}
}

// HttpContext.Session.SetKey<string>(AppConstants.SessionKey_System_Message, "Hello ;)");