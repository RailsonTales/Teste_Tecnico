// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let inputCEP = document.getElementById("idCep");

inputCEP.onblur = function () {
    console.log("blur", "saiu do input CEP", this);

    if (inputCEP.value.length == 8) {
        $.ajax({
            url: '/Cliente/PesquisarCEP',
            type: 'post',
            data: {
                campoPesquisaCEP: inputCEP.value
            },
            success: function (data) {
                debugger
                if (data == null) {
                    alert("CEP inválido!");
                }
                else {
                    document.getElementById("Logradouro").value = data.logradouro;
                    document.getElementById("Complemento").value = data.complemento;
                    document.getElementById("Bairro").value = data.bairro;
                    document.getElementById("Localidade").value = data.localidade;
                    document.getElementById("UF").value = data.uf;
                }
            },
            error: function (request, status, error) {
                if (status == 'error')
                    alert("CEP inválido!");
            }
        });
    }
    else {
        alert("CEP inválido!");
    }
}

let inputEmail = document.getElementById("Email");

inputEmail.onblur = function ()
{
    debugger
    console.log("blur", "saiu do input Email", this);

    var email = document.getElementById("Email").value;

    if (!validarEmail(email))
    {
        alert("E-mail inválido");
    }
}

function validarEmail(email) {
    return /^[\w+.]+@\w+\.\w{2,}(?:\.\w{2})?$/.test(email)
}