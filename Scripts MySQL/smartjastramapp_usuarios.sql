CREATE DATABASE  IF NOT EXISTS `smartjastramapp` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `smartjastramapp`;
-- MySQL dump 10.13  Distrib 8.0.40, for Win64 (x86_64)
--
-- Host: localhost    Database: smartjastramapp
-- ------------------------------------------------------
-- Server version	9.1.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuarios` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Apellidos` varchar(150) COLLATE utf8mb4_unicode_ci NOT NULL,
  `NumeroTelefonico` int NOT NULL,
  `Email` varchar(150) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Contraseña` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `RolID` int NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `Email` (`Email`),
  KEY `usuarios_rol` (`RolID`),
  CONSTRAINT `usuarios_rol` FOREIGN KEY (`RolID`) REFERENCES `roles` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios`
--

LOCK TABLES `usuarios` WRITE;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` VALUES (1,'Lucas Manuel','Serrano Pérez',662430710,'l.serran@jastram.net','0875c6be38e42c3e5467d6794f6b055a6e456271023461b23bf7064d7c1835bac45c70134b9d6c80e74e8ab3b80859b66206d03c44dc9c5c091872f16e75d7e8',3),(6,'Javier','Fernandez Rios',662430610,'user@jastram.net','70e9b857aca8d91bc6407f76262723939ea25cdaf74644820afffd28cfdba12d84121fd225a1c7bdac0c7d9116e04a08bde682716e43d24ac31436b8eb8f575a',1),(7,'Lincia','Dsouza',606017173,'l.dsouza@jastram.net','26647ccb546ca07ac01f4fe7131a97fcbc513fce813a2916e3a5b0bf030c10c4c2630ad595f776f814e077de34d6b8e9097f4949ace8cd469c6c12e97f63c31d',3),(10,'Peter','Hall',606017170,'p.hall@jastram.net','91535b0638eb1671a24404bfbc87749f73903173b3cfe307a3d83d071dde44fdf95135f05f59a89df99b3b21d244eb664a963aab0cb5ff7ca3afe019b940b3e7',2),(12,'Judit','Arcos Corral',606017176,'j.arcos@jastram.net','ca9c6774ecf6b258aaa4a5b97274bae49434dee6252a510d5d4d0247675afabe2538808471723f181fa6baafe3bb2a50622fbf426516a3af9c41ce42d2b2e4f1',3),(14,'Jack','Hode',662440830,'admin@jastram.net','c7ad44cbad762a5da0a452f9e854fdc1e0e7a52a38015f23f3eab1d80b931dd472634dfac71cd34ebc35d16ab7fb8a90c81f975113d6c7538dc69dd8de9077ec',3);
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-06-03 13:29:07
