using BakeryApp_v1.Models;

namespace BakeryApp_v1.Utilidades;

public interface IFuncionesUtiles
{
    public Task<Categoria> GuardarImagenEnSistemaCategoria(Categoria objeto);

    public bool BorrarImagenGuardadaEnSistemaCategoria(Categoria categoria);

    public Task<bool> GuardarImagenEnSistemaProducto(Producto objeto);

    public bool BorrarImagenGuardadaEnSistemaProducto(Producto producto);

    public Task<bool> GuardarImagenEnSistemaSinpe(Pagossinpe pagossinpe);


    public bool BorrarImagenGuardadaEnSistemaPagoSinpe(Pagossinpe pago);
    public Persona EncriptarContra(Persona persona);

    public string GenerarGUID();
}
