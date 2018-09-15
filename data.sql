-- MySQL dump 10.13  Distrib 8.0.12, for Win64 (x86_64)
--
-- Host: localhost    Database: zimmer
-- ------------------------------------------------------
-- Server version	8.0.12

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `linkparts`
--

DROP TABLE IF EXISTS `linkparts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `linkparts` (
  `Code` varchar(20) NOT NULL,
  `Producer` varchar(35) NOT NULL,
  `Type` varchar(25) NOT NULL,
  PRIMARY KEY (`Code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `linkparts`
--

LOCK TABLES `linkparts` WRITE;
/*!40000 ALTER TABLE `linkparts` DISABLE KEYS */;
INSERT INTO `linkparts` VALUES ('13273625','GENERAL MOTORS','Оригинальный'),('1906144','IVECO','Оригинальный'),('19092981','IVECO','Оригинальный'),('29065','ZIMMERMANN','Торговый'),('34106797603','BMW','Оригинальный'),('424920','CITROEN/PEUGEOT','Оригинальный'),('424921','CITROEN/PEUGEOT','Оригинальный'),('569068','OPEL','Оригинальный'),('7H0615301E','VAG','Оригинальный'),('7H0615301F','VAG','Оригинальный'),('9467548387','FIAT','Оригинальный'),('99735140100','PORSCHE','Оригинальный'),('99735140101','PORSCHE','Оригинальный'),('SDB000634','LAND ROVER','Оригинальный'),('SDB000635','LAND ROVER','Оригинальный'),('SDB000636','LAND ROVER','Оригинальный');
/*!40000 ALTER TABLE `linkparts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `links`
--

DROP TABLE IF EXISTS `links`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `links` (
  `PartCode` varchar(20) NOT NULL,
  `LinkedPartCode` varchar(20) NOT NULL,
  PRIMARY KEY (`PartCode`,`LinkedPartCode`),
  KEY `LinkedPartCode` (`LinkedPartCode`),
  CONSTRAINT `links_ibfk_1` FOREIGN KEY (`PartCode`) REFERENCES `parts` (`code`),
  CONSTRAINT `links_ibfk_2` FOREIGN KEY (`LinkedPartCode`) REFERENCES `linkparts` (`code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `links`
--

LOCK TABLES `links` WRITE;
/*!40000 ALTER TABLE `links` DISABLE KEYS */;
INSERT INTO `links` VALUES ('430263270','13273625'),('290652201','1906144'),('290652201','19092981'),('290652201','29065'),('150290520','34106797603'),('440311620','424920'),('440311620','424921'),('430263270','569068'),('600323120','7H0615301E'),('600323120','7H0615301F'),('440311620','9467548387'),('460152920','99735140100'),('460152920','99735140101'),('450520952','SDB000634'),('450520952','SDB000635'),('450520952','SDB000636');
/*!40000 ALTER TABLE `links` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `parts`
--

DROP TABLE IF EXISTS `parts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `parts` (
  `Code` varchar(20) NOT NULL,
  `Url` varchar(120) NOT NULL,
  `Articul` varchar(20) NOT NULL,
  `Brand` varchar(30) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Characteristics` text NOT NULL,
  PRIMARY KEY (`Code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `parts`
--

LOCK TABLES `parts` WRITE;
/*!40000 ALTER TABLE `parts` DISABLE KEYS */;
INSERT INTO `parts` VALUES ('150290520','https://otto-zimmermann.com.ua/autoparts/product/ZIMMERMANN/150290520/','150.2905.20','ZIMMERMANN','Тормозной диск','<table class=proptab><tr class=head><td colspan=2>Характеристики:</td></tr>\n			<tr><td class=tarig>Тип тормозного диска: </td><td>с внутренней вентиляцией</td></tr>\n				<tr><td class=tarig>Обработка: </td><td>Высокоуглеродистый</td></tr>\n				<tr><td class=tarig>Поверхность: </td><td>с покрытием</td></tr>\n				<tr><td class=tarig>Тип тормозного диска: </td><td>с прорезом</td></tr>\n				<tr><td class=tarig>Наружный диаметр: </td><td>370 мм</td></tr>\n				<tr><td class=tarig>Толщина: </td><td>30 мм</td></tr>\n				<tr><td class=tarig>Минимальная толщина: </td><td>28,4 мм</td></tr>\n				<tr><td class=tarig>Высота: </td><td>73 мм</td></tr>\n				<tr><td class=tarig>Расположение/число отверстий: </td><td>6/5</td></tr>\n				<tr><td class=tarig>Вес: </td><td>13,2 кг</td></tr>\n				<tr><td class=tarig>Ø фаски 1: </td><td>120 мм</td></tr>\n				<tr><td class=tarig>Ø отверстия ступицы: </td><td>79 мм</td></tr>\n				<tr><td class=tarig>Номер ABE: </td><td>KBA 61364</td></tr>\n			</table></td><td>'),('290652201','https://otto-zimmermann.com.ua/autoparts/product/ZIMMERMANN/290652201/','29065.220.1','ZIMMERMANN','Комплект тормозных колодок, дисковый тормоз','<table class=proptab><tr class=head><td colspan=2>Характеристики:</td></tr>\n			<tr><td class=tarig>Ширина (мм): </td><td>174,8</td></tr>\n				<tr><td class=tarig>Высота: </td><td>85,6 мм</td></tr>\n				<tr><td class=tarig>Толщина: </td><td>22 мм</td></tr>\n				<tr><td class=tarig>Датчик износа: </td><td>подготовлено для датчика износа колодок</td></tr>\n			</table></td><td>'),('400645500','https://otto-zimmermann.com.ua/autoparts/product/ZIMMERMANN/400645500/','','','',''),('430263270','https://otto-zimmermann.com.ua/autoparts/product/ZIMMERMANN/430263270/','430.2632.70','ZIMMERMANN','Тормозной диск','<table class=proptab><tr class=head><td colspan=2>Характеристики:</td></tr>\n			<tr><td class=tarig>Тип тормозного диска: </td><td>вентилируемый</td></tr>\n				<tr><td class=tarig>Обработка: </td><td>Высокоуглеродистый</td></tr>\n				<tr><td class=tarig>Тип тормозного диска: </td><td>с отверстиями</td></tr>\n				<tr><td class=tarig>Наружный диаметр: </td><td>355 мм</td></tr>\n				<tr><td class=tarig>Толщина: </td><td>32 мм</td></tr>\n				<tr><td class=tarig>Минимальная толщина: </td><td>30 мм</td></tr>\n				<tr><td class=tarig>Высота: </td><td>50 мм</td></tr>\n				<tr><td class=tarig>Расположение/число отверстий: </td><td>6/5</td></tr>\n				<tr><td class=tarig>Вес: </td><td>10,1 кг</td></tr>\n				<tr><td class=tarig>Ø фаски 1: </td><td>120 мм</td></tr>\n				<tr><td class=tarig>Ø отверстия ступицы: </td><td>67,4 мм</td></tr>\n				<tr><td class=tarig>Качество: </td><td>Verbundwerkstoff</td></tr>\n				<tr><td class=tarig>Номер ABE: </td><td>KBA 61183</td></tr>\n			</table></td><td>'),('440311620','https://otto-zimmermann.com.ua/autoparts/product/ZIMMERMANN/440311620/','440.3116.20','ZIMMERMANN','Тормозной диск','<table class=proptab><tr class=head><td colspan=2>Характеристики:</td></tr>\n			<tr><td class=tarig>Тип тормозного диска: </td><td>с внешней вентиляцией</td></tr>\n				<tr><td class=tarig>Поверхность: </td><td>с покрытием</td></tr>\n				<tr><td class=tarig>Наружный диаметр: </td><td>280 мм</td></tr>\n				<tr><td class=tarig>Толщина: </td><td>28 мм</td></tr>\n				<tr><td class=tarig>Минимальная толщина: </td><td>26 мм</td></tr>\n				<tr><td class=tarig>Высота: </td><td>48 мм</td></tr>\n				<tr><td class=tarig>Расположение/число отверстий: </td><td>8/5</td></tr>\n				<tr><td class=tarig>Вес: </td><td>7,9 кг</td></tr>\n				<tr><td class=tarig>Ø фаски 1: </td><td>108 мм</td></tr>\n				<tr><td class=tarig>Ø отверстия ступицы: </td><td>71 мм</td></tr>\n			</table></td><td>'),('450520952','https://otto-zimmermann.com.ua/autoparts/product/ZIMMERMANN/450520952/','450.5209.52','ZIMMERMANN','Тормозной диск','<table class=proptab><tr class=head><td colspan=2>Характеристики:</td></tr>\n			<tr><td class=tarig>Тип тормозного диска: </td><td>с внешней вентиляцией</td></tr>\n				<tr><td class=tarig>Тип тормозного диска: </td><td>с отверстиями</td></tr>\n				<tr><td class=tarig>Поверхность: </td><td>с покрытием</td></tr>\n				<tr><td class=tarig>Наружный диаметр: </td><td>325 мм</td></tr>\n				<tr><td class=tarig>Толщина: </td><td>20 мм</td></tr>\n				<tr><td class=tarig>Минимальная толщина: </td><td>17 мм</td></tr>\n				<tr><td class=tarig>Высота: </td><td>59,5 мм</td></tr>\n				<tr><td class=tarig>Расположение/число отверстий: </td><td>7/5</td></tr>\n				<tr><td class=tarig>Вес: </td><td>7,7 кг</td></tr>\n				<tr><td class=tarig>Ø фаски 1: </td><td>120 мм</td></tr>\n				<tr><td class=tarig>Ø отверстия ступицы: </td><td>79 мм</td></tr>\n				<tr><td class=tarig>Номер ABE: </td><td>KBA 60872</td></tr>\n			</table></td><td>'),('460152920','https://otto-zimmermann.com.ua/autoparts/product/ZIMMERMANN/460152920/','460.1529.20','ZIMMERMANN','Тормозной диск','<table class=proptab><tr class=head><td colspan=2>Характеристики:</td></tr>\n			<tr><td class=tarig>Тип тормозного диска: </td><td>с внутренней вентиляцией</td></tr>\n				<tr><td class=tarig>Тип тормозного диска: </td><td>с отверстиями</td></tr>\n				<tr><td class=tarig>Поверхность: </td><td>с покрытием</td></tr>\n				<tr><td class=tarig>Наружный диаметр: </td><td>330 мм</td></tr>\n				<tr><td class=tarig>Толщина: </td><td>28 мм</td></tr>\n				<tr><td class=tarig>Минимальная толщина: </td><td>26 мм</td></tr>\n				<tr><td class=tarig>Высота: </td><td>69 мм</td></tr>\n				<tr><td class=tarig>Расположение/число отверстий: </td><td>7/5</td></tr>\n				<tr><td class=tarig>Вес: </td><td>9,2 кг</td></tr>\n				<tr><td class=tarig>Ø фаски 1: </td><td>130 мм</td></tr>\n				<tr><td class=tarig>Ø отверстия ступицы: </td><td>98 мм</td></tr>\n			</table></td><td>'),('600323120','https://otto-zimmermann.com.ua/autoparts/product/ZIMMERMANN/600323120/','600.3231.20','ZIMMERMANN','Тормозной диск','<table class=proptab><tr class=head><td colspan=2>Характеристики:</td></tr>\n			<tr><td class=tarig>Тип тормозного диска: </td><td>с внутренней вентиляцией</td></tr>\n				<tr><td class=tarig>Поверхность: </td><td>с покрытием</td></tr>\n				<tr><td class=tarig>Обработка: </td><td>легированный/высокоуглеродистый</td></tr>\n				<tr><td class=tarig>Наружный диаметр: </td><td>333 мм</td></tr>\n				<tr><td class=tarig>Толщина: </td><td>32,5 мм</td></tr>\n				<tr><td class=tarig>Минимальная толщина: </td><td>28,5 мм</td></tr>\n				<tr><td class=tarig>Высота: </td><td>55,9 мм</td></tr>\n				<tr><td class=tarig>Расположение/число отверстий: </td><td>6/5</td></tr>\n				<tr><td class=tarig>Вес: </td><td>11,62 кг</td></tr>\n				<tr><td class=tarig>Ø фаски 1: </td><td>120 мм</td></tr>\n				<tr><td class=tarig>Ø отверстия ступицы: </td><td>76 мм</td></tr>\n			</table></td><td>'),('LR083935','https://otto-zimmermann.com.ua/autoparts/product/ZIMMERMANN/LR083935/','','','','');
/*!40000 ALTER TABLE `parts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'zimmer'
--
/*!50003 DROP PROCEDURE IF EXISTS `GetAllPartsInfo` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetAllPartsInfo`( partCodeFilter varchar(20), partNameFilter varchar(100), linkPartCodeFilter varchar(20))
begin
    select p.code, p.brand, p.name, lp.code, lp.producer from parts p
	left join links l
	on p.code = l.partcode
	left join linkparts lp
	on lp.code = l.linkedpartcode
    where (partCodeFilter = '' OR p.code = partCodeFilter) and 
    (linkPartCodeFilter = '' OR lp.code = linkPartCodeFilter) and
    locate(partNameFilter, p.name) > 0;
end ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-09-15 21:01:29
