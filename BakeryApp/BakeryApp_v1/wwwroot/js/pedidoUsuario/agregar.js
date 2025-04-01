

document.addEventListener("DOMContentLoaded", function () {
    ObtenerCarrito()
    ObtenerMetodosDePago()
    ObtenerTodosLosTiposDeEnvio();
    ObtenerTodosLasDireccionesUsuario();
});




function ObtenerTodosLosTiposDeEnvio() {
    fetch("/Pedido/ObtenerTodosLosTiposDeEnvio", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarSelectTiposEnvio(respuesta)
        AgregarEventListenerSelectTipoEnvio();
    }).catch(error => {
        console.error("Error", error);
    });
}


function LlenarSelectTiposEnvio(respuesta) {
    const select = document.getElementById("tipoEnvio");

    respuesta.arregloTiposDeEnvio.forEach(tipoEnvio => {
        const option = document.createElement("option");
        option.value = tipoEnvio.idTipoEnvio;
        option.textContent = tipoEnvio.nombreTipo;
        select.appendChild(option);
    });
}


function ObtenerTodosLasDireccionesUsuario() {
    fetch("/Pedido/ObtenerDireccionesUsuario", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarSelectDirecciones(respuesta)
    }).catch(error => {
        console.error("Error", error);
    });
}


function LlenarSelectDirecciones(respuesta) {
    const select = document.getElementById("direccion");

    respuesta.arregloDirecciones.forEach(direccion => {
        const option = document.createElement("option");
        option.value = direccion.idDireccion;
        option.textContent = direccion.nombreDireccion;
        select.appendChild(option);
    });
}

function BloquearSelectDireccion() {
    const selectEnvio = document.getElementById("tipoEnvio");
    const selectDireccion = document.getElementById("direccion")

    if (selectEnvio.selectedIndex === 1) {
        selectDireccion.disabled = true;
        selectDireccion.value = "";
    } else {
        selectDireccion.selectedIndex = 0;
        selectDireccion.disabled = false;
    }
}


function AgregarEventListenerSelectTipoEnvio() {
    const select = document.getElementById("tipoEnvio");
 

    select.addEventListener("change", () => {
        BloquearSelectDireccion()
        ActualizarTotalTipoEnvio()
    })
}

function ObtenerCarrito() {
    fetch("/Carrito/ObtenerCarritoUsuarioLogueado", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarTablaCarrito(respuesta)
    }).catch(error => {
        console.error("Error", error);
    });
}


function LlenarTablaCarrito(respuesta) {
    const tabla = document.getElementById("tablaCarrito")

    let sumadorTotal = 0;
    const trTotales = document.createElement("tr")
    const tdNombreTotal = document.createElement("td")
    const tdTotalFinal = document.createElement("td")
    tdTotalFinal.id = "totalFinal"
    respuesta.arregloCarrito.forEach(carrito => {
        const trProducto = document.createElement("tr")
        const tdNombreProducto = document.createElement("td")
        const tdCantidad = document.createElement("td")
        const tdTotal = document.createElement("td")



        tabla.appendChild(trProducto)
        trProducto.appendChild(tdNombreProducto)
        trProducto.appendChild(tdCantidad)
        trProducto.appendChild(tdTotal)
        tabla.appendChild(trTotales)
        trTotales.appendChild(tdNombreTotal)
        trTotales.appendChild(tdTotalFinal)

        tdNombreTotal.innerText = "Total de la orden"
        tdNombreProducto.innerText = carrito.productoDTO.nombreProducto;
        tdCantidad.innerText = carrito.cantidadProducto

        let total = carrito.cantidadProducto * carrito.productoDTO.precioProducto

        sumadorTotal += total;

        tdTotal.innerText = total + "₡"
    })


    tdTotalFinal.setAttribute("total", sumadorTotal)
    //Envio a domicilio costo 2500
    sumadorTotal += 2500;

    let iva = sumadorTotal * 0.14

    let totalConIva = sumadorTotal + iva;

    tdTotalFinal.innerText = totalConIva.toFixed(2) + "₡";
    
}


function ActualizarTotalTipoEnvio() {
    let totalFinal = document.getElementById("totalFinal")

    let iva = 0;

    const selectEnvio = document.getElementById("tipoEnvio");

    let totalConIva = 0;

    let totalAhora = parseFloat(totalFinal.getAttribute("total"));


    if (selectEnvio.selectedIndex === 1) {

        iva = totalAhora * 0.14

        totalConIva = totalAhora + iva;
      
    } else {
       
        totalAhora += 2500;
        iva = totalAhora * 0.14

        totalConIva = totalAhora + iva;
    }


    totalFinal.innerText = totalConIva.toFixed(2) + "₡"
}



function ObtenerMetodosDePago() {
    fetch("/Pedido/ObtenerTodosLosTiposDePago", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarFormMetodoDePago(respuesta)
        AgregarEventListenerRadio()
    }).catch(error => {
        console.error("Error", error);
    });
}


