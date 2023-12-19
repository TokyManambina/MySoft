import { apiUrl, webUrl } from "./apiConfig.js";
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


$(document).ready(() => {
	var role = atob(sessionStorage.getItem("role"));
	if (role == 0) {
		$("#espace_client").hide();
	}
	$.ajax({
		type: "GET",
		url: apiUrl + "api/user/isAdmin",
		headers: {
			'Authorization': sessionStorage.getItem("Authentication")
		},
		xhrFields: { withCredentials: true },
		success: function (result) {
			if (!result) {
				$("[societe-nav]").hide();
			}
		},
		Error: function (x, e) {
			alert("Some error");
			//loading(false);
		}
	});
});
