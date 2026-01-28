$(document).ready(function () {
    listarVentasHoy();
    listarVentasMes();
    listarTodas();
});

function cambiarTab(tipo) {
    $('#seccionHoy, #seccionMes, #seccionTodas').hide();
    $('.tab-btn').removeClass('active');

    if (tipo === 'hoy') {
        $('#seccionHoy').show();
        $('#tabHoy').addClass('active');
    }
    if (tipo === 'mes') {
        $('#seccionMes').show();
        $('#tabMes').addClass('active');
    }
    if (tipo === 'todas') {
        $('#seccionTodas').show();
        $('#tabTodas').addClass('active');
    }
}

function listarVentasHoy() {
    $.get('/Venta/ListarVentasHoy', function (data) {
        llenarTabla('#tablaVentasHoy tbody', data);
    });
}

function listarVentasMes() {
    $.get('/Venta/ListarVentasMes', function (data) {
        llenarTabla('#tablaVentasMes tbody', data);
    });
}

function listarTodas() {
    $.get('/Venta/ListarVentasAdmin', function (data) {
        llenarTabla('#tablaVentasAdmin tbody', data);
    });
}

function llenarTabla(selector, data) {
    let html = '';

    data.forEach(v => {
        html += `
            <tr>
                <td>${v.IdVenta}</td>
                <td>${v.cliente.Nombre}</td>
                <td>${formatearFecha(v.FechaVenta)}</td>
                <td>S/. ${v.Total.toFixed(2)}</td>
                <td>
                    <button onclick="verDetalle(
                        ${v.IdVenta},
                        '${v.cliente.Nombre}',
                        '${formatearFecha(v.FechaVenta)}',
                        ${v.Total}
                    )">👁</button>
                </td>
            </tr>`;
    });

    $(selector).html(html);
}

function verDetalle(id, cliente, fecha, total) {
    $('#detalleId').text(id);
    $('#detalleCliente').text(cliente);
    $('#detalleFecha').text(fecha);
    $('#detalleTotal').text('S/. ' + total.toFixed(2));
    $('#modalDetalle').show();
}

function cerrarModal() {
    $('#modalDetalle').hide();
}

function formatearFecha(valor) {
    const timestamp = parseInt(valor.replace(/\D/g, ''));
    const d = new Date(timestamp);
    return d.toLocaleString();
}
