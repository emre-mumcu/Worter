using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wörter.AppData;
using Wörter.ViewModels;
using System.Globalization;
using System.Net;
using System.Text;
using Wörter.AppData.Entities;

namespace Wörter.Controllers
{
	public class NotizenController : Controller
	{

		[HttpGet("/Notes")]
		public ActionResult Quill()
		{
			return View();
		}

		[HttpGet("/CKE")]
		[HttpGet("/CKE/{id}")]
		public ActionResult CKE([FromServices] AppDbContext appDbContext, [FromServices] IMapper mapper, [FromRoute(Name = "id")] string? noteID)
		{
			var viewModel = new NotizVM();

			if (!string.IsNullOrEmpty(noteID))
			{
				var entity = appDbContext.Notizen.FirstOrDefault(n => n.Id == Convert.ToInt64(noteID));
				mapper.Map(entity, viewModel);
			}

			return View(viewModel);
		}

		[HttpGet("/CKEView/{id}")]
		public ActionResult CKEView([FromServices] AppDbContext appDbContext, [FromServices] IMapper mapper, [FromRoute(Name = "id")] string? noteID)
		{
			var viewModel = new NotizVM();

			if (!string.IsNullOrEmpty(noteID))
			{
				var entity = appDbContext.Notizen.FirstOrDefault(n => n.Id == Convert.ToInt64(noteID));
				mapper.Map(entity, viewModel);
			}

			return View(viewModel);
		}

		[HttpGet("/CKEDelete/{id}")]
		public ActionResult CKEDelete([FromServices] AppDbContext appDbContext, [FromServices] IMapper mapper, [FromRoute(Name = "id")] string? noteID)
		{
			if (!string.IsNullOrEmpty(noteID))
			{
				var entity = appDbContext.Notizen.FirstOrDefault(n => n.Id == Convert.ToInt64(noteID));
				if (entity != null)
				{
					appDbContext.Notizen.Remove(entity);
					appDbContext.SaveChanges();
				}
				
			}

			return RedirectToAction("CKE", new { Id = noteID });
		}

		[HttpPost("/SaveCKE")]
		public ActionResult SaveCKE([FromServices] AppDbContext appDbContext, [FromServices] IMapper mapper, NotizVM vm)
		{
			Notiz entity = new Notiz();

			mapper.Map(vm, entity);

			entity.Slug = NormalizeString(entity.Title);

			if (entity.Id > 0)
			{
				appDbContext.Notizen.Update(entity);
				appDbContext.SaveChanges();
			}
			else
			{
				appDbContext.Notizen.Add(entity);
				appDbContext.SaveChanges();
			}

			return RedirectToAction("CKE", new { Id = entity.Id });
		}

		private string NormalizeString(string input)
		{
			input = WebUtility.HtmlDecode(input);
			return new string(input
				.Normalize(NormalizationForm.FormD) // Decomposes diacritics
				.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark) // Removes diacritics
				.Where(c => !char.IsControl(c)) // Removes control characters
				.ToArray());
		}

		[HttpGet("/NoteList")]
		public IActionResult NoteList([FromServices] AppDbContext appDbContext)
		{
			var list = appDbContext.Notizen.OrderBy(n => n.Title)
			.Where(n => n.State == 1)
			.AsEnumerable() // Fetches data first
			.Select(e =>
			{
				return e;
			})
			.ToList();

			return View("List", list);
		}

	}
}
