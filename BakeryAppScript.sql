
create database if not exists BakeryApp;

create user 'Bakery' identified by "BakeryApp.*";
 
grant all privileges on BakeryApp.* to 'Bakery';
 
use BakeryApp;
 

/* Creación de Tablas */

create table Categorias(
	IdCategoria int not null auto_increment,
	NombreCategoria varchar(40) not null,
    DescripcionCategoria varchar(255) not null,
    ImagenCategoria varchar(80) not null,
    constraint pk_id_categoria primary key (IdCategoria),
    constraint uq_nombre_categoria unique (NombreCategoria)
);

create table UnidadesMedida(
	IdUnidad int not null auto_increment,
	NombreUnidad varchar(50) not null,
    constraint pk_id_unidad_medida primary key (IdUnidad)
);

create table Ingredientes(
	IdIngrediente int not null auto_increment,
    NombreIngrediente varchar(50) not null,
    DescripcionIngrediente varchar(100) not null,
    CantidadIngrediente int not null,
    UnidadMedidaIngrediente int not null,
    PrecioUnidadIngrediente decimal(10,2) not null,
    FechaCaducidadIngrediente date not null,
    constraint pk_id_Ingrediente primary key (IdIngrediente),
    constraint uk_nombre_ingrediente unique (NombreIngrediente),
    constraint fk_unidad_ingrediente foreign key (UnidadMedidaIngrediente) references UnidadesMedida(IdUnidad)
);

create table Recetas(
	IdReceta int not null auto_increment,
    NombreReceta varchar(50) not null,
    Instrucciones longtext not null,
    constraint pk_id_receta primary key (IdReceta),
    constraint uk_nombre_receta unique (NombreReceta)
);

create table IngredientesRecetas(
	IdIngrediente int not null,
    IdReceta int not null,
    constraint pk_id_ingredientes_recetas primary key (IdIngrediente, IdReceta),
    constraint fk_id_ingrediente_rec foreign key(IdIngrediente) references Ingredientes(IdIngrediente) on delete cascade,
    constraint fk_id_receta_rec foreign key(IdReceta) references Recetas(IdReceta) on delete cascade
);

create table Productos(
	IdProducto int not null auto_increment,
	NombreProducto varchar(40) not null,
    DescripcionProducto varchar(255) not null,
    PrecioProducto decimal(10,2) not null,
    IdCategoria int not null, 
    IdReceta int not null,
    ImagenProducto varchar(80) not null,
    constraint pk_id_Producto primary key (IdProducto),
    constraint fk_id_Categoria foreign key (IdCategoria) references Categorias(IdCategoria) on delete cascade,
    constraint fk_id_receta foreign key (IdReceta) references Recetas(IdReceta) on delete cascade,
    constraint uq_nombre_Producto unique (NombreProducto)
);

create table Roles(	
	IdRol int not null,
    NombreRol varchar(20) not null,
    constraint pk_id_rol primary key (IdRol)
);

create table Personas(
	IdPersona int not null auto_increment,
    Nombre varchar(25) not null,
    PrimerApellido varchar(25) not null,
    SegundoApellido varchar(25) not null,
    Correo varchar(80) not null,
    Contra varchar(100) not null,
    Telefono varchar(13) not null,
    IdRol int not null,
    constraint pk_id_persona primary key(IdPersona),
    constraint per_fk_id_rol foreign key(IdRol) references Roles(IdRol),
    constraint uk_correo_per unique (Correo),
    constraint uk_telefono_per unique(telefono)
);

create table Marketing(
	IdMarketing int not null auto_increment,
	Nombre varchar(25) not null,
    Correo varchar(80) not null,
    constraint pk_id_marketing primary key(IdMarketing)
);

create table RecuperarContra(
	IdRecuperacion int not null auto_increment,
	IdPersona int not null,
    CodigoRecuperacion varchar(80) not null,
    FechaExpiracion datetime not null,
    constraint pk_id_recuperar_contra primary key (IdRecuperacion),
    constraint fk_id_usuario_contra foreign key(IdPersona) references Personas(IdPersona) on delete cascade 
);

create table Provincias(
	IdProvincia int not null auto_increment,
    NombreProvincia varchar(20) not null,
    constraint pk_provincias primary key (IdProvincia)
);

create table Cantones(
	IdCanton int not null auto_increment,
    NombreCanton varchar(25) not null,
    IdProvincia int not null,
    constraint pk_canton primary key (IdCanton),
    constraint fk_canton_provincia foreign key(IdProvincia) references Provincias(IdProvincia)
);

