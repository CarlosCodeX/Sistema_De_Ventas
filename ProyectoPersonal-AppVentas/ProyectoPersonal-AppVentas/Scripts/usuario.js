function registrarUsuario() {
    var usuario = {
        IdUsuario: $("#idUsuario").val(),
        NombreUsuario: $("#nombre").val(),
        Clave: $("#clave").length ? $("#clave").val() : "",
        rol: {
            IdRol: $("#idRol").val()
        }
    };

    if (usuario.NombreUsuario.trim() === "") {
        alert("Ingrese el nombre de usuario");
        return;
    }

    // 🔴 SOLO VALIDAR CLAVE SI ES NUEVO
    if (usuario.IdUsuario == 0) {
        if (usuario.Clave.trim() === "") {
            alert("Ingrese la contraseña");
            return;
        }
        if (usuario.Clave.length < 6) {
            alert("La contraseña debe tener al menos 6 caracteres");
            return;
        }
    }

    if (usuario.rol.IdRol === "" || usuario.rol.IdRol === "0") {
        alert("Seleccione un rol");
        return;
    }

    $.ajax({
        url: '/Usuario/Guardar',
        type: 'POST',
        data: usuario,
        success: function (r) {
            if (r.resultado) {
                alert("Usuario guardado correctamente");
                cerrarModal();
                location.reload();
            } else {
                alert(r.mensaje || "Error al guardar usuario");
            }
        }
    });
}


function abrirModal() {
        $("#idUsuario").val(0);
        $("#nombre").val("");
        $("#clave").val("").prop("required", true);
        $("#idRol").val("").change();

        $("#grupoClave").show();
        $("#tituloModal").text("Registrar Usuario");

        $("#modalUsuario").show();
}

function cerrarModal() {
    document.getElementById("modalUsuario").style.display = "none";
}

function cambiarEstado(id, activo) {
    var nuevoEstado = !activo; 
    var mensaje = nuevoEstado ? "activar" : "desactivar";

    if (!confirm("¿Seguro de " + mensaje + " este usuario?")) {
        return;
    }

    console.log("ID:", id, "Estado actual:", activo, "Nuevo estado:", nuevoEstado);

    $.ajax({
        url: '/Usuario/CambiarEstado',
        type: 'POST',
        data: {
            id: id,
            activo: nuevoEstado
        },
        success: function (r) {
            console.log("Respuesta del servidor:", r);
            if (r.resultado) {
                alert("Estado cambiado correctamente");
                location.reload();
            } else {
                alert(r.mensaje || "Error al cambiar estado");
            }
        },
        error: function (xhr, status, error) {
            console.error("Error:", error);
            alert("Error en el servidor: " + error);
        }
    });
}
function editar(idUsuario, nombre, idRol) {
    $("#idUsuario").val(idUsuario);
    $("#nombre").val(nombre);
    $("#clave").val("").prop("required", false); // NO usar clave al editar
    $("#idRol").val(String(idRol)).change();

    $("#grupoClave").hide(); // 🔥 clave fuera al editar
    $("#tituloModal").text("Editar Usuario");

    $("#modalUsuario").show();
}

