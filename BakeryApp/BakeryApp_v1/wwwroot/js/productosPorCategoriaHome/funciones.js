
document.addEventListener("DOMContentLoaded", function () {

    CargarAlInicio();
});

function CargarAlInicio() {
    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    const categoria = parametrosUrl.get("categoria");
    ObtenerCategoriaPorId(categoria)
    ObtenerTodasLosProductosPorCategoria(categoria)
}


function ObtenerTodasLosProductosPorCategoria(categoria) {
    fetch("/Home/ObtenerProductosPorCategoria/" + categoria, {
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

function ObtenerCategoriaPorId(categoria) {
    fetch("/Home/ObtenerCategoriaPorId/" + categoria, {
        method: "GET",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        CrearTitulo(respuesta)
    }).catch(error => {

        console.error("Error", error);
    });
}

function CrearTitulo(respuesta) {
    const divPrincipalTitulo = document.getElementById("divTitulo")
    const h1Titulo = document.createElement("h1");

    divPrincipalTitulo.appendChild(h1Titulo);



    h1Titulo.innerText = "Todos nuestros productos relacionados a la categoría: " + respuesta.categoria.nombreCategoria;

}

function CrearTarjetas(respuesta) {
 

    const divPrincipal = document.getElementById("divPrincipal")

    respuesta.arregloProductos.forEach(producto => {
        const divPrincipalProducto = document.createElement("div");
        const aLink = document.createElement("a")
        const imgProducto = document.createElement("img")
        const h3NombreProducto = document.createElement("h3")
        const strongPrecioProducto = document.createElement("strong")
        const spanIcono = document.createElement("span")
        const imgSpan = document.createElement("img")

        divPrincipalProducto.classList.add("col-12", "col-md-4", "col-lg-3", "mb-5", "mb-md-0", "mt-5")
        aLink.classList.add("product-item")
        imgProducto.classList.add("img-fluid", "product-thumbnail")
        strongPrecioProducto.classList.add("product-price")
        spanIcono.classList.add("icon-cross")
        imgSpan.classList.add("img-fluid")

        divPrincipal.appendChild(divPrincipalProducto)
        divPrincipalProducto.appendChild(aLink)
        aLink.appendChild(imgProducto)
        aLink.appendChild(h3NombreProducto)
        aLink.appendChild(strongPrecioProducto)
        aLink.appendChild(spanIcono)
        spanIcono.appendChild(imgSpan)

        h3NombreProducto.innerText = producto.nombreProducto;

        const precioProducto = document.createTextNode(producto.precioProducto + "₡");

        aLink.href = "/Home/IniciarSesion"
        strongPrecioProducto.appendChild(precioProducto)
        imgProducto.src = "/" + producto.imagenProducto
        imgSpan.src = "/img/cross.svg"
    });
}




