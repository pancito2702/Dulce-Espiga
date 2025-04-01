





function IniciarSesion(event) {
    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
    const correo = document.getElementById("correo").value;

    const contra = document.getElementById("contra").value;


    const persona = {
        Correo: correo,
        Contra: contra,
    }

    fetch("/Home/IniciarSesion", {
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
                    window.location.href = respuesta.mensaje;
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

