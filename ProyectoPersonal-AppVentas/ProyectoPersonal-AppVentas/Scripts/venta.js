$(document).ready(function () {
    listarVentasHoy();
    listarVentasMes();
    listarVentasAdmin();
});

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

function listarVentasHoy() {

    $.ajax({
        url: '/Venta/ListarVentasHoy',
        type: 'GET',
        success: function (ventas) {

            console.log(ventas); 

            let tbody = $("#tablaVentasHoy tbody");
            tbody.empty();

            ventas.forEach(v => {

                let fila = `
                    <tr>
                        <td>${v.IdVenta}</td>
                        <td>${v.cliente.Nombre}</td>
                        <td>${formatearFecha(v.FechaVenta)}</td>
                        <td>S/ ${v.Total.toFixed(2)}</td>
                    </tr>
                `;

                tbody.append(fila);
            });
        },
        error: function () {
            alert("Error al listar ventas de hoy");
        }
    });
}


function listarVentasMes() {

    $.ajax({
        url: '/Venta/ListarVentasMes',
        type: 'GET',
        success: function (ventas) {

            console.log(ventas);

            let tbody = $("#tablaVentasMes tbody");
            tbody.empty();

            ventas.forEach(v => {

                let fila = `
                    <tr>
                        <td>${v.IdVenta}</td>
                        <td>${v.cliente.Nombre}</td>
                        <td>${formatearFecha(v.FechaVenta)}</td>
                        <td>S/ ${v.Total.toFixed(2)}</td>
                    </tr>
                `;

                tbody.append(fila);
            });
        },
        error: function () {
            alert("Error al listar ventas de hoy");
        }
    });
}

function listarVentasAdmin() {

    $.ajax({
        url: '/Venta/ListarVentasAdmin',
        type: 'GET',
        success: function (ventas) {

            console.log(ventas);

            let tbody = $("#tablaVentasAdmin tbody");
            tbody.empty();

            ventas.forEach(v => {

                let fila = `
                    <tr>
                        <td>${v.IdVenta}</td>
                        <td>${v.cliente.Nombre}</td>
                        <td>${formatearFecha(v.FechaVenta)}</td>
                        <td>S/ ${v.Total.toFixed(2)}</td>
                    </tr>
                `;

                tbody.append(fila);
            });
        },
        error: function () {
            alert("Error al listar ventas de hoy");
        }
    });
}

function formatearFecha(fechaNet) {
    if (!fechaNet) return "";

    var timestamp = parseInt(fechaNet.replace("/Date(", "").replace(")/", ""));
    var fecha = new Date(timestamp);

    return fecha.toLocaleDateString("es-PE");
}

