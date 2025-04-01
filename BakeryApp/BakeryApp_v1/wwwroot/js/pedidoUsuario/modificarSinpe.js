

function ModificarPagoSinpe(event) {
    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    const idPedido = parametrosUrl.get("idPedido");

    const archivoSinpe = document.getElementById("ArchivoSinpe").files[0]



    const pedido = new FormData();


    pedido.append("IdPedido", idPedido)





    pedido.append("PagoSinpe.ArchivoSinpe", archivoSinpe)


    fetch("/Pedido/ModificarPagoSinpe", {
        method: "PUT",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        },
        body: pedido
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
    const imagenSinpe = document.getElementById("ArchivoSinpe").files[0];
    const imagenFront = document.getElementById("imagenFront");

    const lector = new FileReader();

    lector.onload = function (evento) {
        imagenFront.src = evento.target.result;
    };

    if (imagenSinpe) {
        lector.readAsDataURL(imagenSinpe);
    }
}
