using System;

namespace Wörter.AppData.Entities;

public class BaseEntity
{
	public int Id { get; set; }
	public int State { get; set; } = 1;
	public DateTime Created { get; set; } = DateTime.Now;
	public DateTime Updated { get; set; } = DateTime.Now;
	public string? Tags { get; set; } 
	public string? Category { get; set; } 
}
