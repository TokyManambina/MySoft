var typed = new Typed(".typing",{
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

	let formData = new FormData();
	formData.append("mail", mail);
	formData.append("password", password);

	$.ajax({
		type: "POST",
		url: "/Auth",
		data: formData,
		xhrFields: { withCredentials: true },
		cache: false,
		contentType: false,
		processData: false,
		async: true,

		success: function (result) {
			var result = JSON.parse(result);

			if (result.type == "error") {
				alert("Veuillez verifier votre mail!");
				return;
			}

			sessionStorage.setItem("Authentication", `Bearer ${result.token}`);

			window.location.href = result.uri;
		},
		Error: function (x, e) {
			alert("Please contact the administrator");
		}
	});
});

