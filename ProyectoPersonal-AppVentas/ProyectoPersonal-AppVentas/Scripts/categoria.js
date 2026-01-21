function guardar() {
    var categoria = {
        IdCategoria: $("#idCategoria").val(),
        Nombre: $("#nombre").val(),
        Descripcion: $("#descripcion").val()
    };

    $.ajax({
        url: '/Categoria/Guardar',
        type: 'POST',
        data: JSON.stringify(categoria),
        contentType: 'application/json; charset=utf-8',
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


function cambiarEstado(id, activo) {

    if (!confirm("¿Seguro de cambiar el estado?"))
        return;

    $.ajax({
        url: '/Categoria/CambiarEstado',
        type: 'POST',
        data: { id: id, activo: activo },
        success: function (r) {
            if (r.resultado) {
                location.reload();
            } else {
                alert(r.mensaje);
            }
        },
        error: function () {
            alert("Error en el servidor");
        }
    });
}


function abrirModal() {
    document.getElementById("idCategoria").value = 0;
    document.getElementById("nombre").value = "";
    document.getElementById("descripcion").value = "";

    document.getElementById("modalCategoria").style.display = "block";
}

function cerrarModal() {
    document.getElementById("modalCategoria").style.display = "none";
}

function editar(id, nombre, descripcion) {

    document.getElementById("idCategoria").value = id;
    document.getElementById("nombre").value = nombre;
    document.getElementById("descripcion").value = descripcion;

    document.getElementById("modalCategoria").style.display = "block";
}

function cambiarEstado(id, activo) {

    if (!confirm("¿Seguro de cambiar el estado?"))
        return;

    $.ajax({
        url: '/Categoria/CambiarEstado',
        type: 'POST',
        data: {
            id: id,
            activo: activo
        },
        success: function (r) {

            if (r.resultado) {
                location.reload();
            } else {
                alert(r.mensaje);
            }
        },
        error: function () {
            alert("Error al cambiar estado");
        }
    });
}

