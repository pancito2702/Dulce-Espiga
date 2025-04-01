
function GuardarMensajeBoletin(event) {



    event.preventDefault();


    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    idBoletin = parametrosUrl.get("idBoletin");

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const mensaje = document.getElementById("mensaje").value;

    const asunto = document.getElementById("asunto").value;

    const mensajePedido = {
        IdBoletin: idBoletin,
        Mensaje: mensaje,
        Asunto: asunto
    }
    


    fetch("/Marketing/GuardarMensajeBoletin", {
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