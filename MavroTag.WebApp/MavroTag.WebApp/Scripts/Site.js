function showError(message, id) {
	$("#error").show();
	$("#error").text(message);
	$(id).focus();
}

function hideError() {
	$("#error").hide();
	$("#success").hide();
}

function validateRequired(id, message) {
	hideError();
	if (!$(id).val()) {
		showError(message, id);
		return false;
	}
	return true;
}
