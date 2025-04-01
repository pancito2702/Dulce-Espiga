document.addEventListener("DOMContentLoaded", function () {
    CargarAlInicio();
});

function CargarAlInicio() {
    ObtenerTodasLasPaginas();

    const url = window.location.search;
    const parametrosUrl = new URLSearchParams(url);
    const pagina = parametrosUrl.get("pagina");

    if (pagina === null) {
        ObtenerTodasLasFacturas(1);
    } else {
        ObtenerTodasLasFacturas(pagina);
    }
}

function ObtenerTodasLasFacturas(pagina) {
    fetch("/FacturaEmpleado/ObtenerTodasLasFacturas/" + pagina, {
        method: "GET",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json();
    }).then(respuesta => {
        CrearFilasTabla(respuesta);
    }).catch(error => {
        console.error("Error", error);
    });
}

function CrearFilasTabla(respuesta) {
    var contador = 1;
    const bodyTabla = document.getElementById("agregar");
    bodyTabla.innerHTML = "";

    respuesta.arregloFacturas.forEach(factura => {
        const nuevaFila = document.createElement("tr");

        const celda1 = document.createElement("td");
        celda1.textContent = contador;

        const celda2 = document.createElement("td");
        celda2.textContent = factura.pedido.persona.correo;

        const celda3 = document.createElement("td");
        celda3.textContent = factura.pedido.tipoPago.nombreTipo;

        const celda4 = document.createElement("td");
        celda4.textContent = factura.idPedido;

        const celda5 = document.createElement("td");
        celda5.textContent = factura.totalPagar + "₡";

        const celdaEstado = document.createElement("td");
        celdaEstado.textContent = factura.pedido.estadoPedido.nombreEstado;

        const celda6 = document.createElement("td");
        const divCentro = document.createElement("div");
        divCentro.className = "d-flex justify-content-center gap-2";

        const botonVer = document.createElement("a");
        botonVer.setAttribute("idFactura", factura.idFactura);
        botonVer.addEventListener("click", (event) => {
            VerPaginaFactura(event);
        });

        nuevaFila.id = factura.idFactura;

        botonVer.className = "btn btn-sm btn-primary text-white";
        botonVer.textContent = "Ver";

        const formBorrar = document.createElement("form");
        formBorrar.addEventListener("submit", (event) => {
            EliminarFactura(event);
        });

        const botonEliminar = document.createElement("button");
        botonEliminar.type = "submit";
        botonEliminar.setAttribute("idFactura", factura.idFactura);
        botonEliminar.className = "btn btn-sm btn-danger";
        botonEliminar.textContent = "Eliminar";

        formBorrar.appendChild(botonEliminar);
        divCentro.appendChild(botonVer);
        divCentro.appendChild(formBorrar);
        celda6.appendChild(divCentro);

        const celda7 = document.createElement("td");
        const divCentro2 = document.createElement("div");
        divCentro2.className = "d-flex justify-content-center gap-2";


        if (factura.notasCreditos.length > 0) {
            const botonVerNota = document.createElement("a");
         
            botonVerNota.setAttribute("idFactura", factura.idFactura);
            botonVerNota.addEventListener("click", (event) => {
                VerNotaCredito(event)
            });


            botonVerNota.className = "btn btn-sm btn-primary text-white";
            botonVerNota.textContent = "Ver";
            divCentro2.appendChild(botonVerNota)
        }


    


        const formCrearNota = document.createElement("form");
        formCrearNota.addEventListener("submit", (event) => {
            CrearNotaCredito(event)
        });

        const botonCrearNota = document.createElement("button");
        botonCrearNota.type = "submit";
        botonCrearNota.setAttribute("idFactura", factura.idFactura);
        botonCrearNota.className = "btn btn-sm btn-danger";
        botonCrearNota.textContent = "Crear";


        formCrearNota.appendChild(botonCrearNota);
        divCentro2.appendChild(formCrearNota);
        celda7.appendChild(divCentro2);



        celda1.className = "text-center";
        celda2.className = "text-center";
        celda3.className = "text-center";
        celda4.className = "text-center";
        celda5.className = "text-center";
        celdaEstado.className = "text-center";
        celda6.className = "text-center";
        celda7.className = "text-center";
        nuevaFila.appendChild(celda1);
        nuevaFila.appendChild(celda2);
        nuevaFila.appendChild(celda3);
        nuevaFila.appendChild(celda4);
        nuevaFila.appendChild(celda5);
        nuevaFila.appendChild(celdaEstado);
        nuevaFila.appendChild(celda6);
        nuevaFila.appendChild(celda7);
        bodyTabla.appendChild(nuevaFila);
        contador++;
    });
}


