function guardar(){
    var producto = {
        IdProducto: $("#idProducto").val(),
        Nombre: $("#Nombre").val(),
        categoria: {
            IdCategoria: $("#idCategoria").val()
        },
        Stock: $("#stock").val(),
        Precio: $("#precio").val()
    };

    $.ajax({
        url: "/Producto/Guardar",
        type: "POST",
        data: JSON.stringify(producto),
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

function cambiarEstado(id, activo) {

    if (!confirm("¿Seguro de cambiar el estado?"))
        return;

    $.ajax({
        url: "/Producto/CambiarEstado",
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
    document.getElementById("idProducto").value = 0;
    document.getElementById("Nombre").value = "";
    document.getElementById("idCategoria").value = 0;
    document.getElementById("stock").value = 0;
    document.getElementById("precio").value = 0;

    document.getElementById("modalProducto").style.display = "block";
}

function cerrarModal() {
    document.getElementById("modalProducto").style.display = "none";
}

function editar(idProducto, Nombre, idCategoria, stock, precio) {
    document.getElementById("idProducto").value = idProducto;
    document.getElementById("Nombre").value = Nombre;
    document.getElementById("idCategoria").value = idCategoria;
    document.getElementById("stock").value = stock;
    document.getElementById("precio").value = precio;

    document.getElementById("modalProducto").style.display = "block";
}