function LlenarFormMetodoDePago(respuesta) {
    const divPago = document.getElementById("divMetodosDePago");

    const h5TituloMetodos = document.createElement("h5")

    h5TituloMetodos.textContent = "Métodos de pago disponibles"
    divPago.appendChild(h5TituloMetodos)

    respuesta.arregloTiposDePago.forEach(metodo => {
    

        

       
        const divRadio = document.createElement("div");
        divRadio.className = "border p-3 mb-3";


        const h3Radio = document.createElement("h3");
        h3Radio.className = "h6 mb-0";

        const inputRadio = document.createElement("input");
        inputRadio.type = "radio";
        inputRadio.id = metodo.idTipoPago;
        inputRadio.name = "metodoDePago";
        inputRadio.value = metodo.idTipoPago;
        inputRadio.className = "form-check-input";


        const labelRadio = document.createElement("label");
        labelRadio.htmlFor = metodo.idTipoPago;
        labelRadio.className = "form-check-label";
        labelRadio.textContent = metodo.nombreTipo;;

        h3Radio.appendChild(inputRadio);
        h3Radio.appendChild(labelRadio);

        divRadio.appendChild(h3Radio);

        divPago.appendChild(divRadio);
    });

    const archivoInputDiv = document.createElement("div");
    archivoInputDiv.className = "form-group";


    const archivoLabel = document.createElement("label");
    archivoLabel.className = "form-label";
    archivoLabel.htmlFor = "archivoSinpe";
    archivoLabel.textContent = "Comprobante del SINPE Móvil";

    const archivoInput = document.createElement("input");
    archivoInput.type = "file";
    archivoInput.className = "form-control";
    archivoInput.id = "ArchivoSinpe";
    archivoInput.disabled = true;
    archivoInputDiv.appendChild(archivoLabel);
    archivoInputDiv.appendChild(archivoInput);

    divPago.appendChild(archivoInputDiv);


    const botonDiv = document.createElement("div");
    botonDiv.className = "form-group mt-3";

    const botonSubir = document.createElement("button");
    botonSubir.className = "btn btn-black btn-lg py-3 btn-block";
    botonSubir.type = "submit"
    botonSubir.textContent = "Pagar.";

    botonDiv.appendChild(botonSubir);

    divPago.appendChild(botonDiv);

    SeleccionarPagoTarjetaPorDefecto()
}


function SeleccionarPagoTarjetaPorDefecto() {
    const pagoTarjeta = document.getElementById("3")

    pagoTarjeta.checked = true;

}



//<div class="form-group">
//                                    <label class="form-label" for="archivoSinpe">Cargar Archivo</label>
//                                    <input type="file" class="form-control" id="archivoSinpe" />
//                                </div>


//                                <div class="form-group mt-3">
//                                    <a class="btn btn-black btn-lg py-3 btn-block" href="@Url.Action("Gracias", "UsuarioRegistrado")">Pagar.</a>
//                                </div >


//<tr>
//                                       <td>Cupcake<strong class="mx-2">x</strong> 1</td>
//                                       <td>₡2500.00</td>
//                                   </tr>
//                                   <tr>
//                                       <td>Cupcake <strong class="mx-2">x</strong>   1</td>
//                                       <td>₡2500.00</td>
//                                   </tr>
//                                   <tr>
//                                       <td class="text-black font-weight-bold"><strong>Subtotal del carrito</strong></td>
//                                       <td class="text-black">₡5000.00</td>
//                                   </tr>
//                                   <tr>
//                                       <td class="text-black font-weight-bold"><strong>Total de la orden</strong></td>
//                                       <td class="text-black font-weight-bold"><strong>₡6200.00</strong></td>
//                                   </tr>


function DesbloquearInputArchivoSinpe() {
    let valorMetodoPago = ObtenerRadioButtonPago();

    const archivoInput = document.getElementById("ArchivoSinpe")

    switch (valorMetodoPago) {
        case "1":
            archivoInput.disabled = true;
            break;
        case "2":
            archivoInput.disabled = false;
            break;
        case "3":
            archivoInput.disabled = true;
            break;
    }
}

function AgregarEventListenerRadio() {
    const radios = document.querySelectorAll('input[name="metodoDePago"]');


    radios.forEach(radio => {

        radio.addEventListener("change", () => {
            DesbloquearInputArchivoSinpe()
        });
    });
}



function ObtenerRadioButtonPago() {
    let valorMetodoPago = document.querySelector('input[name="metodoDePago"]:checked').value;

    return valorMetodoPago;
}



function GuardarPedido(event) {
    event.preventDefault();

    const stripe = Stripe("pk_test_51PgO5SRxHwpNRlo7XSH6j9oWbOmG61iQDS3shFUt5N0mgUCWXfqPuANS1Cumqa8PnGAobiSw2VXHvEBhdFerKs7400FLR2mFcp");

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;


    let metodoPago = ObtenerRadioButtonPago()


    const selectEnvio = document.getElementById("tipoEnvio");

    const selectDireccion = document.getElementById("direccion");

    let idDireccion = selectDireccion.value;

    const idTipoEnvio = selectEnvio.value;

    const archivoSinpe = document.getElementById("ArchivoSinpe").files[0]

    if (idDireccion == "") {
        idDireccion = null;
    }

    const pedido = new FormData();


    pedido.append("IdTipoEnvio", idTipoEnvio);
    pedido.append("IdTipoPago", metodoPago);
    pedido.append("IdDireccion", idDireccion);



 

    pedido.append("PagoSinpe.ArchivoSinpe", archivoSinpe)


    fetch("/Pedido/GuardarPedido", {
        method: "POST",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        },
        body: pedido
    }).then(respuesta => {
        return respuesta.json()
            .then(respuesta => {
                if (respuesta.stripeId) {
                    let stripeId = respuesta.stripeId
                    return stripe.redirectToCheckout({ sessionId: stripeId });
                }

                if (respuesta.correcto) {
                    window.location.href = respuesta.mensaje;
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
