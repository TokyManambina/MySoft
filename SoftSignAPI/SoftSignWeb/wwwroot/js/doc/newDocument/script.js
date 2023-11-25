$(document).ready(() => {
	$("#ISign").hide();
	$(`[card-id="field"]`).hide();
	$('#signature_tab').hide();

	$("#YouSign").removeClass('disabled');
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

$("#YouSign").on("click", (e) => {
	let formData = new FormData();

	var files = $("#inputFile").get(0).files;
	formData.append("upload", files[0]);

	$.ajax({
		type: "POST",
		url: "/Document/UploadDocument",
		data: formData,
		cache: false,
		contentType: false,
		processData: false,
		async: true,

		success: function (result) {
			var result = JSON.parse(result);
			console.log(result);
			alert(result);
			if (result == "")
				return;
			if (result.type == "error") {
				alert("Veuillez verifier votre mail!");
				return;
			}

			SendDocument(result);
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
		recipients: dicoToList1(ListUserDocument),
		pdF_Width: parseFloat($("#pdfViewer").width()),
		pdF_Height: parseFloat($("#pdfViewer").height())
	}

	$.ajax({
		type: "POST",
		url: "https://localhost:7250/api/Document/" + id,
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