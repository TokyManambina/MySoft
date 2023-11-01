$(document).ready(() => {
	$("#ISign").hide();
	$(`[card-id="field"]`).hide();
});


$('input[name="radioSign"]').on('change', (e) => {
	if (e.target.id == "isign") {
		$("#ISign").show();
		$("#sendDocument").hide();
		$("#cardValidator").hide();
		$("#ui_destinataire").hide();
		//$("#ui_paraphe").hide();
	} else {
		$("#ISign").hide();
		$("#sendDocument").show();
		$("#cardValidator").show();
		$("#ui_destinataire").show();
		//$("#ui_paraphe").show();
	}
});