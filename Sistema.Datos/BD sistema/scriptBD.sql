--Tabla categoria
CREATE TABLE Categoria (
	idcategoria INT PRIMARY KEY IDENTITY NOT NULL,
	nombre VARCHAR(50) NOT NULL UNIQUE,
	descripcion VARCHAR(255) NULL,
	estado BIT DEFAULT(1),
);
GO
INSERT INTO Categoria (nombre, descripcion) VALUES 
											('Dispositivos de cómputo', 'Todos los dispositivos de cómputo')

--Tabla articulo
CREATE TABLE articulo (
	idarticulo  INT PRIMARY KEY IDENTITY NOT NULL,
	idcategoria INT NOT NULL,
	codigo VARCHAR(50) null,
	nombre VARCHAR(100) NOT NULL UNIQUE,
	precio_venta DECIMAL(11, 2) NOT NULL,
	stock INT NOT NULL,
	descripcion VARCHAR(255) NULL,
	imagen VARCHAR(20) NULL,
	estado BIT DEFAULT(1),
	FOREIGN KEY (idcategoria) REFERENCES Categoria (idcategoria)
);
GO

--Tabla persona
CREATE TABLE persona (
	idpersona INT PRIMARY KEY IDENTITY NOT NULL,
	tipo_persona VARCHAR(20) NOT NULL,
	nombre VARCHAR(100) NOT NULL,
	tipo_documento VARCHAR(20) NULL,
	numero_documento VARCHAR(20) NULL,
	direccion VARCHAR(70) NULL,
	telefono VARCHAR(20) NULL,
	email VARCHAR(50) NULL
);
GO

--Tabla rol
CREATE TABLE rol (
	idrol INT PRIMARY KEY IDENTITY NOT NULL,
	nombre VARCHAR(30) NOT NULL,
	descripcion VARCHAR(255) NULL,
	estado BIT DEFAULT(1)
);
GO

--Tabla usuario
CREATE TABLE usuario (
	idusuario INT PRIMARY KEY IDENTITY NOT NULL,
	idrol INT NOT NULL,
	nombre VARCHAR(100) NOT NULL,
	tipo_documento VARCHAR(20) NULL,
	num_documento VARCHAR(20) NULL,
	direccion VARCHAR(70) NULL,
	telefono VARCHAR(20) NULL,
	email VARCHAR(50) NOT NULL,
	clave VARBINARY(MAX) NOT NULL,
	estado BIT DEFAULT(1),
	FOREIGN KEY (idrol) REFERENCES rol (idrol)
);
GO

--Tabla ingreso
CREATE TABLE ingreso (
	idingreso INT PRIMARY KEY IDENTITY NOT NULL,
	idproveedor INT NOT NULL,
	idusuario INT NOT NULL,
	tipo_comprobante VARCHAR(20) NOT NULL,
	serie_comprobante VARCHAR(7) NULL,
	num_comprobante VARCHAR(10) NOT NULL,
	fecha DATETIME NOT NULL,
	impuesto DECIMAL(4, 2) NOT NULL,
	total DECIMAL(11, 2) NOT NULL,
	estado VARCHAR(20) NOT NULL,
	FOREIGN KEY (idproveedor) REFERENCES persona (idpersona),
	FOREIGN KEY (idusuario) REFERENCES usuario (idusuario)
);
GO

---Tabla detalle_ingreso
CREATE TABLE detalle_ingreso (
	iddetalle_ingreso INT PRIMARY KEY IDENTITY NOT NULL,
	idingreso INT NOT NULL,
	idarticulo INT NOT NULL,
	cantidad INT NOT NULL,
	precio DECIMAL(11, 2) NOT NULL
	FOREIGN KEY (idingreso) REFERENCES ingreso (idingreso) ON DELETE CASCADE,
	FOREIGN KEY (idarticulo) REFERENCES articulo (idarticulo)
);
GO

--Tabla venta
CREATE TABLE venta (
	idventa INT PRIMARY KEY IDENTITY NOT NULL,
	idcliente INT NOT NULL,
	idusuario INT NOT NULL,
	tipo_comprobante VARCHAR(20) NOT NULL,
	serie_comprobante VARCHAR(7) NULL,
	num_comprobante VARCHAR(10) NOT NULL,
	fecha DATETIME NOT NULL,
	impuesto DECIMAL(4, 2) NOT NULL,
	total DECIMAL(11, 2) NOT NULL,
	FOREIGN KEY (idcliente) REFERENCES persona (idpersona),
	FOREIGN KEY (idusuario) REFERENCES usuario (idusuario)
);
GO

--Tabla detalle_venta
CREATE TABLE detalle_venta (
	iddetalle_venta INT PRIMARY KEY IDENTITY NOT NULL,
	idventa INT NOT NULL,
	idarticulo INT NOT NULL,
	cantidad INT NOT NULL,
	precio DECIMAL(11, 2) NOT NULL,
	descuento DECIMAL(11, 2) NOT NULL,
	FOREIGN KEY (idventa) REFERENCES venta (idventa) ON DELETE CASCADE,
	FOREIGN KEY (idarticulo) REFERENCES articulo (idarticulo)
);
GO




SELECT * FROM articulo