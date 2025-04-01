
function AgregarBoletin(event) {

    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;



    fetch("/UsuarioRegistrado/SuscribirseBoletin", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        }
    }).then(respuesta => {
        return respuesta.json()
            .then(respuesta => {
                if (respuesta.correcto) {
                    swal({
                        text: respuesta.mensaje,
                        icon: "success"
                    });
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

function EliminarBoletin(event) {

    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;



    fetch("/UsuarioRegistrado/BorrarUsuarioBoletin", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        }
    }).then(respuesta => {
        return respuesta.json()
            .then(respuesta => {
                if (respuesta.correcto) {
                    swal({
                        text: respuesta.mensaje,
                        icon: "success"
                    });
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

export { AgregarBoletin, EliminarBoletin }