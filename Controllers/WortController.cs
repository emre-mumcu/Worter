
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wörter.AppData;
using Wörter.AppData.Entities;
using Wörter.ViewModels;
using static Wörter.AppData.AppTypes;

namespace Wörter.Controllers
{
    public class WortController : Controller
    {

		[HttpGet("/List")]
		public IActionResult List([FromServices] AppDbContext appDbContext)
		{
			var list = appDbContext.Wörter.OrderBy(w => w.DE)
			.Where(w => w.State ==  1)
			.AsEnumerable() // Fetches data first
			.Select(e =>
			{
				// e.VerbConjugation = e?.VerbConjugation?.Replace("\r\n", "<br>").Replace("\n", "<br>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");
				return e;
			})
			.ToList();
			return View(list);
		}

		// [HttpGet("/")]
		[HttpGet("/Edit")]
		[HttpGet("/Edit/{id}")]
		public IActionResult Index([FromServices] AppDbContext appDbContext, [FromServices] IMapper mapper, [FromRoute(Name = "id")] string? wordId)
		{
			var viewModel = new WortVM();

			if (!string.IsNullOrEmpty(wordId))
			{
				var entity = appDbContext.Wörter.FirstOrDefault(w => w.Id == Convert.ToInt64(wordId));
				mapper.Map(entity, viewModel);
			}

			viewModel.TypeOptions = Enum.GetValues(typeof(Wortart))
			.Cast<Wortart>()
			.Select(g => new SelectListItem
			{
				Value = g.ToString(),
				Text = g.ToString()
			});

			return View(viewModel);
		}

		[HttpPost("/Save")]
		public IActionResult Save([FromServices] AppDbContext appDbContext, [FromServices] IMapper mapper, WortVM ve)
		{
			Wort we = new Wort();

			mapper.Map(ve, we);

			/* 		we.Word = ve.Word;
					we.Type = ve.Type!.Value;
					we.Pronunciation = ve.Pronunciation;
					we.Plural = ve.Plural;
					we.Gender = ve.Gender;
					we.VerbConjugation = ve.VerbConjugation;
					we.English = ve.English;
					we.Turkish = ve.Turkish;
					we.Sample = ve.Sample;
					we.Detail = ve.Detail; */

			if (we.Id > 0)
			{
				appDbContext.Wörter.Update(we);
				appDbContext.SaveChanges();
			}
			else
			{
				appDbContext.Wörter.Add(we);
				appDbContext.SaveChanges();
			}

			return RedirectToAction("Index", new { Id = we.Id });
		}


		[HttpPost("/Lookup")]
		[ValidateAntiForgeryToken]
		public IActionResult Lookup([FromServices] AppDbContext appDbContext, [FromBody] LookupVM model)
		{
			LookupVM lookupVM = new LookupVM();

			try
			{
				if (model == null || string.IsNullOrEmpty(model.Word))
				{
					lookupVM.Message = "Word is null";
					return BadRequest(lookupVM);
				}

				// HtmlDocument htmlDocument = new HtmlWeb().Load($"https://www.verbformen.com/?w={model.Word}");
				// var vStckKrz = htmlDocument.GetElementbyId("vStckKrz");
				// var vGrnd = vStckKrz.SelectSingleNode(".//*[contains(@class, 'vGrnd')]");
				// lookupVM.Found = vGrnd.InnerText;
				// lookupVM.IsNoun = new[] { "der ", "die ", "das " }.Any(prefix => lookupVM.Found.StartsWith(prefix));
				// var r1Zeile = vStckKrz.SelectSingleNode(".//*[contains(@class, 'r1Zeile')]");
				// lookupVM.Meaning = NormalizeString(r1Zeile.InnerText);

				var w = appDbContext.Wörter.FirstOrDefault(w => w.DE == model.Word);

				if (w is null)
				{

				}
				else
				{
					lookupVM.Found = true;
					lookupVM.Link = $"/Edit/{w.Id}";
				}
			}
			catch (Exception ex)
			{
				lookupVM.Exception = ex?.Message;
			}

			return Json(lookupVM);
		}





	}
}
