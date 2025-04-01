
function LlenarSelectEliminarDireccion(arregloDirecciones) {
    const select = document.getElementById("direccionEliminar");



    select.innerHTML = "";
    arregloDirecciones.forEach(direccion => {
        const option = document.createElement("option");
        option.value = direccion.idDireccion;
        option.textContent = direccion.nombreDireccion;
        select.appendChild(option);
    });

}

function LlenarSelectModificarDireccion(arregloDirecciones) {
    const select = document.getElementById("direccionModificar");



    select.innerHTML = "";
    arregloDirecciones.forEach(direccion => {
        const option = document.createElement("option");
        option.value = direccion.idDireccion;
        option.textContent = direccion.nombreDireccion;
        select.appendChild(option);
    });

}

export { LlenarSelectEliminarDireccion, LlenarSelectModificarDireccion }