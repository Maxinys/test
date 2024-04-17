-- MySQL dump 10.13  Distrib 8.0.30, for Win64 (x86_64)
--
-- Host: localhost    Database: cafemanagement
-- ------------------------------------------------------
-- Server version	8.0.30

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
-- Table structure for table `dishes`
--

DROP TABLE IF EXISTS `dishes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dishes` (
  `DishID` int NOT NULL AUTO_INCREMENT,
  `DishName` varchar(50) NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  `Photo` longblob,
  PRIMARY KEY (`DishID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dishes`
--

LOCK TABLES `dishes` WRITE;
/*!40000 ALTER TABLE `dishes` DISABLE KEYS */;
INSERT INTO `dishes` VALUES (1,'Борщ',150.00,NULL),(2,'Пельмени',120.00,NULL),(3,'Салат \"Цезарь\"',180.00,NULL),(4,'Пицца Маргарита',220.00,NULL),(5,'Стейк \"Медальоны\"',320.00,NULL),(6,'Чай черный',50.00,NULL),(7,'Кофе американо',80.00,NULL),(8,'Сок апельсиновый',70.00,NULL),(9,'Десерт \"Тирамису\"',150.00,NULL),(10,'Лимонад',60.00,NULL);
/*!40000 ALTER TABLE `dishes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employees`
--

DROP TABLE IF EXISTS `employees`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employees` (
  `EmployeeID` int NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(50) NOT NULL,
  `LastName` varchar(50) NOT NULL,
  `Status` varchar(20) NOT NULL,
  `UserID` int DEFAULT NULL,
  PRIMARY KEY (`EmployeeID`),
  KEY `UserID` (`UserID`),
  CONSTRAINT `employees_ibfk_1` FOREIGN KEY (`UserID`) REFERENCES `users` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employees`
--

LOCK TABLES `employees` WRITE;
/*!40000 ALTER TABLE `employees` DISABLE KEYS */;
INSERT INTO `employees` VALUES (1,'Иван','Иванов','Уволен',1),(2,'Петр','Петров','Уволен',2),(3,'Анна','Сидорова','Уволен',3),(4,'Екатерина','Кузнецова','Уволен',4),(5,'Алексей','Алексеев','Работает',5),(6,'Ольга','Смирнова','Работает',6),(9,'Пермякова','Мария','Работает',8),(10,'Шакиров','Марсель','Уволен',9),(11,'Шакиров','Марсель','Работает',10);
/*!40000 ALTER TABLE `employees` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orderdishes`
--

DROP TABLE IF EXISTS `orderdishes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orderdishes` (
  `OrderDishID` int NOT NULL AUTO_INCREMENT,
  `OrderID` int DEFAULT NULL,
  `DishID` int DEFAULT NULL,
  `Quantity` int NOT NULL,
  PRIMARY KEY (`OrderDishID`),
  KEY `OrderID` (`OrderID`),
  KEY `DishID` (`DishID`),
  CONSTRAINT `orderdishes_ibfk_1` FOREIGN KEY (`OrderID`) REFERENCES `orders` (`OrderID`),
  CONSTRAINT `orderdishes_ibfk_2` FOREIGN KEY (`DishID`) REFERENCES `dishes` (`DishID`)
) ENGINE=InnoDB AUTO_INCREMENT=74 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orderdishes`
--

LOCK TABLES `orderdishes` WRITE;
/*!40000 ALTER TABLE `orderdishes` DISABLE KEYS */;
INSERT INTO `orderdishes` VALUES (1,1,1,2),(2,1,3,1),(3,2,4,1),(4,3,2,3),(5,4,5,1),(6,5,6,2),(7,5,7,1),(8,5,8,3),(71,99,1,1),(72,99,6,1),(73,100,1,1);
/*!40000 ALTER TABLE `orderdishes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orders` (
  `OrderID` int NOT NULL AUTO_INCREMENT,
  `TableNumber` int NOT NULL,
  `CustomerCount` int NOT NULL,
  `OrderStatus` varchar(20) NOT NULL,
  `ShiftID` int DEFAULT NULL,
  PRIMARY KEY (`OrderID`),
  KEY `ShiftID` (`ShiftID`),
  CONSTRAINT `orders_ibfk_1` FOREIGN KEY (`ShiftID`) REFERENCES `shifts` (`ShiftID`)
) ENGINE=InnoDB AUTO_INCREMENT=101 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orders`
--

LOCK TABLES `orders` WRITE;
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
INSERT INTO `orders` VALUES (1,1,4,'Готов',1),(2,2,2,'Оплачен',1),(3,3,3,'Оплачен',2),(4,4,5,'Оплачен',2),(5,5,2,'Готов',3),(99,1,1,'Новый',3),(100,2,2,'Новый',3);
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `shiftemployees`
--

DROP TABLE IF EXISTS `shiftemployees`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shiftemployees` (
  `ShiftEmployeeID` int NOT NULL AUTO_INCREMENT,
  `ShiftID` int DEFAULT NULL,
  `EmployeeID` int DEFAULT NULL,
  PRIMARY KEY (`ShiftEmployeeID`),
  KEY `ShiftID` (`ShiftID`),
  KEY `EmployeeID` (`EmployeeID`),
  CONSTRAINT `shiftemployees_ibfk_1` FOREIGN KEY (`ShiftID`) REFERENCES `shifts` (`ShiftID`),
  CONSTRAINT `shiftemployees_ibfk_2` FOREIGN KEY (`EmployeeID`) REFERENCES `employees` (`EmployeeID`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `shiftemployees`
--

LOCK TABLES `shiftemployees` WRITE;
/*!40000 ALTER TABLE `shiftemployees` DISABLE KEYS */;
INSERT INTO `shiftemployees` VALUES (1,1,1),(2,1,2),(3,2,3),(4,2,4),(5,3,5),(6,3,6),(7,3,3),(8,3,2),(9,3,3),(10,3,3),(11,3,4),(12,2,5),(13,3,5),(14,2,4),(15,3,4),(16,3,4),(17,3,6),(18,3,9),(19,1,5),(20,2,9),(21,1,9),(22,1,11);
/*!40000 ALTER TABLE `shiftemployees` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `shifts`
--

DROP TABLE IF EXISTS `shifts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `shifts` (
  `ShiftID` int NOT NULL AUTO_INCREMENT,
  `ShiftType` varchar(50) NOT NULL,
  `StartTime` datetime NOT NULL,
  `EndTime` datetime NOT NULL,
  PRIMARY KEY (`ShiftID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `shifts`
--

LOCK TABLES `shifts` WRITE;
/*!40000 ALTER TABLE `shifts` DISABLE KEYS */;
INSERT INTO `shifts` VALUES (1,'Утренняя смена','2024-02-26 08:00:00','2024-02-26 16:00:00'),(2,'Вечерняя смена','2024-02-26 16:00:00','2024-02-26 23:00:00'),(3,'Ночная смена','2024-02-27 23:00:00','2024-02-27 08:00:00');
/*!40000 ALTER TABLE `shifts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `UserID` int NOT NULL AUTO_INCREMENT,
  `Username` varchar(50) NOT NULL,
  `Password` varchar(50) NOT NULL,
  `Role` varchar(20) NOT NULL,
  PRIMARY KEY (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'1','1','Администратор'),(2,'2','2','Официант'),(3,'waiter2','waiterpass','Официант'),(4,'3','3','Повар'),(5,'cook2','cookpass','Повар'),(6,'manager','managerpass','Администратор'),(8,'12','12','Повар'),(9,'1','1','Официант'),(10,'Marsel','Loowe12','Повар');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-02-28  0:04:44
