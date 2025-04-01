
import { ActualizarInterfazUsuarioModificado, ActualizarDatosUsuariosModificados } from "./interfazUsuario.js"

function EliminarPersona(event) {
    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const contra = document.getElementById("contraEliminar").value;

    const persona = {
        Contra: contra
    }

    fetch("/UsuarioRegistrado/EliminarUsuario", {
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
                    window.location.href = respuesta.mensaje
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




function EditarPersona(event) {
    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const nombre = document.getElementById("nombre").value;

    const primerApellido = document.getElementById("primerApellido").value;

    const segundoApellido = document.getElementById("segundoApellido").value;

    const telefono = document.getElementById("telefono").value;

    const persona = {
        Nombre: nombre,
        PrimerApellido: primerApellido,
        SegundoApellido: segundoApellido,
        Telefono: telefono
    }

    fetch("/UsuarioRegistrado/EditarUsuario", {
        method: "PUT",
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
                        text: respuesta.mensaje,
                        icon: "success"
                    });
                    ActualizarInterfazUsuarioModificado()
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






export { EliminarPersona, EditarPersona}