function VerNotaCredito(event) {
    const idFactura = event.currentTarget.getAttribute("idFactura");
    var urlVer = "/FacturaEmpleado/VerNotaCredito?idFactura=" + idFactura;
    window.location.replace(urlVer);
}

function CrearNotaCredito(event) {
    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
    const formCrear = event.currentTarget;
    const botonCrear = formCrear.querySelector('button[type="submit"]');
    const idFactura = botonCrear.getAttribute("idFactura");

    fetch("/FacturaEmpleado/CrearNotaCredito/", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        },
        body: JSON.stringify(idFactura)
    }).then(respuesta => {
        return respuesta.json()
            .then(respuesta => {
                if (respuesta.correcto) {
                    swal({
                        text: respuesta.mensaje,
                        icon: "success"
                    }).then(() => {
                        window.location.reload()
                    })
                } else {
                    swal({
                        text: respuesta.mensaje,
                        icon: "error"
                    });
                }
            });
    }).catch(error => {
        console.error("Error", error);

    });
}







function VerPaginaFactura(event) {
    const idFactura = event.currentTarget.getAttribute("idFactura");
    var urlVer = "/FacturaEmpleado/VerFactura?idFactura=" + idFactura;
    window.location.replace(urlVer);
}

function EliminarFactura(event) {
    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
    const formBorrar = event.currentTarget;
    const botonBorrar = formBorrar.querySelector('button[type="submit"]');
    const idFactura = botonBorrar.getAttribute("idFactura");

    fetch("/FacturaEmpleado/EliminarFactura/" + idFactura, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        },
    }).then(respuesta => {
        return respuesta.json()
    .then(respuesta => {
            if (respuesta.correcto) {
                swal({
                    text: respuesta.mensaje,
                    icon: "success"
                })
                const filaAEliminar = document.getElementById(idFactura)
                filaAEliminar.remove()
            } else {
                swal({
                    text: respuesta.mensaje,
                    icon: "error"
                });
            }
        });
    }).catch(error => {
        console.error("Error", error);
       
    });
}

function ObtenerTodasLasPaginas() {
    fetch("/FacturaEmpleado/ObtenerTotalPaginas", {
        method: "GET",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json();
    }).then(respuesta => {
        CrearPaginacion(respuesta.paginas);
    }).catch(error => {
        console.error("Error", error);
    });
}

function CrearPaginacion(paginas) {
    const lista = document.createElement("ul");
    const paginacion = document.getElementById("paginacion");

    lista.classList.add("pagination", "justify-content-center", "m-3");

    var contador = 1;
    while (contador <= paginas) {
        var elementoLista = document.createElement("li");
        var aLista = document.createElement("a");

        elementoLista.classList.add("page-item");
        aLista.classList.add("page-link", "bg-dark");

        elementoLista.appendChild(aLista);
        lista.appendChild(elementoLista);

        aLista.setAttribute("pagina", contador);
        aLista.href = "?pagina=" + contador;
        aLista.innerText = contador;

        aLista.addEventListener("click", function (event) {
         
            ObtenerTodasLasFacturas(event.target.getAttribute("pagina"));
        });

        contador++;
    }

    paginacion.appendChild(lista);
}
