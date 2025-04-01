
document.addEventListener("DOMContentLoaded", function () {

    CargarAlInicio();
});

function CargarAlInicio() {
    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    const idPedido = parametrosUrl.get("idPedido");
    ObtenerPedidoPorId(idPedido)

}



function ObtenerPedidoPorId(idPedido) {
    fetch("/PedidoEmpleado/ObtenerPedidoDTOPorId/" + idPedido, {
        method: "GET",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        CrearPedido(respuesta.pedido)
    }).catch(error => {

        console.error("Error", error);
    });
}



function CrearPedido(pedido) {
    const divAgregar = document.getElementById("agregar")
    const strongUsuario = document.createElement("strong")
    const pUsuario = document.createElement("p")
    const hr0 = document.createElement("hr")
    const strongCorreo = document.createElement("strong")
    const pCorreo = document.createElement("p")
    const hr1 = document.createElement("hr")
    const strongPedido = document.createElement("strong")
    const pNumeroPedido = document.createElement("p")
    const hr2 = document.createElement("hr")
    const strongProductos = document.createElement("strong")
    const tablaProductosPedido = document.createElement("table")
    const trInicioTabla = document.createElement("tr")
    const thNombre = document.createElement("th")
    const thCantidad = document.createElement("th")
    const thPrecioUnitario = document.createElement("th")
    const hr3 = document.createElement("hr")
    const strongMetodoPago = document.createElement("strong")
    const pMetodoDePago = document.createElement("p")
    const hr4 = document.createElement("hr");
    const strongTipoEntrega = document.createElement("strong")
    const pTipoEntrega = document.createElement("p")
    const hr5 = document.createElement("hr");
    const strongDireccionEntrega = document.createElement("strong")
    const pDireccionEntrega = document.createElement("p")
    const hr6 = document.createElement("hr");
    const strongIndicacionesAdicionales = document.createElement("strong")
    const pIndicacionesAdicionales = document.createElement("p")
    const hr7 = document.createElement("hr")
    const strongImagenSinpe = document.createElement("strong")
    const saltoLinea = document.createElement("br")
    const imgSinpe = document.createElement("img")


    pUsuario.classList.add("text-muted")
    pNumeroPedido.classList.add("text-muted")
    tablaProductosPedido.classList.add("table")
    imgSinpe.classList.add("img-fluid")

    const textUsuario = document.createTextNode("Nombre")

    const textPedido = document.createTextNode("Pedido")

    const textProducto = document.createTextNode("Productos")

    const textCorreo = document.createTextNode("Correo")

    const textMetodoDePago = document.createTextNode("Metodo de pago")

    const textTipoEntrega = document.createTextNode("Tipo de entrega")

    const textUbicacion = document.createTextNode("Dirección de entrega")

    const textIndicacionesAdicionales = document.createTextNode("Ubicacion exacta de la entrega")

    const textImagenSinpe = document.createTextNode("Imagen Sinpe")

    divAgregar.appendChild(strongCorreo)
    strongCorreo.appendChild(textCorreo)
    divAgregar.appendChild(pCorreo)
    divAgregar.appendChild(hr0)
    divAgregar.appendChild(strongUsuario)
    strongUsuario.appendChild(textUsuario)
    divAgregar.appendChild(pUsuario)
    divAgregar.appendChild(hr1)
    divAgregar.appendChild(strongPedido)
    strongPedido.appendChild(textPedido)
    divAgregar.appendChild(pNumeroPedido)
    divAgregar.appendChild(hr2)
    divAgregar.appendChild(strongProductos)
    strongProductos.appendChild(textProducto)
    divAgregar.appendChild(tablaProductosPedido)
    tablaProductosPedido.appendChild(trInicioTabla)
    trInicioTabla.appendChild(thNombre)
    trInicioTabla.appendChild(thCantidad)
    trInicioTabla.appendChild(thPrecioUnitario)
    divAgregar.appendChild(hr3)
    divAgregar.appendChild(strongMetodoPago)
    strongMetodoPago.appendChild(textMetodoDePago)

    divAgregar.appendChild(pMetodoDePago)
    divAgregar.appendChild(hr4)
    divAgregar.appendChild(strongTipoEntrega)
    strongTipoEntrega.appendChild(textTipoEntrega)
    divAgregar.appendChild(pTipoEntrega)



    if (pedido.tipoEnvio.idTipoEnvio == 1) {

        divAgregar.appendChild(hr5)
        divAgregar.appendChild(strongDireccionEntrega)
        strongDireccionEntrega.appendChild(textUbicacion)
        divAgregar.appendChild(pDireccionEntrega)
        pDireccionEntrega.innerText = pedido.direccion.provinciaDTO.nombreProvincia + ", " + pedido.direccion.cantonDTO.nombreCanton + ", " + pedido.direccion.distritoDTO.nombreDistrito

        divAgregar.appendChild(hr6)
        divAgregar.appendChild(strongIndicacionesAdicionales)
        strongIndicacionesAdicionales.appendChild(textIndicacionesAdicionales)
        divAgregar.appendChild(pIndicacionesAdicionales)
        pIndicacionesAdicionales.innerText = pedido.direccion.direccionExacta
    }

    if (pedido.tipoPago.idTipoPago == 2) {

        divAgregar.appendChild(hr7)
        divAgregar.appendChild(strongImagenSinpe)
        strongImagenSinpe.appendChild(textImagenSinpe)
        divAgregar.appendChild(saltoLinea)
        divAgregar.appendChild(imgSinpe)
        imgSinpe.src = "/" + pedido.pagoSinpe.rutaImagenSinpe
    }


    thNombre.innerText = "Nombre Producto"
    thCantidad.innerText = "Cantidad"
    thPrecioUnitario.innerText = "Precio Unitario"



    pCorreo.innerText = pedido.persona.correo
    pUsuario.innerText = pedido.persona.nombre
    pMetodoDePago.innerText = pedido.tipoPago.nombreTipo
    pTipoEntrega.innerText = pedido.tipoEnvio.nombreTipo


    pNumeroPedido.innerText = pedido.idPedido



    pedido.productosPedido.forEach(productoPedido => {
        const trProducto = document.createElement("tr")
        const tdNombreProducto = document.createElement("td")
        const tdCantidadProducto = document.createElement("td")
        const tdPrecioProducto = document.createElement("td")

        trProducto.appendChild(tdNombreProducto)
        trProducto.appendChild(tdCantidadProducto)
        trProducto.appendChild(tdPrecioProducto)
        tdNombreProducto.innerText = productoPedido.producto.nombreProducto
        tdCantidadProducto.innerText = productoPedido.cantidadProducto
        tdPrecioProducto.innerText = productoPedido.producto.precioProducto + "₡"

        tablaProductosPedido.appendChild(trProducto)
    })
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
