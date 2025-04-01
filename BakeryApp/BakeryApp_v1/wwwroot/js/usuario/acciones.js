

import {
    ObtenerDireccionesUsuario, LlenarSelectEliminarDireccion, LlenarSelectModificarDireccion, LlenarTablaDirecciones,
    EliminarDireccionUsuario, ModificarDireccion, AgregarDireccion
} from "./direcciones.js"

import { AgregarBoletin, EliminarBoletin } from "./boletin.js";

import { EliminarPersona, EditarPersona } from "./accionesUsuario.js"

import { ObtenerTodosLosPedidosPorCliente, CancelarPedido } from "./pedidoUsuario.js"

import { ObtenerTodasLasProvincias, IniciarEventListeners } from "./datosDirecciones.js"

document.addEventListener('DOMContentLoaded', () => {
    ObtenerDireccionesUsuario()


    IniciarEventListeners()

    IniciarTabPane()

    ObtenerTodosLosPedidosPorCliente()
});


function IniciarTabPane() {

    const contenedorPaneles = document.getElementById("contenerPaneles")
    const links = contenedorPaneles.querySelectorAll('.nav-link');

    links.forEach(link => {
        link.addEventListener("click", (event) => {

            event.preventDefault();

            links.forEach(link => {
                link.classList.remove("active")
            });


            const panelActual = document.querySelector(link.getAttribute("href"));
            document.querySelectorAll(".tab-pane").forEach(pane => {
                pane.classList.remove("active")
            });

            link.classList.add("active");
            panelActual.classList.add("active");


            FiltrarContenidoPorPanel(panelActual.id);
        });
    });

    const tabPaneActivo = document.querySelector(".tab-pane.active");
    if (tabPaneActivo) {
        FiltrarContenidoPorTabPane(tabPaneActivo.id);
    }
}

function FiltrarContenidoPorPanel(idPanel) {
    const formEditarCuenta = document.getElementById("formEditarCuenta");
    const formEliminarCuenta = document.getElementById("formEliminarCuenta");
    const formAgregarDireccion = document.getElementById("formAgregarDireccion");
    const formModificarDireccion = document.getElementById("formModificarDireccion");
    const formEliminarDireccion = document.getElementById("formEliminarDireccion");
    const formAgregarBoletin = document.getElementById("formSuscribirseBoletin")
    const formBorrarBoletin = document.getElementById("formBorrarBoletin")



    switch (idPanel) {
        case "verpedidos":
            // Aqui de igual manera no se ocupa llamar a ver pedidos ya que se llama apenas se carga el DOM
            break;
        case "editarcuenta":
            if (formEditarCuenta) {
                formEditarCuenta.removeEventListener("submit", EditarPersona);
                formEditarCuenta.addEventListener("submit", EditarPersona);
            }
            break;
        case "eliminarcuenta":
            if (formEliminarCuenta) {
                formEliminarCuenta.removeEventListener("submit", EliminarPersona);
                formEliminarCuenta.addEventListener("submit", EliminarPersona);
            }
            break;
        case "agregardireccion":
            if (formAgregarDireccion) {
                ObtenerTodasLasProvincias();
                formAgregarDireccion.removeEventListener("submit", AgregarDireccion);
                formAgregarDireccion.addEventListener("submit", AgregarDireccion);
            }

            break;
        case "modificardireccion":
            if (formModificarDireccion) {
                ObtenerTodasLasProvincias();


                formModificarDireccion.removeEventListener("submit", ModificarDireccion);
                formModificarDireccion.addEventListener("submit", ModificarDireccion);
            }

            break;
        case "eliminardireccion":
            if (formEliminarDireccion) {
                formEliminarDireccion.removeEventListener("submit", EliminarDireccionUsuario);
                formEliminarDireccion.addEventListener("submit", EliminarDireccionUsuario);
            }
            break;
        case "verdirecciones":
            // Aqui no se ocupa llamar a  ObtenerDireccionesUsuario(), ya que se llama apenas se carga el DOM
            break;
        case "boletin":
            if (formAgregarBoletin) {
                formAgregarBoletin.removeEventListener("submit", AgregarBoletin);
                formAgregarBoletin.addEventListener("submit", AgregarBoletin);
            }

            if (formBorrarBoletin) {
                formBorrarBoletin.removeEventListener("submit", EliminarBoletin);
                formBorrarBoletin.addEventListener("submit", EliminarBoletin);
            }

            break;
        default:
            swal({
                text: "Opción Invalida",
                icon: "error"
            });
    }
}





