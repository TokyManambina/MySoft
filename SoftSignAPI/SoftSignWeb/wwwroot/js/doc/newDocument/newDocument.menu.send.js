import { apiUrl, webUrl } from "../../apiConfig.js";

let signExist = false;
let parapheExist = false;

//#region USign
function dicoToList(obj) {
	var list = Object.values(obj);
	for (var i = 0; i < list.length; i++) {
		list[i].fields = Object.values(list[i].fields);
	}
	return list;
}

$(`[data-action="sendDocument"]`).on("click", (e) => {
	$(`[field-id]`).mousemove();
	if (Object.keys(ListUserDocument).length === 0) {
		alert("Veuillez ajouter un déstinataire au minimum.")
		return;
	}

	if (!VerifyAllRequiredDynamicField()) return;

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
	formData.append("Recipients", JSON.stringify(dicoToList(ListUserDocument)));
	formData.append("Title", $(`[detail-id="title"]`).val());
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
			console.log(result);
			alert("document envoyé");
			window.location = webUrl + "documents";
		},

		Error: function (x, e) {
			alert("Please contact the administrator");
		}
	});
});
//#endregion

//#region ISign
$("#signature_tab").on('show.bs.modal', function (e) {
	signExist = false;
	parapheExist = false;

	//$("[sign-modal-dialog]").removeClass("modal-sm").addClass("modal-lg");
	//$("#paraphe-pad").removeClass("col-12").addClass("col-4");

	//$("#signature-pad").hide();
	//$("#paraphe-pad").hide();

	if ($(`[page-id][data-type="signature"]`).length > 0) {
		$("#signature-pad").show();
		signExist = true;
	}
	if ($(`[page-id][data-type="paraphe"]`).length > 0) {
		$("#paraphe-pad").show();
		parapheExist = true;
		//if (!signExist) {
		//	$("#paraphe-pad").removeClass("col-4").addClass("col-12");
		//	$("[sign-modal-dialog]").removeClass("modal-lg").addClass("modal-sm");
		//}
	}

	if (!signExist && !parapheExist) {
		alert("Veuillez renseigner les signatures ou/et paraphes");
		e.preventDefault();
	}
})

$(`[sign-confirm]`).on("click", (e) => {
	$(`[field-id]`).mousemove();
	if (!VerifyAllRequiredDynamicField()) return;

	var files = $("#inputFile").get(0).files;

	if (!files.length) {
		alert("Veuillez selectionner un document.");
		return;
	}
	
	if (signExist && !signaturePad.isSign) {
		alert("Vous avez oublié le signature");
		return;
	}
	if (parapheExist && !paraphePad.isSign) {
		alert("Vous avez oublié le paraphe");
		return;
	}

	let signImage = signaturePad.toDataURL();
	let parapheImage = paraphePad.toDataURL();
	
	console.log(ListUserDocument["me"].fields)
	let formData = new FormData();

	formData.append("Files", files[0]);
	formData.append("Fields", JSON.stringify(Object.values(ListUserDocument["me"].fields)));
	formData.append("Title", $(`[detail-id="title"]`).val());
	formData.append("SignImage", signImage);
	formData.append("ParapheImage", parapheImage);

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

			alert("document Signé");

			window.location = webUrl + "documents";
			//SendDocument(result);
		},

		Error: function (x, e) {
			alert("Please contact the administrator");
		}
	});
});

function VerifyAllRequiredDynamicField() {
	let allOk = true;
	$(`[detail-id]`).each((k, v) => {
		if (!$(v).val()) {
			allOk = false;
			alert("Veuillez completer les champs obligatoires");
			return false;
		}
	});
	return allOk;
}
//#endregion		