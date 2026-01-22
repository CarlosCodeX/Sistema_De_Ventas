function guardar() {
    var cliente = {
        IdCliente: $("#idCliente").val(),
        nombre: $("#Nombre").val(),
        documento: $("#documento").val(),
        telefono: $("#telefono").val(),
        email: $("#email").val()
    };

    $.ajax({
        url: "/Cliente/Guardar",
        type: "POST",
        data: JSON.stringify(cliente),
        contentType: "application/json; charset=utf-8",
        success: function (r) {
            if (r.resultado) {
                cerrarModal();
                location.reload();
            } else {
                alert(r.mensaje);
            }
        },
        error: function (err) {
            console.error(err);
        }
    });
}
    
function abrirModal() {
    document.getElementById("idCliente").value = 0;
    document.getElementById("Nombre").value = "";
    document.getElementById("documento").value = "";
    document.getElementById("telefono").value = "";
    document.getElementById("email").value = "";

    document.getElementById("modalCliente").style.display = "block";
}

function cerrarModal() {
    document.getElementById("modalCliente").style.display = "none";
}

function editar(id, nombre, documento, telefono, email) {
    document.getElementById("idCliente").value = id;
    document.getElementById("Nombre").value = nombre;
    document.getElementById("documento").value = documento;
    document.getElementById("telefono").value = telefono;
    document.getElementById("email").value = email;

    document.getElementById("modalCliente").style.display = "block";
}