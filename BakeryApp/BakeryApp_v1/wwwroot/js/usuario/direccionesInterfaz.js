import { LlenarSelectEliminarDireccion, LlenarSelectModificarDireccion } from "./selectDirecciones.js";
function ActualizarInterfazDireccionEliminada(idDireccion) {
    const selectEliminarDireccion = document.getElementById("direccionEliminar");

    const selectModificarDireccion = document.getElementById("direccionModificar");


    for (let i = 0; i < selectEliminarDireccion.options.length; i++) {
        if (selectEliminarDireccion.options[i].value == idDireccion) {
            selectEliminarDireccion.remove(i);
            break;
        }
    }

    for (let i = 0; i < selectModificarDireccion.options.length; i++) {
        if (selectModificarDireccion.options[i].value == idDireccion) {
            selectModificarDireccion.remove(i);
            break;
        }
    }


    ActualizarInterfazDireccionEliminadaTabla()
    
}


function ActualizarInterfazDireccionModificada(idDireccion, nombreDireccion) {
    const selectModificarDireccion = document.getElementById("direccionModificar");



    for (let i = 0; i < selectModificarDireccion.options.length; i++) {
        if (selectModificarDireccion.options[i].value == idDireccion) {
            selectModificarDireccion.options[i].textContent = nombreDireccion;
            break;
        }
    }

    const selectEliminarDireccion = document.getElementById("direccionEliminar");



    for (let i = 0; i < selectEliminarDireccion.options.length; i++) {
        if (selectEliminarDireccion.options[i].value == idDireccion) {
            selectEliminarDireccion.options[i].textContent = nombreDireccion
            break;
        }
    }


    ActualizarInterfazDireccionModificadaTabla()
}


function ActualizarInterfazDireccionAgregada() {
    fetch("/UsuarioRegistrado/ObtenerDireccionUsuario", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarTablaDireccionesInterfaz(respuesta.arregloDirecciones)
        LlenarSelectEliminarDireccion(respuesta.arregloDirecciones)
        LlenarSelectModificarDireccion(respuesta.arregloDirecciones)
        EliminarMensajeUsuarioSinDirecciones()
    }).catch(error => {
        console.error("Error", error);
    });
}


function EliminarMensajeUsuarioSinDirecciones() {
    const h3Mensaje = document.getElementById("mensajeSinDirecciones")

    h3Mensaje.remove()
}

function ActualizarInterfazDireccionEliminadaTabla() {
    fetch("/UsuarioRegistrado/ObtenerDireccionUsuario", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarTablaDireccionesInterfaz(respuesta.arregloDirecciones)
    }).catch(error => {
        console.error("Error", error);
    });
}


function ActualizarInterfazDireccionModificadaTabla() {
    fetch("/UsuarioRegistrado/ObtenerDireccionUsuario", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarTablaDireccionesInterfaz(respuesta.arregloDirecciones)
    }).catch(error => {
        console.error("Error", error);
    });
}




function LlenarTablaDireccionesInterfaz(arregloDirecciones) {
    const bodyTabla = document.getElementById("tablaDirecciones")

    //Se limpia la tabla
    bodyTabla.innerHTML = "";

    arregloDirecciones.forEach(direccion => {
        const filaTabla = document.createElement("tr")
        const tdNombre = document.createElement("td")
        const tdProvincia = document.createElement("td")
        const tdCanton = document.createElement("td")
        const tdDistrito = document.createElement("td")
        const tdDireccion = document.createElement("td")

        bodyTabla.appendChild(filaTabla)
        filaTabla.appendChild(tdNombre)
        filaTabla.appendChild(tdProvincia)
        filaTabla.appendChild(tdCanton)
        filaTabla.appendChild(tdDistrito)
        filaTabla.appendChild(tdDireccion)

        tdNombre.innerText = direccion.nombreDireccion
        tdProvincia.innerText = direccion.provinciaDTO.nombreProvincia
        tdCanton.innerText = direccion.cantonDTO.nombreCanton
        tdDistrito.innerText = direccion.distritoDTO.nombreDistrito
        tdDireccion.innerText = direccion.direccionExacta

    })

}

export {
    ActualizarInterfazDireccionEliminada, ActualizarInterfazDireccionModificada, ActualizarInterfazDireccionAgregada,
    ActualizarInterfazDireccionEliminadaTabla, ActualizarInterfazDireccionModificadaTabla, LlenarTablaDireccionesInterfaz
}