--Procedimiento Listar
CREATE PROC articulo_listar
AS
SELECT a.idarticulo AS ID, a.idcategoria, c.nombre AS Categoria,
a.codigo AS Codigo, a.nombre AS Nombre, a.precio_venta AS Precio_Venta,
a.stock AS Stock, a.descripcion AS Descripcion, a.imagen AS Imagen, a.estado AS Estado
FROM articulo a INNER JOIN Categoria c ON a.idcategoria = c.idcategoria
ORDER BY a.idarticulo DESC
GO

--Procedimiento Buscar
CREATE PROC articulo_buscar
@valor VARCHAR(50)
AS
SELECT a.idarticulo AS ID, a.idcategoria, c.nombre AS Categoria,
a.codigo AS Codigo, a.nombre AS Nombre, a.precio_venta AS Precio_Venta,
a.stock AS Stock, a.descripcion AS Descripcion, a.imagen AS Imagen, a.estado AS Estado
FROM articulo a INNER JOIN Categoria c ON a.idcategoria = c.idcategoria 
WHERE a.nombre LIKE '%' + @valor + '%' OR a.descripcion LIKE '%' + @valor + '%'
ORDER BY a.nombre ASC
GO

--Procedimiento Insertar
CREATE PROC articulo_insertar
@idcategoria INT,
@codigo VARCHAR(50),
@nombre VARCHAR(100),
@precio_venta DECIMAL(11,2),
@stock INT,
@descripcion VARCHAR(255),
@imagen VARCHAR(20)
AS
INSERT INTO articulo (idcategoria, codigo, nombre, precio_venta, stock, descripcion, imagen)
VALUES (@idcategoria, @codigo, @nombre, @precio_venta, @stock, @descripcion, @imagen)
GO

--Procedimiento Actualizar
CREATE PROC articulo_actualizar
@idarticulo INT,
@idcategoria INT,
@codigo VARCHAR(50),
@nombre VARCHAR(100),
@precio_venta DECIMAL(11,2),
@stock INT,
@descripcion VARCHAR(255),
@imagen VARCHAR(20)
AS
UPDATE articulo 
SET
idcategoria = @idcategoria,
codigo = @codigo,
nombre = @nombre,
precio_venta = @precio_venta,
stock = @stock,
descripcion = @descripcion,
imagen = @imagen
WHERE idarticulo = @idarticulo
GO

--Procedimiento Eliminar
CREATE PROC articulo_eliminar
@idarticulo INT
AS
DELETE FROM articulo 
WHERE idarticulo = @idarticulo
GO

--Procedimiento Desactivar 
CREATE PROC articulo_desactivar
@idarticulo INT
AS
UPDATE articulo SET estado = 0
WHERE idarticulo = @idarticulo
GO

--Procedimiento Activar
CREATE PROC articulo_activar
@idarticulo INT
AS
UPDATE articulo SET estado = 1
WHERE idarticulo = @idarticulo
GO

--Procedimiento existe
CREATE PROC articulo_existe
@valor VARCHAR(100),
@existe BIT OUTPUT
AS
IF EXISTS (SELECT nombre FROM articulo WHERE nombre = LTRIM(RTRIM(@valor)))
	BEGIN
		SET @existe = 1
	END
ELSE
	BEGIN
		SET @existe = 0
	END