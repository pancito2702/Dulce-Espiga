


document.addEventListener("DOMContentLoaded", function () {
    ObtenerTodosLosRoles();
});




function ObtenerTodosLosRoles() {
    fetch("/Perfil/ObtenerTodosLosRoles", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarSelect(respuesta)
    }).catch(error => {
        console.error("Error", error);
    });
}


function LlenarSelect(respuesta) {
    const select = document.getElementById("rol");

    respuesta.arregloRoles.forEach(rol => {
        const option = document.createElement("option");
        option.value = rol.idRol;
        option.textContent = rol.nombreRol;
        select.appendChild(option);
    });
}


function GuardarPersona(event) {
    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const nombre = document.getElementById("nombrePersona").value;

    const primerApellido = document.getElementById("primerApellidoPersona").value;

    const segundoApellido = document.getElementById("segundoApellidoPersona").value;

    const correo = document.getElementById("correo").value;

    const contra = document.getElementById("contra").value;

    const telefono = document.getElementById("telefono").value;

    const selectRol = document.getElementById("rol");

    const idRol = selectRol.value;


    const persona = {
        Nombre: nombre,
        PrimerApellido: primerApellido,
        SegundoApellido: segundoApellido,
        Correo: correo,
        Contra: contra,
        Telefono: telefono,
        IdRol: idRol
    }

    fetch("/Perfil/GuardarPerfil", {
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