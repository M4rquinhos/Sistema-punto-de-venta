--Procedimiento Listar
CREATE PROC usuario_listar
AS
SELECT 
u.idusuario AS ID, 
u.idrol, r.nombre AS Rol, 
u.nombre AS Nombre,
u.tipo_documento AS Tipo_Documento,
u.num_documento AS Num_Documento,
u.direccion AS Direccion,
u.telefono AS Telefono,
u.email AS Email,
u.estado AS Estado
FROM usuario u INNER JOIN rol r ON u.idrol = r.idrol
ORDER BY u.idusuario DESC
GO

--Procedimiento Buscar
CREATE PROC usuario_buscar
@valor VARCHAR(50)
AS
SELECT 
u.idusuario AS ID, 
u.idrol, r.nombre AS Rol, 
u.nombre AS Nombre,
u.tipo_documento AS Tipo_Documento,
u.num_documento AS Num_Documento,
u.direccion AS Direccion,
u.telefono AS Telefono,
u.email AS Email,
u.estado AS Estado
FROM usuario u INNER JOIN rol r ON u.idrol = r.idrol
WHERE u.nombre LIKE '%' + @valor + '%' OR  u.email LIKE '%' + @valor + '%'
ORDER BY u.nombre ASC
GO

--Procedimiento Insertar
CREATE PROC usuario_insertar
@idrol INT,
@nombre VARCHAR(100),
@tipo_documento VARCHAR(20),
@num_documento VARCHAR(20),
@direccion VARCHAR(70),
@telefono VARCHAR(20),
@email VARCHAR(50),
@clave VARCHAR(50)
AS
INSERT INTO usuario (idrol, nombre, tipo_documento, num_documento, direccion, telefono, email, clave) 
VALUES (
@idrol, @nombre, @tipo_documento, @num_documento, @direccion, @telefono, @email, HASHBYTES('SHA_256', @clave)
)
GO

--Procedimiento Actualizar
CREATE PROC usuario_actualizar
@idusuario INT,
@idrol INT,
@nombre VARCHAR(100),
@tipo_documento VARCHAR(20),
@num_documento VARCHAR(20),
@direccion VARCHAR(70),
@telefono VARCHAR(20),
@email VARCHAR(50),
@clave VARCHAR(50)
AS
IF @clave <> ''
	UPDATE usuario SET
	idrol = @idrol,
	nombre = @nombre,
	tipo_documento = @tipo_documento,
	num_documento = @num_documento,
	direccion = @direccion,
	telefono = @telefono,
	email = @email,
	clave = HASHBYTES('SHA_256', @clave)
	WHERE idusuario = @idusuario;
ELSE
	UPDATE usuario SET
	idrol = @idrol,
	nombre = @nombre,
	tipo_documento = @tipo_documento,
	num_documento = @num_documento,
	direccion = @direccion,
	telefono = @telefono,
	email = @email
	WHERE idusuario = @idusuario;
GO

--Procedimiento Eliminar
CREATE PROC usuario_eliminar
@idusuario INT
AS
DELETE FROM usuario
WHERE idusuario = @idusuario
GO

--Procedimiento Desactivar
CREATE PROC usuario_desactivar
@idusuario INT
AS
UPDATE usuario set estado = 0
WHERE idusuario = @idusuario
GO

--Procedimiento Activar
CREATE PROC usuario_activar
@idusuario INT
AS
UPDATE usuario set estado = 1
WHERE idusuario = @idusuario
GO



--Procedimiento existe
CREATE PROC usuario_existe
@valor VARCHAR(100),
@existe BIT OUTPUT
AS
	IF EXISTS (SELECT email FROM usuario WHERE email = LTRIM(RTRIM(@valor)))
		BEGIN
			SET @existe = 1
		END
	ELSE
		BEGIN
			SET @existe = 0
		END
