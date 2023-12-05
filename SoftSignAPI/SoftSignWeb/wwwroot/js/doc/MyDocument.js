﻿import { apiUrl, webUrl } from "../apiConfig.js";

$(document).ready(() => {
	$("#sign").text("");
	MyDocument();
})

function MyDocument() {
	$.ajax({
		type: "GET",
		url: apiUrl + "api/Document/filter/posted",
		contentType: "application/json",
		datatype: 'json',
		headers: {
			'Authorization': sessionStorage.getItem("Authentication")
		},
		xhrFields: { withCredentials: true },
		success: function (result) {
			console.log(result)
			
			$(`#listDocument`).text("");

			let code = ``;
			$.each(result, (k, v) => {
				//var date = v.DateSign;
				var icon = "fa-star";
				switch (v.Status) {
					case 0 : icon = "fa-spinner fa-spin"; break;
					case 1 : icon = "fa-file-signature"; break;
					case 2 : icon = "fa-file-archive"; break;
					default: icon = "fa-star"; break;
				}

				code += `
					<tr data-stat="${v.Status}" ViewDocument="${v.code}" >
                        <td>
                          <div class="icheck-primary">
                            <input type="checkbox" value="" id="check1">
                            <label for="check1"></label>
                          </div>
                        </td>
                        <td class="mailbox-star"><a href="#"><i class="fa ${icon} text-secondary"></i></a></td>
                        <td class="mailbox-name"></td>
                        <td class="mailbox-subject pointer" style="min-width : 250px">
							<span class="text-bold">${v.object}</span>
							<span>${convertToPlain(v.message)}</span>
                        </td>
                        <td class="mailbox-date">${DateLast(v.dateSend)}</td>
                        <td> <div></div> </td>
					</tr>`;
			});

			$(`#listDocument`).append(code);
		},

		Error: function (x, e) {
			alert("Please contact the administrator");
		}
	});
}

$(document).on('click', '[ViewDocument]', (e) => {
	let header = $(e.target).closest('[ViewDocument');
	let id = header.attr('ViewDocument');

	$.ajax({
		type: "GET",
		url: apiUrl + "api/Document/" + id,
		contentType: "application/json",
		datatype: 'json',
		headers: {
			'Authorization': sessionStorage.getItem("Authentication")
		},
		xhrFields: { withCredentials: true },
		success: function (result) {
			console.log(result)

			if (result == "error") {
				window.location = "../";
			}

			var panel = $(`#Panel`).find("div[data-type='panel']:not(div#p_MyDocument)");

			panel.remove();

			$(`#Panel`).append(documentUI(result));

			$(`#p_MyDocument`).hide();
			$("#sign").text("");
			/*
			if (Datas.Resender == true) $("#sign").append(`<div class="btn btn-success bg-gradient mb-3 col-12" data-bs-toggle="modal" data-bs-target="#MconfirmMail" id="btnvalidator" data-target="${id}"><i class="fa fa-paper-plane"></i> Envoyer</div>`);
			else if (Datas.Validators == true) $("#sign").append(`<div class="btn btn-success bg-gradient mb-3 col-12" data-bs-toggle="modal" data-bs-target="#MconfirmMail" id="btnvalidator" data-target="${id}"><i class="fas fa-check"></i> Valider</div>`);
			else if (Datas.Sign == true || Datas.Receiver == "" && Datas.Stat != "Sign") $("#sign").append(`<div class="btn btn-success bg-gradient mb-3 col-12" data-bs-toggle="modal" data-bs-target="#signature_tab" id="btnSign" data-target="${id}"><i class="fas fa-signature"></i> Signé</div>`);
			*/
			//signaturePad.clear();
			//loading(false);

			$.ajax({
				type: "GET",
				url: apiUrl + "api/Document/" + id,
				contentType: "application/json",
				datatype: 'json',
				headers: {
					'Authorization': sessionStorage.getItem("Authentication")
				},
				xhrFields: { withCredentials: true },
				success: function (result) {

				},
				Error: function (x, e) {
					alert("Some error");
					//loading(false);
				}
			})
		},

		Error: function (x, e) {
			alert("Some error");
			//loading(false);
		}
	});
});

function documentUI(document) {
	return `
		<div id="p_doc" data-type="panel">
			<div class="card card-primary card-outline mb-5">
				<div class="card-header">
					<button class="btn btn-default btn-sm" onclick="changeList('All')">
							<i class="fas fa-reply"></i>
					</button><br><br>
					<h6 class="col"><u>Objet</u> : ${document.object}</h6>
					<div class="col">
						<span class="mailbox-read-time"></span>
						<span class="mailbox-read-time float-right">${document.dateSend}</span>
					</div>
				</div>

				<div class="card-body p-0">
					<div class="mailbox-controls with-border text-center">${convertToPlain(document.message)}</div>

					<div class="mailbox-read-message text-center flex-column">
						<embed type="application/pdf" src="/dossier/20231126081603-facture.pdf" width="80%" height="800">
					</div>
				</div>
			</div>
		</div>
	`;
}