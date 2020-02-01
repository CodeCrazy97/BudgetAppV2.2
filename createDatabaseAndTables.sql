-- --------------------------------------------------------
-- Host:                         localhost
-- Server version:               10.3.16-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win64
-- HeidiSQL Version:             10.2.0.5599
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for budget
CREATE DATABASE IF NOT EXISTS `budget` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `budget`;

-- Dumping structure for table budget.charity
CREATE TABLE IF NOT EXISTS `charity` (
  `trans_date` date DEFAULT NULL,
  `description` text DEFAULT NULL,
  `amount` double DEFAULT NULL,
  `trans_id` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`trans_id`)
) ENGINE=InnoDB AUTO_INCREMENT=42 DEFAULT CHARSET=latin1;

-- Data exporting was unselected.

-- Dumping structure for table budget.expenses
CREATE TABLE IF NOT EXISTS `expenses` (
  `trans_date` date NOT NULL,
  `description` text NOT NULL,
  `amount` double NOT NULL,
  `trans_id` int(11) NOT NULL AUTO_INCREMENT,
  `expense_type` varchar(25) DEFAULT NULL,
  PRIMARY KEY (`trans_id`),
  KEY `expensetype_fk` (`expense_type`),
  CONSTRAINT `expensetype_fk` FOREIGN KEY (`expense_type`) REFERENCES `expense_types` (`type_name`)
) ENGINE=InnoDB AUTO_INCREMENT=1060 DEFAULT CHARSET=latin1;

-- Data exporting was unselected.

-- Dumping structure for table budget.expense_types
CREATE TABLE IF NOT EXISTS `expense_types` (
  `type_name` varchar(25) NOT NULL,
  PRIMARY KEY (`type_name`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.

-- Dumping structure for table budget.gross_wages
CREATE TABLE IF NOT EXISTS `gross_wages` (
  `wage_year` int(4) NOT NULL,
  `job` varchar(30) NOT NULL,
  `amount` double NOT NULL,
  PRIMARY KEY (`wage_year`,`job`),
  KEY `job_fk2` (`job`),
  CONSTRAINT `job_fk2` FOREIGN KEY (`job`) REFERENCES `jobs` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='This holds the gross amount earned at a particular job for a particular year.';

-- Data exporting was unselected.

-- Dumping structure for table budget.jobs
CREATE TABLE IF NOT EXISTS `jobs` (
  `name` varchar(20) NOT NULL,
  PRIMARY KEY (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.

-- Dumping structure for table budget.other_earnings
CREATE TABLE IF NOT EXISTS `other_earnings` (
  `earning_date` date NOT NULL,
  `description` text NOT NULL,
  `amount` double NOT NULL,
  `earning_id` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`earning_id`)
) ENGINE=InnoDB AUTO_INCREMENT=281 DEFAULT CHARSET=latin1;

-- Data exporting was unselected.

-- Dumping structure for table budget.taxation
CREATE TABLE IF NOT EXISTS `taxation` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `job` varchar(30) NOT NULL,
  `transaction_year` int(4) NOT NULL,
  `tax_type_name` varchar(20) NOT NULL,
  `amount` double NOT NULL,
  PRIMARY KEY (`id`),
  KEY `job_fk` (`job`),
  KEY `taxTypeName_fk` (`tax_type_name`),
  CONSTRAINT `job_fk` FOREIGN KEY (`job`) REFERENCES `jobs` (`name`),
  CONSTRAINT `taxTypeName_fk` FOREIGN KEY (`tax_type_name`) REFERENCES `tax_types` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=latin1;

-- Data exporting was unselected.

-- Dumping structure for table budget.tax_liability
CREATE TABLE IF NOT EXISTS `tax_liability` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `tax_year` int(4) NOT NULL,
  `amount` double NOT NULL,
  `description` varchar(1000) DEFAULT NULL,
  `tax_type` varchar(20) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `taxType_fk3` (`tax_type`),
  CONSTRAINT `taxType_fk3` FOREIGN KEY (`tax_type`) REFERENCES `tax_types` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;

-- Data exporting was unselected.

-- Dumping structure for table budget.tax_return
CREATE TABLE IF NOT EXISTS `tax_return` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `tax_year` int(4) NOT NULL,
  `amount` double NOT NULL,
  `description` varchar(1000) DEFAULT NULL,
  `tax_type` varchar(20) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `taxType_fk2` (`tax_type`),
  CONSTRAINT `taxType_fk2` FOREIGN KEY (`tax_type`) REFERENCES `tax_types` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

-- Data exporting was unselected.

-- Dumping structure for table budget.tax_types
CREATE TABLE IF NOT EXISTS `tax_types` (
  `name` varchar(20) NOT NULL,
  PRIMARY KEY (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
