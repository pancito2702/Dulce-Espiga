

import {
    LlenarSelectProvincias, LlenarSelectProvinciasModificar, LlenarSelectCantones, LlenarSelectCantonesModificar,
    LlenarSelectDistritos, LlenarSelectDistritosModificar, ObtenerCantones, ObtenerCantonesModificar, ObtenerDistritosModificar, ObtenerDistritos
} from "./selectsDatosDirecciones.js"

function ObtenerTodasLasProvincias() {
    fetch("/UsuarioRegistrado/ObtenerTodasProvincias", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarSelectProvincias(respuesta)
        LlenarSelectProvinciasModificar(respuesta)
    }).catch(error => {
        console.error("Error", error);
    });
}


function ObtenerTodasLosCantonesPorProvincia(idProvincia) {



    fetch("/UsuarioRegistrado/ObtenerTodasLosCantonesPorProvincia/" + idProvincia, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarSelectCantones(respuesta)
        LlenarSelectCantonesModificar(respuesta)
    }).catch(error => {
        console.error("Error", error);
    });
}


function ObtenerTodasLosDistritosPorCanton(idCanton) {

    fetch("/UsuarioRegistrado/ObtenerTodasLosDistritosPorCanton/" + idCanton, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarSelectDistritos(respuesta)
        LlenarSelectDistritosModificar(respuesta)
    }).catch(error => {
        console.error("Error", error);
    });
}


function IniciarEventListeners() {
    const selectProvincia = document.getElementById("provinciaAgregar");
    const selectCanton = document.getElementById("cantonAgregar");
    const selectProvinciaModificar = document.getElementById("provinciaModificar");
    const selectCantonModificar = document.getElementById("cantonModificar")


    selectProvinciaModificar.addEventListener("change", () => {
        ObtenerCantonesModificar();
        ObtenerDistritosModificar();
    });

    selectCantonModificar.addEventListener("change", () => {
        ObtenerDistritosModificar();
    });


    selectProvincia.addEventListener("change", () => {
        ObtenerCantones();
        ObtenerDistritos();

    });

    selectCanton.addEventListener("change", () => {
        ObtenerDistritos();
    });

}






export { ObtenerTodasLasProvincias, ObtenerTodasLosCantonesPorProvincia, ObtenerTodasLosDistritosPorCanton, IniciarEventListeners }