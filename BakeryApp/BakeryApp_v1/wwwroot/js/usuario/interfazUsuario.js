


function ActualizarInterfazUsuarioModificado() {
    fetch("/UsuarioRegistrado/ObtenerDatosUsuarioLogueado", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {

        ActualizarDatosUsuariosModificados(respuesta.mensaje)
    }).catch(error => {
        console.error("Error", error);
    });
}

function ActualizarDatosUsuariosModificados(mensaje) {
    const nombre = document.getElementById("idNombre")

    const primerApellido = document.getElementById("idPrimerApellido")

    const segundoApellido = document.getElementById("idSegundoApellido")

    const correo = document.getElementById("idCorreo")

    const telefono = document.getElementById("idTelefono")

    nombre.innerText = mensaje.nombre

    primerApellido.innerText = mensaje.primerApellido

    segundoApellido.innerText = mensaje.segundoApellido

    correo.innerText = mensaje.correo;

    telefono.innerText = mensaje.telefono

}

export { ActualizarInterfazUsuarioModificado, ActualizarDatosUsuariosModificados }