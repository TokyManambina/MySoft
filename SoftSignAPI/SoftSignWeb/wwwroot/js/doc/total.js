$(document).ready(() => {
	$.ajax({
		type: "GET",
		url: "/Document/GetInfoDoc",
		data: null,

		success: function (result) {
			var result = JSON.parse(result);
			if (result.type == "error") {
				return;
			}
			console.log(result);
			let count = 0;
			$.each(result.data, (k, v) => {
                switch (v.Key) {
					case 0: $(`[data-id="ct_DocRemain"]`).text(v.Value); count += v.Value; break;
					case 2: $(`[data-id="ct_DocSign"]`).text(v.Value); count += v.Value; break;
					case 3: $(`[data-id="ct_DocArc"]`).text(v.Value); count += v.Value; break;
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