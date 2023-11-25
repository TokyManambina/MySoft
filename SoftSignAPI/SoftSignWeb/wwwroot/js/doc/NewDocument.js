$(document).ready(() => {
	$("#ISign").hide();
});

$('input[name="radioSign"]').on('change', (e) => { 
	console.log(e.target);
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

$("#inputFile").change(function () {
	if (currentPDF.file == null) {
		if ($("#addSignature").hasClass("fa-ban")) {
			$("#addSignature").removeClass("fa-ban");
			$("#addSignature").addClass("fa-plus");
		}
		if ($("#addParaphe").hasClass("fa-ban")) {
			$("#addParaphe").removeClass("fa-ban");
			$("#addParaphe").addClass("fa-plus");
		}
	}
})

$("#valid").change(function () {
	if ($(this).prop("checked") == true) {
		$("#btn-menu").removeClass("hidden");
		$("#valid-menu").removeClass("hidden");
		if ($("#minus").hasClass("fa-minus")) {
			$("#valid-menu").css("display", "block");
		}
	} else {
		$("#btn-menu").addClass("hidden");
		$("#valid-menu").addClass("hidden");
		if ($("#valid-menu").css("display") == "block") {
			$("#valid-menu").css("display", "none");
		}
	}
});

//validator
var countValidator = 0;

//#region Windows Resize : DropDown Position
$(window).on('resize', (e) => {
	var win = $(this);
	if (win.width() <= 700 && $(`[target="drop"]`).hasClass("dropright"))
		$(`[target="drop"]`).removeClass("dropright").addClass("dropdown");
	else if (win.width() > 700 && $(`[target="drop"]`).hasClass("dropdown"))
		$(`[target="drop"]`).removeClass("dropright").addClass("dropright");
})
//#endregion

$(`[data-action="sendDocumen"]`).on('click', (e) => {
	if ($(e.target).hasClass("disabled"))
		return;


	$(e.target).addClass("disabled");
	var files = $("#inputFile").get(0).files;

	if (files.length == 0) {
		alert("Auncun Document sélectionné.");
		return;
	}

	if (!canSend()) {
		alert("Veuillez vérifier le(s) Déstinataire(s).");
		return;
	}
		

	let formData = new FormData();
	formData.append("upload", files[0]);

	//#region Mail
	formData.append("document.Object", $("#object").val());
	formData.append("document.Receiver", $("#receiver").val());
	formData.append("document.Message", encodeHTMLEntities($("#message").val()));

	//get list receiver

	//get list Cc
	if ($("#ccCheck").is(":checked")) {
		$(mailCc.getValue()).each((k, v) => {
			formData.append(`Cc[${k}]`,v);
		});
	}
		

	//#endregion

	//#region Validator
	if ($("#valid").is(":checked")) {
		var validList = $("#listValidator").find("span");
		var countValid = validList.length;
		if (countValid != 0) {
			validList.each(function (k,v) {
				formData.append(`Validator[${k}]`, $(this).text());
			});
		}
	}
	//#endregion

	//#region PDF
	let pdfContainer = $('#pdfViewer').offset();
	let posSign = { top: null, left: null };
	
	//#region PDF pdfDetail.pdfInfo
	formData.append("pdfDetail.pdfInfo", parseFloat($("#pdfViewer").width()).toString().replace('.', ',') +
		"|" + parseFloat($("#pdfViewer").height()).toString().replace('.', ','));
	//#endregion

	//#region Signature pdfDetail._SignInfo
	if (!$("#signatureListBox").hasClass("hidden")) {
		var listsignaturebox = $("#signatureListBox").find(`[data-type="signature"]`);
		listsignaturebox.each(function (k, v) {
			var offsetSign = $(this).offset();
			posSign = {
				top: offsetSign.top - pdfContainer.top,
				left: offsetSign.left - pdfContainer.left
			};

			id = $(this).attr("data-id");

			formData.append(`pdfDetail._SignInfo[${k}].FirstPage`, $(`#signature${id}`).data("value"));
			formData.append(`pdfDetail._SignInfo[${k}].X`, parseFloat(posSign.left).toString().replace('.', ','));
			formData.append(`pdfDetail._SignInfo[${k}].Y`, parseFloat(posSign.top).toString().replace('.', ','));
			formData.append(`pdfDetail._SignInfo[${k}].W`, parseFloat($(this).width()).toString().replace('.', ','));
			formData.append(`pdfDetail._SignInfo[${k}].H`, parseFloat($(this).height()).toString().replace('.', ','));

		});

		//formData.append("pdfDetail._SignInfo", JSON.stringify(list));
	}
	//#endregion
	
	//#region Paraphe pdfDetail._ParapheInfo
	if (!$("#paraphe").hasClass("hidden")) {
		var offsetSign = $("#paraphe").offset();

		posSign = {
			top: offsetSign.top - pdfContainer.top,
			left: offsetSign.left - pdfContainer.left
		};

		var parapheList = $("#actionmenu").find('[data-type="paraphe"]');
		let page = "";
		var countparapheList = parapheList.length;
		if (countparapheList != 0) {
			parapheList.each(function (k,v) {

				page = $(this).attr("data-value").split("|");

				formData.append(`pdfDetail._ParapheInfo[${k}].FirstPage`, page[0]);
				formData.append(`pdfDetail._ParapheInfo[${k}].LastPage`, page[1]);
				formData.append(`pdfDetail._ParapheInfo[${k}].X`, parseFloat(posSign.left).toString().replace('.', ','));
				formData.append(`pdfDetail._ParapheInfo[${k}].Y`, parseFloat(posSign.top).toString().replace('.', ','));
				formData.append(`pdfDetail._ParapheInfo[${k}].W`, parseFloat($(this).width()).toString().replace('.', ','));
				formData.append(`pdfDetail._ParapheInfo[${k}].H`, parseFloat($(this).height()).toString().replace('.', ','));
			});

		}
	}
	//#endregion
	
	$.ajax({
		type: "POST",
		url: "/Document/SendDocument",
		data: formData,
		cache: false,
		contentType: false,
		processData: false,
		async: true,

		success: function (result) {
			var result = JSON.parse(result);
			console.log(result);
			if (result.type == "error") {
				alert("Veuillez verifier votre mail!");
				return;
			}

			//window.location.href = result.message;
		},

		Error: function (x, e) {
			alert("Please contact the administrator");
		}
	});
})


