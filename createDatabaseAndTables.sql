-- --------------------------------------------------------
-- Host:                         C:\Users\Ethan\Documents\Projects\C#\BudgetApp\budget.db
-- Server version:               3.39.4
-- Server OS:                    
-- HeidiSQL Version:             12.3.0.6589
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES  */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for budget
DROP DATABASE IF EXISTS "budget";
CREATE DATABASE IF NOT EXISTS "budget";
;

-- Dumping structure for table budget.expenses
DROP TABLE IF EXISTS "expenses";
CREATE TABLE IF NOT EXISTS `expenses` (
  `trans_date` date NOT NULL,
  `description` text NOT NULL,
  `amount` double NOT NULL,
  `trans_id` INTEGER PRIMARY KEY AUTOINCREMENT,
  `expense_type` TEXT NOT NULL,
  CONSTRAINT `expenses_ibfk_1` FOREIGN KEY (`expense_type`) REFERENCES `expense_types` (`type_name`)
);

-- Data exporting was unselected.

-- Dumping structure for table budget.expense_types
DROP TABLE IF EXISTS "expense_types";
CREATE TABLE IF NOT EXISTS `expense_types` (
    `type_name` TEXT PRIMARY KEY
);

-- Data exporting was unselected.

-- Dumping structure for table budget.gross_wages
DROP TABLE IF EXISTS "gross_wages";
CREATE TABLE IF NOT EXISTS `gross_wages` (
  `wage_year` int(11) NOT NULL,
  `amount` double NOT NULL DEFAULT 0,
  `job_name` varchar(50) NOT NULL DEFAULT 'PhishingBox',
  `description` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`wage_year`,`job_name`),
  CONSTRAINT `gross_wages_fk` FOREIGN KEY (`job_name`) REFERENCES `jobs` (`name`) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- Data exporting was unselected.

-- Dumping structure for table budget.jobs
DROP TABLE IF EXISTS "jobs";
CREATE TABLE IF NOT EXISTS `jobs` (
  `name` varchar(20) NOT NULL,
  PRIMARY KEY (`name`)
);

-- Data exporting was unselected.

-- Dumping structure for table budget.other_earnings
DROP TABLE IF EXISTS "other_earnings";
CREATE TABLE IF NOT EXISTS `other_earnings` (
  `earning_date` date NOT NULL,
  `description` text NOT NULL,
  `amount` double NOT NULL,
  `earning_id` INTEGER PRIMARY KEY AUTOINCREMENT
);

-- Data exporting was unselected.

-- Dumping structure for table budget.taxation
DROP TABLE IF EXISTS "taxation";
CREATE TABLE IF NOT EXISTS `taxation` (
  `id` INTEGER PRIMARY KEY AUTOINCREMENT,
  `job` varchar(30) NOT NULL,
  `transaction_year` int(4) NOT NULL,
  `tax_type_name` varchar(20) NOT NULL,
  `amount` double NOT NULL,
  CONSTRAINT `taxation_ibfk_1` FOREIGN KEY (`job`) REFERENCES `jobs` (`name`),
  CONSTRAINT `taxation_ibfk_2` FOREIGN KEY (`tax_type_name`) REFERENCES `tax_types` (`name`)
);

-- Data exporting was unselected.

-- Dumping structure for table budget.tax_liability
DROP TABLE IF EXISTS "tax_liability";
CREATE TABLE IF NOT EXISTS `tax_liability` (
  `tax_year` int(11) NOT NULL,
  `amount` double NOT NULL,
  `description` varchar(50) DEFAULT NULL,
  `tax_type` varchar(50) NOT NULL DEFAULT 'Federal',
  PRIMARY KEY (`tax_year`,`tax_type`),
  CONSTRAINT `tax_liability_fk` FOREIGN KEY (`tax_type`) REFERENCES `tax_types` (`name`) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- Data exporting was unselected.

-- Dumping structure for table budget.tax_return
DROP TABLE IF EXISTS "tax_return";
CREATE TABLE IF NOT EXISTS `tax_return` (
  `tax_year` int(11) NOT NULL,
  `amount` double NOT NULL DEFAULT 0,
  `tax_type` varchar(50) NOT NULL DEFAULT 'Federal',
  `description` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`tax_year`,`tax_type`),
  CONSTRAINT `tax_types_fk` FOREIGN KEY (`tax_type`) REFERENCES `tax_types` (`name`) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- Data exporting was unselected.

-- Dumping structure for table budget.tax_types
DROP TABLE IF EXISTS "tax_types";
CREATE TABLE IF NOT EXISTS `tax_types` (
  `name` varchar(20) NOT NULL,
  PRIMARY KEY (`name`)
);

-- Data exporting was unselected.

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
