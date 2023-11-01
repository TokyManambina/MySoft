$(document).ready(() => {
	$("#ISign").hide();
	$(`[card-id="field"]`).hide();
});

$('input[name="radioSign"]').on('change', (e) => {
	if (Object.keys(ListUserDocument).length > 0) {
		$("[page-id]").remove();
		$("[field-id]").remove();
		$("[recipient-id]").remove();

		ListUserDocument = [];
		selectedRecipient = "";
	}

	if (e.target.id == "isign") {
		$("#ISign").show();
		$("#YouSign").hide();
		$(`[card-id="field"]`).show();
		$(`[card-id="recipient"]`).hide();

		selectedRecipient = "me";
		ListUserDocument[selectedRecipient] = {
			role: "",
			mail: selectedRecipient,
			cc: "",
			message: "",
			color: RandomColor(),
			fields: []
		};
	} else {
		$("#ISign").hide();
		$("#YouSign").show();
		$(`[card-id="field"]`).hide();
		$(`[card-id="recipient"]`).show();
	}
});

