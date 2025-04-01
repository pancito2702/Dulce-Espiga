

document.addEventListener("DOMContentLoaded", () => {
    ObtenerProductos();
})


function ObtenerProductos() {
    fetch("/Home/ObtenerProductosMasVendidos", {
        method: "GET"
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        CrearCardsProducto(respuesta)
    }).catch(error => {
        console.error("Error", error);
    });
}


function CrearCardsProducto(respuesta) {
    const divPrincipal = document.getElementById("divPrincipal")

   

    respuesta.arregloProductosMasVendidos.forEach(productoMasVendido => {
        const divPrincipalProducto = document.createElement("div");
        const aLink = document.createElement("a")
        const imgProducto = document.createElement("img")
        const h3NombreProducto = document.createElement("h3")
        const strongPrecioProducto = document.createElement("strong")
        const spanIcono = document.createElement("span")
        const imgSpan = document.createElement("img")

        divPrincipalProducto.classList.add("col-12", "col-md-4", "col-lg-3", "mb-5", "mb-md-0")
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

        h3NombreProducto.innerText = productoMasVendido.producto.nombreProducto;

        const precioProducto = document.createTextNode(productoMasVendido.producto.precioProducto + "₡");

        aLink.href = "/Home/Tienda"
        strongPrecioProducto.appendChild(precioProducto)
        imgProducto.src = "/" + productoMasVendido.producto.imagenProducto
        imgSpan.src = "/img/cross.svg"
    });
}