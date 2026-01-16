CREATE DATABASE App_Sistema_Ventas

USE	App_Sistema_Ventas
GO

---TABLAS GENERALES
CREATE TABLE Cliente (
    IdCliente INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Documento NVARCHAR(20),
    Telefono NVARCHAR(20),
    Email NVARCHAR(100),
    FechaRegistro DATETIME DEFAULT GETDATE(),
    Activo BIT DEFAULT 1
);

CREATE TABLE Producto (
    IdProducto INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    IdCategoria INT NOT NULL,
    Stock INT NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    FechaRegistro DATETIME DEFAULT GETDATE(),
    Activo BIT DEFAULT 1,

    CONSTRAINT FK_Producto_Categoria
        FOREIGN KEY (IdCategoria) REFERENCES Categoria(IdCategoria)
);

CREATE TABLE Categoria (
    IdCategoria INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(150),
    Activo BIT DEFAULT 1 NOT NULL
);

CREATE TABLE Venta (
    IdVenta INT IDENTITY PRIMARY KEY,
    IdCliente INT NOT NULL,
    FechaVenta DATETIME DEFAULT GETDATE(),
    Total DECIMAL(10,2) NOT NULL,

    CONSTRAINT FK_Venta_Cliente
        FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente)
);


CREATE TABLE DetalleVenta (
    IdDetalleVenta INT IDENTITY PRIMARY KEY,
    IdVenta INT NOT NULL,
    IdProducto INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    SubTotal AS (Cantidad * PrecioUnitario),

    CONSTRAINT FK_DetalleVenta_Venta
        FOREIGN KEY (IdVenta) REFERENCES Venta(IdVenta),

    CONSTRAINT FK_DetalleVenta_Producto
        FOREIGN KEY (IdProducto) REFERENCES Producto(IdProducto)
);

CREATE TABLE Usuario (
    IdUsuario INT IDENTITY PRIMARY KEY,
    Usuario NVARCHAR(50) NOT NULL UNIQUE,
    Clave NVARCHAR(255) NOT NULL,
    IdRol INT NOT NULL,
    Activo BIT DEFAULT 1,

    CONSTRAINT FK_Usuario_Rol
        FOREIGN KEY (IdRol) REFERENCES Rol(IdRol)
);

CREATE TABLE Rol (
    IdRol INT IDENTITY PRIMARY KEY,
    NombreRol NVARCHAR(50) NOT NULL
);

---SPS PRINCIPALES

---Cliente
GO
CREATE PROC sp_InsertarCliente
@Nombre VARCHAR(100),
@Documento VARCHAR(20),
@Telefono VARCHAR(20),
@Email VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Cliente (Nombre, Documento, Telefono, Email, FechaRegistro, Activo)
    VALUES(@Nombre, @Documento, @Telefono, @Email, GETDATE(), 1)

    SELECT SCOPE_IDENTITY() AS IdCliente;
END

GO
CREATE PROC sp_ActualizarCliente
@IDCliente INT,
@Nombre VARCHAR(100),
@Documento VARCHAR(20),
@Telefono VARCHAR(20),
@Email VARCHAR(100)
AS 
BEGIN
    SET NOCOUNT ON;
    UPDATE Cliente
    SET Nombre = @Nombre,
    Documento = @Documento,
    Telefono = @Telefono,
    Email = @Email
    WHERE IdCliente = @IDCliente
    AND Activo = 1
END

GO
CREATE PROC sp_ListarCliente
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
    IdCliente,      --0
    Nombre,         --1
    Documento,      --2
    Telefono,       --3
    Email,          --4
    FechaRegistro,  --5
    Activo          --6
    FROM Cliente    
END

