


document.addEventListener("DOMContentLoaded", function () {
    ObtenerTodosLasRecetas()
    ObtenerTodosLasCategorias()
});



function ObtenerProductoEspecifico() {


    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    idProducto = parametrosUrl.get("idProducto");


    fetch("/ProductoEmpleado/DevolverProductoEspecifico/" + idProducto, {
        method: "GET",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        RellenarDatosFormulario(respuesta.producto)
    }).catch(error => {
        console.error("Error", error);
    });
}




function RellenarDatosFormulario(producto) {
    const inputNombre = document.getElementById("nombreProducto")
    inputNombre.value = producto.nombreProducto

    const inputDescripcion = document.getElementById("descripcionProducto")
    inputDescripcion.value = producto.descripcionProducto;

    const imagen = document.getElementById("imagenProducto")


    const selectCategoria = document.getElementById("categorias")

    selectCategoria.value = producto.idCategoria;

    const selectReceta = document.getElementById("recetas")

    selectReceta.value = producto.idReceta;

    const inputPrecio = document.getElementById("precioProducto")

    inputPrecio.value = producto.precioProducto;

    imagen.src = "\\" + producto.imagenProducto;

    const imagenBorrar = document.getElementById("imagenBorrar")

    imagenBorrar.value = producto.imagenProducto;

}



function EditarProducto(event) {
    event.preventDefault();


    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const nombre = document.getElementById("nombreProducto").value;

    const detalles = document.getElementById("descripcionProducto").value;

    const precio = document.getElementById("precioProducto").value;

    const imagenProducto = document.getElementById("imagenFormulario").files[0];

    const selectCategoria = document.getElementById("categorias");

    const categoria = selectCategoria.value;

    const selectReceta = document.getElementById("recetas")

    const receta = selectReceta.value;

    const producto = new FormData();



  
    producto.append("IdProducto", parametrosUrl.get("idProducto"))
    producto.append("NombreProducto", nombre);
    producto.append("DescripcionProducto", detalles);
    producto.append("ArchivoProducto", imagenProducto);
    producto.append("PrecioProducto", precio);
    producto.append("IdCategoria", categoria);
    producto.append("IdReceta", receta);






    fetch("/ProductoEmpleado/GuardarEditado", {
        method: "PUT",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        },
        body: producto
    }).then(respuesta => {
        return respuesta.json()
            .then(respuesta => {
                if (respuesta.correcto) {

                    swal({
                        text: respuesta.mensajeInfo,
                        icon: "success"
                    }).then(() => {
                        EliminarImagenEditar()
                        window.location.href = respuesta.mensaje;
                    });;
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






function ObtenerTodosLasRecetas() {
    fetch("/ProductoEmpleado/ObtenerTodosLasRecetas", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarSelectRecetas(respuesta)
    }).catch(error => {
        console.error("Error", error);
    });
}


function ObtenerTodosLasCategorias() {
    fetch("/ProductoEmpleado/ObtenerTodosLasCategorias", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarSelectCategorias(respuesta)
        ObtenerProductoEspecifico();
    }).catch(error => {
        console.error("Error", error);
    });
}




function LlenarSelectRecetas(respuesta) {
    const select = document.getElementById("recetas");

    respuesta.arregloRecetas.forEach(receta => {
        const option = document.createElement("option");
        option.value = receta.idReceta;
        option.textContent = receta.nombreReceta;
        select.appendChild(option);
    });
}

function LlenarSelectCategorias(respuesta) {
    const select = document.getElementById("categorias");

    respuesta.arregloCategorias.forEach(categoria => {
        const option = document.createElement("option");
        option.value = categoria.idCategoria;
        option.textContent = categoria.nombreCategoria;
        select.appendChild(option);
    });
}



function mostrarImagenSeleccionada() {
    const imagenFormulario = document.getElementById("imagenFormulario").files[0];
    const imagenFront = document.getElementById("imagenProducto");

    const lector = new FileReader();

    lector.onload = function (evento) {
        imagenFront.src = evento.target.result;
    };

    if (imagenFormulario) {
        lector.readAsDataURL(imagenFormulario);
    }
}



function EliminarImagenEditar() {

    const rutaBorrar = document.getElementById("imagenBorrar").value;

    const producto = {
        ImagenProducto: rutaBorrar,
    }

    fetch("/ProductoEmpleado/BorrarImagenEditar", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest"
        },
        body: JSON.stringify(producto)
    }).then(respuesta => {
        return respuesta.json()
            .then(respuesta => {
                if (respuesta.mensaje) {
                    swal({
                        text: respuesta.mensaje
                    });
                }
            })
    }).catch(error => {
        console.error("Error", error);
    });

}






