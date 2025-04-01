


document.addEventListener("DOMContentLoaded", function () {
    ObtenerTodasLasUnidadesDeMedida();
});


function ObtenerTodasLasUnidadesDeMedida() {
    fetch("/IngredienteEmpleado/ObtenerTodasLasUnidadesDeMedida", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        LlenarSelect(respuesta)
        ObtenerIngredienteEspecifico();
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


function ObtenerIngredienteEspecifico() {


    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    idIngrediente = parametrosUrl.get("idIngrediente");


    fetch("/IngredienteEmpleado/DevolverIngredienteEspecifico/" + idIngrediente, {
        method: "GET",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        RellenarDatosFormulario(respuesta.ingrediente)
    }).catch(error => {
        console.error("Error", error);
    });
}



function EditarIngrediente(event) {
    event.preventDefault();


    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const nombre = document.getElementById("nombreIngrediente").value;

    const detalle = document.getElementById("detalleIngrediente").value;

    const cantidad = document.getElementById("cantidadIngrediente").value;


    const precioUnitario = document.getElementById("precioIngrediente").value;

    const fechaVencimiento = document.getElementById("fechaVencimiento").value


    const selectUnidadMedida = document.getElementById("unidadMedida");

    const unidadMedida = selectUnidadMedida.value;


    const ingrediente = {
        idIngrediente: parametrosUrl.get("idIngrediente"),
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



    fetch("/IngredienteEmpleado/GuardarEditado", {
        method: "PUT",
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


function RellenarDatosFormulario(ingrediente) {

    const nombre = document.getElementById("nombreIngrediente");

    const descripcion = document.getElementById("detalleIngrediente");

    const cantidad = document.getElementById("cantidadIngrediente");


    const selectUnidadMedida = document.getElementById("unidadMedida");

    const precio = document.getElementById("precioIngrediente")

    const fechaVencimiento = document.getElementById("fechaVencimiento")

    nombre.value = ingrediente.nombreIngrediente;

    descripcion.value = ingrediente.descripcionIngrediente;

    cantidad.value = ingrediente.cantidadIngrediente;


    precio.value = ingrediente.precioUnidadIngrediente;

    fechaVencimiento.value = ingrediente.fechaCaducidadIngrediente;
  
    selectUnidadMedida.value = ingrediente.unidadMedidaIngrediente

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