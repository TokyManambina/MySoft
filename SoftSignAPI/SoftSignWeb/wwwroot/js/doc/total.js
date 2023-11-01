$(document).ready(() => {
	$.ajax({
		type: "GET",
		url: "/Document/GetDocumentInfo",
		data: null,

		success: function (result) {
			var result = JSON.parse(result);
			if (result.type == "error") {
				return;
			}

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