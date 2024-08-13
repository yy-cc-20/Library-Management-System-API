-- MySQL Script generated by MySQL Workbench
-- Tue Jul 23 09:59:35 2024
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema library-management-system
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema library-management-system
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `library-management-system` DEFAULT CHARACTER SET utf8 ;
USE `library-management-system` ;

-- -----------------------------------------------------
-- Table `library-management-system`.`Category`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `library-management-system`.`Category` ;

CREATE TABLE IF NOT EXISTS `library-management-system`.`Category` (
  `Id` CHAR(36) NOT NULL,
  `Name` VARCHAR(45) NOT NULL UNIQUE,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `library-management-system`.`Book`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `library-management-system`.`Book` ;

CREATE TABLE IF NOT EXISTS `library-management-system`.`Book` (
  `Id` CHAR(36) NOT NULL,
  `Name` VARCHAR(45) NOT NULL,
  `Author` VARCHAR(45) NOT NULL,
  `Category_Id` CHAR(36) NOT NULL,
  PRIMARY KEY (`Id`, `Category_Id`),
  INDEX `fk_Book_Category_idx` (`Category_Id` ASC) VISIBLE,
  CONSTRAINT `fk_Book_Category`
    FOREIGN KEY (`Category_Id`)
    REFERENCES `library-management-system`.`Category` (`Id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
