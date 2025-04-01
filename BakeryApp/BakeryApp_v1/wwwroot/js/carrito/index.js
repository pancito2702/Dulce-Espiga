
document.addEventListener("DOMContentLoaded", function () {
    ObtenerCarritoUsuario()
});

function DesactivarBotonCheckout(arregloCarrito) {
    const divCheckout = document.getElementById("divCheckout")

    const anchorCheckout = document.createElement("a")
    divCheckout.innerText = "";

    divCheckout.appendChild(anchorCheckout)
    anchorCheckout.className = "btn btn-black btn-lg py-2 btn block"
    anchorCheckout.innerText = "Proceder a pagar"
    if (arregloCarrito.length == 0) {

        anchorCheckout.href = "/UsuarioRegistrado/TiendaUsuario"

    } else {
        anchorCheckout.href = "/Pedido/Checkout"
    }


}




function ObtenerCarritoUsuario() {
    fetch("/Carrito/ObtenerCarritoUsuarioLogueado/", {
        method: "GET",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        CrearCards(respuesta)
    }).catch(error => {

        console.error("Error", error);
    });
}




function CrearCards(respuesta) {

    let esMovil = /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent) 


  
    
    const tablaBody = document.getElementById("tablaPrincipal");
    let total = 0;

    tablaBody.innerHTML = "";
    respuesta.arregloCarrito.forEach(carrito => {
       
        const tdImagenProducto = document.createElement("td");
        const imgProducto = document.createElement("img");

        const tdNombre = document.createElement("td");
        const h2Nombre = document.createElement("h2");

        const tdPrecio = document.createElement("td");
        const precio = document.createTextNode(carrito.productoDTO.precioProducto);

        const tdSumarRestar = document.createElement("td");
        const divSumarRestar = document.createElement("div");
        const divRestar = document.createElement("div");
        const formRestar = document.createElement("form");
        const botonRestar = document.createElement("button");
        const inputCantidad = document.createElement("input");
        const divSumar = document.createElement("div");
        const formSumar = document.createElement("form");
        const botonSumar = document.createElement("button");

        const tdEliminar = document.createElement("td");
        const formBorrar = document.createElement("form")
        const botonEliminar = document.createElement("button");
        const tdTotal = document.createElement("td")
  
        imgProducto.src = carrito.productoDTO.imagenProducto;

        imgProducto.classList.add("img-fluid");

        h2Nombre.textContent = carrito.productoDTO.nombreProducto;
        h2Nombre.classList.add("h5", "text-black");

        divSumarRestar.classList.add("input-group", "mb-3", "d-flex", "align-items-center", "quantity-container");
        divSumarRestar.style.maxWidth = "120px";
        divSumarRestar.style.margin = "0 auto";

        divRestar.classList.add("input-group-prepend");
        formRestar.appendChild(botonRestar);
        divRestar.appendChild(formRestar);

        botonRestar.classList.add("btn", "btn-outline-black", "decrease");
        botonRestar.textContent = "−";
        botonRestar.setAttribute("idCarrito", carrito.idCarrito)
        botonRestar.type = "submit"

        inputCantidad.type = "text";
        inputCantidad.classList.add("form-control", "text-center", "quantity-amount");
        inputCantidad.value = carrito.cantidadProducto;
        inputCantidad.placeholder = "";
        inputCantidad.disabled = true;

        divSumar.classList.add("input-group-append");
        formSumar.appendChild(botonSumar);
        divSumar.appendChild(formSumar);

        botonSumar.classList.add("btn", "btn-outline-black", "increase");
        botonSumar.textContent = "＋";
        botonSumar.setAttribute("idCarrito", carrito.idCarrito)
        botonSumar.type = "submit"

        formSumar.onsubmit = (event) => {
            ModificarCarrito(event, "Agregar");
        }

        formRestar.onsubmit = (event) => {
            ModificarCarrito(event, "Restar");
        }

        botonEliminar.classList.add("btn", "btn-black", "btn-sm");
        botonEliminar.type = "submit"
        botonEliminar.textContent = "X";
        botonEliminar.setAttribute("idCarrito", carrito.idCarrito)

        formBorrar.appendChild(botonEliminar)
        formBorrar.onsubmit = (event) => {
            EliminarCarrito(event);
        }

  


        tdImagenProducto.classList.add("product-thumbnail");
        tdNombre.classList.add("product-name");

        tdTotal.innerText = carrito.cantidadProducto * carrito.productoDTO.precioProducto  + "₡"
        divSumarRestar.appendChild(divRestar);
        divSumarRestar.appendChild(inputCantidad);
        divSumarRestar.appendChild(divSumar);

        tdImagenProducto.appendChild(imgProducto);
        tdNombre.appendChild(h2Nombre);
        tdPrecio.appendChild(precio);  

        tdSumarRestar.appendChild(divSumarRestar);
        tdEliminar.appendChild(formBorrar);
 
        const nuevaFila = document.createElement("tr");
        nuevaFila.appendChild(tdImagenProducto);
        nuevaFila.appendChild(tdNombre);
        nuevaFila.appendChild(tdPrecio); 
        nuevaFila.appendChild(tdSumarRestar);
        nuevaFila.appendChild(tdTotal)
        nuevaFila.appendChild(tdEliminar);
        nuevaFila.id = carrito.idCarrito;
        tablaBody.appendChild(nuevaFila);
        total += parseInt(tdTotal.innerText);
    });

    const strongTotal = document.getElementById("total")

    strongTotal.innerText = total + "₡";
   
    const strongTotalIva = document.getElementById("totalIva")

    let iva = total * 0.14 

    let totalConIva = total + iva;

    strongTotalIva.innerText = totalConIva.toFixed(2) + "₡";

    DesactivarBotonCheckout(respuesta.arregloCarrito)
}


