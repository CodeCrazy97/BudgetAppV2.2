-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               10.1.30-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win32
-- HeidiSQL Version:             9.5.0.5196
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for budget
CREATE DATABASE IF NOT EXISTS `budget` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `budget`;

-- Dumping structure for table budget.expenses
CREATE TABLE IF NOT EXISTS `expenses` (
  `trans_date` date NOT NULL,
  `description` text NOT NULL,
  `amount` double NOT NULL,
  `transid` int(11) NOT NULL AUTO_INCREMENT,
  `expensetype` varchar(25) DEFAULT NULL,
  PRIMARY KEY (`transid`),
  KEY `expensetype_fk` (`expensetype`),
  CONSTRAINT `expensetype_fk` FOREIGN KEY (`expensetype`) REFERENCES `expensetypes` (`typeName`)
) ENGINE=InnoDB AUTO_INCREMENT=238 DEFAULT CHARSET=latin1;

-- Data exporting was unselected.
-- Dumping structure for table budget.expensetypes
CREATE TABLE IF NOT EXISTS `expensetypes` (
  `typeName` varchar(25) NOT NULL,
  PRIMARY KEY (`typeName`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.
-- Dumping structure for table budget.grosswages
CREATE TABLE IF NOT EXISTS `grosswages` (
  `wageYear` int(4) NOT NULL,
  `job` varchar(30) NOT NULL,
  `amount` double NOT NULL,
  PRIMARY KEY (`wageYear`,`job`),
  KEY `job_fk2` (`job`),
  CONSTRAINT `job_fk2` FOREIGN KEY (`job`) REFERENCES `jobs` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.
-- Dumping structure for table budget.jobs
CREATE TABLE IF NOT EXISTS `jobs` (
  `name` varchar(20) NOT NULL,
  PRIMARY KEY (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.
-- Dumping structure for table budget.otherearnings
CREATE TABLE IF NOT EXISTS `otherearnings` (
  `earning_date` date NOT NULL,
  `description` text NOT NULL,
  `amount` double NOT NULL,
  `earningid` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`earningid`)
) ENGINE=InnoDB AUTO_INCREMENT=256 DEFAULT CHARSET=latin1;

-- Data exporting was unselected.
-- Dumping structure for table budget.taxation
CREATE TABLE IF NOT EXISTS `taxation` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `job` varchar(30) NOT NULL,
  `transactionYear` int(4) NOT NULL,
  `taxTypeName` varchar(20) NOT NULL,
  `amount` double NOT NULL,
  PRIMARY KEY (`id`),
  KEY `job_fk` (`job`),
  KEY `taxTypeName_fk` (`taxTypeName`),
  CONSTRAINT `job_fk` FOREIGN KEY (`job`) REFERENCES `jobs` (`name`),
  CONSTRAINT `taxTypeName_fk` FOREIGN KEY (`taxTypeName`) REFERENCES `taxtypes` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=latin1;

-- Data exporting was unselected.
-- Dumping structure for table budget.taxliability
CREATE TABLE IF NOT EXISTS `taxliability` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `taxYear` int(4) NOT NULL,
  `amount` double NOT NULL,
  `description` varchar(1000) DEFAULT NULL,
  `taxType` varchar(20) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `taxType_fk3` (`taxType`),
  CONSTRAINT `taxType_fk3` FOREIGN KEY (`taxType`) REFERENCES `taxtypes` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;

-- Data exporting was unselected.
-- Dumping structure for table budget.taxreturn
CREATE TABLE IF NOT EXISTS `taxreturn` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `taxYear` int(4) NOT NULL,
  `amount` double NOT NULL,
  `description` varchar(1000) DEFAULT NULL,
  `taxType` varchar(20) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `taxType_fk2` (`taxType`),
  CONSTRAINT `taxType_fk2` FOREIGN KEY (`taxType`) REFERENCES `taxtypes` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;

-- Data exporting was unselected.
-- Dumping structure for table budget.taxtypes
CREATE TABLE IF NOT EXISTS `taxtypes` (
  `name` varchar(20) NOT NULL,
  PRIMARY KEY (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
