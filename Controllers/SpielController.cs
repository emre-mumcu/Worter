using Microsoft.AspNetCore.Mvc;
using Wörter.AppData;
using Wörter.AppLib;
using Wörter.ViewModels;

namespace Wörter.Controllers
{
	public class SpielController : Controller
	{
		[HttpGet("/Game")]
		public ActionResult Index([FromServices] AppDbContext appDbContext)
		{
			var listAll = appDbContext.Wörter
			.Where(w => w.State == 1)
			.AsEnumerable() // Fetches data first
			.Select(e =>
			{
				return new { e.DE, e.TR, e.EN };
			})
			.ToList();

			var listQuestions = appDbContext.Wörter
			.Where(w => w.State == 1)
			.AsEnumerable() // Fetches data first
			.OrderBy(arg => Guid.NewGuid()).Take(5).ToList()
			.Select(e =>
			{
				return new { e.DE, e.TR, e.EN };
			})
			.ToList();

			var model = new List<SpielVM>();

			foreach (var item in listQuestions)
			{
				string antwort = $"{item.EN} {item.TR}";

				var wrongOptions = listAll.Where(e => e.DE != item.DE)
					.OrderBy(arg => Guid.NewGuid())
					.Take(3)
					.Select(i => $"{i.EN} {i.TR}")
					.ToList();

				wrongOptions.Add(antwort);

				wrongOptions.Shuffle();

				model.Add(new SpielVM
				{
					Frage = item.DE,
					Antwort = antwort,
					Optionen = wrongOptions
				});
			}

			return View(model);
		}

	}
}
