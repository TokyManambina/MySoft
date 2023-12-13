import { apiUrl, webUrl } from "../../apiConfig.js";

$(document).on('click', `[document-action="validate"]`, (e) => {
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

				switch (v.status) {
					case 0: icon = "fa-spinner fa-spin"; break;
					case 1: icon = "fa-file-signature"; break;
					case 2: icon = "fa-file-archive"; break;
					default: icon = "fa-star"; break;
				}

				$(`#listDocument`).append(documentListUI(v, icon));
			});

		},

		Error: function (x, e) {
			alert("Please contact the administrator");
		}
	});
});
