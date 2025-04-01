



document.addEventListener("DOMContentLoaded", function () {
    CargarAlInicio();
});



function CargarAlInicio() {
    ObtenerTodasLasPaginas()

    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    const pagina = parametrosUrl.get("pagina");

    if (pagina === null) {
        ObtenerTodasLasPersonas(1)
    } else {
        ObtenerTodasLasPersonas(pagina)
    }


}



function ObtenerTodasLasPersonas(pagina) {
    fetch("/Perfil/ObtenerTodasLasPersonas/" + pagina, {
        method: "GET",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
            .then(respuesta => {
                CrearFilasTabla(respuesta)
            })
    }).catch(error => {
        console.error("Error", error);
    });
}

function ObtenerTodasLasPaginas() {



    fetch("/Perfil/ObtenerTotalPaginas", {
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

    const tabla = document.getElementById("tabla")

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
            ObtenerTodasLasPersonas(event.target.getAttribute("pagina"));
        });

        aLista.innerText = contador;
        contador++;
    }

    tabla.appendChild(lista);

}

function CrearFilasTabla(respuesta) {

    const bodyTabla = document.getElementById("agregar");

    var contador = 1;
    respuesta.arregloPersonas.forEach(persona => {
        const fila = document.createElement("tr")
        const tdId = document.createElement("td")
        const tdNombre = document.createElement("td")
        const tdPrimerApellido = document.createElement("td")
        const tdSegundoApellido = document.createElement("td")
        const tdCorreo = document.createElement("td")
        const tdTelefono = document.createElement("td")
        const tdRol = document.createElement("td")
        const tdEditar = document.createElement("td")
        const tdEliminar = document.createElement("td")
        const divEditar = document.createElement("div")
        const aEditar = document.createElement("a")
        const formBorrar = document.createElement("form")
        const divEliminar = document.createElement("div")
        const btnEliminar = document.createElement("button")


        fila.appendChild(tdId);
        fila.appendChild(tdNombre);
        fila.appendChild(tdPrimerApellido);
        fila.appendChild(tdSegundoApellido);
        fila.appendChild(tdCorreo);
        fila.appendChild(tdTelefono);
        fila.appendChild(tdRol);
        fila.appendChild(tdEditar)
        fila.appendChild(tdEliminar)
        tdEditar.appendChild(divEditar);
        divEditar.appendChild(aEditar);
        tdEliminar.appendChild(formBorrar);
        formBorrar.appendChild(divEliminar)
        divEliminar.appendChild(btnEliminar);


        fila.id = persona.idPersona;

        divEditar.classList.add("text-center")
        aEditar.classList.add("btn", "btn-sm", "btn-primary", "text-white")

        divEliminar.classList.add("text-center")
        btnEliminar.classList.add("btn", "btn-sm")

        tdId.textContent = contador;
        tdNombre.textContent = persona.nombre;
        tdPrimerApellido.textContent = persona.primerApellido;
        tdSegundoApellido.textContent = persona.segundoApellido;
        tdCorreo.textContent = persona.correo;
        tdTelefono.textContent = persona.telefono;
        tdRol.textContent = persona.rol.nombreRol;
        aEditar.innerText = "Editar";

        aEditar.setAttribute("idPersona", persona.idPersona)

        aEditar.onclick = (event) => {
            VerPaginaEditar(event)
        }

        btnEliminar.textContent = "Eliminar";
        btnEliminar.type = "submit";
        btnEliminar.setAttribute("idPersona", persona.idPersona)


        formBorrar.onsubmit = (event) => {

            EliminarPerfil(event);
        }


        bodyTabla.appendChild(fila);
        contador++;
    })
}


function EliminarPerfil(event) {
    event.preventDefault();
    const boton = event.submitter;

    const idPersona = boton.getAttribute("idPersona");
    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;






    fetch("/Perfil/EliminarPerfil/" + idPersona, {
        method: "DELETE",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        if (respuesta.correcto) {
            swal({
                text: respuesta.mensaje,
                icon: "success"
            })
            const filaEliminar = document.getElementById(idPersona)
            filaEliminar.remove()
        } else {
            swal({
                text: respuesta.mensaje,
                icon: "error"
            });
        }
      
    }).catch(error => {
        console.error("Error", error);
    })

}



function VerPaginaEditar(event) {
    const idPersona= event.currentTarget.getAttribute("idPersona");



    var urlEditar = "/Perfil/EditarPerfil?idPersona=" + idPersona;


    window.location.replace(urlEditar);
}




//<div class="text-center">
//                                              <a href="@Url.Action("EditarInventario", "Administrador")" class="btn btn-sm btn-primary">
//                                                  Editar
//                                              </a>
//                                              <a href="#" class="btn btn-sm ">
//                                                  Eliminar
//                                              </a>
//                                          </div >//<div class="text-center">
//                                              <a href="@Url.Action("EditarInventario", "Administrador")" class="btn btn-sm btn-primary">
//                                                  Editar
//                                              </a>
//                                              <a href="#" class="btn btn-sm ">
//                                                  Eliminar
//                                              </a>
//                                          </div >




//<tr>
//    <td>1</td>
//    <td>
//        Prueba
//    </td>
//    <td>Efectivo</td>
//    <td> 3</td>
//    <td>₡5000.00</td>
//    <td>₡5000.00</td>
//    <td>
//        <div class="text-center">
//            <a href="@Url.Action(" VerFactura", "Administrador")" class="btn btn-sm btn-primary">
//            Ver
//        </a>
//        <a href="#" class="btn btn-sm ">
//            Eliminar
//        </a>
//    </div>
//    </td>
//</tr > 