GO
CREATE PROC sp_BuscarCliente
@Nombre VARCHAR (100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
    IdCliente,      --0
    Nombre,         --1
    Documento,      --2
    Telefono,       --3
    Email,          --4
    FechaRegistro,  --5
    Activo          --6
    FROM Cliente    
    WHERE Nombre LIKE '%'+ @Nombre +'%'
END
GO

--Producto
CREATE PROC sp_AgregarProducto
@Nombre VARCHAR (100),
@IDCategoria INT,
@Stock INT,
@Precio DECIMAL(10, 2)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Producto (Nombre, IdCategoria, Stock, Precio, FechaRegistro, Activo)
                VALUES (@Nombre, @IDCategoria, @Stock, @Precio, GETDATE(), 1)
END

GO
CREATE PROC sp_ActualizarProducto
@IDProducto INT,
@Nombre VARCHAR(100),
@IDCategoria INT,
@Stock INT,
@Precio DECIMAL(10, 2)
AS 
BEGIN
    SET NOCOUNT ON;
    UPDATE Producto
    SET 
    Nombre = @Nombre,
    IdCategoria = @IDCategoria,
    Stock = @Stock,
    Precio = @Precio
    WHERE IdProducto = @IDProducto
    AND Activo = 1
END

GO
CREATE PROC sp_EliminarProducto
    @IDProducto INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Producto
    SET Activo = 0
    WHERE IdProducto = @IDProducto
END

GO
CREATE PROC sp_ListarProducto
AS 
BEGIN
    SET NOCOUNT ON;
    SELECT 
    IdProducto,     --0
    Nombre,         --1
    IdCategoria,    --2
    Stock,          --3
    Precio,         --4
    FechaRegistro,  --5
    Activo          --6
    FROM Producto   --7
    WHERE Activo = 1
END

GO
CREATE PROC sp_BuscarProducto
@Nombre VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
    IdProducto,     --0
    Nombre,         --1
    IdCategoria,    --2
    Stock,          --3
    Precio,         --4
    FechaRegistro,  --5
    Activo          --6
    FROM Producto   --7
    WHERE Activo = 1
    AND Nombre LIKE '%'+ @Nombre +'%'
END

GO
CREATE PROC sp_ListarProducto_Todos
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
    IdProducto,     --0
    Nombre,         --1
    IdCategoria,    --2
    Stock,          --3
    Precio,         --4
    FechaRegistro,  --5
    Activo          --6
    FROM Producto
END

GO
CREATE PROC sp_ReactivarProducto
@IDProducto INT
AS
BEGIN
    UPDATE Producto
    SET Activo = 1
    WHERE IdProducto = @IDProducto
END
GO

--Venta
GO
CREATE TYPE TVP_DetalleVenta AS TABLE (
    IdProducto INT,
    Cantidad INT
);
GO
CREATE PROC sp_InsertarVenta_Multiple
    @IdCliente INT,
    @Detalle TVP_DetalleVenta READONLY
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IdVenta INT;

    BEGIN TRY
        BEGIN TRAN;

        -- Validar que la venta tenga productos
        IF NOT EXISTS (SELECT 1 FROM @Detalle)
        BEGIN
            RAISERROR('La venta no contiene productos', 16, 1);
            ROLLBACK;
            RETURN;
        END

        -- Validar stock suficiente para TODOS los productos
        IF EXISTS (
            SELECT 1
            FROM @Detalle d
            INNER JOIN Producto p ON p.IdProducto = d.IdProducto
            WHERE p.Activo = 0
               OR p.Stock < d.Cantidad
        )
        BEGIN
            RAISERROR('Stock insuficiente o producto inactivo', 16, 1);
            ROLLBACK;
            RETURN;
        END

        -- Crear la venta
        INSERT INTO Venta (IdCliente, Total)
        VALUES (@IdCliente, 0);

        SET @IdVenta = SCOPE_IDENTITY();

        -- Insertar detalles (N productos)
        INSERT INTO DetalleVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario)
        SELECT
            @IdVenta,
            p.IdProducto,
            d.Cantidad,
            p.Precio
        FROM @Detalle d
        INNER JOIN Producto p ON p.IdProducto = d.IdProducto;

        -- Descontar stock
        UPDATE p
        SET p.Stock = p.Stock - d.Cantidad
        FROM Producto p
        INNER JOIN @Detalle d ON p.IdProducto = d.IdProducto;

        -- Calcular total de la venta
        UPDATE Venta
        SET Total = (
            SELECT SUM(SubTotal)
            FROM DetalleVenta
            WHERE IdVenta = @IdVenta
        )
        WHERE IdVenta = @IdVenta;

        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        THROW;
    END CATCH
END
GO


GO
CREATE PROC sp_ListarVentasHoy
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        IdVenta,
        IdCliente,
        FechaVenta,
        Total
    FROM Venta
    WHERE CAST(FechaVenta AS DATE) = CAST(GETDATE() AS DATE)
END


GO
CREATE PROC sp_TotalVentasHoy
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ISNULL(SUM(Total), 0) AS TotalVentasHoy
    FROM Venta
    WHERE CAST(FechaVenta AS DATE) = CAST(GETDATE() AS DATE)
END


GO
CREATE PROC sp_ListarVentasMes
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        IdVenta,
        IdCliente,
        FechaVenta,
        Total
    FROM Venta
    WHERE MONTH(FechaVenta) = MONTH(GETDATE())
      AND YEAR(FechaVenta)  = YEAR(GETDATE())
END