create table Distritos(
	IdDistrito int not null auto_increment,
    NombreDistrito varchar(50) not null,
    IdCanton int not null,
    constraint pk_distrito primary key (IdDistrito),
    constraint fk_distrito_canton foreign key(IdCanton) references Cantones(IdCanton)
);

create table DireccionesUsuario(
	IdDireccion int not null auto_increment,
    NombreDireccion varchar(50) not null,
    IdPersona int not null,
    IdProvincia int not null,
    IdCanton int not null,
    IdDistrito int not null,
    DireccionExacta varchar(1000) not null,
    constraint pk_direcciones_usuario primary key(IdDireccion),
    constraint fk_id_usuario_direccion foreign key(IdPersona) references Personas(IdPersona) on delete cascade ,
    constraint fk_id_provincia_direccion foreign key(IdProvincia) references Provincias(IdProvincia) on delete cascade,
    constraint fk_id_canton_direccion foreign key(IdCanton) references Cantones(IdCanton) on delete cascade,
    constraint fk_id_distrito_direccion foreign key(IdDistrito) references Distritos(IdDistrito) on delete cascade
);

create table CarritoCompras(
	IdCarrito int not null auto_increment,
	IdPersona int not null,
    IdProducto int not null,
	CantidadProducto int not null,
    constraint pk_id_carrito primary key(IdCarrito),
    constraint fk_id_persona_carrito foreign key(IdPersona) references Personas(IdPersona) on delete cascade,
    constraint fk_id_producto foreign key(IdProducto) references Productos(IdProducto) on delete cascade
);

create table TiposEnvio(
	IdTipoEnvio int not null auto_increment,
    NombreTipo varchar(40) not null,
    constraint pk_id_tipo_envio primary key(IdTipoEnvio)
);

create table EstadosPedido(
	IdEstadoPedido int not null auto_increment,
    NombreEstado varchar(40) not null,
    constraint pk_id_estado_pedido primary key(IdEstadoPedido)
);


create table TiposPago(
	IdTipoPago int not null auto_increment,
    NombreTipo varchar(30) not null,
    constraint pk_id_tipos_pago primary key (IdTipoPago)
);

create table Pedidos(
	IdPedido int not null auto_increment,
    IdEstadoPedido int not null,
    IdPersona int not null,
    IdTipoEnvio int not null,
    IdTipoPago int not null,
    IdDireccion int null,
    FechaPedido datetime not null,
    constraint pk_id_pedido primary key(IdPedido),
    constraint fk_id_estado_pedido foreign key(IdEstadoPedido) references EstadosPedido(IdEstadoPedido) on delete cascade,
    constraint fk_id_cliente_pedido foreign key(IdPersona) references Personas(IdPersona) on delete cascade,
    constraint fk_id_tipo_envio foreign key(IdTipoEnvio) references TiposEnvio(IdTipoEnvio) on delete cascade,
    constraint fk_id_direccion_pedido foreign key(IdDireccion) references DireccionesUsuario(IdDireccion) on delete cascade,
    constraint fk_id_tipo_pago_pedido foreign key(IdTipoPago) references TiposPago(IdTipoPago) on delete cascade
);


create table PedidoProducto(
	IdPedidoProducto int not null auto_increment,
    IdProducto int not null,
	IdPedido int not null,
    CantidadProducto int not null,
    constraint pk_pedidos_producto primary key(IdPedidoProducto),
    constraint fk_producto_pedido foreign key(IdProducto) references Productos(IdProducto) on delete cascade,
    constraint fk_pedido_producto foreign key(IdPedido) references Pedidos(IdPedido) on delete cascade
);


create table PagosSinpe(
	IdPagoSinpe int not null auto_increment,
    IdPedido int not null,
    rutaImagenSinpe varchar(80) not null,
    constraint pk_pago_sinpe primary key(IdPagoSinpe),
    constraint fk_id_pedido_sinpe foreign key (IdPedido) references Pedidos(IdPedido) on delete cascade
);

create table Facturas(
	IdFactura int not null auto_increment,
    IdPedido int not null,
    TotalPagar decimal(10,2) null,
    IVA decimal(10,2) null,
    Envio decimal(10,2) null,
    FechaFactura datetime not null,
    constraint pk_id_factura primary key(IdFactura),
    constraint fk_id_pedido_factura foreign key(IdPedido) references Pedidos(IdPedido) on delete cascade
);

