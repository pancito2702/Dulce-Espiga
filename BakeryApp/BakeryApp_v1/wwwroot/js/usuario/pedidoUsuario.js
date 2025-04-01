
function ObtenerTodosLosPedidosPorCliente() {
    fetch("/Pedido/ObtenerPedidosUsuarioLogueado", {
        method: "GET",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        CrearFilasTabla(respuesta)
    }).catch(error => {

        console.error("Error", error);
    });
}


function CancelarPedido(event) {
    event.preventDefault()
    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
    const botonCancelar = event.currentTarget;
    const idPedido = botonCancelar.getAttribute("idPedido");



    const pedido = {
        IdPedido: idPedido,
    }


    fetch("/Pedido/CancelarPedido", {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        },
        body: JSON.stringify(pedido)
    }).then(respuesta => {
        return respuesta.json()
            .then(respuesta => {
                if (respuesta.correcto) {
                    swal({
                        text: respuesta.mensaje,
                        icon: "success"
                    });
                    ObtenerTodosLosPedidosPorCliente()
                } else {
                    swal({
                        text: respuesta.mensaje,
                        icon: "error"
                    });
                }

            })
    }).catch(error => {
        console.error("Error", error);
    });
}


function CrearFilasTabla(respuesta) {
    var contador = 1;
    const bodyTabla = document.getElementById("agregar");
    bodyTabla.innerHTML = "";

    if (respuesta.arregloPedidos.length == 0) {
        bodyTabla.innerText = "El usuario no tiene pedidos realizados";
    }

    respuesta.arregloPedidos.forEach(pedido => {
        const fila = document.createElement("tr");

        const tdId = document.createElement("td");
        const tdFecha = document.createElement("td");
        const tdEstado = document.createElement("td");
        const tdVer = document.createElement("td");
        const tdCancelarEstado = document.createElement("td");
        const tdModificarSinpe = document.createElement("td");
        const tdVerFactura = document.createElement("td");

        const aVer = document.createElement("a");
        const formCancelar = document.createElement("form");
        const botonCancelar = document.createElement("button");
        const aModificarSinpe = document.createElement("a");
        const aVerFactura = document.createElement("a");


        tdId.classList.add("text-center", "align-middle");
        tdFecha.classList.add("text-center", "align-middle");
        tdEstado.classList.add("text-center", "align-middle");
        tdVer.classList.add("text-center", "align-middle");
        tdCancelarEstado.classList.add("text-center", "align-middle");
        tdModificarSinpe.classList.add("text-center", "align-middle");

        aVer.classList.add("btn", "btn-primary", "btn-sm", "text-white");
        botonCancelar.classList.add("btn", "btn-danger", "btn-sm", "text-white");
        aModificarSinpe.classList.add("btn", "btn-danger", "btn-sm", "text-white");
        aVerFactura.classList.add("btn", "btn-danger", "btn-sm", "text-white");

        botonCancelar.innerText = "Cancelar Pedido";
        tdId.innerText = "Pedido: " + contador;
        tdFecha.innerText = pedido.fechaPedido;
        tdEstado.innerText = pedido.estadoPedido.nombreEstado;
        aVer.innerText = "Ver Pedido";
        aVerFactura.innerText = "Ver Factura del pedido"

        tdVerFactura.appendChild(aVerFactura)
        tdVer.appendChild(aVer);
        formCancelar.appendChild(botonCancelar);
        tdCancelarEstado.appendChild(formCancelar);

        fila.appendChild(tdId);
        fila.appendChild(tdFecha);
        fila.appendChild(tdEstado);
        fila.appendChild(tdVer);
        fila.appendChild(tdCancelarEstado);
        fila.appendChild(tdModificarSinpe);
        fila.appendChild(tdVerFactura);

        if (pedido.tipoPago.idTipoPago == 2) {
            aModificarSinpe.innerText = "Modificar Imagen del Sinpe";
            tdModificarSinpe.appendChild(aModificarSinpe);
        }


        aVerFactura.setAttribute("idPedido", pedido.idPedido);
        aVerFactura.addEventListener("click", (event) => {
            VerPaginaFactura(event);
        });

        bodyTabla.appendChild(fila);

        contador++;

        botonCancelar.setAttribute("idPedido", pedido.idPedido);
        botonCancelar.addEventListener("click", (event) => {
            CancelarPedido(event);
        });


        aModificarSinpe.setAttribute("idPedido", pedido.idPedido);
        aModificarSinpe.addEventListener("click", (event) => {
            VerPaginaModificarSinpe(event);
        });

        aVer.setAttribute("idPedido", pedido.idPedido);
        aVer.addEventListener("click", (event) => {
            VerPaginaPedido(event);
        });
    });
}





function VerPaginaFactura(event) {
    const idPedido = event.currentTarget.getAttribute("idPedido");



    var urlVer = "/Pedido/VerFactura?idPedido=" + idPedido;


    window.location.replace(urlVer);
}



function VerPaginaPedido(event) {
    const idPedido = event.currentTarget.getAttribute("idPedido");



    var urlVer = "/Pedido/VerPedido?idPedido=" + idPedido;


    window.location.replace(urlVer);
}


function VerPaginaModificarSinpe(event) {
    const idPedido = event.currentTarget.getAttribute("idPedido");



    var urlVer = "/Pedido/ModificarSinpe?idPedido=" + idPedido;


    window.location.replace(urlVer);
}

export { ObtenerTodosLosPedidosPorCliente, CancelarPedido }