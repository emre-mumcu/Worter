$.validator.addMethod("atleastonerequired", function (value, element, params) {

console.log("hehe");

	var fields = params.fields.split(',');
	var filledCount = 0;

	for (var i = 0; i < fields.length; i++) {
		var field = fields[i];
		var $field = $('[name="' + field + '"]');
		if ($field.length > 0 && $field.val().trim() !== "") {
			filledCount++;
		}
	}

	return filledCount >= 1;
});

$.validator.unobtrusive.adapters.add("atleastonerequired", ["fields"], function (options) {
	options.rules["atleastonerequired"] = { fields: options.params.fields };
	options.messages["atleastonerequired"] = options.message;
});