


document.addEventListener("DOMContentLoaded", function () {
    ObtenerTodosLosIngredientes();
});




function ObtenerTodosLosIngredientes() {
    fetch("/Receta/ObtenerTodosLosIngredientes", {
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
    const select = document.getElementById("ingredientes");

    respuesta.arregloIngredientes.forEach(ingrediente => {
        const option = document.createElement("option");
        option.value = ingrediente.idIngrediente;
        option.textContent = ingrediente.nombreIngrediente;
        select.appendChild(option);
    });
}


function GuardarReceta(event) {
    event.preventDefault();

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const nombre = document.getElementById("nombreReceta").value;

    const instruccionesExactas = document.getElementById("instruccionesReceta").value;

    const selectIngredientes = document.getElementById("ingredientes")

    const ingredientes = ObtenerValoresSelect(selectIngredientes)

    const receta = {
        NombreReceta: nombre,
        Instrucciones: instruccionesExactas,
        IdIngredientes: ingredientes.map(id => ({ idIngrediente: id }))
    }

    fetch("/Receta/GuardarReceta", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "X-Requested-With": "XMLHttpRequest",
            "RequestVerificationToken": token
        },
        body: JSON.stringify(receta)
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

function ObtenerValoresSelect(select) {
    var valores = [];
    var opciones = select && select.options;
    var opt;

    for (var i = 0, iLen = opciones.length; i < iLen; i++) {
        opt = opciones[i];

        if (opt.selected) {
            valores.push(opt.value);
        }
    }
    return valores;
}