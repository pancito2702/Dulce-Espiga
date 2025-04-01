
document.addEventListener("DOMContentLoaded", function () {

    CargarAlInicio();
});

function CargarAlInicio() {
    ObtenerTodasLasPaginas()

    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    const pagina = parametrosUrl.get("pagina");

    if (pagina === null) {
        ObtenerTodasLasCategorias(1)
    } else {
        ObtenerTodasLasCategorias(pagina)
    }


}


function ObtenerTodasLasCategorias(pagina) {
    fetch("/CategoriaEmpleado/ObtenerCategorias/"+pagina, {
        method: "GET",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        VerificarCategoriasVacias(respuesta)
        CrearTarjetas(respuesta)
    }).catch(error => {

        console.error("Error", error);
    });
}

function CrearTarjetas(respuesta) {
    var contador = 1;

    respuesta.arregloCategorias.forEach(categoria => {
        const divAgregar = document.getElementById("agregar");
        const divPrincipal = document.createElement("div");
        const hijoDivPrincipal = document.createElement("div");
        const cardHeader = document.createElement("div");
        const saltoLinea1 = document.createElement("br");
        const cardBody = document.createElement("div");
        const rowCardBody = document.createElement("div");
        const colRowCardBody = document.createElement("div");
        const h2ColRowCardBody = document.createElement("h2");
        const negritaH2 = document.createElement("b");
        const negrita1 = document.createElement("b");
        const negrita2 = document.createElement("b");
        const pColRowCardBody1 = document.createElement("p");
        const pColRowCardBody2 = document.createElement("p");
        const col5RowCardBody = document.createElement("div");
        const imgCard = document.createElement("img");
        const divFooter = document.createElement("div");
        const divFooterRight = document.createElement("div");
        const formBorrar = document.createElement("form");
        const aFooter1 = document.createElement("a");
        const bFooter2 = document.createElement("button");

        divAgregar.appendChild(divPrincipal);
        divPrincipal.appendChild(hijoDivPrincipal);
        hijoDivPrincipal.appendChild(cardHeader);
        hijoDivPrincipal.appendChild(saltoLinea1);
        hijoDivPrincipal.appendChild(cardBody);
        cardBody.appendChild(rowCardBody);
        rowCardBody.appendChild(colRowCardBody);
        colRowCardBody.appendChild(h2ColRowCardBody);
        colRowCardBody.appendChild(pColRowCardBody1);
        colRowCardBody.appendChild(pColRowCardBody2);
        rowCardBody.appendChild(col5RowCardBody);
        col5RowCardBody.appendChild(imgCard);
        hijoDivPrincipal.appendChild(divFooter);
        divFooter.appendChild(divFooterRight);
        divFooterRight.appendChild(formBorrar);
        divFooterRight.appendChild(aFooter1);
        formBorrar.appendChild(bFooter2);



        divPrincipal.classList.add("col-12", "col-sm-6", "col-md-4", "d-flex", "align-items-stretch");
        hijoDivPrincipal.classList.add("card", "bg-light");
        cardHeader.classList.add("card-header", "text-muted", "border-bottom-0");
        cardBody.classList.add("card-body", "pt-0");
        rowCardBody.classList.add("row");
        colRowCardBody.classList.add("col-7");
        h2ColRowCardBody.classList.add("lead");
        pColRowCardBody1.classList.add("text-muted", "text-sm");
        pColRowCardBody2.classList.add("text-muted", "text-sm");
        col5RowCardBody.classList.add("col-5", "text-center");
        imgCard.classList.add("img-circle", "img-fluid");
        divFooter.classList.add("card-footer");
        divFooterRight.classList.add("text-right", "d-flex", "justify-content-end");
        aFooter1.classList.add("btn", "btn-sm", "btn-primary", "text-white");
        bFooter2.classList.add("btn", "btn-sm", "text-white", "mr-2");



        divPrincipal.id = categoria.idCategoria;



        cardHeader.textContent = contador;

        negritaH2.textContent = "Categoria: ";
        h2ColRowCardBody.appendChild(negritaH2);
        h2ColRowCardBody.appendChild(document.createTextNode(categoria.nombreCategoria));

        negrita1.textContent = "Nombre: ";
        pColRowCardBody1.appendChild(negrita1);
        pColRowCardBody1.appendChild(document.createTextNode(categoria.nombreCategoria));

        negrita2.textContent = "Detalles: ";
        pColRowCardBody2.appendChild(negrita2);
        pColRowCardBody2.appendChild(document.createTextNode(categoria.descripcionCategoria));




        imgCard.src = categoria.imagenCategoria;
        imgCard.alt = "No se pudo cargar la imagen";

        aFooter1.innerText = "Editar";
        aFooter1.setAttribute("idCategoria", categoria.idCategoria)

        aFooter1.onclick = (event) => {
            VerPaginaEditar(event)
        }


        bFooter2.innerText = "Eliminar";
        bFooter2.type = "submit";
        bFooter2.setAttribute("idCategoria", categoria.idCategoria)



        formBorrar.onsubmit = (event) => {

            EliminarCategoria(event);
        }

        contador++;
    });
}



function EliminarCategoria(event) {
    event.preventDefault();
    const boton = event.submitter;

    const idCategoria = boton.getAttribute("idCategoria");
    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;






    fetch("/CategoriaEmpleado/EliminarCategoria/" + idCategoria, {
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
            const cardAEliminar = document.getElementById(idCategoria)
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
    const idCategoria = event.currentTarget.getAttribute("idCategoria");



    var urlEditar = "/CategoriaEmpleado/EditarCategoria?idCategoria=" + idCategoria;


    window.location.replace(urlEditar);
}

function ObtenerTodasLasPaginas() {



    fetch("/CategoriaEmpleado/ObtenerTotalPaginas", {
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

    const navFooter = document.getElementById("navFooter")

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
            ObtenerTodasLasCategorias(event.target.getAttribute("pagina"));
        });

        aLista.innerText = contador;
        contador++;
    }

    navFooter.appendChild(lista);

}



function VerificarCategoriasVacias(respuesta) {

    if (respuesta.arregloCategorias.length === 0) {
        const divAgregar = document.getElementById("agregar");


        const h2Agregar = document.createElement("h2");

        divAgregar.appendChild(h2Agregar)

        h2Agregar.textContent = "No hay categorias registradas en esta pagina";
        h2Agregar.classList.add("lead", "text-center")

    }

}

