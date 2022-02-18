-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versión del servidor:         5.7.33 - MySQL Community Server (GPL)
-- SO del servidor:              Win64
-- HeidiSQL Versión:             11.2.0.6213
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Volcando estructura de base de datos para tallercell
CREATE DATABASE IF NOT EXISTS `tallercell` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `tallercell`;

-- Volcando estructura para procedimiento tallercell.Buscarfechas
DELIMITER //
CREATE PROCEDURE `Buscarfechas`(

IN fechaInicial DATETIME,
IN fechaFinal DATETIME
)
BEGIN
  SELECT idOrden,clientes.Nombres,Nombre,Marca,Modelo,SERIAL,Clave,Accesorios,Observaciones,FallaEquipo,Reparacion,FechaEntrada,FechaEntrega,
 tecnicos.Nombres AS Tecnico,PagoAdelantado,TotalPagar,orden.Estado
   FROM orden 
   inner JOIN Clientes ON Clientes.id = orden.idCliente
   inner JOIN tecnicos ON tecnicos.idTecnicos = orden.Idempleado
		WHERE FechaEntrada BETWEEN fechaInicial AND fechaFinal
		ORDER BY idOrden desc;
END//
DELIMITER ;

-- Volcando estructura para tabla tallercell.clientes
CREATE TABLE IF NOT EXISTS `clientes` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `Nombres` varchar(450) NOT NULL,
  `Direccion` varchar(450) NOT NULL,
  `Telefono` char(50) NOT NULL,
  `Email` varchar(50) NOT NULL,
  `Dni` char(50) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8;

-- Volcando datos para la tabla tallercell.clientes: ~4 rows (aproximadamente)
/*!40000 ALTER TABLE `clientes` DISABLE KEYS */;
REPLACE INTO `clientes` (`id`, `Nombres`, `Direccion`, `Telefono`, `Email`, `Dni`) VALUES
	(1, 'Cristian izquierdo', 'lima', '1234566', 'demo@gmail.com', '12345678'),
	(5, 'Thiago silva', 'olivos', '1234563', 'demso@gmail.com', '12345674'),
	(30, 'Yamila sanchez', 'lima olicos', '1232', 'demo@gmail.com', '1233'),
	(31, 'usuariodemo', 'lima los olivos', '123456', 'demo@demo.pe', '123456');
/*!40000 ALTER TABLE `clientes` ENABLE KEYS */;

