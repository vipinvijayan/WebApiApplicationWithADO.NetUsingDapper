-- MySQL dump 10.13  Distrib 8.0.31, for Win64 (x86_64)
--
-- Host   Database: DryCleanerDB
-- ------------------------------------------------------
-- Server version	

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
-- Table structure for table `companyEntities`
--

DROP TABLE IF EXISTS `companyEntities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `companyEntities` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `CompanyName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CompanyDescription` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `CompanyAddress` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `CompanyCity` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `CompanyState` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `CompanyCountry` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `CompanyPhone` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `CompanyEmail` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `CreatedBy` int NOT NULL,
  `UpdatedBy` int NOT NULL,
  `CreatedOn` int NOT NULL,
  `UpdatedOn` int NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT '0',
  `IsActive` tinyint(1) NOT NULL DEFAULT '1',
  `LandMark` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Place` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `loginLog`
--

DROP TABLE IF EXISTS `loginLog`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `loginLog` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreatedOn` int NOT NULL,
  `UserAgent` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `UserDataId` int NOT NULL,
  `CompanyId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_loginLog_CompanyId` (`CompanyId`),
  KEY `IX_loginLog_UserDataId` (`UserDataId`),
  CONSTRAINT `FK_loginLog_companyEntities_CompanyId` FOREIGN KEY (`CompanyId`) REFERENCES `companyEntities` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_loginLog_users_UserDataId` FOREIGN KEY (`UserDataId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `refreshTokenEntities`
--

DROP TABLE IF EXISTS `refreshTokenEntities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `refreshTokenEntities` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `RefreshToken` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Expires` datetime(6) NOT NULL,
  `CreatedOn` datetime(6) NOT NULL,
  `CreatedByID` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ReplaceByToken` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `CompanyId` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LastName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `MobileNo` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreatedBy` int NOT NULL,
  `UpdatedBy` int NOT NULL,
  `CreatedOn` int NOT NULL,
  `UpdatedOn` int NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `Username` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Password` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CompanyId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_users_CompanyId` (`CompanyId`),
  CONSTRAINT `FK_users_companyEntities_CompanyId` FOREIGN KEY (`CompanyId`) REFERENCES `companyEntities` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `usersAddress`
--

DROP TABLE IF EXISTS `usersAddress`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usersAddress` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `AddressType` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `AddressFor` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Location` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Address` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PostalCode` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `City` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `State` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Country` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ContactNo` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LandMark` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreatedBy` int NOT NULL,
  `UpdatedBy` int NOT NULL,
  `CreatedOn` int NOT NULL,
  `UpdatedOn` int NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `UserEntityId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_usersAddress_UserEntityId` (`UserEntityId`),
  CONSTRAINT `FK_usersAddress_users_UserEntityId` FOREIGN KEY (`UserEntityId`) REFERENCES `users` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping routines for database 'DryCleanerDB'
--
/*!50003 DROP PROCEDURE IF EXISTS `CreateCompany` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE PROCEDURE `CreateCompany`(IN CompanyName varchar(100),IN CompanyDescription varchar(300),IN CompanyAddress varchar(300)
,IN CompanyCity varchar(100),IN CompanyState varchar(100),IN CompanyCountry varchar(100),IN CompanyPhone varchar(100)
,IN CompanyEmail varchar(100),IN CreatedBy int,IN LandMark varchar(100),IN Place varchar(100))
BEGIN

Declare AlreadyCount int;
Declare ResultMessage varchar(10) Default 'Failed';

DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT 'SQLException encountered' ResultMessage; 
START TRANSACTION;
set AlreadyCount=(select count(`Id`) from `companyEntities` where `companyEntities`.`CompanyName`=CompanyName and 
`companyEntities`.`CompanyPhone`= CompanyPhone and `companyEntities`.`CompanyAddress`=CompanyAddress and 
`companyEntities`.`Place`=Place and `companyEntities`.`IsActive`=b'1');
if(AlreadyCount>0) then
	Begin
    set ResultMessage='Already' ;
	End;
Else
	Begin
		INSERT INTO `companyEntities`(`CompanyName`,`CompanyDescription`,`CompanyAddress`,`CompanyCity`,`CompanyState`,`CompanyCountry`,
		`CompanyPhone`,`CompanyEmail`,`CreatedBy`,`UpdatedBy`,`CreatedOn`,`UpdatedOn`,`IsDeleted`,`IsActive`,`LandMark`,`Place`)
		VALUES
		(CompanyName,CompanyDescription,CompanyAddress,CompanyCity,CompanyState,CompanyCountry,
		CompanyPhone,CompanyEmail,CreatedBy,CreatedBy,unix_timestamp(),unix_timestamp(),0,1,LandMark,Place);
		set ResultMessage='Success';
        Commit;  
	End;
	End If;
select ResultMessage;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `DeleteCompany` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE PROCEDURE `DeleteCompany`(IN Id int,IN UpdatedBy int)
BEGIN

Declare AlreadyCount int;
Declare ResultMessage varchar(10) Default 'Failed';

DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT 'SQLException encountered' ResultMessage; 
START TRANSACTION;
set AlreadyCount=(select count(`Id`) from `companyEntities` where `companyEntities`.`Id`=Id);
if(AlreadyCount<=0) then
	Begin
		set ResultMessage='No Company Exist' ;
	End;
Else
	Begin
		Update `companyEntities` set `IsActive`=b'0',`IsDeleted`=b'1',`UpdatedBy`=UpdatedBy,`UpdatedOn`=unix_timestamp()
		where `companyEntities`.`Id`=Id;
		set ResultMessage='Success';
		Commit;
	End;
    End If;
	
select ResultMessage;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateCompany` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE PROCEDURE `UpdateCompany`(IN Id int,IN CompanyName varchar(100),IN CompanyDescription varchar(300),IN CompanyAddress varchar(300)
,IN CompanyCity varchar(100),IN CompanyState varchar(100),IN CompanyCountry varchar(100),IN CompanyPhone varchar(100)
,IN CompanyEmail varchar(100),IN UpdatedBy int,IN LandMark varchar(100),IN Place varchar(100))
BEGIN

Declare AlreadyCount int;
Declare ResultMessage varchar(50) Default 'Failed';

DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT 'SQLException encountered' ResultMessage; 
START TRANSACTION;
set AlreadyCount=(select count(`Id`) from `companyEntities` where `companyEntities`.`Id`=Id);
if(AlreadyCount<=0) then
	Begin
		set ResultMessage='No Company Exist' ;
	End;
Else
	Begin
		Update `companyEntities` set `CompanyName`=CompanyName,`CompanyDescription`=CompanyDescription,`CompanyAddress`=CompanyAddress
		,`CompanyCity`=CompanyCity,`CompanyState`=CompanyState,`CompanyCountry`=CompanyCountry,	`CompanyPhone`=CompanyPhone
		,`CompanyEmail`=CompanyEmail,`UpdatedBy`=UpdatedBy,`UpdatedOn`=unix_timestamp(),`LandMark`=LandMark,`Place`=Place where `companyEntities`.`Id`=Id;
		set ResultMessage='Success';
		Commit;
	End;
    End If;
	
select ResultMessage;
END ;;
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

