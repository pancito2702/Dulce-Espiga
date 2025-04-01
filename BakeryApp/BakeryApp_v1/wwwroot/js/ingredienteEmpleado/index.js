
document.addEventListener("DOMContentLoaded", function () {

    CargarAlInicio();
});

function CargarAlInicio() {
    ObtenerTodasLasPaginas()

    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    const pagina = parametrosUrl.get("pagina");

    if (pagina === null) {
        ObtenerTodosLosIngredientes(1)
    } else {
        ObtenerTodosLosIngredientes(pagina)
    }


}


function ObtenerTodosLosIngredientes(pagina) {
    fetch("/IngredienteEmpleado/ObtenerIngredientes/"+pagina, {
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

 
    respuesta.arregloIngredientes.forEach(ingrediente => {
        const fila = document.createElement("tr")
        const tdId = document.createElement("td")
        const tdNombre = document.createElement("td")
        const tdDescripcion = document.createElement("td")
        const tdCantidad = document.createElement("td")
        const tdUnidadMedida = document.createElement("td")
        const tdPrecioUnitario = document.createElement("td")
        const tdFechaVencimiento = document.createElement("td")
        const tdEditar = document.createElement("td")
        const tdEliminar = document.createElement("td")
        const divEditar = document.createElement("div")
        const aEditar = document.createElement("a")
        const formBorrar = document.createElement("form")
        const divEliminar = document.createElement("div")
        const btnEliminar = document.createElement("button")


        fila.appendChild(tdId);
        fila.appendChild(tdNombre);
        fila.appendChild(tdDescripcion);
        fila.appendChild(tdCantidad);
        fila.appendChild(tdUnidadMedida);
        fila.appendChild(tdPrecioUnitario);
        fila.appendChild(tdFechaVencimiento)
        fila.appendChild(tdEditar)
        fila.appendChild(tdEliminar)
        tdEditar.appendChild(divEditar);
        divEditar.appendChild(aEditar);
        tdEliminar.appendChild(formBorrar);
        formBorrar.appendChild(divEliminar)
        divEliminar.appendChild(btnEliminar);


        fila.id = ingrediente.idIngrediente;

        divEditar.classList.add("text-center")
        aEditar.classList.add("btn", "btn-sm", "btn-primary", "text-white")

        divEliminar.classList.add("text-center")
        btnEliminar.classList.add("btn", "btn-sm")

        tdId.textContent = contador;
        tdNombre.textContent = ingrediente.nombreIngrediente;
        tdDescripcion.textContent = ingrediente.descripcionIngrediente;
        tdPrecioUnitario.textContent = ingrediente.precioUnidadIngrediente;
        tdCantidad.textContent = ingrediente.cantidadIngrediente;
        tdUnidadMedida.textContent = ingrediente.unidadMedidaDTO.nombreUnidad;
        tdFechaVencimiento.textContent = ingrediente.fechaCaducidadIngrediente;
        aEditar.innerText = "Editar";

        aEditar.setAttribute("idIngrediente", ingrediente.idIngrediente)

        aEditar.onclick = (event) => {
            VerPaginaEditar(event)
        }

        btnEliminar.textContent = "Eliminar";
        btnEliminar.type = "submit";
        btnEliminar.setAttribute("idIngrediente", ingrediente.idIngrediente)


        formBorrar.onsubmit = (event) => {

            EliminarIngrediente(event);
        }


        bodyTabla.appendChild(fila);
        contador++;
    })

}

  //<td>Azucar</td>
  //                                      <td>
  //                                          Tiene sabor dulce
  //                                      </td>

  //                                      <td> 4</td>
  //                                      <td>MG</td>
  //                                      <td>₡500</td>
  //                                      <td>
  //                                          <div class="text-center">
  //                                              <a href="@Url.Action("EditarInventario", "Administrador")" class="btn btn-sm btn-primary">
  //                                                  Editar
  //                                              </a>
  //                                              <a href="#" class="btn btn-sm ">
  //                                                  Eliminar
  //                                              </a>
  //                                          </div>
 /*                                       </td >*/


function EliminarIngrediente(event) {
    event.preventDefault();
    const boton = event.submitter;

    const idIngrediente = boton.getAttribute("idIngrediente");
    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;






    fetch("/IngredienteEmpleado/EliminarIngrediente/" + idIngrediente, {
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
            const filaEliminar = document.getElementById(idIngrediente)
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
    const idIngrediente = event.currentTarget.getAttribute("idIngrediente");



    var urlEditar = "/IngredienteEmpleado/EditarIngrediente?idIngrediente=" + idIngrediente;


    window.location.replace(urlEditar);
}

function ObtenerTodasLasPaginas() {



    fetch("/IngredienteEmpleado/ObtenerTotalPaginas", {
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
            ObtenerTodosLosIngredientes(event.target.getAttribute("pagina"));
        });

        aLista.innerText = contador;
        contador++;
    }

    tabla.appendChild(lista);

}




