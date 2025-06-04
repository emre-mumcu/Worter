using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Wörter.AppData.AppTypes;

namespace Wörter.AppData.Entities;

public class Wort: BaseEntity
{
	public Wortart Art { get; set; }
	public string DE { get; set; } = null!;
	public string EN { get; set; } = null!;
	public string TR { get; set; } = null!;
	public string? Geschlecht { get; set; }	
	public string? Aussprache { get; set; }	
	public string? Plural { get; set; } 
	public string? Verbkonjugation { get; set; } 
	public string? Beispiel { get; set; }
	public string? Erläuterung { get; set; }
}

public class WortKonfiguration : IEntityTypeConfiguration<Wort>
{
	public void Configure(EntityTypeBuilder<Wort> builder)
	{
		// builder.HasKey(b => b.Col1);
		// builder.HasKey(b => new { b.Col1, b.Col2 });
		// builder.ToTable("Wörter");
		
		builder.Property(b => b.DE).IsRequired();
		builder.HasIndex(b => new { b.DE, b.Art }).IsUnique(true);
	}
}