-- Volcando estructura para tabla tallercell.empresa
CREATE TABLE IF NOT EXISTS `empresa` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `numero` char(50) NOT NULL DEFAULT '',
  `NombreComercial` varchar(100) NOT NULL DEFAULT '',
  `logo` varchar(100) NOT NULL DEFAULT '',
  `telefono` int(11) NOT NULL DEFAULT '0',
  `email` varchar(100) NOT NULL DEFAULT '0',
  `direccion` varchar(100) NOT NULL DEFAULT '0',
  `estado` char(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Volcando datos para la tabla tallercell.empresa: ~1 rows (aproximadamente)
/*!40000 ALTER TABLE `empresa` DISABLE KEYS */;
REPLACE INTO `empresa` (`id`, `numero`, `NombreComercial`, `logo`, `telefono`, `email`, `direccion`, `estado`) VALUES
	(1, '0962883773-001', 'Compuservices-Manabi', '', 994138371, 'Compuservicesmanta2018@gmail.com', 'Manta av13 entré calle 13 y 14.', '1');
/*!40000 ALTER TABLE `empresa` ENABLE KEYS */;

-- Volcando estructura para procedimiento tallercell.generar
DELIMITER //
CREATE PROCEDURE `generar`()
BEGIN
SELECT MAX(idOrden)+1 as numero FROM orden ;

END//
DELIMITER ;

-- Volcando estructura para procedimiento tallercell.InsertarOrden
DELIMITER //
CREATE PROCEDURE `InsertarOrden`(
IN idCliente INT(11),
IN Nombre VARCHAR(100),
IN Marca VARCHAR(45),
IN Modelo VARCHAR(45),
IN Serial VARCHAR(45),
IN Clave VARCHAR(45),
IN Accesorios VARCHAR(45),
IN Observaciones VARCHAR(100),
IN FallaEquipo VARCHAR(100),
IN Reparacion VARCHAR(200),
IN FechaEntrada DATETIME,
IN FechaEntrega DATETIME,
IN PagoAdelantado DECIMAL(11,2),
IN TotalPagar DECIMAL(11,2),
IN Estado VARCHAR(50),
IN IdEmpleado INT(11)
)
BEGIN
insert INTO orden(idCliente,Nombre,Marca,Modelo,SERIAL,Clave,Accesorios,Observaciones,FallaEquipo,Reparacion,FechaEntrada,
FechaEntrega,PagoAdelantado,TotalPagar,Estado,IdEmpleado
) VALUES(idCliente,Nombre,Marca,Modelo,SERIAL,Clave,Accesorios,Observaciones,FallaEquipo,Reparacion,FechaEntrada,fechaEntrega,
PagoAdelantado,TotalPagar,Estado,IdEmpleado
);
END//
DELIMITER ;

-- Volcando estructura para procedimiento tallercell.listarCliente
DELIMITER //
CREATE PROCEDURE `listarCliente`(
    IN idcliente INT(2))
BEGIN
  SELECT * FROM clientes WHERE id=idcliente;
END//
DELIMITER ;

-- Volcando estructura para procedimiento tallercell.listarOrden
DELIMITER //
CREATE PROCEDURE `listarOrden`(IN idordenes INT(11))
BEGIN
  SELECT idOrden,clientes.Nombres,Nombre,Marca,Modelo,SERIAL,Clave,Accesorios,Observaciones,FallaEquipo,Reparacion,FechaEntrada,FechaEntrega,
  PagoAdelantado,TotalPagar,orden.Estado,tecnicos.Nombres
   FROM orden 
   inner JOIN Clientes ON Clientes.id = orden.idCliente
   inner JOIN tecnicos ON tecnicos.idTecnicos = orden.Idempleado
		WHERE idOrden=idordenes;
END//
DELIMITER ;

-- Volcando estructura para procedimiento tallercell.obtenerOrdenPorEstado
DELIMITER //
CREATE PROCEDURE `obtenerOrdenPorEstado`(IN nombre_estado VARCHAR(255))
BEGIN
    SELECT * 
    FROM orden
    WHERE estado = nombre_estado;
END//
DELIMITER ;

-- Volcando estructura para tabla tallercell.orden
CREATE TABLE IF NOT EXISTS `orden` (
  `idOrden` int(11) NOT NULL AUTO_INCREMENT,
  `idCliente` int(11) NOT NULL DEFAULT '0',
  `Nombre` varchar(100) DEFAULT NULL,
  `Marca` varchar(45) DEFAULT NULL,
  `Modelo` varchar(45) DEFAULT NULL,
  `Serial` varchar(45) DEFAULT NULL,
  `Clave` varchar(45) DEFAULT NULL,
  `Accesorios` varchar(45) DEFAULT NULL,
  `Observaciones` varchar(100) DEFAULT NULL,
  `FallaEquipo` varchar(100) DEFAULT NULL,
  `Reparacion` varchar(200) DEFAULT NULL,
  `FechaEntrada` datetime DEFAULT NULL,
  `FechaEntrega` datetime DEFAULT NULL,
  `PagoAdelantado` decimal(11,2) DEFAULT NULL,
  `TotalPagar` decimal(11,2) DEFAULT NULL,
  `Estado` varchar(50) DEFAULT NULL,
  `Idempleado` int(11) DEFAULT NULL,
  PRIMARY KEY (`idOrden`),
  KEY `key_idtecnico_idx` (`Idempleado`),
  KEY `FKclientes` (`idCliente`),
  KEY `idOrden` (`idOrden`),
  CONSTRAINT `FKclientes` FOREIGN KEY (`idCliente`) REFERENCES `clientes` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `key_idtecnico` FOREIGN KEY (`Idempleado`) REFERENCES `tecnicos` (`idTecnicos`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=80 DEFAULT CHARSET=utf8;

-- Volcando datos para la tabla tallercell.orden: ~4 rows (aproximadamente)
/*!40000 ALTER TABLE `orden` DISABLE KEYS */;
REPLACE INTO `orden` (`idOrden`, `idCliente`, `Nombre`, `Marca`, `Modelo`, `Serial`, `Clave`, `Accesorios`, `Observaciones`, `FallaEquipo`, `Reparacion`, `FechaEntrada`, `FechaEntrega`, `PagoAdelantado`, `TotalPagar`, `Estado`, `Idempleado`) VALUES
	(18, 5, 'celular', 'xiomi', 'M9', '1234', '123', 'no prende', 'no prende', 'Diagnostico', 'cambio de bateria', '2021-04-01 00:00:00', '2021-04-04 00:00:00', 60.00, 60.00, 'No entregado', 1),
	(77, 5, 'celular', 'xiomi', 'M9', '1234', '123', 'no prende', 'no prende', 'Diagnostico', 'cambio de bateria', '2021-04-01 00:00:00', '2021-04-04 00:00:00', 60.00, 60.00, 'No entregado', 1),
	(78, 5, 'celular', 'xiomi', 'M9', '1234', '123', 'no prende', 'no prende', 'Diagnostico', 'cambio de bateria', '2021-04-01 00:00:00', '2021-04-04 00:00:00', 60.00, 60.00, 'No entregado', 1),
	(79, 1, 'dess', 'adsa', 'e3242', '333', 'sdfs', 'adsa', 'adsa', 'dasda', 'sadadda', '2022-01-23 00:00:00', '2022-01-28 00:00:00', 20.00, 20.00, 'Entregado', 3);
/*!40000 ALTER TABLE `orden` ENABLE KEYS */;

-- Volcando estructura para procedimiento tallercell.ProcedimientoInsertarClientes
DELIMITER //
CREATE PROCEDURE `ProcedimientoInsertarClientes`(
IN id INT(11),
IN Nombres VARCHAR(450),
IN Direccion VARCHAR(450),
in Telefono INT(11),
IN Email VARCHAR(50),
IN Dni INT(11)
)
BEGIN
insert INTO clientes(Nombres,Direccion,Telefono,Email,Dni) VALUES(Nombres,Direccion,Telefono,Email,Dni);
END//
DELIMITER ;

-- Volcando estructura para tabla tallercell.tecnicos
CREATE TABLE IF NOT EXISTS `tecnicos` (
  `idTecnicos` int(11) NOT NULL AUTO_INCREMENT,
  `Nombres` varchar(100) DEFAULT NULL,
  `Direccion` varchar(45) DEFAULT NULL,
  `Telefono` int(11) DEFAULT NULL,
  `Email` char(50) DEFAULT NULL,
  `Documento` varchar(50) DEFAULT NULL,
  `Sueldo` varchar(45) DEFAULT NULL,
  `Estado` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`idTecnicos`),
  UNIQUE KEY `idTecnicos_UNIQUE` (`idTecnicos`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- Volcando datos para la tabla tallercell.tecnicos: ~4 rows (aproximadamente)
/*!40000 ALTER TABLE `tecnicos` DISABLE KEYS */;
REPLACE INTO `tecnicos` (`idTecnicos`, `Nombres`, `Direccion`, `Telefono`, `Email`, `Documento`, `Sueldo`, `Estado`) VALUES
	(1, 'DEV', 'LIMA LOS OLIVOS', 1234567, NULL, '45800089', '12300', 'Activo'),
	(2, 'Rafael Santos', 'LIMA LOS OLIVOS', 1234567, NULL, '45800089', '12300', 'Activo'),
	(3, 'Luis sanchez', 'lmaa', 1124, 'demo@demo.com', '1223', '100', 'Activo'),
	(4, 'ari', 'dem', 12345678, 'demo@hotmail.com', '12345', '1234', 'Activo');
/*!40000 ALTER TABLE `tecnicos` ENABLE KEYS */;

-- Volcando estructura para procedimiento tallercell.UpdateOrden
DELIMITER //
CREATE PROCEDURE `UpdateOrden`(
IN idOrdenes INT(11),
IN idCliente INT(11),
IN Nombre VARCHAR(100),
IN Marca VARCHAR(45),
IN Modelo VARCHAR(45),
IN Serial VARCHAR(45),
IN Clave VARCHAR(45),
IN Accesorios VARCHAR(45),
IN Observaciones VARCHAR(100),
IN FallaEquipo VARCHAR(100),
IN Reparacion VARCHAR(200),
IN FechaEntrada DATETIME,
IN FechaEntrega DATETIME,
IN PagoAdelantado DECIMAL(11,2),
IN TotalPagar DECIMAL(11,2),
IN Estado VARCHAR(50),
IN IdEmpleado INT(11)
)
BEGIN
UPDATE orden SET idCliente=idCliente,Nombre=Nombre,Marca=Marca,Modelo=Modelo,Serial=SERIAL,Clave=Clave,Accesorios=Accesorios,Observaciones=Observaciones,FallaEquipo=FallaEquipo,Reparacion=Reparacion,FechaEntrada=FechaEntrada,
FechaEntrega=FechaEntrega,PagoAdelantado=PagoAdelantado,TotalPagar=TotalPagar,Estado=Estado,IdEmpleado=IdEmpleado
WHERE idOrden=idOrdenes;
END//
DELIMITER ;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