GO
CREATE PROC sp_TotalVentasMes
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ISNULL(SUM(Total), 0) AS TotalVentasMes
    FROM Venta
    WHERE MONTH(FechaVenta) = MONTH(GETDATE())
      AND YEAR(FechaVenta)  = YEAR(GETDATE())
END

GO
CREATE PROC sp_ListarVentas --Listar de ADMINISTRADOR -- Muestra TODAS las VENTAS
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        IdVenta,
        c.Nombre AS Cliente, 
        FechaVenta,
        Total
    FROM Venta v
    INNER JOIN Cliente c ON c.IdCliente = v.IdCliente
END

GO
CREATE PROC sp_BuscarVenta
@NombreCliente VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        IdVenta,
        c.Nombre AS Cliente,
        FechaVenta,
        Total
    FROM Venta v
    INNER JOIN Cliente c ON c.IdCliente = v.IdCliente
    WHERE c.Nombre LIKE '%'+ @NombreCliente +'%' 
END

GO
CREATE PROC sp_DetalleVenta
@IDVenta INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        v.IdVenta AS IdVenta,
        c.Nombre AS Cliente,
        p.Nombre AS Producto,
        d.Cantidad AS Cantidad,
        d.PrecioUnitario AS PrecioUnitario,
        d.SubTotal AS SubTotal,
        v.Total AS Total,
        v.FechaVenta AS FechaVenta
    FROM Venta v
    INNER JOIN Cliente c ON v.IdCliente = c.IdCliente
    INNER JOIN DetalleVenta d ON v.IdVenta = d.IdVenta
    INNER JOIN Producto p ON d.IdProducto = p.IdProducto
    WHERE v.idVenta = @IDVenta
END
GO

--Usuario
GO
CREATE PROC sp_InsertarUsuario 
@NombreUsuario VARCHAR(50),
@Clave VARCHAR(255),
@IDRol INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Usuario (Usuario, Clave, IdRol, Activo)
    VALUES (@NombreUsuario, @Clave, @IDRol, 1)
END

GO
CREATE PROC sp_ActualizarUsuario
@IDUsuario INT,
@NombreUsuario VARCHAR(50),
@IDRol INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Usuario
    SET Usuario = @NombreUsuario,
        IdRol = @IDRol
        WHERE IdUsuario = @IDUsuario
END

GO
CREATE PROC sp_DesactivarUsuario 
@IDUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    Update Usuario
    SET Activo = 0
    WHERE IdUsuario = @IDUsuario
END

GO
CREATE PROC sp_ReactivarUsuario
@IDUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Usuario
    SET Activo = 1
    WHERE IdUsuario = @IDUsuario
END

GO
CREATE PROC sp_ListarUsuarios
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        u.IdUsuario AS IdUsuario,
        u.Usuario AS Usuario,
        r.NombreRol AS NombreRol,
        u.Activo AS Activo
    FROM Usuario u
    INNER JOIN Rol r ON u.IdRol = r.IdRol
END

GO
CREATE PROC sp_LoginUsuario
@NombreUsuario VARCHAR(50),
@Clave VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
    u.Usuario,
    r.NombreRol,
    u.Activo
    FROM Usuario u
    INNER JOIN Rol r ON u.IdRol = r.IdRol
    WHERE u.Usuario = @NombreUsuario
    AND u.Clave = @Clave AND Activo = 1
END
GO

--Categoria
GO
CREATE PROC sp_AgregarCategoria
@Nombre VARCHAR(50),
@Descripcion VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Categoria(Nombre, Descripcion, Activo)
    VALUES(@Nombre, @Descripcion, 1)
END
GO

GO
CREATE PROC sp_ActualizarCategoria
@IDCategoria INT,
@Nombre VARCHAR(50),
@Descripcion VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Categoria
    SET Nombre = @Nombre,
        Descripcion = @Descripcion
        WHERE IdCategoria = @IDCategoria
END
GO

GO
CREATE PROC sp_DesactivarCategoria
@IDCategoria INT
AS
BEGIN
    SET NOCOUNT ON;
    Update Categoria
    SET Activo = 0
    WHERE IdCategoria = @IDCategoria
END

GO
CREATE PROC sp_ReactivarCategoria
@IDCategoria INT
AS 
BEGIN
    SET NOCOUNT ON;
    UPDATE Categoria
    SET Activo = 1
    WHERE IdCategoria = @IDCategoria
END

GO
CREATE PROC sp_ListarCategoria
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
    IdCategoria, 
    Nombre,
    Descripcion,
    Activo
    FROM Categoria
    WHERE Activo = 1
END
GO
