using Microsoft.AspNetCore.Mvc;
using Wörter.AppData;

namespace Wörter.Controllers
{
	public class SpielController : Controller
	{
		public ActionResult Index([FromServices] AppDbContext appDbContext)
		{
			var list = appDbContext.Wörter.OrderBy(w => w.DE)
			.AsEnumerable() // Fetches data first
			.Select(e =>
			{
				return new { e.DE, e.TR, e.EN };
			})
			.ToList();

			return View();
		}

	}
}
