CREATE TABLE Clientes(
    Cedula_Cliente INT PRIMARY KEY,
    Nombre_Cliente NVARCHAR(30),
    Direccion_Cliente NVARCHAR(50),
    Telefono_Cliente NVARCHAR(30)
);

CREATE TABLE Compras(
    ID_Compra INT PRIMARY KEY IDENTITY(1, 1),
    Cedula_Cliente INT,
    Fecha_Compra DATE,
    Monto_Total DECIMAL(10, 2),
    FOREIGN KEY (Cedula_Cliente) REFERENCES Clientes(Cedula_Cliente)
);
