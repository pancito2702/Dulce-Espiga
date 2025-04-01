





function GuardarPersona(event) {
    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const nombre = document.getElementById("nombrePersona").value;

    const primerApellido = document.getElementById("primerApellidoPersona").value;

    const segundoApellido = document.getElementById("segundoApellidoPersona").value;

    const correo = document.getElementById("correo").value;

    const contra = document.getElementById("contra").value;

    const telefono = document.getElementById("telefono").value;

    const persona = {
        Nombre: nombre,
        PrimerApellido: primerApellido,
        SegundoApellido: segundoApellido,
        Correo: correo,
        Contra: contra,
        Telefono: telefono,
        IdRol: 3
    }

    fetch("/Home/Registrarse", {
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

function formatoTelefono(input) {

    var telefono = input.value.replace(/\-/g, '');

    if (telefono.length > 4) {
        telefono = telefono.substring(0, 4) + '-' + telefono.substring(4);
    }

    if (telefono.length > 9) {
        telefono = telefono.substring(0,8);
    }

    input.value = telefono;
}