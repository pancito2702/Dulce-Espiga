
document.addEventListener("DOMContentLoaded", function () {

    CargarAlInicio();
});

function CargarAlInicio() {
    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    const idFactura = parametrosUrl.get("idFactura");
    ObtenerNotaCreditoPorFactura(idFactura)

}



function ObtenerNotaCreditoPorFactura(idFactura) {

    fetch("/FacturaEmpleado/ObtenerNotaCreditoPorIdFactura/" + idFactura, {
        method: "GET",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        CrearNotaCredito(respuesta.notaCredito)
    }).catch(error => {

        console.error("Error", error);
    });
}



function CrearNotaCredito(notaCredito) {
    const divAgregar = document.getElementById("agregar")




    const row1 = document.createElement("div");
    row1.className = "row";

    const col1_1 = document.createElement("div");
    col1_1.className = "col-12";

    const h4 = document.createElement("h4");
    h4.textContent = "Dulce Espiga Bakery.";

    const small = document.createElement("small");
    small.className = "float-right";
    small.textContent = "Factura generada el: " + notaCredito.factura.fechaFactura;

    h4.appendChild(small);
    col1_1.appendChild(h4);
    row1.appendChild(col1_1);
    divAgregar.appendChild(row1);


    const row2 = document.createElement("div");
    row2.className = "row invoice-info";


    const col2_1 = document.createElement("div");
    col2_1.className = "col-sm-4 invoice-col";

    const strongDe = document.createElement("strong");
    strongDe.textContent = "Dulce Espiga Bakery.";

    const addressDe = document.createElement("address");
    addressDe.appendChild(strongDe);
    addressDe.appendChild(document.createElement("br"));
    addressDe.appendChild(document.createTextNode("Heredia, Costa Rica"));
    addressDe.appendChild(document.createElement("br"));
    addressDe.appendChild(document.createTextNode("Teléfono: +506 8622-8993"));
    addressDe.appendChild(document.createElement("br"));
    addressDe.appendChild(document.createTextNode("Correo: dulceespiga@gmail.com"));

    col2_1.appendChild(document.createTextNode("De"));
    col2_1.appendChild(addressDe);


    const col2_2 = document.createElement("div");
    col2_2.className = "col-sm-4 invoice-col";

    const addressPara = document.createElement("address");
    addressPara.appendChild(document.createTextNode("Correo: " + notaCredito.factura.pedido.persona.correo));

    col2_2.appendChild(document.createTextNode("Para"));
    col2_2.appendChild(addressPara);


    const col2_3 = document.createElement("div");
    col2_3.className = "col-sm-4 invoice-col";

    const bFactura = document.createElement("b");
    bFactura.textContent = "Número de factura: " + notaCredito.factura.idFactura;

    const br1 = document.createElement("br");
    const br2 = document.createElement("br");

    const bOrden = document.createElement("b");
    bOrden.textContent = "Orden relacionada: ";

    const textoOrden = document.createTextNode(notaCredito.factura.pedido.idPedido);

    const br3 = document.createElement("br");

    const bUsuario = document.createElement("b");
    bUsuario.textContent = "Nombre Usuario: ";

    const textoUsuario = document.createTextNode(notaCredito.factura.pedido.persona.nombre);

    col2_3.appendChild(bFactura);
    col2_3.appendChild(br1);
    col2_3.appendChild(br2);
    col2_3.appendChild(bOrden);
    col2_3.appendChild(textoOrden);
    col2_3.appendChild(br3);
    col2_3.appendChild(bUsuario);
    col2_3.appendChild(textoUsuario);


    row2.appendChild(col2_1);
    row2.appendChild(col2_2);
    row2.appendChild(col2_3);


    divAgregar.appendChild(row2);


    const row3 = document.createElement("div");
    row3.className = "row";

    const col3_1 = document.createElement("div");
    col3_1.className = "col-12 table-responsive";

    const table = document.createElement("table");
    table.className = "table table-striped";

    const thead = document.createElement("thead");
    const trHead = document.createElement("tr");

    const thCantidad = document.createElement("th");
    thCantidad.textContent = "Cantidad Comprada";

    const thProducto = document.createElement("th");
    thProducto.textContent = "Producto";

    const thPrecioUnitario = document.createElement("th");
    thPrecioUnitario.textContent = "Precio Unitario";

    const thTotalLinea = document.createElement("th")
    thTotalLinea.textContent = "Total Linea"

    trHead.appendChild(thCantidad);
    trHead.appendChild(thProducto);
    trHead.appendChild(thPrecioUnitario);
    trHead.appendChild(thTotalLinea);
    thead.appendChild(trHead);

    const tbody = document.createElement("tbody");



    notaCredito.factura.detalleFactura.forEach(detalle => {
        const trBody = document.createElement("tr");

        const tdCantidad = document.createElement("td");
        tdCantidad.textContent = detalle.productoPedido.cantidadProducto

        const tdProducto = document.createElement("td");
        tdProducto.textContent = detalle.productoPedido.producto.nombreProducto;

        const tdIdProducto = document.createElement("td");
        tdIdProducto.textContent = detalle.productoPedido.producto.precioProducto + "₡";

        const tdSubtotal = document.createElement("td");
        tdSubtotal.textContent = detalle.totalLinea + "₡";


        trBody.appendChild(tdCantidad);
        trBody.appendChild(tdProducto);
        trBody.appendChild(tdIdProducto);
        trBody.appendChild(tdSubtotal);

        tbody.appendChild(trBody);
    })



    table.appendChild(thead);
    table.appendChild(tbody);

    col3_1.appendChild(table);
    row3.appendChild(col3_1);
    divAgregar.appendChild(row3);





    const col4_2 = document.createElement("div");
    col4_2.className = "col-6";

    const pTotal = document.createElement("p");
    pTotal.className = "lead";
    pTotal.textContent = "Total devuelto: ";

    const tableTotal = document.createElement("table");
    tableTotal.className = "table";

    const trSubtotal = document.createElement("tr");
    const thSubtotal = document.createElement("th");
    thSubtotal.textContent = "Subtotal: "
    const tdSubtotal = document.createElement("td");
    



    let iva = notaCredito.factura.iva 

    const trIva = document.createElement("tr");
    const thIva = document.createElement("th");
    thIva.textContent = "IVA";
    const tdIva = document.createElement("td");
    tdIva.textContent = iva.toFixed(2) + "₡";
    trIva.appendChild(thIva);
    trIva.appendChild(tdIva);

    const trEnvio = document.createElement("tr");
    const thEnvio = document.createElement("th");
    const tdEnvio = document.createElement("td");


    thEnvio.textContent = "Envio:";


    if (notaCredito.factura.envio == null) {
        tdEnvio.textContent = 0 + "₡"
    } else {
        tdEnvio.textContent = notaCredito.factura.envio + "₡"
    }

    
    trEnvio.appendChild(thEnvio);
    trEnvio.appendChild(tdEnvio);




    const trTotal = document.createElement("tr");
    const thTotal = document.createElement("th");
    thTotal.textContent = "Total:";
    const tdTotal = document.createElement("td");
    tdTotal.textContent = notaCredito.factura.totalPagar + "₡";
    trTotal.appendChild(thTotal);
    trTotal.appendChild(tdTotal);

    tableTotal.appendChild(trSubtotal);
    tableTotal.appendChild(trIva);
    tableTotal.appendChild(trEnvio);
    tableTotal.appendChild(trTotal);

    col4_2.appendChild(pTotal);
    col4_2.appendChild(tableTotal);


    const row4 = document.createElement("div");
    row4.className = "row"; 

    row4.appendChild(col4_2); 
    divAgregar.appendChild(row4);  


}



//<strong> Usuario</strong>

//<p class="text-muted">
//    prueba
//</p>

//<hr>

//<strong> Número de Pedido</strong>

//<p class="text-muted">
//    1
//</p>

//<hr>

//<strong> Productos</strong>

//<table class="table ">
//    <tr>
//        <th>Producto</th>
//        <th>Cantidad</th>
//        <th>Precio unitario</th>
//    </tr>
//    <tr>
//        <td>Cupcake</td>
//        <td>1</td>
//        <td>₡2500</td>
//    </tr>
//    <tr>
//        <td>Cupcake</td>
//        <td>1</td>
//        <td>₡2500</td>
//    </tr>

//</table>

//<hr>

//<strong> Metodo de pago</strong>

//<p class="text-muted">
//    Efectivo
//</p>

//<hr>

//<strong> Tipo de entrega</strong>
//<p class="text-muted">Entrega por Motorizado</p>

//<hr>
//<strong> Direccion de entrega</strong>

//<p class="text-muted">Heredia, Heredia, Mercedes, Por la iglesia</p>



//<hr>
