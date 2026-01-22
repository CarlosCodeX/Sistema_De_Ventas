function registrarUsuario() {

    var usuario = {
        IdUsuario: $("#idUsuario").val(),
        NombreUsuario: $("#nombre").val(),
        Clave: $("#clave").val(),
        rol: {
            IdRol: $("#idRol").val(),
        }
    };

    $.ajax({
        url: '/Usuario/Guardar',
        type: 'POST',
        data: usuario,
        success: function (r) {
            if (r.resultado) {
                alert("Usuario registrado correctamente");
            } else {
                alert(r.mensaje);
            }
        },
        error: function () {
            alert("Error al registrar usuario");
        }
    });
}