create table DetalleFactura(
	Linea int not null,
    IdFactura int not null,
    IdPedidoProducto int not null,
    TotalLinea decimal(10,2) not null,
    constraint pk_id_detalle_factura primary key (Linea, IdFactura),
    constraint fk_id_factura foreign key (IdFactura) references Facturas(IdFactura) on delete cascade,
    constraint fk_producto_pedido_factura foreign key (IdPedidoProducto) references PedidoProducto(IdPedidoProducto) on delete cascade
);

create table NotasCredito(
	IdNotaCredito int not null auto_increment,
    IdFactura int not null,
    constraint pk_id_nota_credito primary key (IdNotaCredito),
    constraint fk_id_factura_nota foreign key (IdFactura) references Facturas(IdFactura) on delete cascade
);

create table BoletinNoticias(
	IdBoletinNoticias int not null,
    NombreBoletin varchar(20) not null,
    constraint pk_id_boletin_noticias primary key(IdBoletinNoticias)
);

create table Boletin(
	IdBoletin int not null auto_increment,
    IdBoletinNoticias int not null,
    IdUsuario int not null,
    constraint pk_id_boletin primary key (IdBoletin),
    constraint fk_id_usuario_boletin foreign key (IdUsuario) references Personas(IdPersona) on delete cascade,
    constraint pk_id_boletin_noticias foreign key (IdBoletinNoticias) references BoletinNoticias(IdBoletinNoticias) on delete cascade
);

create table MensajesBoletin(
	IdMensajeBoletin int not null auto_increment,
    IdBoletin int not null,
    Mensaje varchar(2500) not null,
    Asunto varchar(80) not null,
    constraint pk_id_mensaje_boletin primary key (IdMensajeBoletin),
    CONSTRAINT fk_id_boletin_mensaje FOREIGN KEY (IdBoletin) REFERENCES Boletin(IdBoletin) ON DELETE CASCADE
);

/* Creacion de Roles */
insert into Roles(IdRol, NombreRol)
values (1, 'ADMINISTRADOR'),
('2', 'EMPLEADO'),
('3', 'CLIENTE');
 
/* Creacion unidades de medida */

INSERT INTO UnidadesMedida (NombreUnidad) VALUES
('No tiene'),
('Gramos'),
('Mililitros'),
('Litros'),
('Tazas'),
('Cucharadas'),
('Cucharaditas'),
('Onzas'),
('Libras'),
('Kilogramos'),
('Litros'),
('Pulgadas'),
('Centímetros'),
('Metros'),
('Pies'),
('Pulgadas cuadradas'),
('Pies cuadrados'),
('Pulgadas cúbicas'),
('Pies cúbicos'),
('Miligramos'),
('Microgramos'),
('Galones'),
('Cuartos'),
('Pintas'),
('Cucharas'),
('Cucharaditas'),
('Tazas'),
('Cucharadas de sopa'),
('Libras de peso'),
('Onzas líquidas'),
('Unidades'),
('Paquetes'),
('Bolsas'),
('Tiras'),
('Granos'),
('Barras'),
('Rollos'),
('Envases'),
('Tabletas'),
('Palets'),
('Botellas'),
('Ampollas'),
('Sacos'),
('Latas'),
('Sobres'),
('Tubos'),
('Placas'),
('Pliegos'),
('Cartuchos');
 
/* Creacion Usuario admin */

insert into Personas(Nombre, PrimerApellido, SegundoApellido, Correo, Contra, Telefono, IdRol)
values ('admin', 'admin', 'admin', 'adminbakery@gmail.com', '$2a$11$VN60QaOYMoc1i4/8K3gMMe1TFjAkg/6bNCK0kwr.NudfxKAsda9Iy', '0000-0000', 1);
 
/* Creacion Provincias */

insert into Provincias(NombreProvincia)
values ('San José'),
('Alajuela'),
('Heredia'),
('Cartago');

/* Creacion cantones */

insert into Cantones (NombreCanton, IdProvincia) VALUES
('Barva', 3),
('Flores', 3),
('Heredia', 3),
('San Isidro', 3),
('San Pablo', 3),
('San Rafael', 3),
('Santo Domingo', 3),
('Santa Barbara', 3),
('Belén', 3),
('Alajuela', 2),
('Grecia', 2),
('Atenas', 2),
('Cartago', 4),
('Paraíso', 4),
('San José', 1),
('Escazú', 1),
('Desamparados', 1),
('Goicoechea', 1),
('Santa Ana', 1),
('Alajuelita', 1),
('Moravia', 1),
('Tibás', 1),
('Curridabat', 1),
('Montes de Oca', 1);

