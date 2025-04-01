


document.addEventListener("DOMContentLoaded", function () {
    ObtenerTodosLosIngredientes();
    ObtenerRecetaEspecifica();
});



function ObtenerRecetaEspecifica() {


    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    idReceta = parametrosUrl.get("idReceta");


    fetch("/Receta/DevolverRecetaEspecifica/" + idReceta, {
        method: "GET",
        headers: {
            "X-Requested-With": "XMLHttpRequest",
        }
    }).then(respuesta => {
        return respuesta.json()
    }).then(respuesta => {
        RellenarDatosFormulario(respuesta.receta)
    }).catch(error => {
        console.error("Error", error);
    });
}





function ObtenerTodosLosIngredientes() {
    fetch("/Receta/ObtenerTodosLosIngredientes", {
        method: "GET",
        headers: {
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



function EditarReceta(event) {
    event.preventDefault();


    const url = window.location.search;

    const parametrosUrl = new URLSearchParams(url);

    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    const nombre = document.getElementById("nombreReceta").value;

    const instruccionesExactas = document.getElementById("instruccionesReceta").value;

    const selectIngredientes = document.getElementById("ingredientes")

    const ingredientes = obtenerValoresSelect(selectIngredientes)

    const receta = {
        IdReceta: parametrosUrl.get("idReceta"),
        NombreReceta: nombre,
        Instrucciones: instruccionesExactas,
        IdIngredientes: ingredientes.map(id => ({ idIngrediente: id }))
    }


    fetch("/Receta/GuardarEditada", {
        method: "PUT",
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

function RellenarDatosFormulario(receta) {

    const nombre = document.getElementById("nombreReceta");

    nombre.value = receta.nombreReceta

    const instrucciones = document.getElementById("instruccionesReceta")

    instrucciones.value = receta.instrucciones

    const ulIngredientes = document.getElementById("listaIngredientes")


    receta.ingredientes.forEach(ingrediente => {
        const liIngrediente = document.createElement("li")
        ulIngredientes.appendChild(liIngrediente)
        liIngrediente.textContent = ingrediente.nombreIngrediente
    })

}

function obtenerValoresSelect(select) {
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