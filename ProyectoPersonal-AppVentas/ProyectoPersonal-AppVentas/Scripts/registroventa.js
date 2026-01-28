var detalles = [];

function agregarProducto() {
    var idProducto = $("#idProducto").val();
    var nombre = $("#idProducto option:selected").text();
    var precio = parseFloat($("#idProducto option:selected").data("precio"));
    var cantidad = parseInt($("#cantidad").val());

    if (!idProducto || cantidad <= 0) {
        alert("Datos inválidos");
        return;
    }

    detalles.push({
        IdProducto: idProducto,
        Cantidad: cantidad
    });

    var subtotal = precio * cantidad;

    $("#tablaDetalle tbody").append(`
        <tr>
            <td>${nombre}</td>
            <td>${cantidad}</td>
            <td>${precio}</td>
            <td>${subtotal}</td>
            <td><button onclick="this.parentElement.parentElement.remove()">X</button></td>
        </tr>
    `);
}

function registrarVenta() {
    var venta = {
        IdCliente: $("#idCliente").val(),
        Detalles: detalles
    };

    $.ajax({
        url: '/Venta/RegistrarVenta',
        type: 'POST',
        data: JSON.stringify(venta),
        contentType: 'application/json',
        success: function (r) {
            if (r.resultado) {
                alert("Venta registrada correctamente");
                location.reload();
            } else {
                alert(r.mensaje);
            }
        }
    });
}