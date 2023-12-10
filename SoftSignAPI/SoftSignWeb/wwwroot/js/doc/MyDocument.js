import { apiUrl, webUrl } from "../apiConfig.js";

$(document).ready(() => {
	$("#sign").text("");
	history.pushState(null, null, document.URL);
	$(window).on('popstate', function () {
		history.pushState(null, null, document.URL);
		console.log("Back navigation blocked");
	});
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
			
			$.each(result, (k, v) => {
				//var date = v.DateSign;
				var icon = "fa-star";
				switch (v.Status) {
					case 0 : icon = "fa-spinner fa-spin"; break;
					case 1 : icon = "fa-file-signature"; break;
					case 2 : icon = "fa-file-archive"; break;
					default: icon = "fa-star"; break;
				}

				$(`#listDocument`).append(documentListUI(v, icon));
			});
			
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

			$("#sign").append(`<div class="btn btn-success bg-gradient mb-3 col-12"><i class="fa fa-check p-2"> </i> Valider</div>`);
			$("#sign").append(`<div class="btn btn-success bg-gradient mb-3 col-12"><i class="fa fa-signature p-2"> </i> Signé</div>`);

			/*
			if (Datas.Resender == true) $("#sign").append(`<div class="btn btn-success bg-gradient mb-3 col-12" data-bs-toggle="modal" data-bs-target="#MconfirmMail" id="btnvalidator" data-target="${id}"><i class="fa fa-paper-plane"></i> Envoyer</div>`);
			else if (Datas.Validators == true) $("#sign").append(`<div class="btn btn-success bg-gradient mb-3 col-12" data-bs-toggle="modal" data-bs-target="#MconfirmMail" id="btnvalidator" data-target="${id}"><i class="fas fa-check"></i> Valider</div>`);
			else if (Datas.Sign == true || Datas.Receiver == "" && Datas.Stat != "Sign") $("#sign").append(`<div class="btn btn-success bg-gradient mb-3 col-12" data-bs-toggle="modal" data-bs-target="#signature_tab" id="btnSign" data-target="${id}"><i class="fas fa-signature"></i> Signé</div>`);
			*/
			//signaturePad.clear();
			//loading(false);



			$.ajax({
				type: "GET",
				url: apiUrl + "api/Document/d/" + id,
				contentType: "application/json",
				datatype: 'json',
				headers: {
					'Authorization': sessionStorage.getItem("Authentication")
				},
				xhrFields: {
					withCredentials: true,
					responseType: 'blob'
				},
				success: function (result) {
					var blobUrl = URL.createObjectURL(result);

					$("[pdf_Viewer]").attr("src", blobUrl);
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

function documentListUI(document, icon) {
	return `
		<tr data-stat="${document.Status}" ViewDocument="${document.code}" >
            <td>
                <div class="icheck-primary">
                <input type="checkbox" value="" id="check1">
                <label for="check1"></label>
                </div>
            </td>
            <td class="mailbox-star"><a href="#"><i class="fa ${icon} text-secondary"></i></a></td>
            <td class="mailbox-name"></td>
            <td class="mailbox-subject pointer" style="min-width : 250px">
				<span class="text-bold">${document.object}</span>
				<span>${convertToPlain(document.message)}</span>
            </td>
            <td class="mailbox-date">${DateLast(document.dateSend)}</td>
            <td> <div></div> </td>
		</tr>
	`;
}
function documentUI(document) {
	return `
		<div id="p_doc" data-type="panel">
			<div class="card card-primary card-outline mb-1">
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

				<div class="card-body p-2">
					<h6><u>Message </u>:</h6>
					<div class="mailbox-controls with-border p-3">${document.message}</div>

					<div class="mailbox-read-message text-center flex-column">
						<embed pdf_Viewer type="application/pdf" src="" width="100%" height="1000">
					</div>
				</div>
			</div>
		</div>
	`;
}