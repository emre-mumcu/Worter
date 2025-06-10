using Microsoft.AspNetCore.Mvc.Rendering;
using Wörter.AppData.Entities;
using Wörter.AppLib;
using static Wörter.AppData.AppTypes;

namespace Wörter.ViewModels;


public class WortVM : Wort
{
	// Since the base  class property Wort.Art is NOT nullable,
	// we use new keyword and hide the base member to make it nullable and 
	// select elements first item as 'Select a value' string.
	public new Wortart? Art { get; set; }
	public IEnumerable<SelectListItem>? TypeOptions { get; set; }
	public IEnumerable<SelectListItem>? GenderOptions { get; set; }
}