
document.addEventListener("DOMContentLoaded", function () {

    CargarAlInicio();
});

function CargarAlInicio() {
    ObtenerTodasLasPaginas()

    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    const pagina = parametrosUrl.get("pagina");

    if (pagina === null) {
        ObtenerTodosLosUsuariosMarketing(1)
    } else {
        ObtenerTodosLosUsuariosMarketing(pagina)
    }


}


function ObtenerTodosLosUsuariosMarketing(pagina) {
    fetch("/Marketing/ObtenerTodosLosUsuariosBoletin/" + pagina, {
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

    respuesta.arregloBoletin.forEach(boletin => {
        // Crear elementos de la fila
        const fila = document.createElement("tr");

        const celdaNumero = document.createElement("td");
        celdaNumero.textContent = contador++;

        const celdaNombre = document.createElement("td");
        celdaNombre.textContent = boletin.persona.nombre; 

        const celdaApellido = document.createElement("td");
        celdaApellido.textContent = boletin.persona.primerApellido + " " +boletin.persona.segundoApellido; 

        const celdaCorreo = document.createElement("td");
        celdaCorreo.textContent = boletin.persona.correo;

        const celdaTelefono = document.createElement("td");
        celdaTelefono.textContent = "+506" + " " + boletin.persona.telefono;

        const celdaAccion = document.createElement("td");
        const divAccion = document.createElement("div");
        divAccion.className = "text-center";

        const enlaceCorreo = document.createElement("a");
        enlaceCorreo.href = "/Marketing/EnviarCorreo?idBoletin="+ boletin.idBoletin; 
        enlaceCorreo.className = "btn btn-sm btn-primary";
        enlaceCorreo.textContent = "Enviar Correo";

        // Ensamblar los elementos
        divAccion.appendChild(enlaceCorreo);
        celdaAccion.appendChild(divAccion);

        fila.appendChild(celdaNumero);
        fila.appendChild(celdaNombre);
        fila.appendChild(celdaApellido);
        fila.appendChild(celdaCorreo);
        fila.appendChild(celdaTelefono);
        fila.appendChild(celdaAccion);

        // Agregar la fila al body de la tabla
        bodyTabla.appendChild(fila);
    })

}

//<tr>
//    <td>1</td>
//    <td>
//        Prueba
//    </td>
//    <td>Prueba, Prueba</td>
//    <td> prueba@gmail.com</td>
//    <td> +506 1838-3221</td>
//    <td>
//        <div class="text-center">
//            <a href="@Url.Action(" EnviarCorreo", "Administrador")" class="btn btn-sm btn-primary">
//            Enviar Correo
//        </a>
//    </div>
//</td>
//                                </tr >



function ObtenerTodasLasPaginas() {
    fetch("/Marketing/ObtenerTotalPaginas", {
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
            ObtenerTodosLosUsuariosMarketing(event.target.getAttribute("pagina"))
        });

        aLista.innerText = contador;
        contador++;
    }

    paginacion.appendChild(lista);

}




