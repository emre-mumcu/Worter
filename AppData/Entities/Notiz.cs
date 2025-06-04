using System;

namespace Wörter.AppData.Entities;

public class Notiz : BaseEntity
{
	public string Title { get; set; } = null!;
	public string Slug { get; set; } = null!;
	public string TextContent { get; set; } = null!;
	public string? ReferenceLink { get; set; } 
}
