


document.addEventListener("DOMContentLoaded", function () {

    CargarAlInicio();
});

function CargarAlInicio() {
    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    const producto = parametrosUrl.get("producto");
    BuscarProducto(producto)

}



function BuscarProducto(producto) {


    fetch("/UsuarioRegistrado/VerDetallesProductoBuscado/" + producto, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
            .then(respuesta => {
                if (respuesta.correcto) {
                    CrearProducto(respuesta.mensaje)
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



function CrearProducto(producto) {


    const divPrincipal = document.getElementById("divPrincipal")
    const divImg = document.createElement("div")
    const imgProducto = document.createElement("img")

    const divInfoProducto = document.createElement("div")
    const h3Nombre = document.createElement("h3")
    const pDescripcion = document.createElement("p")
    const divPrecio = document.createElement("div")
    const h2Precio = document.createElement("h2")
    const divCarrito = document.createElement("div")
    const formCarrito = document.createElement("form")
    const inputHiddenProducto = document.createElement("input")
    const botonCarrito = document.createElement("button")
    const iconoCarrito = document.createElement("i")

    divPrincipal.appendChild(divImg)
    divImg.appendChild(imgProducto)

    divPrincipal.appendChild(divInfoProducto)
    divInfoProducto.appendChild(h3Nombre)
    divInfoProducto.appendChild(pDescripcion)
    divInfoProducto.appendChild(divPrecio)
    divPrecio.appendChild(h2Precio)
    divInfoProducto.appendChild(divCarrito)
    divCarrito.appendChild(formCarrito)
    formCarrito.appendChild(botonCarrito)
    formCarrito.appendChild(inputHiddenProducto)
    botonCarrito.appendChild(iconoCarrito)

    divImg.classList.add("img-container");
    imgProducto.classList.add("img-fluid", "product-image");
    divInfoProducto.classList.add("product-info")
    h3Nombre.classList.add("my-3")
    divPrecio.classList.add("bg-gray", "mt-4")
    h2Precio.classList.add("mb-0")
    divCarrito.classList.add("mt-4")
    botonCarrito.classList.add("btn", "btn-secondary", "me-2")
    iconoCarrito.classList.add("fas", "fa-cart-plus", "fa-lg", "mr-2")
    imgProducto.src = "/" + producto.imagenProducto


    inputHiddenProducto.type = "hidden";
    inputHiddenProducto.value = producto.idProducto;
    inputHiddenProducto.id = "idProducto"


    h3Nombre.innerText = "Nombre: " + producto.nombreProducto

    pDescripcion.innerText = "Descripcion: " + producto.descripcionProducto
    h2Precio.innerText = "Precio: " + producto.precioProducto + "₡";
    const textCarrito = document.createTextNode("Agregar al carrito")
    botonCarrito.appendChild(textCarrito)
    botonCarrito.type = "submit"

    formCarrito.onsubmit = (event) => {

        AgregarAlCarrito(event);
    }

}


function AgregarAlCarrito(event) {
    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const idProducto = document.getElementById("idProducto").value;





    const carrito = {
        IdProducto: idProducto,
    }

    fetch("/Carrito/AgregarCarrito", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        },
        body: JSON.stringify(carrito)
    }).then(respuesta => {
        return respuesta.json()
            .then(respuesta => {
                if (respuesta.correcto) {
                    window.location.href = respuesta.mensaje
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
