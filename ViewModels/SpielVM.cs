using System;

namespace Wörter.ViewModels;

public class SpielVM
{
	public string Frage { get; set; } = null!;
	public List<String> Optionen { get; set; } = null!;
	public string Antwort { get; set; } = null!;
}
