

document.addEventListener("DOMContentLoaded", function () {
    LlamarAlInicio()
    ObtenerTodasLasUnidadesDeMedida();
});

function LlamarAlInicio() {
    const cantidad = document.getElementById("cantidadIngrediente");


    const precioUnitario = document.getElementById("precioIngrediente");

    const fechaVencimiento = document.getElementById("fechaVencimiento")

    const FechaActual = new Date();
    fechaVencimiento.value = FechaActual.toISOString().substr(0, 10);

    cantidad.value = 0;

    precioUnitario.value = 0;
}

function ObtenerTodasLasUnidadesDeMedida() {
    fetch("/Ingrediente/ObtenerTodasLasUnidadesDeMedida", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarSelect(respuesta)
    }).catch(error => {
        console.error("Error", error);
    });
}


function LlenarSelect(respuesta) {
    const select = document.getElementById("unidadMedida");

    respuesta.arregloUnidadesMedida.forEach(unidadMedida => {
        const option = document.createElement("option");
        option.value = unidadMedida.idUnidad;
        option.textContent = unidadMedida.nombreUnidad;
        select.appendChild(option);
    });
}


function GuardarIngrediente(event) {
    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const nombre = document.getElementById("nombreIngrediente").value;

    const detalle = document.getElementById("detalleIngrediente").value;

    const selectUnidadMedida = document.getElementById("unidadMedida");

    const unidadMedida = selectUnidadMedida.value;



    const cantidad = document.getElementById("cantidadIngrediente").value;


    const precioUnitario = document.getElementById("precioIngrediente").value;

    const fechaVencimiento = document.getElementById("fechaVencimiento").value


    const ingrediente = {
        NombreIngrediente: nombre,
        DescripcionIngrediente: detalle,
        CantidadIngrediente: cantidad,
        UnidadMedidaIngrediente: unidadMedida,
        PrecioUnidadIngrediente: precioUnitario,
        FechaCaducidadIngrediente: fechaVencimiento
    }






    if (VerificarCantidadVacia(cantidad)) {
        swal({
            text: "El campo de cantidad no puede estar vacio",
            icon: "error"
        });
        return;
    }

    if (VerificarPrecioVacio(precioUnitario)) {
        swal({
            text: "El campo de precio unitario no puede estar vacio",
            icon: "error"
        });
        return;
    }


    if (VerificarFechaVacia(fechaVencimiento)) {
        swal({
            text: "La fecha no puede estar vacia",
            icon: "error"
        });
        return;
    }


    fetch("/Ingrediente/GuardarIngrediente", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        },
        body: JSON.stringify(ingrediente)
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


function VerificarFechaVacia(fechaVencimiento) {



    if (fechaVencimiento == "") {
        return true;
    }

    return false;
}

function VerificarCantidadVacia(cantidad) {

    if (cantidad === "") {

        return true;
    }

    return false;
}

function VerificarPrecioVacio(precio) {

    if (precio === "") {

        return true;
    }

    return false;
}