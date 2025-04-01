
document.addEventListener("DOMContentLoaded", function () {
    ObtenerCantidadPedidosNuevos();
    ObtenerCantidadPersonas();
});

function ObtenerCantidadPedidosNuevos() {
    fetch("/Administrador/ObtenerPedidosNuevos", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarInterfazPedidosNuevos(respuesta)
    }).catch(error => {
        console.error("Error", error);
    });
}

function ObtenerCantidadPersonas() {
    fetch("/Administrador/ObtenerTotalPersonas", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarInterfazCantidadPersonas(respuesta)
    }).catch(error => {
        console.error("Error", error);
    });
}


function LlenarInterfazPedidosNuevos(respuesta) {
    const textoCantidad = document.getElementById("ordenesNuevas")
    textoCantidad.innerText = respuesta.cantidadPedidosNuevos;
}

function LlenarInterfazCantidadPersonas(respuesta) {
    const textoCantidad = document.getElementById("cantidadPersonas")
    textoCantidad.innerText = respuesta.cantidadPersonas;
}