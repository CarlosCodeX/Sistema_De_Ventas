$(document).ready(function () {

    $.get('/Venta/TotalVentasHoy', function (r) {
        if (r.resultado)
            $("#ventasHoy").text("S/. " + r.ventas);
    });

    $.get('/Venta/TotalVentasMes', function (r) {
        if (r.resultado)
            $("#ventasMes").text("S/. " + r.ventas);
    });

    $.get('/Producto/ProductosBajoStock', function (r) {
        if (r.resultado) {
            $("#totalBajoStock").text(r.data.length + " productos");

            r.data.forEach(p => {
                $("#tablaBajoStock").append(`
                    <tr>
                        <td>${p.Nombre}</td>
                        <td>${p.Stock}</td>
                    </tr>
                `);
            });
        }
    });

    $.get('/Producto/ProductoMasVendidoMes', function (r) {
        if (r.resultado && r.data) {
            $("#productoTop").text(r.data.Nombre);
            $("#detalleProductoTop").html(`
                <div style = "display: block;">
                    <p>Producto: ${r.data.Nombre}</p>
                    <p>Vendidos: ${r.data.TotalVendido}</p>
                </div>
            `);
        }
    });

});