function EliminarCarrito(event) {
    
    event.preventDefault();
    const boton = event.submitter;

    const idCarrito = boton.getAttribute("idCarrito");
    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;






    fetch("/Carrito/EliminarCarrito/" + idCarrito, {
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
            ObtenerCarritoUsuario() 
        } else {
            swal({
                text: respuesta.mensaje,
                icon: "error"
            })
        }
    }).catch(error => {
        console.error("Error", error);
    })

}


function ModificarCarrito(event, accion) {

    event.preventDefault();
    const boton = event.submitter;

    const idCarrito = boton.getAttribute("idCarrito");
    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;



    const carrito = {
        IdCarrito: idCarrito,
        Accion: accion
    }


    fetch("/Carrito/ModificarCarrito/", {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        },
        body: JSON.stringify(carrito)
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        if (respuesta.correcto) {
            swal({
                text: respuesta.mensaje,
                icon: "success"
            })
            ObtenerCarritoUsuario() 
        } else {
            swal({
                text: respuesta.mensaje,
                icon: "error"
            })
        }
    }).catch(error => {
        console.error("Error", error);
    })

}




//<td class="product-thumbnail">
//                                    <img src="~/img/CupcakePrueba.jpg" alt="Image" class="img-fluid">
//                                </td>
//                                <td class="product-name">
//                                    <h2 class="h5 text-black">Cupcake</h2>
//                                </td>
//                                <td>2500.00</td>
//                                <td>
//                                    <div class="input-group mb-3 d-flex align-items-center quantity-container" style="max-width: 120px;">
//                                        <div class="input-group-prepend">
//                                            <button class="btn btn-outline-black decrease" type="button">&minus;</button>
//                                        </div>
//                                        <input type="text" class="form-control text-center quantity-amount" value="1" placeholder="" aria-label="Example text with button addon" aria-describedby="button-addon1">
//                                        <div class="input-group-append">
//                                            <button class="btn btn-outline-black increase" type="button">&plus;</button>
//                                        </div>
//                                    </div>

//                                </td>
//                                <td>2500.00</td>
//                                <td><a href="#" class="btn btn-black btn-sm">X</a></td>
