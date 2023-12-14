import { apiUrl, webUrl } from "../apiConfig.js";

$(document).ready(() => {
    GetSocieties();
    GetSociete();
});
function GetSociete() {
    $.ajax({
        type: "Get",
        url: apiUrl + "api/society/statistique",
        contentType: "application/json",
        datatype: 'json',
        headers: {
            'Authorization': sessionStorage.getItem("Authentication")
        },
        xhrFields: { withCredentials: true },
        success: function (Datas) {
            $("#societe_nombre").html(Datas);
        },

        Error: function (x, e) {
            alert("Some error");
            //loading(false);
        }
    });
}
function GetUser(UserId) {
    $.ajax({
        type: "Get",
        url: apiUrl + "api/society/" + UserId,
        //data: JSON.stringify(WorkspaceId),
        contentType: "application/json",
        datatype: 'json',

        success: function (Datas) {
            console.log(Datas);
            let User = Datas;
            $("#id").val(User.id);
            $("#name").val(User.name);
            $("#storage").val(User.storage);
        },

        Error: function (x, e) {
            alert("Some error");
            //loading(false);
        }
    });
}
function DeleteSociety(UserId) {
    $.ajax({
        type: "Delete",
        url: apiUrl + "api/society/" + UserId,
        //data: JSON.stringify(WorkspaceId),
        contentType: "application/json",
        datatype: 'json',

        success: function (Datas) {
            alert(Datas);
        },

        Error: function (x, e) {
            alert("Some error");
            //loading(false);
        }
    });
}
function GetSocieties() {
    $.ajax({
        type: "GET",
        url: apiUrl + "api/society",
        headers: {
            'Authorization': sessionStorage.getItem("Authentication")
        },
        xhrFields: { withCredentials: true },
        success: function (result) {
            /*if (result == "login") {
                ToLogin();
            }*/
            if (result == "error") {
                Toast.fire({
                    icon: 'error',
                    title: Datas.msg
                });
            }

            let code = ``;
            $.each(result.data, function (k, v) {
                code += UserUI(v,result.role);
            });
            $(`#liste_user`).html(code);
        },

        Error: function (x, e) {
            alert("Some error");
            //loading(false);
        }
    });
}




$(document).on('click', '[user-create]', (e) => {
    let id = $("#id").val();
    let name = $("#name").val();
    let storage = $("#storage").val();

    if (name == "") {
        Toast.fire({
            icon: 'error',
            title: "Le nom est obligatoire."
        });
        return;
    }


    //Goto Back
    if (id.length === 0) {
        $.ajax({
            type: "POST",
            url: apiUrl + "api/society",
            xhrFields: { withCredentials: true },

            contentType: "application/json",
            datatype: 'json',
            data: JSON.stringify({
                "name": name,
                "storage": storage
            }),
            success: function (Datas) {
                console.log(Datas);
                if (Datas.type == "error") {
                    Toast.fire({
                        icon: 'error',
                        title: Datas.msg
                    });
                    return;
                }

                Toast.fire({
                    icon: 'success',
                    title: `Insertion Utilisateur`
                });
            },

            Error: function (x, e) {
                Toast.fire({
                    icon: 'error',
                    title: e
                });
            }
        });
        $("#modal-user").modal("hide");
    }
    else {
        $.ajax({
            type: "Put",
            url: apiUrl + "api/society/" + id,
            xhrFields: { withCredentials: true },

            contentType: "application/json",
            datatype: 'json',
            data: JSON.stringify({
                "name": name,
                "storage": storage
            }),

            success: function (Datas) {
                console.log(Datas);
                if (Datas.type == "error") {
                    Toast.fire({
                        icon: 'error',
                        title: Datas.msg
                    });
                    return;
                }

                Toast.fire({
                    icon: 'success',
                    title: `Mis à jour Utilisateur`
                });
            },

            Error: function (x, e) {
                Toast.fire({
                    icon: 'error',
                    title: e
                });
            }
        });
        $("#modal-user").modal("hide");
    }
    alert("Mis à jour réussit!");
    window.location.reload();
});
$(document).on('click', '[user-update]', (e) => {
    let header = $(e.target).closest(`[user-update]`);
    let id = header.attr("user-update");
    GetUser(id);
    $("#modal-user").modal("toggle");

});

$(document).on('click', '[user-modal]', (e) => {
    $("#name").val("");
    $("#society").val("");
    $("#id").val("");
    $("#modal-user").modal("toggle");
});
$(document).on('click', '[modal-closed]', (e) => {
    $("#modal-user").modal("hide");
});

$(document).on('click', '[user-delete]', (e) => {
    let header = $(e.target).closest(`[user-delete]`);
    let id = header.attr("user-delete");
    if (confirm('Etes-vous sûre de supprimer?')) {
        DeleteSociety(id);
    }
    window.location.reload();
});

function UserUI(user, userconnected) {
    let config = "";
    if (userconnected) {
        config = `<td><button class="btn btn-primary" user-update="${user.id}">Update</button></td>
            <td><button class="btn btn-danger" user-delete="${user.id}">Delete</button></td>`;
    }
    return `
        <tr>
            <td>${user.id}</td>
            <td>${user.name}</td>
            <td>${user.storage}</td>
            ${config}
        </tr>
    `;
}
