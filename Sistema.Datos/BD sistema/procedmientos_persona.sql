--Procedimiento Listar (Todas las personas)
CREATE PROC persona_listar
AS
SELECT idpersona AS ID, tipo_persona AS Tipo_Persona, nombre AS Nombre, 
tipo_documento AS Tipo_Documento, numero_documento AS Num_Documento,
direccion AS Direccion, telefono AS Telefono, email AS Email
FROM persona
ORDER BY idpersona DESC
GO

--Procedimiento Listar Proveedores
CREATE PROC persona_listar_proveedores
AS
SELECT idpersona AS ID, tipo_persona AS Tipo_Persona, nombre AS Nombre, 
tipo_documento AS Tipo_Documento, numero_documento AS Num_Documento,
direccion AS Direccion, telefono AS Telefono, email AS Email
FROM persona
WHERE tipo_persona = 'Proveedor'
ORDER BY idpersona DESC
GO

--Procedimiento Listar Clientes
CREATE PROC persona_listar_clientes
AS
SELECT idpersona AS ID, tipo_persona AS Tipo_Persona, nombre AS Nombre, 
tipo_documento AS Tipo_Documento, numero_documento AS Num_Documento,
direccion AS Direccion, telefono AS Telefono, email AS Email
FROM persona
WHERE tipo_persona = 'Cliente'
ORDER BY idpersona DESC
GO

--Procedimiento Buscar (Todas las personas)
CREATE PROC persona_buscar
@valor VARCHAR(50)
AS
SELECT idpersona AS ID, tipo_persona AS Tipo_Persona, nombre AS Nombre, 
tipo_documento AS Tipo_Documento, numero_documento AS Num_Documento,
direccion AS Direccion, telefono AS Telefono, email AS Email
FROM persona
WHERE nombre LIKE '%' + @valor + '%' OR email LIKE '%' + @valor + '%'
ORDER BY nombre ASC
GO

--Procedimiento Buscar Proveedores
CREATE PROC persona_buscar_proveedores
@valor VARCHAR(50)
AS
SELECT idpersona AS ID, tipo_persona AS Tipo_Persona, nombre AS Nombre, 
tipo_documento AS Tipo_Documento, numero_documento AS Num_Documento,
direccion AS Direccion, telefono AS Telefono, email AS Email
FROM persona
WHERE (nombre LIKE '%' + @valor + '%' OR email LIKE '%' + @valor + '%')
AND tipo_persona = 'Proveedor'
ORDER BY nombre ASC
GO

--Procedimiento Buscar Clientes
CREATE PROC persona_buscar_clientes
@valor VARCHAR(50)
AS
SELECT idpersona AS ID, tipo_persona AS Tipo_Persona, nombre AS Nombre, 
tipo_documento AS Tipo_Documento, numero_documento AS Num_Documento,
direccion AS Direccion, telefono AS Telefono, email AS Email
FROM persona
WHERE (nombre LIKE '%' + @valor + '%' OR email LIKE '%' + @valor + '%')
AND tipo_persona = 'Cliente'
ORDER BY nombre ASC
GO

--Procedimiento Insertar
CREATE PROC persona_insertar
@tipo_persona VARCHAR(20),
@nombre VARCHAR(100),
@tipo_documento VARCHAR(100),
@num_documento VARCHAR(20),
@direccion VARCHAR(70),
@telefono VARCHAR(20),
@email VARCHAR(50)
AS
INSERT INTO persona (tipo_persona, nombre, tipo_documento, numero_documento, direccion, telefono, email)
VALUES(
@tipo_persona, @nombre, @tipo_documento, @num_documento, @direccion, @telefono, @email
)
GO

--Procedimiento Actualizar
CREATE PROC persona_actualizar
@idpersona INT,
@tipo_persona VARCHAR(20),
@nombre VARCHAR(100),
@tipo_documento VARCHAR(100),
@num_documento VARCHAR(20),
@direccion VARCHAR(70),
@telefono VARCHAR(20),
@email VARCHAR(50)
AS
UPDATE persona 
SET
tipo_persona = @tipo_persona,
nombre = @nombre,
tipo_documento = @tipo_documento,
numero_documento = @num_documento,
direccion = @direccion,
telefono = @telefono,
email = @email
WHERE idpersona = @idpersona
GO

--Procedimiento Eliminar
CREATE PROC persona_eliminar
@idpersona INT
AS
DELETE FROM persona 
WHERE idpersona = @idpersona
GO

--Procedimiento Persona Existe
CREATE PROC pesona_existe
@valor VARCHAR(100),
@existe BIT OUTPUT
AS
IF EXISTS (SELECT nombre FROM persona WHERE nombre = LTRIM(RTRIM(@valor)))
	BEGIN
		SET @existe = 1
	END
ELSE
	BEGIN
		SET @existe = 0
	END
GO