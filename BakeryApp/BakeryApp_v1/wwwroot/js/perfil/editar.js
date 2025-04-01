


document.addEventListener("DOMContentLoaded", function () {
    ObtenerTodosLosRoles();
});



function ObtenerPerfilEspecifico() {


    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    idPersona = parametrosUrl.get("idPersona");


    fetch("/Perfil/DevolverPerfilEspecifico/" + idPersona, {
        method: "GET",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        RellenarDatosFormulario(respuesta.persona)
    }).catch(error => {
        console.error("Error", error);
    });
}





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
        ObtenerPerfilEspecifico()
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


function EditarPersona(event) {
    event.preventDefault();


    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

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
        IdPersona: parametrosUrl.get("idPersona"),
        Nombre: nombre,
        PrimerApellido: primerApellido,
        SegundoApellido: segundoApellido,
        Correo: correo,
        Contra: contra,
        Telefono: telefono,
        IdRol: idRol
    }

    fetch("/Perfil/GuardarEditado", {
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

function RellenarDatosFormulario(persona) {

    const nombre = document.getElementById("nombrePersona");

    nombre.value = persona.nombre

    const primerApellido = document.getElementById("primerApellidoPersona");

    primerApellido.value = persona.primerApellido

    const segundoApellido = document.getElementById("segundoApellidoPersona");

    segundoApellido.value = persona.segundoApellido;

    const correo = document.getElementById("correo");

    correo.value = persona.correo

    const contra = document.getElementById("contra");

    contra.value = "";

    const telefono = document.getElementById("telefono");

    telefono.value = persona.telefono;

    const selectRol = document.getElementById("rol");

    selectRol.value = persona.idRol;

}