using System.ComponentModel.DataAnnotations;

namespace Wörter.AppLib;

public class AtLeastOneRequiredAttribute : ValidationAttribute
{
	private readonly string _otherPropertyName;

	public AtLeastOneRequiredAttribute(string otherPropertyName)
	{
		_otherPropertyName = otherPropertyName;
	}

	protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
	{
		var otherProperty = validationContext.ObjectType.GetProperty(_otherPropertyName);

		if (otherProperty == null)
			return new ValidationResult($"Unknown property: {_otherPropertyName}");

		var otherValue = otherProperty.GetValue(validationContext.ObjectInstance, null);

		if (value == null && (otherValue == null || string.IsNullOrWhiteSpace(otherValue.ToString())))
		{
			return new ValidationResult(ErrorMessage ?? $"At least one of the fields must be provided.");
		}

		return ValidationResult.Success;
	}
}