/* Creacion distritos*/

insert into Distritos (IdCanton, NombreDistrito) values
-- Barva
(1, 'Barva'),
(1, 'San Pedro'),
(1, 'San Pablo'),
(1, 'San Roque'),
(1, 'Santa Lucía'),
(1, 'San José de la Montaña'),
(1, 'Puente Salas'),
-- Flores
(2, 'San Joaquín'),
(2, 'Barrantes'),
(2, 'Llorente'),
-- Heredia
(3, 'Heredia'),
(3, 'Mercedes'),
(3, 'San Francisco'),
(3, 'Ulloa'),
(3, 'Varablanca'),
-- San Isidro
(4, 'San Isidro'),
(4, 'San José'),
(4, 'Concepción'),
(4, 'San Francisco'),
-- San Pablo
(5, 'San Pablo'),
(5, 'Santa Lucía'),
(5, 'San Roque'),
-- San Rafael
(6, 'San Rafael'),
(6, 'San Josecito'),
(6, 'Santiago'),
(6, 'Angeles'),
(6, 'Concepción'),
-- Santo Domingo
(7, 'Santo Domingo'),
(7, 'San Vicente'),
(7, 'San Miguel'),
(7, 'Paracito'),
(7, 'Santo Tomás'),
(7, 'Santa Rosa'),
(7, 'Tures'),
(7, 'Pará'),
-- Santa Bárbara
(8, 'Santa Bárbara'),
(8, 'San Pedro'),
(8, 'San Juan'),
(8, 'Jesús'),
(8, 'Santo Domingo'),
(8, 'Purabá'),
-- Belén
(9, 'San Antonio'),
(9, 'La Ribera'),
(9, 'La Asunción'),
-- Alajuela
(10, 'Alajuela'),
(10, 'San José'),
(10, 'Carrizal'),
(10, 'San Antonio'),
(10, 'Guácima'),
(10, 'San Isidro'),
(10, 'San Rafael'),
(10, 'Rio Segundo'),
(10, 'Desamparados'),
(10, 'Turrúcares'),
(10, 'Tambor'),
(10, 'La Garita'),
-- Grecia
(11, 'Grecia'),
(11, 'San Isidro'),
(11, 'San José'),
(11, 'San Roque'),
(11, 'Tacares'),
(11, 'Puente de Piedra'),
(11, 'Bolivar'),
-- Atenas
(12, 'Atenas'),
(12, 'Jesús'),
(12, 'Mercedes'),
(12, 'San Isidro'),
(12, 'Concepción'),
(12, 'San José'),
-- Cartago
(13, 'Oriental'),
(13, 'Occidental'),
(13, 'Carmen'),
(13, 'San Nicolás'),
(13, 'Aguascalientes'),
(13, 'Guadalupe'),
(13, 'Corralillo'),
(13, 'Tierra Blanca'),
(13, 'Dulce Nombre'),
(13, 'Llano Grande'),
(13, 'Quebradilla'),
-- Paraíso
(14, 'Paraíso'),
(14, 'Santiago'),
(14, 'Orosi'),
(14, 'Cachí'),
(14, 'Llanos de Santa Lucia'),
(14, 'Birrisito'),
-- San José
(15, 'Carmen'),
(15, 'Merced'),
(15, 'Hospital'),
(15, 'Catedral'),
(15, 'Zapote'),
(15, 'San Francisco de Dos Rios'),
(15, 'La Uruca'),
(15, 'Mata Redonda'),
(15, 'Pavas'),
(15, 'Hatillo'),
(15, 'San Sebastián'),
-- Escazú
(16, 'Escazú'),
(16, 'San Antonio'),
(16, 'San Rafael'),
-- Desamparados
(17, 'Desamparados'),
(17, 'San Miguel'),
(17, 'San Juan de Dios'),
(17, 'San Rafael Arriba'),
(17, 'San Antonio'),
(17, 'Frailes'),
(17, 'Patarrá'),
(17, 'San Cristóbal'),
(17, 'Damas'),
(17, 'San Rafael Abajo'),
(17, 'Gravillas'),
(17, 'Los Guido'),
-- Goicoechea
(18, 'Guadalupe'),
(18, 'San Francisco'),
(18, 'Calle Blancos'),
(18, 'Mata de Plátano'),
(18, 'Ipís'),
(18, 'Rancho Redondo'),
(18, 'Purral'),
-- Santa Ana
(19, 'Santa Ana'),
(19, 'Salitral'),
(19, 'Pozos'),
(19, 'Uruca'),
(19, 'Piedades'),
(19, 'Brasil'),
-- Alajuelita
(20, 'Alajuelita'),
(20, 'San Josecito'),
(20, 'San  Antonio'),
(20, 'Concepción'),
(20, 'San Felipe'),
-- Moravia
(21, 'San Vicente'),
(21, 'San Jerónimo'),
(21, 'La Trinidad'),
-- Tibás
(22, 'San Juan'),
(22, 'Cinco Esquinas'),
(22, 'Anselmo Llorente'),
(22, 'León XIII'),
(22, 'Colima'),
-- Curridabat
(23, 'Curridabat'),
(23, 'Granadilla'),
(23, 'Sánchez'),
(23, 'Tirrases'),
-- Montes de Oca
(24, 'San Pedro'),
(24, 'Sabanilla'),
(24, 'Mercedes'),
(24, 'San Rafael');

