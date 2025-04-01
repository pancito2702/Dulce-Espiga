


document.addEventListener("DOMContentLoaded", function () {
    ObtenerTodosLasRecetas()
    ObtenerTodosLasCategorias()
});



function GuardarProducto(event) {
    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const nombre = document.getElementById("nombreProducto").value;

    const detalles = document.getElementById("descripcionProducto").value;

    const precio = document.getElementById("precioProducto").value;

    const imagenProducto = document.getElementById("imagenProducto").files[0];

   

    const selectCategoria = document.getElementById("categorias");

    const categoria = selectCategoria.value;

    const selectReceta = document.getElementById("recetas")

    const receta = selectReceta.value;

    const producto = new FormData();


    producto.append("NombreProducto", nombre);
    producto.append("DescripcionProducto", detalles);
    producto.append("ArchivoProducto", imagenProducto);
    producto.append("PrecioProducto", precio);
    producto.append("IdCategoria", categoria);
    producto.append("IdReceta", receta);


    fetch("/Producto/GuardarProducto", {
        method: "POST",
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
    fetch("/Producto/ObtenerTodosLasRecetas", {
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
    fetch("/Producto/ObtenerTodosLasCategorias", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarSelectCategorias(respuesta)
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
    const imagenProducto = document.getElementById("imagenProducto").files[0];
    const imagenFront = document.getElementById("imagenFront");

    const lector = new FileReader();

    lector.onload = function (evento) {
        imagenFront.src = evento.target.result;
    };

    if (imagenProducto) {
        lector.readAsDataURL(imagenProducto);
    }
}
