-- --------------------------------------------------------
-- Servidor:                     127.0.0.1
-- Versão do servidor:           8.1.0 - MySQL Community Server - GPL
-- OS do Servidor:               Win64
-- HeidiSQL Versão:              12.6.0.6782
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Copiando estrutura do banco de dados para task_mgmt
CREATE DATABASE IF NOT EXISTS `task_mgmt` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_tr_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `task_mgmt`;

-- Copiando estrutura para tabela task_mgmt.context_task
CREATE TABLE IF NOT EXISTS `context_task` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(50) COLLATE utf8mb4_tr_0900_ai_ci NOT NULL,
  `Description` varchar(150) COLLATE utf8mb4_tr_0900_ai_ci DEFAULT NULL,
  `DueDate` datetime NOT NULL,
  `Priority` tinyint NOT NULL DEFAULT (0),
  `Status` tinyint NOT NULL DEFAULT (0),
  `ProjectId` int NOT NULL,
  `UpdateAt` datetime NOT NULL DEFAULT (now()),
  `UserId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_context_task_project` (`ProjectId`),
  CONSTRAINT `FK_context_task_project` FOREIGN KEY (`ProjectId`) REFERENCES `project` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_tr_0900_ai_ci;

-- Exportação de dados foi desmarcado.

-- Copiando estrutura para tabela task_mgmt.historical_task
CREATE TABLE IF NOT EXISTS `historical_task` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_tr_0900_ai_ci NOT NULL,
  `Description` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_tr_0900_ai_ci DEFAULT NULL,
  `DueDate` datetime NOT NULL,
  `Priority` tinyint NOT NULL,
  `Status` tinyint NOT NULL,
  `ContextTaskId` int NOT NULL,
  `UpdateAt` datetime NOT NULL,
  `UserId` int NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  KEY `FK__project` (`ContextTaskId`) USING BTREE,
  CONSTRAINT `FK_historical_task_context_task` FOREIGN KEY (`ContextTaskId`) REFERENCES `context_task` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_tr_0900_ai_ci ROW_FORMAT=DYNAMIC;

-- Exportação de dados foi desmarcado.

-- Copiando estrutura para tabela task_mgmt.project
CREATE TABLE IF NOT EXISTS `project` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(50) COLLATE utf8mb4_tr_0900_ai_ci NOT NULL,
  `Description` varchar(100) COLLATE utf8mb4_tr_0900_ai_ci NOT NULL,
  `UpdateAt` datetime NOT NULL DEFAULT (now()),
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_tr_0900_ai_ci;

-- Exportação de dados foi desmarcado.

-- Copiando estrutura para tabela task_mgmt.task_comment
CREATE TABLE IF NOT EXISTS `task_comment` (
  `Id` int NOT NULL,
  `ContextTaskId` int DEFAULT NULL,
  `Comment` text COLLATE utf8mb4_tr_0900_ai_ci,
  `UserId` int NOT NULL,
  `UpdateAt` datetime NOT NULL DEFAULT (now()),
  `Enabled` bit(1) NOT NULL DEFAULT (1),
  PRIMARY KEY (`Id`),
  KEY `FK_task_comment_context_task` (`ContextTaskId`),
  CONSTRAINT `FK_task_comment_context_task` FOREIGN KEY (`ContextTaskId`) REFERENCES `context_task` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_tr_0900_ai_ci;

-- Exportação de dados foi desmarcado.

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
