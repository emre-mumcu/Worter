using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Wörter.AppLib;

/*
The below attribute will validate on the server automatically when you call ModelState.IsValidin your controller.

ASP.NET Core MVC client-side validation uses jQuery Validation and unobtrusive validation. To make this custom validation work client-side, you have to:
1. Implement IClientModelValidatorinterface in your attributes,
2. Write a custom jQuery validation method.

Client-side validation metadata (the data-val-* attributes) is generated only for properties, so register this attributes to properties only.
*/

public class AtLeastOneRequiredAttribute : ValidationAttribute, IClientModelValidator
{
	private readonly string[] _propertyNames;

	public AtLeastOneRequiredAttribute(params string[] propertyNames)
	{
		_propertyNames = propertyNames;
		ErrorMessage = $"At least one of the fields [{string.Join(",", _propertyNames)}] must be provided.";
	}

	public void AddValidation(ClientModelValidationContext context)
	{
		context.Attributes.Add("data-val", "true");
		context.Attributes.Add("data-val-atleastonerequired", ErrorMessage ?? "errmsg");
		context.Attributes.Add("data-val-atleastonerequired-fields", string.Join(",", _propertyNames));
	}



	protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
	{
		/* var otherProperty = validationContext.ObjectType.GetProperty(_otherPropertyName);
		if (otherProperty == null) return new ValidationResult($"Unknown property: {_otherPropertyName}");
		var otherValue = otherProperty.GetValue(validationContext.ObjectInstance, null); 
		if (value == null && (otherValue == null || string.IsNullOrWhiteSpace(otherValue.ToString())))
		{
			return new ValidationResult(ErrorMessage ?? $"At least one of the fields must be provided.");
		} */

		var propertyValues = _propertyNames
			.Select(name => validationContext.ObjectType.GetProperty(name)?.GetValue(validationContext.ObjectInstance, null))
			.ToList();

		int filledCount = propertyValues.Count(v => v != null && !string.IsNullOrWhiteSpace(v.ToString()));

		if (filledCount >= 1) return ValidationResult.Success;

		return new ValidationResult(ErrorMessage ?? $"At least one of the fields must be provided.");
	}
}

/*  Add jQuery validation method (usually in a JS file or inline in your view):

$.validator.addMethod("atleasttworequired", function (value, element, params) {
    var fields = params.fields.split(',');
    var filledCount = 0;

    for (var i = 0; i < fields.length; i++) {
        var field = fields[i];
        var $field = $('[name="' + field + '"]');
        if ($field.length > 0 && $field.val().trim() !== "") {
            filledCount++;
        }
    }

    return filledCount >= 2;
});

$.validator.unobtrusive.adapters.add("atleasttworequired", ["fields"], function (options) {
    options.rules["atleasttworequired"] = { fields: options.params.fields };
    options.messages["atleasttworequired"] = options.message;
});

*/