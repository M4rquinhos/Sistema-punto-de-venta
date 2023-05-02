--Actualizar stock+
CREATE TRIGGER Ingreso_ActualizarStock
ON detalle_ingreso
FOR INSERT 
AS
UPDATE a set a.stock = a.stock + d.cantidad
FROM articulo a INNER JOIN inserted d ON d.idarticulo = a.idarticulo
GO

--Procedimiento listar deatlle de ingreso
CREATE PROC ingreso_listar_detalle
@idingreso INT
AS
SELECT d.idarticulo AS ID, a.codigo AS CODIGO, a.nombre AS ARTICULO, d.cantidad as CANTIDAD,
d.precio AS PRECIO, (d.precio * d.cantidad) AS IMPORTE
FROM detalle_ingreso d INNER JOIN articulo a
ON d.idarticulo = a.idarticulo
WHERE d.idingreso = @idingreso
GO
