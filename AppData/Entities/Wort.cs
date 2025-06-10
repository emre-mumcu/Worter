using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wörter.AppLib;
using static Wörter.AppData.AppTypes;

namespace Wörter.AppData.Entities;

public class Wort: BaseEntity
{
	public Wortart Art { get; set; } // Word Type
	public string DE { get; set; } = null!;
	[AtLeastOneRequired("EN", "TR", ErrorMessage = "Either EN or TR is required.")]
	public string? EN { get; set; }
	public string? TR { get; set; } 
	public string? Geschlecht { get; set; } // Gender (for Nouns)
	public string? Aussprache { get; set; } // Pronunciation
	public string? Plural { get; set; }  // Plural (for Nouns)
	public string? Verbkonjugation { get; set; } // Verb conjugation (for Verbs)
	public string? Beispiel { get; set; } // Example
	public string? Erläuterung { get; set; } // Explanation
	public string? Synonym { get; set; } // Synonyms
	public string? Gegenteil { get; set; } // Opposite
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
