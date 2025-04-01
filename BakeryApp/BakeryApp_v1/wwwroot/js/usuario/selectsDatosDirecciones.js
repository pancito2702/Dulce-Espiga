
import { ObtenerTodasLasProvincias, ObtenerTodasLosCantonesPorProvincia, ObtenerTodasLosDistritosPorCanton }
from "./datosDirecciones.js";

function ObtenerCantonesModificar() {
    // Como el orden de ejecucion se establece bien, aca se obtiene el id de la provincia
    const selectProvincia = document.getElementById("provinciaModificar");

    let idProvincia = selectProvincia.value;





    ObtenerTodasLosCantonesPorProvincia(idProvincia);
}


function ObtenerDistritosModificar() {
    // Como el orden de ejecucion se establece bien, aca se obtiene el id del canton
    const selectCanton = document.getElementById("cantonModificar");

    let idCanton = selectCanton.value;



    ObtenerTodasLosDistritosPorCanton(idCanton)
}





function ObtenerCantones() {
    // Como el orden de ejecucion se establece bien, aca se obtiene el id de la provincia
    const selectProvincia = document.getElementById("provinciaAgregar");

    let idProvincia = selectProvincia.value;





    ObtenerTodasLosCantonesPorProvincia(idProvincia);
}


function ObtenerDistritos() {
    // Como el orden de ejecucion se establece bien, aca se obtiene el id del canton
    const selectCanton = document.getElementById("cantonAgregar");

    let idCanton = selectCanton.value;



    ObtenerTodasLosDistritosPorCanton(idCanton)
}



function LlenarSelectProvincias(respuesta) {
    const select = document.getElementById("provinciaAgregar");

    select.innerHTML = "";
    respuesta.arregloProvincias.forEach(provincia => {
        const option = document.createElement("option");
        option.value = provincia.idProvincia;
        option.textContent = provincia.nombreProvincia;
        select.appendChild(option);
    });

    // Se establece como seleccionado el primer elemento del select de provincias
    select.selectedIndex = 0;
    ObtenerCantones();



}

function LlenarSelectProvinciasModificar(respuesta) {
    const select = document.getElementById("provinciaModificar");

    select.innerHTML = "";
    respuesta.arregloProvincias.forEach(provincia => {
        const option = document.createElement("option");
        option.value = provincia.idProvincia;
        option.textContent = provincia.nombreProvincia;
        select.appendChild(option);
    });

    // Se establece como seleccionado el primer elemento del select de provincias
    select.selectedIndex = 0;
    ObtenerCantonesModificar();



}


function LlenarSelectCantones(respuesta) {
    const select = document.getElementById("cantonAgregar");

    select.innerHTML = ""


    respuesta.arregloCantones.forEach(canton => {
        const option = document.createElement("option");
        option.value = canton.idCanton;
        option.textContent = canton.nombreCanton;
        select.appendChild(option);
    });

    select.selectedIndex = 0;
    // Se establece como seleccionado el primer elemento del select de cantones
    ObtenerDistritos();



}

function LlenarSelectCantonesModificar(respuesta) {
    const select = document.getElementById("cantonModificar");

    select.innerHTML = ""


    respuesta.arregloCantones.forEach(canton => {
        const option = document.createElement("option");
        option.value = canton.idCanton;
        option.textContent = canton.nombreCanton;
        select.appendChild(option);
    });

    select.selectedIndex = 0;
    // Se establece como seleccionado el primer elemento del select de cantones
    ObtenerDistritosModificar();



}


function LlenarSelectDistritos(respuesta) {
    const select = document.getElementById("distritoAgregar");

    select.innerHTML = ""

    respuesta.arregloDistritos.forEach(distrito => {
        const option = document.createElement("option");
        option.value = distrito.idDistrito;
        option.textContent = distrito.nombreDistrito;
        select.appendChild(option);
    });

}

function LlenarSelectDistritosModificar(respuesta) {
    const select = document.getElementById("distritoModificar");

    select.innerHTML = ""

    respuesta.arregloDistritos.forEach(distrito => {
        const option = document.createElement("option");
        option.value = distrito.idDistrito;
        option.textContent = distrito.nombreDistrito;
        select.appendChild(option);
    });

}

export {
    LlenarSelectProvincias, LlenarSelectProvinciasModificar, LlenarSelectCantones, LlenarSelectCantonesModificar,
    LlenarSelectDistritos, LlenarSelectDistritosModificar, ObtenerCantones, ObtenerCantonesModificar, ObtenerDistritosModificar, ObtenerDistritos
}