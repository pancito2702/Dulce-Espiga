
document.addEventListener("DOMContentLoaded", function () {

    CargarAlInicio();
});

function CargarAlInicio() {
    ObtenerTodasLasPaginas()

    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    const pagina = parametrosUrl.get("pagina");

    if (pagina === null) {
        ObtenerTodasLasRecetas(1)
    } else {
        ObtenerTodasLasRecetas(pagina)
    }


}


function ObtenerTodasLasRecetas(pagina) {
    fetch("/Recetas/ObtenerTodasLasRecetas/"+pagina, {
        method: "GET",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        CrearTarjetas(respuesta)
    }).catch(error => {

        console.error("Error", error);
    });
}

function CrearTarjetas(respuesta) {
    var contador = 1;
    const divAgregar = document.getElementById("agregar");

    respuesta.arregloRecetas.forEach(receta => {
        const divPrincipal = document.createElement("div");
        const divCard = document.createElement("div")
        const divCardHeader = document.createElement("div");
        const brDivPrincipal = document.createElement("br")
        const cardBody = document.createElement("div")
        const rowCardBody = document.createElement("div")
        const col7RowCardBody = document.createElement("div")
        const h2CardBody = document.createElement("h2")
        const p1CardBody = document.createElement("p")
        const p2CardBody = document.createElement("p")
        const negritaH2 = document.createElement("b")
        const p1Negrita = document.createElement("b")
        const p2Negrita = document.createElement("b")
        const ulListaIngredientes = document.createElement("ul")
        const cardFooter = document.createElement("div")
        const divFooter = document.createElement("div")
        const aEditar = document.createElement("a")
        const formBorrar = document.createElement("form")
        const botonBorrar = document.createElement("button")

        divAgregar.appendChild(divPrincipal);
        divPrincipal.appendChild(divCard);
        divCard.appendChild(divCardHeader);
        divCard.appendChild(brDivPrincipal);
        divCard.appendChild(cardBody);
        cardBody.appendChild(rowCardBody)
        rowCardBody.appendChild(col7RowCardBody)
        col7RowCardBody.appendChild(h2CardBody)
        col7RowCardBody.appendChild(p1CardBody);
        col7RowCardBody.appendChild(p2CardBody);
        col7RowCardBody.appendChild(ulListaIngredientes);
        cardBody.appendChild(cardFooter)
        cardFooter.appendChild(divFooter)
        divFooter.appendChild(aEditar)
        divFooter.appendChild(formBorrar)
        formBorrar.appendChild(botonBorrar)

        

        divPrincipal.id = receta.idReceta;


        divPrincipal.classList.add("col-12", "col-sm-6", "col-md-4", "mb-4")
        divCard.classList.add("card", "bg-light", "h-100")
        divCardHeader.classList.add("card-header", "text-muted", "border-bottom-0")
        cardBody.classList.add("card-body", "pt-0", "d-flex", "flex-column")
        rowCardBody.classList.add("row", "flex-fill")
        col7RowCardBody.classList.add("col-7")
        h2CardBody.classList.add("lead")
        p1CardBody.classList.add("text-muted", "text-sm")
        p2CardBody.classList.add("text-muted", "text-sm")
        divFooter.classList.add("text-right")
        aEditar.classList.add("btn", "btn-sm", "btn-primary", "text-white", "mb-2")
        botonBorrar.classList.add("btn", "btn-sm")

        divCardHeader.textContent = contador;

        negritaH2.textContent = "Receta: ";
        h2CardBody.appendChild(negritaH2);
        h2CardBody.appendChild(document.createTextNode(receta.nombreReceta));

        p1Negrita.textContent = "Instrucciones: ";
        p1CardBody.appendChild(p1Negrita);
        p1CardBody.appendChild(document.createTextNode(receta.instrucciones));

        p2Negrita.textContent = "Ingredientes: ";
        p2CardBody.appendChild(p2Negrita);




        receta.ingredientes.forEach(ingrediente => {


            const liIngrediente = document.createElement("li");
            ulListaIngredientes.appendChild(liIngrediente);

            liIngrediente.textContent = ingrediente.nombreIngrediente;

            liIngrediente.classList.add("text-muted", "text-sm")
        })


        aEditar.textContent = "Editar"


        aEditar.setAttribute("idReceta", receta.idReceta)

        aEditar.onclick = (event) => {
            VerPaginaEditar(event)
        }


        botonBorrar.textContent = "Borrar"


        botonBorrar.type = "submit";
        botonBorrar.setAttribute("idReceta", receta.idReceta)

        formBorrar.onsubmit = (event) => {

            EliminarReceta(event);
        }


        contador++;
    })

}






//<div class="col-12 col-sm-6 col-md-4 d-flex align-items-stretch">
//    <div class="card bg-light">
//        <div class="card-header text-muted border-bottom-0">
//            Receta 3
//        </div>
//        <br />
//        <div class="card-body pt-0">
//            <div class="row">
//                <div class="col-7">
//                    <h2 class="lead"><b>Receta para cupcake</b></h2>
//                    <p class="text-muted text-sm"><b>Detalles: </b> Receta para preparar cupcakes </p>
//                    <p class="text-muted text-sm">
//                        <b>Instrucciones: </b>
//                        Preparar la masa, Lavar la masa
//                    </p>
//                    <p class="text-muted text-sm">
//                        <b>Ingredientes necesarios: </b>
//                        Azucar, Harina
//                    </p>



//                </div>
//            </div>
//        </div>
//        <div class="card-footer">
//            <div class="text-right">
//                <a href="@Url.Action(" EditarReceta", "Administrador")" class="btn btn-sm btn-primary">
//                Editar
//            </a>
//            <a href="#" class="btn btn-sm">
//                Eliminar
//            </a>
//        </div>
//    </div>
//</div>
//                        </div >




function EliminarReceta(event) {
    event.preventDefault();
    const boton = event.submitter;

    const idReceta = boton.getAttribute("idReceta");
    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;






    fetch("/Receta/EliminarReceta/" + idReceta, {
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
            const cardAEliminar = document.getElementById(idReceta)
            cardAEliminar.remove()    
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
    const idReceta = event.currentTarget.getAttribute("idReceta");



    var urlEditar = "/Receta/EditarReceta?idReceta=" + idReceta;


    window.location.replace(urlEditar);
}

function ObtenerTodasLasPaginas() {



    fetch("/Receta/ObtenerTotalPaginas", {
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

    const tabla = document.getElementById("navFooter")

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
            ObtenerTodasLasRecetas(event.target.getAttribute("pagina"));
        });

        aLista.innerText = contador;
        contador++;
    }

    tabla.appendChild(lista);

}




