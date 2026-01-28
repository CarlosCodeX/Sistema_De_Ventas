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
            <td>S/. ${precio.toFixed(2)}</td>
            <td>S/. ${subtotal.toFixed(2)}</td>
            <td><button class="btn-eliminar-tabla" onclick="eliminarFila(this)">❌</button></td>
        </tr>
    `);

    $("#idProducto").val('');
    $("#cantidad").val('');

    actualizarTotal();
}

function eliminarFila(btn) {
    var index = $(btn).closest('tr').index();
    detalles.splice(index, 1); 
    $(btn).closest('tr').remove(); 
    actualizarTotal(); 
}

function actualizarTotal() {
    var total = 0;

    $('#tablaDetalle tbody tr').each(function () {
        var subtotalText = $(this).find('td:eq(3)').text().replace('S/.', '').trim();
        var subtotal = parseFloat(subtotalText);
        if (!isNaN(subtotal)) {
            total += subtotal;
        }
    });

    $('#totalVenta').text('S/. ' + total.toFixed(2));
}

function registrarVenta() {
    var idCliente = $("#idCliente").val();

    if (!idCliente || idCliente === "") {
        alert("Seleccione un cliente");
        return;
    }

    if (detalles.length === 0) {
        alert("Agregue al menos un producto");
        return;
    }

    var venta = {
        IdCliente: idCliente,
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
        },
        error: function () {
            alert("Error al registrar la venta");
        }
    });
}

$(document).ready(function () {
    actualizarTotal();
});