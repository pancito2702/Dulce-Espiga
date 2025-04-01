





function EnviarCorreo(event) {
    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const correo = document.getElementById("correo").value;

    const botonSubmit = document.getElementById("botonSubmit");

    botonSubmit.disabled = true;

    const persona = {
        Correo: correo
    }

    fetch("/Home/EnviarCorreoContra", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        },
        body: JSON.stringify(persona)
    }).then(respuesta => {
        return respuesta.json()
            .then(respuesta => {
                if (respuesta.correcto) {
                    swal({
                        text: respuesta.mensajeInfo,
                        icon: "success"
                    }).then(() => {
                        window.location.href = respuesta.mensaje
                    })

                } else {
                    swal({
                        text: respuesta.mensaje,
                        icon: "error"
                    });
                }
               
            })
    }).catch(error => {
        console.error("Error", error);
    }).finally(() => {
        botonSubmit.disabled = false;
    });
}