/* Creacion Tipos de Envio */

insert into TiposEnvio(NombreTipo)
values('Entrega a domicilio'),
('Retirar en la sucursal');

/* Creacion Tipos de Pago */

insert into TiposPago(NombreTipo)
values ('Efectivo'),
('Sinpe Móvil'),
('Tarjeta');

/*Creacion Estados Pedido */

insert into EstadosPedido(NombreEstado)
values ('Recibido'),
('Procesando'),
('Completo'),
('Pagado'),
('Cancelado');

/* Creacion Boletin Noticias */

insert into BoletinNoticias(IdBoletinNoticias, NombreBoletin)
values (1, "Bakery App Noticias");

/* Selects */

select * from UnidadesMedida;


select * from Categorias;

select * from Roles;

select * from Personas;
 
select * from Ingredientes;
 
select * from Recetas;

select * from ingredientesRecetas;

select * from Productos;

select * from Marketing;

select * from Provincias;

select * from Cantones;

select * from Distritos;

select * from RecuperarContra;

select * from DireccionesUsuario;

select * from CarritoCompras;

select * from TiposPago;

select * from Pedidos;

select * from EstadosPedido;

select * from PedidoProducto;

select * from PagosSinpe;


select * from Facturas;


select * from DetalleFactura;

select * from NotasCredito;


select * from BoletinNoticias;

select * from Boletin;

select * from MensajesBoletin;

/* Consulta para ver los productos mas vendidos*/
select pro.nombreProducto, sum(pedPro.CantidadProducto) as CantidadProductoVendido, pro.IdProducto
from pedidoproducto pedPro inner join productos pro on pro.IdProducto = pedPro.IdProducto 
group by(pro.IdProducto);

/* Consulta para ver el tamaño de la base de datos en MB */

SELECT ROUND(SUM(data_length + index_length) / 1024 / 1024, 2) AS "Database Size (MB)"

FROM information_schema.tables

WHERE table_schema = 'BakeryApp';
 
/* Nombre de las tablas para Linux */

RENAME TABLE UnidadesMedida TO unidadesmedida;
RENAME TABLE Categorias TO categorias;
RENAME TABLE Roles TO roles;
RENAME TABLE Personas TO personas;
RENAME TABLE Ingredientes TO ingredientes;
RENAME TABLE Recetas TO recetas;
RENAME TABLE IngredientesRecetas TO ingredientesrecetas;
RENAME TABLE Productos TO productos;
RENAME TABLE Marketing TO marketing;
RENAME TABLE Provincias TO provincias;
RENAME TABLE Cantones TO cantones;
RENAME TABLE Distritos TO distritos;
RENAME TABLE RecuperarContra TO recuperarcontra;
RENAME TABLE DireccionesUsuario TO direccionesusuario;
RENAME TABLE CarritoCompras TO carritocompras;
RENAME TABLE TiposPago TO tipospago;
RENAME TABLE Pedidos TO pedidos;
RENAME TABLE EstadosPedido TO estadospedido;
RENAME TABLE TiposEnvio to tiposenvio;
RENAME TABLE PedidoProducto TO pedidoproducto;
RENAME TABLE PagosSinpe TO pagossinpe;
RENAME TABLE Facturas TO facturas;
RENAME TABLE DetalleFactura TO detallefactura;
RENAME TABLE BoletinNoticias TO boletinnoticias;
RENAME TABLE Boletin TO boletin;
RENAME TABLE MensajesBoletin TO mensajesboletin;
RENAME TABLE NotasCredito to notascredito;
