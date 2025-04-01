using BakeryApp_v1.Models;

namespace BakeryApp_v1.Utilidades;

public class FuncionesUtiles : IFuncionesUtiles
{
    private readonly IWebHostEnvironment ambiente;

    public FuncionesUtiles(IWebHostEnvironment ambiente)
    {
        this.ambiente = ambiente;
    }

    public async Task<Categoria> GuardarImagenEnSistemaCategoria(Categoria categoria)
    {
        string carpetaImagenes = ambiente.WebRootPath;
        carpetaImagenes = Path.Combine(carpetaImagenes, "img", "categorias");


        try
        {
            if (categoria.ArchivoCategoria.Length > 0)
            {

                string identificadorImagen = Guid.NewGuid().ToString() + Path.GetExtension(categoria.ArchivoCategoria.FileName);
                string rutaImagenSistema = Path.Combine(carpetaImagenes, identificadorImagen);
                string rutaBaseDatos = "";
                rutaBaseDatos = Path.Combine(rutaBaseDatos, "img", "categorias", identificadorImagen);

                using (Stream stream = File.Create(rutaImagenSistema))
                {
                    await categoria.ArchivoCategoria.CopyToAsync(stream);
                }

                categoria.ImagenCategoria = rutaBaseDatos;
            }
        }
        catch (Exception ex)
        {
            return null;
        }



        return categoria;
    }


    public bool BorrarImagenGuardadaEnSistemaCategoria(Categoria categoria)
    {
        string carpetaImagenes = ambiente.WebRootPath;

        try
        {
            string identificadorImagen = "";
            string rutaImagenSistema = Path.Combine(carpetaImagenes, identificadorImagen);
            rutaImagenSistema = Path.Combine(rutaImagenSistema, categoria.ImagenCategoria);
            File.Delete(rutaImagenSistema);
        }
        catch (Exception ex)
        {
            return false;
        }



        return true;
    }




    public async Task<bool> GuardarImagenEnSistemaProducto(Producto producto)
    {
        string carpetaImagenes = ambiente.WebRootPath;
        carpetaImagenes = Path.Combine(carpetaImagenes, "img", "productos");


        try
        {
            if (producto.ArchivoProducto.Length > 0)
            {

                string identificadorImagen = Guid.NewGuid().ToString() + Path.GetExtension(producto.ArchivoProducto.FileName);
                string rutaImagenSistema = Path.Combine(carpetaImagenes, identificadorImagen);
                string rutaBaseDatos = "";
                rutaBaseDatos = Path.Combine(rutaBaseDatos, "img", "productos", identificadorImagen);

                using (Stream stream = File.Create(rutaImagenSistema))
                {
                    await producto.ArchivoProducto.CopyToAsync(stream);
                }

                producto.ImagenProducto = rutaBaseDatos;
            }
        }
        catch (Exception ex)
        {
            return false;
        }



        return true;
    }



 


    public bool BorrarImagenGuardadaEnSistemaProducto(Producto producto)
    {
        string carpetaImagenes = ambiente.WebRootPath;

        try
        {
            string identificadorImagen = "";
            string rutaImagenSistema = Path.Combine(carpetaImagenes, identificadorImagen);
            rutaImagenSistema = Path.Combine(rutaImagenSistema, producto.ImagenProducto);
            File.Delete(rutaImagenSistema);
        }
        catch (Exception ex)
        {
            return false;
        }



        return true;
    }


    public async Task<bool> GuardarImagenEnSistemaSinpe(Pagossinpe pago)
    {
        string carpetaImagenes = ambiente.WebRootPath;
        carpetaImagenes = Path.Combine(carpetaImagenes, "img", "sinpes");


        try
        {
            if (pago.ArchivoSinpe.Length > 0)
            {

                string identificadorImagen = Guid.NewGuid().ToString() + Path.GetExtension(pago.ArchivoSinpe.FileName);
                string rutaImagenSistema = Path.Combine(carpetaImagenes, identificadorImagen);
                string rutaBaseDatos = "";
                rutaBaseDatos = Path.Combine(rutaBaseDatos, "img", "sinpes", identificadorImagen);

                using (Stream stream = File.Create(rutaImagenSistema))
                {
                    await pago.ArchivoSinpe.CopyToAsync(stream);
                }

                pago.RutaImagenSinpe = rutaBaseDatos;
            }
        }
        catch (Exception ex)
        {
            return false;
        }



        return true;
    }

    public bool BorrarImagenGuardadaEnSistemaPagoSinpe(Pagossinpe pago)
    {
        string carpetaImagenes = ambiente.WebRootPath;

        try
        {
            string identificadorImagen = "";
            string rutaImagenSistema = Path.Combine(carpetaImagenes, identificadorImagen);
            rutaImagenSistema = Path.Combine(rutaImagenSistema, pago.RutaImagenSinpe);
            File.Delete(rutaImagenSistema);
        }
        catch (Exception ex)
        {
            return false;
        }



        return true;
    }





    public Persona EncriptarContra(Persona persona)
    {
        try
        {
            persona.Contra = BCrypt.Net.BCrypt.HashPassword(persona.Contra);
            return persona;
        }
        catch (Exception ex)
        {

            return null;
        }


    }

    public string GenerarGUID()
    {

        return Guid.NewGuid().ToString("N");
    }
}
