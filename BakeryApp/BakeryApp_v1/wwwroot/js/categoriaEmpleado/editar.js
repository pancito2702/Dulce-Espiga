

document.addEventListener("DOMContentLoaded", function () {
    ObtenerCategoriaEspecifica()
});


function ObtenerCategoriaEspecifica() {


    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    IdCategoria = parametrosUrl.get("idCategoria");


    fetch("/CategoriaEmpleado/DevolverCategoriaEspecifica/" + IdCategoria, {
        method: "GET",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        RellenarDatosFormulario(respuesta.categoria)
    }).catch(error => {
        console.error("Error", error);
    });
}


function RellenarDatosFormulario(categoria) {
    const inputNombre = document.getElementById("nombreCategoria")
    inputNombre.value = categoria.nombreCategoria;

    const inputDescripcion = document.getElementById("descripcionCategoria")
    inputDescripcion.value = categoria.descripcionCategoria;

    const imagen = document.getElementById("imagenCategoria")

    imagen.src = "\\" + categoria.imagenCategoria;

    const imagenBorrar = document.getElementById("imagenBorrar")
    imagenBorrar.value = categoria.imagenCategoria;
}




function mostrarImagenSeleccionada() {
    const imagenCategoria = document.getElementById("imagenFormulario").files[0];
    const imagenFront = document.getElementById("imagenCategoria");

    const lector = new FileReader();

    lector.onload = function (evento) {
        imagenFront.src = evento.target.result;
    };

    if (imagenCategoria) {
        lector.readAsDataURL(imagenCategoria);
    }
}

function EditarCategoria(event) {
    event.preventDefault();


    if (VerificarImagenFormulario()) {
        swal({
            text: "Debe seleccionar una imagen"
        });
        return
    }

    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    var token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const nombre = document.getElementById("nombreCategoria").value;

    const detalles = document.getElementById("descripcionCategoria").value;

    const imagenCategoria = document.getElementById("imagenFormulario").files[0];



    const categoria = new FormData();

    categoria.append("IdCategoria", parametrosUrl.get("idCategoria"))
    categoria.append("NombreCategoria", nombre);
    categoria.append("DescripcionCategoria", detalles);
    categoria.append("ArchivoCategoria", imagenCategoria);

    fetch("/CategoriaEmpleado/GuardarEditada", {
        method: "PUT",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        },
        body: categoria
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

function VerificarImagenFormulario() {

    const imagenCategoria = document.getElementById("imagenFormulario").files[0];

    if (imagenCategoria === undefined) {
        return true
    }
    return false
}




function EliminarImagenEditar() {

    const rutaBorrar = document.getElementById("imagenBorrar").value;

    const categoria = {
        ImagenCategoria: rutaBorrar
    }

    fetch("/CategoriaEmpleado/BorrarImagenEditar", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest"
        },
        body: JSON.stringify(categoria)
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
