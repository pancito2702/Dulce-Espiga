
document.addEventListener("DOMContentLoaded", function () {

    CargarAlInicio();
});

function CargarAlInicio() {
    ObtenerTodasLasPaginas()

    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    const pagina = parametrosUrl.get("pagina");

    if (pagina === null) {
        ObtenerTodosLosPedidos(1)
    } else {
        ObtenerTodosLosPedidos(pagina)
    }


}


function ObtenerTodosLosPedidos(pagina) {
    fetch("/PedidoAdmin/ObtenerTodosLosPedidos/" + pagina, {
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


function CrearFilasTabla(respuesta) {
    var contador = 1;
    const bodyTabla = document.getElementById("agregar");
    bodyTabla.innerHTML = "";

    respuesta.arregloPedidos.forEach(pedido => {
        const fila = document.createElement("tr")
        const tdId = document.createElement("td")
        const tdFecha = document.createElement("td")
        const tdEstado = document.createElement("td")
        const tdVer = document.createElement("td")
        const aVer = document.createElement("a")
    
    
        const tdActualizarEstado = document.createElement("td");
        const selectEstado = document.createElement("select");

        tdId.classList.add("text-center");
        tdFecha.classList.add("text-center");
        tdEstado.classList.add("text-center");
        tdVer.classList.add("text-center")
        
        tdActualizarEstado.classList.add("text-center");
        aVer.classList.add("btn", "btn-primary", "btn-sm", "text-white")
        
        selectEstado.classList.add("form-select", "form-select-sm", "text-center");

        fetch("/PedidoAdmin/ObtenerEstadosPedido/", {
            method: "GET",
            headers: {
                "X-Requested-With": "XMLHttpRequest",
            }
        }).then(respuesta => {
            return respuesta.json()
        }).then(respuesta => {
            respuesta.arregloEstadosPedido.forEach(estado => {
                const opcion = document.createElement("option");
                opcion.value = estado.idEstadoPedido;
                opcion.text = estado.nombreEstado;
                selectEstado.appendChild(opcion);

                if (estado.idEstadoPedido === pedido.estadoPedido.idEstadoPedido) {
                    opcion.selected = true;
                }
            });
        }).catch(error => {
            console.error("Error", error);
        });


        fila.appendChild(tdId);
        fila.appendChild(tdFecha);
        fila.appendChild(tdEstado);
        fila.appendChild(tdVer)
        fila.appendChild(tdActualizarEstado)
        tdActualizarEstado.appendChild(selectEstado)
        tdVer.appendChild(aVer)
        



        aVer.setAttribute("idPedido", pedido.idPedido)

        aVer.addEventListener("click", (event)  => {
            VerPaginaPedido(event)
        })


        selectEstado.setAttribute("idPedido", pedido.idPedido)

        selectEstado.addEventListener("change", (event) => {
            ActualizarEstadoPedido(event)
        })


        tdId.innerText = "Pedido: " + contador;
        tdFecha.innerText = pedido.fechaPedido
        tdEstado.innerText = pedido.estadoPedido.nombreEstado
        aVer.innerText = "Ver Pedido"
        bodyTabla.appendChild(fila);
        contador++;
    })

}


function ActualizarEstadoPedido(event) {
    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
    const selectEstado = event.currentTarget;
    const idPedido = selectEstado.getAttribute("idPedido");
    const estadoPedido = selectEstado.value;
    

    const pedido = {
        IdPedido: idPedido,
        IdEstadoPedido: estadoPedido
    }




    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    const pagina = parametrosUrl.get("pagina");
    


    fetch("/PedidoAdmin/ActualizarEstadoPedido", {
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
                    if (pagina === null) {
                        ObtenerTodosLosPedidos(1)
                    } else {
                        ObtenerTodosLosPedidos(pagina)
                    }
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



function VerPaginaPedido(event) {
    const idPedido = event.currentTarget.getAttribute("idPedido");



    var urlVer = "/PedidoAdmin/VerPedido?idPedido=" + idPedido;


    window.location.replace(urlVer);
}



//<tr>
//    <td>
//        1
//    </td>
//    <td>
//        <small>
//            Hecho el 1/1/2024
//        </small>
//    </td>


//    <td class="project-state">
//        <span class="badge badge-primary">En progreso</span>
//    </td>
//    <td class="project-actions text-right">
//        <a class="btn btn-primary btn-sm" href="@Url.Action(" VerPedido", "Administrador")">
//        <i class="fas fa-folder">
//        </i>
//        Ver
//    </a>
//    <a class="btn btn-danger btn-sm" href="#">
//        Cancelar pedido
//    </a>
//</td>
//                        </tr >



function ObtenerTodasLasPaginas() {
    fetch("/PedidoAdmin/ObtenerTotalPaginas", {
        method: "GET",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        CrearPaginacion(respuesta.paginas)
    }).catch(error => {
        console.error("Error", error);
    });
}

function CrearPaginacion(paginas) {
    const lista = document.createElement("ul");

    const paginacion = document.getElementById("paginacion")

    lista.classList.add("pagination", "justify-content-center", "m-3")



    var contador = 1;
    while (contador <= paginas) {
        var elementoLista = document.createElement("li")
        var aLista = document.createElement("a")

        elementoLista.classList.add("page-item")
        aLista.classList.add("page-link", "bg-dark")


        lista.appendChild(elementoLista);
        elementoLista.appendChild(aLista)


        aLista.setAttribute("pagina", contador);
        aLista.href = "?pagina=" + contador;
        aLista.setAttribute("pagina", contador);

        aLista.addEventListener("click", function (event) {
            ObtenerTodosLosPedidos(event.target.getAttribute("pagina"));
        });

        aLista.innerText = contador;
        contador++;
    }

    paginacion.appendChild(lista);

}




