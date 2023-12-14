import { apiUrl, webUrl } from "../apiConfig.js";

$(document).ready(() => {
    GetUsers();
    GetListSociete();
    showListRole();
    GetStatistique();
});

function GetStatistique() {
    $.ajax({
        type: "Get",
        url: apiUrl + "api/User/statistique",
        contentType: "application/json",
        datatype: 'json',
        headers: {
            'Authorization': sessionStorage.getItem("Authentication")
        },
        xhrFields: { withCredentials: true },
        success: function (Datas) {
            var icon_max = !Datas.max_user ? `<i class="fa-solid fa-infinity"></i>` : Datas.max_user;
            var reste_user = !Datas.reste_user ? `<i class="fa-solid fa-infinity"></i>` : Datas.reste_user;
            $("#max_user").html(icon_max);
            $("#curr_user").html(Datas.curr_user);
            $("#reste_user").html(reste_user);
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
        url: apiUrl + "api/User/" + UserId,
        //data: JSON.stringify(WorkspaceId),
        contentType: "application/json",
        datatype: 'json',

        success: function (Datas) {
            console.log(Datas);
            let User = Datas;
            $("#id").val(User.id);  
            $("#prenom").val(User.firstName);
            $("#nom").val(User.lastName);
            $("#email").val(User.email);
            $("#role").val(User.role);
            $("#password").val(User.password);
            $("#societe").val(User.societyId);
        },

        Error: function (x, e) {
            alert("Some error");
            //loading(false);
        }
    });
}
function DeleteUser(UserId) {
    $.ajax({
        type: "Delete",
        url: apiUrl + "api/User/" + UserId,
        //data: JSON.stringify(WorkspaceId),
        contentType: "application/json",
        datatype: 'json',

        success: function (Datas) {

            let User = Datas.data;
            $("#nom").val(User.firstName);
            $("#prenom").val(User.lastName);
            $("#email").val(User.email);
            $("#role").val(User.role);
        },

        Error: function (x, e) {
            alert("Some error");
            //loading(false);
        }
    });
}
async function GetUsers() {
    var list_role = await GetListRole();
    $.ajax({
        type: "GET",
        url: apiUrl + "api/user/get",
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
                v.role = list_role[v.role].name;
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

async function showListRole() {
    var result = await GetListRole();
    let code = ``;
    $.each(result, function (k, v) {
        code += OptionUI(v);
    });
    $(`#role`).html(code);
}
async function GetListRole() {
    let data = await fetch(apiUrl + "api/user/getlistrole");
    let result = await data.json();
    return result;

};

function GetListSociete() {
    $.ajax({
        type: "GET",
        url: apiUrl + "api/society",

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

                code += societeUI(v);
            });
            $(`#society`).html(code);
        },

        Error: function (x, e) {
            alert("Some error");
            //loading(false);
        }
    });
}


$(document).on('click', '[user-create]', (e) => {
    let id = $("#id").val();
    let nom = $("#nom").val();
    let prenom = $("#prenom").val();
    let password = $("#password").val();
    let email = $("#email").val();
    let role = parseInt($("#role").val());
    let society = $("#societe").val();

    if (email == "") {
        Toast.fire({
            icon: 'error',
            title: "L'email est obligatoire."
        });
        return;
    }


    //Goto Back
    if (id.length === 0) {
        $.ajax({
            type: "POST",
            url: apiUrl + "api/Auth/Register",
            xhrFields: { withCredentials: true },

            contentType: "application/json",
            datatype: 'json',
            data: JSON.stringify({
                "email": email,
                "password": password,
                "SocietyId":society

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
            url: apiUrl + "api/User/" + id,
            xhrFields: { withCredentials: true },

            contentType: "application/json",
            datatype: 'json',
            data: JSON.stringify({
                "email": email,
                "password": password,
                "firstName": nom,
                "lastName": prenom,
                "role": role,
                "SocietyId" : society
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
    window.location.reload();
});
$(document).on('click', '[user-update]', (e) => {
    let header = $(e.target).closest(`[user-update]`);
    let id = header.attr("user-update");
    GetUser(id);
    $("#id_nom").show();
    $("#id_prenom").show();
    $("#id_role").show();
    $("#id_password").hide();
    $("#modal-user").modal("toggle");

});

$(document).on('click', '[user-modal]', (e) => {
    $("#email").val("");
    $("#password").val("");
    $("#nom").val("");
    $("#prenom").val("");
    $("#id").val("");
    $("#societe").val("");
    $("#id_password").show();
    $("#id_nom").hide();
    $("#id_prenom").hide();
    $("#id_role").hide();
    $("#role").val("1");
    $("#modal-user").modal("toggle");
});
$(document).on('click', '[modal-closed]', (e) => {
    $("#modal-user").modal("hide");
});

$(document).on('click', '[user-delete]', (e) => {
    let header = $(e.target).closest(`[user-delete]`);
    let id = header.attr("user-delete");
    if (confirm('Etes-vous sûre de supprimer?')) {
        DeleteUser(id);
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
            <td>${user.lastName}</td>
            <td>${user.firstName}</td>
            <td>${user.email}</td>
            <td>${user.role}</td>
            ${config}
        </tr>
    `;
}
function OptionUI(role) {
    return `
        <option value="${role.value}">${role.name}</option>
    `;
}
function societeUI(role) {
    return `
        <option value="${role.id}">${role.name}</option>
    `;
}
