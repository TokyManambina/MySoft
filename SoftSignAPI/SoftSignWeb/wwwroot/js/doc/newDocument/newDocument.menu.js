import { apiUrl, webUrl } from "../../apiConfig.js";
import * as Action from './newDocument.menu.action.js?v=0.1.0';
import * as Attachement from './newDocument.menu.attachement.js?v=0.1.0';
import * as Field from './newDocument.menu.field.js?v=0.1.0';
import * as Recipient from './newDocument.menu.recipient.js?v=0.1.0';
import * as Send from './newDocument.menu.send.js?v=0.1.0';

$(`[card-id="attachement"]`).remove();
$(`[card-id="dynamic-field"]`).remove();



$(document).ready(() => {
	fullHide();
	return;
	$("#ISign").hide();
	$(`[card-id="field"]`).hide();
	$('#signature_tab').hide();

	$("#YouSign").removeClass('disabled');
});


$('input[name="radioSign"]').on('change', (e) => {
	RemoveAllField();

	if (e.target.id == "isign") {
		$("[usign]").removeClass('active');
		$("[isign]").addClass('active');

		$(`[card-id="detail"]`).show();
		$(`[card-id="field"]`).show();
		$(`[card-id="recipient"]`).hide();

		$("#ISign").show();
		$("#YouSign").hide();
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
		$("[isign]").removeClass('active');
		$("[usign]").addClass('active');

		$("#ISign").hide();
		$("#YouSign").show();
		$(`[card-id="field"]`).hide();
		$(`[card-id="recipient"]`).show();

	}
});