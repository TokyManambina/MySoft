import { apiUrl, webUrl } from "../apiConfig.js";

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
					<tr data-stat="${v.Status}" >
                        <td>
                          <div class="icheck-primary">
                            <input type="checkbox" value="" id="check1">
                            <label for="check1"></label>
                          </div>
                        </td>
                        <td class="mailbox-star"><a href="#"><i class="fa ${icon} text-secondary"></i></a></td>
                        <td onclick="ViewDocument('${v.code}')" class="mailbox-name"></td>
                        <td onclick="ViewDocument('${v.code}')" class="mailbox-subject pointer" style="min-width : 250px">
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

//$(`[data-action="newDocument"]`).on('click', (k, v) => {
//	return;
//	$.ajax({
//		type: "GET",
//		url: "/NewDocument",
//		data: null,
//		cache: false,
//		contentType: false,
//		processData: false,
//		async: true,
//		Error: function (x, e) {
//			alert("Some error");
//		}
//	});
//});