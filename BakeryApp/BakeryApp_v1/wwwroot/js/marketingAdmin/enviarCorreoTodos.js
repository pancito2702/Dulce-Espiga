
function GuardarMensajeBoletin(event) {



    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const mensaje = document.getElementById("mensaje").value;

    const asunto = document.getElementById("asunto").value;

    const mensajePedido = {
        Mensaje: mensaje,
        Asunto: asunto
    }
    


    fetch("/Marketing/GuardarMensajeBoletinTodos", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        },
        body: JSON.stringify(mensajePedido)
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