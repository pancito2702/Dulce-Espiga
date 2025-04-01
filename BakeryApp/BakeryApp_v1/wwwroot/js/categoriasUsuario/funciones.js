
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
    fetch("/UsuarioRegistrado/ObtenerCategorias/"+pagina, {
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
    const divPrincipal = document.getElementById("divPrincipal")

    respuesta.arregloCategorias.forEach(categoria => {
        const divPrincipalCategoria = document.createElement("div");
        const aLink = document.createElement("a")
        const imgCategoria = document.createElement("img")
        const strongNombreCategoria = document.createElement("strong")
        const spanIcono = document.createElement("span")
        const imgSpan = document.createElement("img")

        divPrincipalCategoria.classList.add("col-12", "col-md-4", "col-lg-3", "mb-5", "mb-md-0", "mt-5")
        aLink.classList.add("product-item")
        imgCategoria.classList.add("img-fluid", "product-thumbnail")
        strongNombreCategoria.classList.add("product-price")
        spanIcono.classList.add("icon-cross")
        imgSpan.classList.add("img-fluid")

        divPrincipal.appendChild(divPrincipalCategoria)
        divPrincipalCategoria.appendChild(aLink)
        aLink.appendChild(imgCategoria)
        aLink.appendChild(strongNombreCategoria)
        aLink.appendChild(spanIcono)
        spanIcono.appendChild(imgSpan)

        const nombreCategoria = document.createTextNode(categoria.nombreCategoria);

        aLink.href = "/UsuarioRegistrado/ProductosPorCategoria?categoria=" + categoria.idCategoria;
        strongNombreCategoria.appendChild(nombreCategoria)
        imgCategoria.src = "/"+ categoria.imagenCategoria
        imgSpan.src = "/img/cross.svg"
    });
}


//<div class="col-12 col-md-4 col-lg-3 mb-5 mb-md-0">
//    <a class="product-item" href="@Url.Action(" Categorias", "UsuarioRegistrado")">
//    <img src="~/img/CupcakePrueba.jpg" class="img-fluid product-thumbnail">
//        <strong class="product-price">Cupcakes</strong>

//        <span class="icon-cross">
//            <img src="~/img/cross.svg" class="img-fluid">
//        </span>
//    </a>
//</div>


function ObtenerTodasLasPaginas() {



    fetch("/UsuarioRegistrado/ObtenerTotalPaginas", {
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

    const divPaginacionCategoria = document.getElementById("paginacionCategoria")

    lista.classList.add("pagination", "justify-content-center", "m-3")



    var contador = 1;
    while (contador <= paginas) {
        var elementoLista = document.createElement("li")
        var aLista = document.createElement("a")

        elementoLista.classList.add("page-item")
        aLista.classList.add("page-link", "bg-dark", "text-white")


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

    divPaginacionCategoria.appendChild(lista);

}



