
document.addEventListener("DOMContentLoaded", function () {
    ObtenerCantidadPedidosNuevos();
});

function ObtenerCantidadPedidosNuevos() {
    fetch("/Empleado/ObtenerPedidosNuevos", {
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


function LlenarInterfazPedidosNuevos(respuesta) {
    const textoCantidad = document.getElementById("ordenesNuevas")
    textoCantidad.innerText = respuesta.cantidadPedidosNuevos;
}
