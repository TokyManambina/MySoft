// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('contextmenu',
	event => event.preventDefault());

function verifyMail(email) {
	const regex = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
	return regex.test(String(email).toLowerCase());
}

function decodeHTMLEntities(text) {
	return $("<textarea/>").html(text).text();
}
function encodeHTMLEntities(text) {
	return $("<textarea/>").text(text).html();
}

function convertToPlain(html) {
	var tempDivElement = document.createElement("div");
	tempDivElement.innerHTML = html;
	return tempDivElement.textContent || tempDivElement.innerText || "";
}

function DateLast(date) {
	date = dateDiff(new Date(), new Date(Date.parse(date)));

	var s = "il y a ";


	if (date.year == 0) {
		if (date.month == 0) {
			if (date.day == 0) {
				if (date.hour == 0) {
					if (date.min == 0) return s + date.sec + " s";
					else {
						if (date.min < 6) return s + "environ " + date.min + " mn";
						return s + date.min + " mn";
					}
				}
				return s + date.hour + " h";
			} else {
				if (date.day == 1) return s + date.day + " jour";
				return s + date.day + " jours";
			}

		}
		return s + date.month + " mois";
	} else {
		if (date.year == 1) return s + date.year + " an";
		return s + date.year + " ans";
	}
}


function dateDiff(date1, date2) {
	var diff = {}
	var tmp = date1 - date2;

	tmp = Math.floor(tmp / 1000);
	diff.sec = tmp % 60;

	tmp = Math.floor((tmp - diff.sec) / 60);
	diff.min = tmp % 60;

	tmp = Math.floor((tmp - diff.min) / 60);
	diff.hour = tmp % 24;

	tmp = Math.floor((tmp - diff.hour) / 24);
	diff.day = tmp % 30;

	tmp = Math.floor((tmp - diff.day) / 30);
	diff.month = tmp % 12;


	tmp = Math.floor((tmp - diff.month) / 12);
	diff.year = tmp;

	return diff;
}