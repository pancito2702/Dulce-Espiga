





function BuscarProducto(event) {

    event.preventDefault()

    const nombreProducto = document.getElementById("nombreProducto").value;



    if (nombreProducto == "") {
        swal({
            text: "El nombre del producto no puede estar vacio",
            icon: "error"
        });
        return;
    }
    



    fetch("/UsuarioRegistrado/BuscarProducto/" + nombreProducto, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
            .then(respuesta => {
                if (respuesta.correcto) {
                    window.location.href = respuesta.mensaje;
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


