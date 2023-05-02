--Insertar roles 
INSERT INTO rol (nombre) VALUES 
						('Administrador'),
						('Vendedor'),
						('Almacenista')
GO

--Procedimiento listar
CREATE PROC rol_listar
AS
SELECT idrol, nombre FROM rol
WHERE estado = 1
GO