



function GuardarCategoria(event) {
    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const nombre = document.getElementById("nombre").value;

    const detalles = document.getElementById("detalles").value;

    const imagenCategoria = document.getElementById("imagenCategoria").files[0];



    const categoria = new FormData();


    categoria.append("NombreCategoria", nombre);
    categoria.append("DescripcionCategoria", detalles);
    categoria.append("ArchivoCategoria", imagenCategoria);

    fetch("/CategoriaEmpleado/GuardarCategoria", {
        method: "POST",
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




function mostrarImagenSeleccionada() {
    const imagenCategoria = document.getElementById("imagenCategoria").files[0];
    const imagenFront = document.getElementById("imagenFront");

    const lector = new FileReader();

    lector.onload = function (evento) {
        imagenFront.src = evento.target.result;
    };

    if (imagenCategoria) {
        lector.readAsDataURL(imagenCategoria);
    }
}

