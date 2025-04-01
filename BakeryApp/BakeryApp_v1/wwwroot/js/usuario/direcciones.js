

import {
    ActualizarInterfazDireccionEliminada, ActualizarInterfazDireccionModificada, ActualizarInterfazDireccionAgregada,
    ActualizarInterfazDireccionEliminadaTabla, ActualizarInterfazDireccionModificadaTabla, LlenarTablaDireccionesInterfaz
} from "./direccionesInterfaz.js";

import { LlenarSelectEliminarDireccion, LlenarSelectModificarDireccion } from "./selectDirecciones.js";

function ObtenerDireccionesUsuario() {
    fetch("/UsuarioRegistrado/ObtenerDireccionUsuario", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarTablaDirecciones(respuesta.arregloDirecciones)
        LlenarSelectEliminarDireccion(respuesta.arregloDirecciones)
        LlenarSelectModificarDireccion(respuesta.arregloDirecciones)
    }).catch(error => {
        console.error("Error", error);
    });
}




function LlenarTablaDirecciones(arregloDirecciones) {
    const bodyTabla = document.getElementById("tablaDirecciones")

    if (arregloDirecciones.length == 0) {
        const divTabla = document.getElementById("cardTabla")
        const divHeader = document.createElement("div")
        const h3Mensaje = document.createElement("h3")
        h3Mensaje.setAttribute("id", "mensajeSinDirecciones")
        divHeader.classList.add("card-header")
        h3Mensaje.classList.add("card-title")

        divTabla.append(divHeader)
        divHeader.appendChild(h3Mensaje)

        h3Mensaje.innerText = "El usuario no tiene direcciones asociadas"
    }

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



function EliminarDireccionUsuario(event) {
    event.preventDefault()

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
    const selectEliminarDireccion = document.getElementById("direccionEliminar");

    const idDireccion = selectEliminarDireccion.value;


    if (idDireccion == "") {
        swal({
            text: "No puede eliminar una direccion, ya que no hay direcciones registradas",
            icon: "error"
        })
    }

    fetch("/UsuarioRegistrado/EliminarDireccion/" + idDireccion, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        }
    }).then(respuesta => respuesta.json())
        .then(respuesta => {
            if (respuesta.correcto) {
                swal({
                    text: respuesta.mensaje,
                    icon: "success"
                });
                ActualizarInterfazDireccionEliminada(idDireccion)

            } else {
                swal({
                    text: respuesta.mensaje,
                    icon: "error"
                });
            }

        }).catch(error => {
            console.error("Error", error);
        });
}


function ModificarDireccion(event) {
    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const selectModificarDireccion = document.getElementById("direccionModificar");
    const idDireccion = selectModificarDireccion.value;
    const nombreDireccion = document.getElementById("nombreDireccionModificar").value;
    const direccionExacta = document.getElementById("direccionExactaModificar").value;
    const selectProvincia = document.getElementById("provinciaModificar");
    const selectCanton = document.getElementById("cantonModificar");
    const selectDistrito = document.getElementById("distritoModificar")

    const idProvincia = selectProvincia.value;
    const idCanton = selectCanton.value;
    const idDistrito = selectDistrito.value;





    const direccion = {
        IdDireccion: idDireccion,
        NombreDireccion: nombreDireccion,
        DireccionExacta: direccionExacta,
        IdProvincia: idProvincia,
        idCanton: idCanton,
        IdDistrito: idDistrito
    }

    fetch("/UsuarioRegistrado/ModificarDireccion", {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        },
        body: JSON.stringify(direccion)
    }).then(respuesta => {
        return respuesta.json()
            .then(respuesta => {
                if (respuesta.correcto) {
                    swal({
                        text: respuesta.mensaje,
                        icon: "success"
                    });
                    ActualizarInterfazDireccionModificada(idDireccion, nombreDireccion)

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





function AgregarDireccion(event) {
    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;


    const nombreDireccion = document.getElementById("nombreDireccionAgregar").value;
    const direccionExacta = document.getElementById("direccionExactaAgregar").value;
    const selectProvincia = document.getElementById("provinciaAgregar");
    const selectCanton = document.getElementById("cantonAgregar");
    const selectDistrito = document.getElementById("distritoAgregar")

    const idProvincia = selectProvincia.value;
    const idCanton = selectCanton.value;
    const idDistrito = selectDistrito.value;




    const direccion = {
        NombreDireccion: nombreDireccion,
        DireccionExacta: direccionExacta,
        IdProvincia: idProvincia,
        idCanton: idCanton,
        IdDistrito: idDistrito
    }

    fetch("/UsuarioRegistrado/AgregarDireccion", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        },
        body: JSON.stringify(direccion)
    }).then(respuesta => {
        return respuesta.json()
            .then(respuesta => {
                if (respuesta.correcto) {
                    swal({
                        text: respuesta.mensaje,
                        icon: "success"
                    });
                    ActualizarInterfazDireccionAgregada();
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



export {
    ObtenerDireccionesUsuario, LlenarSelectEliminarDireccion, LlenarSelectModificarDireccion, LlenarTablaDirecciones,
    EliminarDireccionUsuario, ModificarDireccion, AgregarDireccion
}