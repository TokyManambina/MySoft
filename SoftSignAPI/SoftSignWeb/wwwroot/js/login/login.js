import { apiUrl, webUrl } from "../apiConfig.js";

var typed = new Typed(".typing", {
    strings: ["Zéro Papier","Facile à utiliser", "Rapide et Sécurisé"],
    typeSpeed: 100,
    backSpeed: 70,
    loop:true
});

$(document).ready(() => {

});

$(`[data-id="mail"], [data-id="password"]`).on("keydown", (e) => {
	if (e.keyCode == 13)
		$(`[data-id="login"]`).click();
});

$(`[data-id="login"]`).on('click', (k, v) => {

	let mail = $(`[data-id="mail"]`).val();
	let password = $(`[data-id="password"]`).val();

	if (mail == "" || password == "") {
		alert("Veuillez verifier votre login!");
		return;
	}

	var login = {
		email: mail,
		password: password
	};

	$.ajax({
		type: "POST",
		url: apiUrl + "api/Auth/login",
		data: JSON.stringify(login),
		xhrFields: { withCredentials: true },
		contentType: "application/json",
		datatype: 'json',

		success: function (result) {
			console.log(result)
			sessionStorage.setItem("Authentication", `Bearer ${result.token}`);
			sessionStorage.setItem("role",btoa(result.role));
			$.ajax({
				type: "GET",
				url: webUrl + "Auth",
				contentType: "application/json",
				success: function (data) {

					localStorage.setItem("webBaseUrl", data.baseUrl);
					window.location = data.url;
				},

				Error: function (x, e) {
					alert("Some error");
				}
			})
		},
		Error: function (x, e) {
			alert("Please contact the administrator");
		}
	});
});

