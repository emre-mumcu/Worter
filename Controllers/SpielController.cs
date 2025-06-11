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
			// EF Core cannot translate complex C# string operations like .Split() and .Take() into SQL — especially in SQLite, which has limited built-in string handling, and EF Core has no built-in support for splitting strings into arrays in SQL.
			// So we use AsEnumerable to Bring data into memory
			.AsEnumerable() // Fetches data first and Brings data into memory
			.Select(e => { return new { e.DE, e.TR, e.EN }; })
			.Select(e => new
			{
				e.DE,
				TR = string.Join(",", e.TR?.Split(',').Take(4) ?? Enumerable.Empty<string>()),
				EN = string.Join(",", e.EN?.Split(',').Take(4) ?? Enumerable.Empty<string>()),
			})
			.ToList();

			var listQuestions = appDbContext.Wörter
			.Where(w => w.State == 1)
			.AsEnumerable() // Fetches data first
			.OrderBy(arg => Guid.NewGuid()).Take(5).ToList()
			// .Select(e => { return new { e.DE, e.TR, e.EN }; })
			.Select(e => new
			{
				e.DE,
				TR = string.Join(",", e.TR?.Split(',').Take(4) ?? Enumerable.Empty<string>()),
				EN = string.Join(",", e.EN?.Split(',').Take(4) ?? Enumerable.Empty<string>()),
				e.Aussprache
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
					Aussprache = item.Aussprache,
					Antwort = antwort,
					Optionen = wrongOptions
				});
			}

			return View(model);
		}

	}
}
