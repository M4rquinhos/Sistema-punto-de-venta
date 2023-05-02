--Procedimiento almacenado Listar
CREATE PROC categoria_listar
AS
SELECT idcategoria AS ID, nombre AS Nombre, descripcion AS Descripcion, estado AS Estado
FROM Categoria
ORDER BY idcategoria DESC
GO

--Procedimiento almacenado Buscar
CREATE PROC categoria_buscar
@valor VARCHAR(50)
AS
SELECT idcategoria AS ID, nombre AS Nombre, descripcion AS Descripcion, estado AS Estado
FROM Categoria 
WHERE nombre LIKE '%' + @valor + '%' OR descripcion LIKE '%' + @valor + '%'
ORDER BY idcategoria ASC
GO

--Procedimiento almacenado Insertar
CREATE PROC categoria_insertar
@nombre VARCHAR(50),
@descripcion VARCHAR(255)
AS
INSERT INTO Categoria (nombre, descripcion) VALUES (@nombre, @descripcion)
GO

--Procedimiento almacenado Actualizar
CREATE PROC categoria_actualizar
@idcategoria INT,
@nombre VARCHAR(50),
@descripcion VARCHAR(255)
AS
UPDATE Categoria SET nombre = @nombre, descripcion = @descripcion 
WHERE idcategoria = @idcategoria
GO

--Procedimiento almacenado Eliminar
CREATE PROC categoria_eliminar
@idcategoria INT
AS
DELETE FROM Categoria WHERE idcategoria = @idcategoria
GO

--Procedimiento almacenado Desactivar
CREATE PROC categoria_desactivar
@idcategoria INT
AS
UPDATE Categoria set estado = 0
WHERE idcategoria = @idcategoria
GO

--Procedimiento almacenado Activar
CREATE PROC categoria_activar
@idcategoria INT
AS
UPDATE Categoria set estado = 1
WHERE idcategoria = @idcategoria
GO