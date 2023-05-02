CREATE PROC categoria_existe
@valor VARCHAR(100),
@existe BIT OUTPUT
AS
	IF EXISTS (SELECT nombre FROM Categoria WHERE nombre = LTRIM(RTRIM(@valor)))
		BEGIN
			SET @existe = 1
		END
	ELSE
		BEGIN
			SET @existe = 0
		END