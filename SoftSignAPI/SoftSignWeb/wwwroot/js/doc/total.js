import { apiUrl, webUrl } from "../apiConfig.js";

$(document).ready(() => {
	$.ajax({
		type: "GET",
		url: apiUrl + "api/Document/u/info",
		contentType: "application/json",
		datatype: 'json',
		headers: {
			'Authorization': sessionStorage.getItem("Authentication")
		},
		xhrFields: { withCredentials: true },
		success: function (result) {

			let count = 0;
			$.each(result, (k, v) => {
                switch (v.stat) {
					case 0: $(`[data-id="ct_DocRemain"]`).text(v.count); count += v.count; break;
					case 1: $(`[data-id="ct_DocSign"]`).text(v.count); count += v.count; break;
					case 2: $(`[data-id="ct_DocArc"]`).text(v.count); count += v.count; break;
					default: break;
                }
			});

			$(`[data-id="ct_DocTot"]`).text(count);
		},

		Error: function (x, e) {
			alert("Please contact the administrator");
		}
	});
});