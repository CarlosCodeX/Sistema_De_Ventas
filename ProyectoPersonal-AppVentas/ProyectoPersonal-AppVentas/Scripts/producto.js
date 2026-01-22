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