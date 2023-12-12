import { apiUrl, webUrl } from "../apiConfig.js";
import * as Action from './NewDocument.action.js?v=0.1.0';
import * as Attachement from './NewDocument.attachement.js?v=0.1.0';
import * as Field from './NewDocument.field.js?v=0.1.0';
import * as Recipient from './NewDocument.recipient.js?v=0.1.0';



$(document).ready(() => {
	$("#ISign").hide();
	$(`[card-id="field"]`).hide();
	$('#signature_tab').hide();

	$("#YouSign").removeClass('disabled');
});



$(`[data-action="sendDocument"]`).on("click", (e) => {
	$(`[field-id]`).mousemove();
	if (Object.keys(ListUserDocument).length === 0) {
		alert("Veuillez ajouter un déstinataire au minimum.")
		return;
	}

	let object = $("#objectId").val();
	let message = $("#mailMessage").summernote("code");
	if ($("#mailMessage").summernote("code") == '<p><br></p>')
		message = "";
	if (object == "") {
		alert("Veuillez renseigner l'objet du document.")
	}

	var files = $("#inputFile").get(0).files;

	let formData = new FormData();
	//for (var i = 0; i < 5; i++) {
	//	formData.append("Files", files[0]);
	//}



	formData.append("Files", files[0]);
	formData.append("Recipients", JSON.stringify(dicoToList1(ListUserDocument)));
	formData.append("Object", object);
	formData.append("Message", message);

	$.ajax({
		type: "POST",
		data: formData,
		contentType: false,
		processData: false,
		async: true,
		url: apiUrl + "api/document",
		xhrFields: { withCredentials: true },
		headers: {
			'Authorization': sessionStorage.getItem("Authentication")
		},


		success: function (result) {
			alert("document envoyé");
			window.location = webUrl + "documents";
		},

		Error: function (x, e) {
			alert("Please contact the administrator");
		}
	});
});

$(`[sign-confirm]`).on("click", (e) => {
	$(`[field-id]`).mousemove();

	var files = $("#inputFile").get(0).files;

	if (!files.length) {
		alert("Veuillez selectionner un document.");
		return;
	}

	if (Object.keys(ListUserDocument).length === 0) {
		alert("Veuillez ajouter un déstinataire au minimum.")
		return;
	}

	if (!signTest) {
		alert("Vous avez oublié de signer");
		return;
	}

	let signImage = signaturePad.toDataURL();
	
	/*let object = $("#objectId").val();
	let message = $("#mailMessage").summernote("code");
	if ($("#mailMessage").summernote("code") == '<p><br></p>')
		message = "";
	if (object == "") {
		alert("Veuillez renseigner l'objet du document.")
	}*/

	

	let formData = new FormData();
	//for (var i = 0; i < 5; i++) {
	//	formData.append("Files", files[0]);
	//}
	let width = $("#pdfViewer").width();
	let height = $("#pdfViewer").height();



	formData.append("Files", files[0]);
	formData.append("Fields", JSON.stringify(Object.values(ListUserDocument["me"].fields)));
	formData.append("Object", object);
	formData.append("Message", message);
	formData.append("Sign", signImage);

	$.ajax({
		type: "POST",
		data: formData,
		contentType: false,
		processData: false,
		async: true,
		url: apiUrl + "api/document/me",
		xhrFields: { withCredentials: true },
		headers: {
			'Authorization': sessionStorage.getItem("Authentication")
		},


		success: function (result) {
			console.log(result);
			alert(result);
			if (result == "")
				return;
			if (result.type == "error") {
				alert("Veuillez verifier votre mail!");
				return;
			}

			alert("document envoyé");

			window.location = webUrl + "documents";
			//SendDocument(result);
		},

		Error: function (x, e) {
			alert("Please contact the administrator");
		}
	});
});
function dicoToList1(obj) {
	var list = Object.values(obj);
	for (var i = 0; i < list.length; i++) {
		list[i].fields = Object.values(list[i].fields);
	}
	return list;
}

function SendDocument(id) {
	var list = {
		recipients: dicoToList1(Recipient.GetListUserDocument()),
		pdF_Width: parseFloat($("#pdfViewer").width()),
		pdF_Height: parseFloat($("#pdfViewer").height())
	}

	$.ajax({
		type: "POST",
		url: apiUrl + "/api/Document/" + id,
		data: JSON.stringify(list),
		contentType: "application/json",
		datatype: 'json',
		success: function (data) {
			console.log(data);
			if (data.type == "error") {
				Toast.fire({
					icon: 'error',
					title: data.message
				});
				return;
			}

		},

		Error: function (x, e) {
			alert("Some error");
		}
	});
}