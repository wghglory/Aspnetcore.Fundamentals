/*
 Navicat Premium Data Transfer

 Source Server         : mysqlRoot
 Source Server Type    : MySQL
 Source Server Version : 50719
 Source Host           : localhost
 Source Database       : AspnetcoreFundamentalsFood

 Target Server Type    : MySQL
 Target Server Version : 50719
 File Encoding         : utf-8

 Date: 07/20/2017 12:06:59 PM
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
--  Table structure for `AspNetRoleClaims`
-- ----------------------------
DROP TABLE IF EXISTS `AspNetRoleClaims`;
CREATE TABLE `AspNetRoleClaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  `RoleId` varchar(127) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
--  Table structure for `AspNetRoles`
-- ----------------------------
DROP TABLE IF EXISTS `AspNetRoles`;
CREATE TABLE `AspNetRoles` (
  `Id` varchar(127) NOT NULL,
  `ConcurrencyStamp` longtext,
  `Name` varchar(256) DEFAULT NULL,
  `NormalizedName` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
--  Table structure for `AspNetUserClaims`
-- ----------------------------
DROP TABLE IF EXISTS `AspNetUserClaims`;
CREATE TABLE `AspNetUserClaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  `UserId` varchar(127) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
--  Table structure for `AspNetUserLogins`
-- ----------------------------
DROP TABLE IF EXISTS `AspNetUserLogins`;
CREATE TABLE `AspNetUserLogins` (
  `LoginProvider` varchar(127) NOT NULL,
  `ProviderKey` varchar(127) NOT NULL,
  `ProviderDisplayName` longtext,
  `UserId` varchar(127) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
--  Table structure for `AspNetUserRoles`
-- ----------------------------
DROP TABLE IF EXISTS `AspNetUserRoles`;
CREATE TABLE `AspNetUserRoles` (
  `UserId` varchar(127) NOT NULL,
  `RoleId` varchar(127) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
--  Table structure for `AspNetUserTokens`
-- ----------------------------
DROP TABLE IF EXISTS `AspNetUserTokens`;
CREATE TABLE `AspNetUserTokens` (
  `UserId` varchar(127) NOT NULL,
  `LoginProvider` varchar(127) NOT NULL,
  `Name` varchar(127) NOT NULL,
  `Value` longtext,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
--  Table structure for `AspNetUsers`
-- ----------------------------
DROP TABLE IF EXISTS `AspNetUsers`;
CREATE TABLE `AspNetUsers` (
  `Id` varchar(127) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  `ConcurrencyStamp` longtext,
  `Email` varchar(256) DEFAULT NULL,
  `EmailConfirmed` bit(1) NOT NULL,
  `LockoutEnabled` bit(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `NormalizedEmail` varchar(256) DEFAULT NULL,
  `NormalizedUserName` varchar(256) DEFAULT NULL,
  `PasswordHash` longtext,
  `PhoneNumber` longtext,
  `PhoneNumberConfirmed` bit(1) NOT NULL,
  `SecurityStamp` longtext,
  `TwoFactorEnabled` bit(1) NOT NULL,
  `UserName` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
--  Records of `AspNetUsers`
-- ----------------------------
BEGIN;
INSERT INTO `AspNetUsers` VALUES ('e077bff8-70b2-4e67-86f2-f8c8e3ac6475', '0', '4bcc2c6d-19bd-4495-b746-e2afe8fdc6ab', null, b'0', b'1', null, null, 'WGHGLORY', 'AQAAAAEAACcQAAAAEMfe7duCnl4XEFZx9D6FjqwUlL/RlCITBhUvivTMVv8+5f2il2JZuF8UEULYm/iGhg==', null, b'0', 'fce5a890-6b78-4549-9c6b-f9fdbb963509', b'0', 'wghglory');
COMMIT;

-- ----------------------------
--  Table structure for `Restaurants`
-- ----------------------------
DROP TABLE IF EXISTS `Restaurants`;
CREATE TABLE `Restaurants` (
  `Id` int(11) NOT NULL,
  `Cuisine` int(11) NOT NULL,
  `Name` varchar(80) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
--  Records of `Restaurants`
-- ----------------------------
BEGIN;
INSERT INTO `Restaurants` VALUES ('1', '3', 'Ramen'), ('2', '3', 'Hotpot'), ('3', '3', 'Chicken'), ('4', '2', 'Pork Rice'), ('5', '2', 'Zuni '), ('6', '2', 'Apple Pie');
COMMIT;

-- ----------------------------
--  Table structure for `__EFMigrationsHistory`
-- ----------------------------
DROP TABLE IF EXISTS `__EFMigrationsHistory`;
CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
--  Records of `__EFMigrationsHistory`
-- ----------------------------
BEGIN;
INSERT INTO `__EFMigrationsHistory` VALUES ('20170719062805_InitCreate', '1.1.2'), ('20170720030818_Identity', '1.1.2'), ('20170720035906_UpdateConnectionString', '1.1.2'), ('20170720040447_UpdateConnectionString', '1.1.2');
COMMIT;

SET FOREIGN_KEY_CHECKS = 1;
