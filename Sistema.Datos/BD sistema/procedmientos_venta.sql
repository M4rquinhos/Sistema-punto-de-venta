--Procedimiento listar venta
CREATE PROC venta_listar
AS
SELECT v.idventa AS ID, v.idusuario, u.nombre AS Usuario, p.nombre AS Cliente, 
v.tipo_comprobante AS Tipo_Comprobante, v.serie_comprobante AS Serie, v.num_comprobante AS Numero,
v.fecha AS Fecha, v.impuesto AS Impuesto, v.total AS Total, v.estado AS Estado
FROM venta v INNER JOIN usuario u ON v.idusuario = u.idusuario
INNER JOIN persona p ON v.idcliente = p.idpersona
ORDER BY v.idventa DESC
GO

--Procedimiento buscar venta
CREATE PROC venta_buscar
@valor VARCHAR(50)
AS
SELECT v.idventa AS ID, v.idusuario, u.nombre AS Usuario, p.nombre AS Cliente, 
v.tipo_comprobante AS Tipo_Comprobante, v.serie_comprobante AS Serie, v.num_comprobante AS Numero,
v.fecha AS Fecha, v.impuesto AS Impuesto, v.total AS Total, v.estado AS Estado
FROM venta v INNER JOIN usuario u ON v.idusuario = u.idusuario
INNER JOIN persona p ON v.idcliente = p.idpersona
WHERE v.num_comprobante LIKE '%' + @valor + '%' OR p.nombre LIKE '%' + @valor + '%'
ORDER BY v.fecha ASC
GO

--Procedimiento anular venta
CREATE PROC venta_anular
@idventa INT
AS
UPDATE venta 
SET estado = 'Anulado'
WHERE idventa = @idventa
GO


--Procedimiento insertar venta
--crear tipo de dato para venta
CREATE TYPE type_detalle_venta AS TABLE
(
	idarticulo INT,
	codigo VARCHAR(50),
	articulo VARCHAR(100),
	stock INT,
	cantidad INT,
	precio DECIMAL(11, 2),
	descuento DECIMAL(11, 2),
	importe DECIMAL (11, 2)
);
GO

CREATE PROC venta_insertar
@idusuario INT,
@idcliente INT,
@tipo_comprobante VARCHAR(20),
@serie_comprobante VARCHAR(7),
@num_comprobante VARCHAR(10),
@impuesto DECIMAL(11, 2),
@total DECIMAL(11, 2),
@detalle type_detalle_venta READONLY
AS
BEGIN
	INSERT INTO venta (
		idusuario, idcliente,
		tipo_comprobante, serie_comprobante,
		num_comprobante, fecha, impuesto, total, estado
	) VALUES (
		@idusuario, @idcliente,
		@tipo_comprobante, @serie_comprobante, 
		@num_comprobante, GETDATE(), @impuesto, @impuesto, 'Aceptado'
	);

	INSERT detalle_venta (idventa, idarticulo, cantidad, precio, descuento)
	SELECT @@IDENTITY, d.idarticulo, d.cantidad, d.precio, d.descuento
	FROM @detalle d;
END
GO