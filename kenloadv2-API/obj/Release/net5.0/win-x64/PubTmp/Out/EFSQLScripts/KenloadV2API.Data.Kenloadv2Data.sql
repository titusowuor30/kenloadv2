CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET utf8mb4;

START TRANSACTION;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    ALTER DATABASE CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `AccessControlPasswords` (
        `id` int NOT NULL AUTO_INCREMENT,
        `screenid` int NOT NULL,
        `acp_pass` longtext CHARACTER SET utf8mb4 NULL,
        `username` longtext CHARACTER SET utf8mb4 NULL,
        `level` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_AccessControlPasswords` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `AccessPassPolicy` (
        `id` int NOT NULL AUTO_INCREMENT,
        `app_char` int NOT NULL,
        `app_day_exp` int NOT NULL,
        `app_capital` int NOT NULL,
        `app_small` int NOT NULL,
        `app_numbers` int NOT NULL,
        `app_logoff_period` int NOT NULL,
        `app_min_char` int NOT NULL,
        CONSTRAINT `PK_AccessPassPolicy` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `AspNetRoles` (
        `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `Name` varchar(256) CHARACTER SET utf8mb4 NULL,
        `NormalizedName` varchar(256) CHARACTER SET utf8mb4 NULL,
        `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_AspNetRoles` PRIMARY KEY (`Id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `AspNetUsers` (
        `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `UserName` varchar(256) CHARACTER SET utf8mb4 NULL,
        `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 NULL,
        `Email` varchar(256) CHARACTER SET utf8mb4 NULL,
        `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 NULL,
        `EmailConfirmed` tinyint(1) NOT NULL,
        `PasswordHash` longtext CHARACTER SET utf8mb4 NULL,
        `SecurityStamp` longtext CHARACTER SET utf8mb4 NULL,
        `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 NULL,
        `PhoneNumber` longtext CHARACTER SET utf8mb4 NULL,
        `PhoneNumberConfirmed` tinyint(1) NOT NULL,
        `TwoFactorEnabled` tinyint(1) NOT NULL,
        `LockoutEnd` datetime(6) NULL,
        `LockoutEnabled` tinyint(1) NOT NULL,
        `AccessFailedCount` int NOT NULL,
        CONSTRAINT `PK_AspNetUsers` PRIMARY KEY (`Id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `AuditLogs` (
        `id` int NOT NULL AUTO_INCREMENT,
        `datetime` datetime(6) NOT NULL,
        `useremail` longtext CHARACTER SET utf8mb4 NULL,
        `application` longtext CHARACTER SET utf8mb4 NULL,
        `details` longtext CHARACTER SET utf8mb4 NULL,
        `computer` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_AuditLogs` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `AutoWeigh` (
        `id` int NOT NULL AUTO_INCREMENT,
        `wbt_no` longtext CHARACTER SET utf8mb4 NULL,
        `deck1` double NOT NULL,
        `deck2` double NOT NULL,
        `deck3` double NOT NULL,
        `deck4` double NOT NULL,
        `gvw` double NOT NULL,
        `autodatetime` datetime(6) NOT NULL,
        `autoweighbridge` datetime(6) NOT NULL,
        `autouser` longtext CHARACTER SET utf8mb4 NULL,
        `ipaddress` longtext CHARACTER SET utf8mb4 NULL,
        `autostatus` longtext CHARACTER SET utf8mb4 NULL,
        `anpr` longtext CHARACTER SET utf8mb4 NULL,
        `wbtno` longtext CHARACTER SET utf8mb4 NULL,
        `anprb` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_AutoWeigh` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `AVWdemeritPoints` (
        `id` int NOT NULL AUTO_INCREMENT,
        `category` int NOT NULL,
        `point` int NOT NULL,
        CONSTRAINT `PK_AVWdemeritPoints` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `AVWoverloadCharges` (
        `id` int NOT NULL AUTO_INCREMENT,
        `overloadkg` int NOT NULL,
        `steeringaxle` double NOT NULL,
        `singledriveaxle` double NOT NULL,
        `tandemaxle` double NOT NULL,
        `tridemaxle` double NOT NULL,
        `period` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_AVWoverloadCharges` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `AxleWeightConfig` (
        `id` int NOT NULL AUTO_INCREMENT,
        `axle_code` varchar(20) CHARACTER SET utf8mb4 NULL,
        `axle_name` varchar(20) CHARACTER SET utf8mb4 NULL,
        `axle_number` int NOT NULL,
        `axle_camea` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_AxleWeightConfig` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `AxleWeights` (
        `id` int NOT NULL AUTO_INCREMENT,
        `wbrg_ticket_no` longtext CHARACTER SET utf8mb4 NULL,
        `axle_actualweight` double NOT NULL,
        `axle_permissibleweight` double NOT NULL,
        `axle_variantweight` double NOT NULL,
        `axle_time` datetime(6) NOT NULL,
        `axle_group` longtext CHARACTER SET utf8mb4 NULL,
        `axle_grouping` longtext CHARACTER SET utf8mb4 NULL,
        `timestamp` datetime(6) NOT NULL,
        `axle_number` int NOT NULL,
        `fee` double NOT NULL,
        `weighbridgetransactionsid` int NOT NULL,
        CONSTRAINT `PK_AxleWeights` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `AxleWeightXreff` (
        `id` int NOT NULL AUTO_INCREMENT,
        `axle_code` varchar(20) CHARACTER SET utf8mb4 NULL,
        `axle_code2` int NOT NULL,
        `axle_legalweight` double NOT NULL,
        `axle_group` longtext CHARACTER SET utf8mb4 NULL,
        `axle_grouping` longtext CHARACTER SET utf8mb4 NULL,
        `axle_deckgrouping` longtext CHARACTER SET utf8mb4 NULL,
        `axle_typeoftyres` longtext CHARACTER SET utf8mb4 NULL,
        `cameawim` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_AxleWeightXreff` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `BackUpDB` (
        `id` int NOT NULL AUTO_INCREMENT,
        `backup_delete` int NOT NULL,
        `backup_time` datetime(6) NOT NULL,
        `dayoftheweek` longtext CHARACTER SET utf8mb4 NULL,
        `backup_name` longtext CHARACTER SET utf8mb4 NULL,
        `backup_path` longtext CHARACTER SET utf8mb4 NULL,
        `fullincr` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_BackUpDB` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `BackupLogs` (
        `backuptype` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `filename` longtext CHARACTER SET utf8mb4 NULL,
        `folderpath` longtext CHARACTER SET utf8mb4 NULL,
        `daysofweek` longtext CHARACTER SET utf8mb4 NULL,
        `timestamp` datetime(6) NOT NULL,
        CONSTRAINT `PK_BackupLogs` PRIMARY KEY (`backuptype`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `Backups` (
        `id` int NOT NULL AUTO_INCREMENT,
        `backupName` longtext CHARACTER SET utf8mb4 NULL,
        `bkPath` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_Backups` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `CalibrationCert` (
        `id` int NOT NULL AUTO_INCREMENT,
        `station` longtext CHARACTER SET utf8mb4 NULL,
        `weighbridge` longtext CHARACTER SET utf8mb4 NULL,
        `type` longtext CHARACTER SET utf8mb4 NULL,
        `status` int NOT NULL,
        `issued` datetime(6) NOT NULL,
        `expiry` datetime(6) NOT NULL,
        `userid` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_CalibrationCert` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `Camera` (
        `id` int NOT NULL AUTO_INCREMENT,
        `position` longtext CHARACTER SET utf8mb4 NULL,
        `weighbridge` longtext CHARACTER SET utf8mb4 NULL,
        `cameraname` longtext CHARACTER SET utf8mb4 NULL,
        `ipaddress` longtext CHARACTER SET utf8mb4 NULL,
        `port` int NOT NULL,
        `file` longtext CHARACTER SET utf8mb4 NULL,
        `protocol` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_Camera` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `Cargo` (
        `id` int NOT NULL AUTO_INCREMENT,
        `cargo_name` longtext CHARACTER SET utf8mb4 NULL,
        `cargodetails` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_Cargo` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `CaseDetails` (
        `id` int NOT NULL AUTO_INCREMENT,
        `caseid` longtext CHARACTER SET utf8mb4 NULL,
        `station` longtext CHARACTER SET utf8mb4 NULL,
        `caseticket` longtext CHARACTER SET utf8mb4 NULL,
        `caseregno` longtext CHARACTER SET utf8mb4 NULL,
        `caseexcessload` double NOT NULL,
        `driverid` longtext CHARACTER SET utf8mb4 NULL,
        `driverslicense` longtext CHARACTER SET utf8mb4 NULL,
        `drivername` longtext CHARACTER SET utf8mb4 NULL,
        `driversurname` longtext CHARACTER SET utf8mb4 NULL,
        `drivergender` longtext CHARACTER SET utf8mb4 NULL,
        `drivernationality` longtext CHARACTER SET utf8mb4 NULL,
        `driverage` int NULL,
        `driveraddress` longtext CHARACTER SET utf8mb4 NULL,
        `alongroad` longtext CHARACTER SET utf8mb4 NULL,
        `district` longtext CHARACTER SET utf8mb4 NULL,
        `weighbrige` longtext CHARACTER SET utf8mb4 NULL,
        `county` longtext CHARACTER SET utf8mb4 NULL,
        `court` longtext CHARACTER SET utf8mb4 NULL,
        `comppolice` longtext CHARACTER SET utf8mb4 NULL,
        `trafficofficer` longtext CHARACTER SET utf8mb4 NULL,
        `repeatedoffence` longtext CHARACTER SET utf8mb4 NULL,
        `vehicleins` longtext CHARACTER SET utf8mb4 NULL,
        `movevehicleto` longtext CHARACTER SET utf8mb4 NULL,
        `repairsneeded` longtext CHARACTER SET utf8mb4 NULL,
        `offloadreg` longtext CHARACTER SET utf8mb4 NULL,
        `transportername` longtext CHARACTER SET utf8mb4 NULL,
        `transporteradd` longtext CHARACTER SET utf8mb4 NULL,
        `vehiclemake` longtext CHARACTER SET utf8mb4 NULL,
        `weighdate` datetime(6) NULL,
        `tickettransporter` longtext CHARACTER SET utf8mb4 NULL,
        `ticketaxletype` longtext CHARACTER SET utf8mb4 NULL,
        `casedatetime` datetime(6) NULL,
        `createdby` longtext CHARACTER SET utf8mb4 NULL,
        `editedby` longtext CHARACTER SET utf8mb4 NULL,
        `editeddate` datetime(6) NULL,
        `WBT_operator` longtext CHARACTER SET utf8mb4 NULL,
        `chargedincourt` longtext CHARACTER SET utf8mb4 NULL,
        `ownerid` longtext CHARACTER SET utf8mb4 NULL,
        `ownername` longtext CHARACTER SET utf8mb4 NULL,
        `ownergender` longtext CHARACTER SET utf8mb4 NULL,
        `ownernationality` longtext CHARACTER SET utf8mb4 NULL,
        `ownerage` int NULL,
        `ownersurname` longtext CHARACTER SET utf8mb4 NULL,
        `owneraddress` longtext CHARACTER SET utf8mb4 NULL,
        `prohibitionorder` longtext CHARACTER SET utf8mb4 NULL,
        `dl` longtext CHARACTER SET utf8mb4 NULL,
        `transporterno` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_CaseDetails` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `Census` (
        `id` int NOT NULL AUTO_INCREMENT,
        `censusid` longtext CHARACTER SET utf8mb4 NULL,
        `date` datetime(6) NOT NULL,
        `shift` longtext CHARACTER SET utf8mb4 NULL,
        `route` longtext CHARACTER SET utf8mb4 NULL,
        `hour` longtext CHARACTER SET utf8mb4 NULL,
        `truck7t` int NOT NULL,
        `truck357t` int NOT NULL,
        `buses` int NOT NULL,
        `station` longtext CHARACTER SET utf8mb4 NULL,
        `userid` longtext CHARACTER SET utf8mb4 NULL,
        `deletestatus` int NOT NULL,
        CONSTRAINT `PK_Census` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `ClusterWeighbridges` (
        `id` int NOT NULL AUTO_INCREMENT,
        `cluster_code` longtext CHARACTER SET utf8mb4 NULL,
        `cluster_name` longtext CHARACTER SET utf8mb4 NULL,
        `prefix` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_ClusterWeighbridges` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `ControlSelections` (
        `id` int NOT NULL AUTO_INCREMENT,
        `roleid` int NOT NULL,
        `controlid` int NOT NULL,
        `select` int NOT NULL,
        CONSTRAINT `PK_ControlSelections` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `DataExchange` (
        `id` int NOT NULL AUTO_INCREMENT,
        `weighbridgetickets` int NOT NULL,
        `connectionType` longtext CHARACTER SET utf8mb4 NULL,
        `server_address` longtext CHARACTER SET utf8mb4 NULL,
        `server_port` int NOT NULL,
        `server_email` longtext CHARACTER SET utf8mb4 NULL,
        `server_password` longtext CHARACTER SET utf8mb4 NULL,
        `client_address` longtext CHARACTER SET utf8mb4 NULL,
        `client_email` longtext CHARACTER SET utf8mb4 NULL,
        `client_password` longtext CHARACTER SET utf8mb4 NULL,
        `tags` int NOT NULL,
        `timestamp` datetime(6) NOT NULL,
        CONSTRAINT `PK_DataExchange` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `DeckWeights` (
        `wbrg_name` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `deck_1` double NOT NULL,
        `deck_2` double NOT NULL,
        `deck_3` double NOT NULL,
        `deck_4` double NOT NULL,
        `total` double NOT NULL,
        CONSTRAINT `PK_DeckWeights` PRIMARY KEY (`wbrg_name`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `Destination` (
        `id` int NOT NULL AUTO_INCREMENT,
        `destination_name` longtext CHARACTER SET utf8mb4 NULL,
        `deletedstatus` tinyint(1) NOT NULL,
        CONSTRAINT `PK_Destination` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `DollarRate` (
        `id` int NOT NULL AUTO_INCREMENT,
        `rate` double NOT NULL,
        `datetime` datetime(6) NOT NULL,
        `username` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_DollarRate` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `Driver` (
        `id` int NOT NULL AUTO_INCREMENT,
        `license_no` longtext CHARACTER SET utf8mb4 NULL,
        `driver_name` longtext CHARACTER SET utf8mb4 NULL,
        `driver_dob` longtext CHARACTER SET utf8mb4 NULL,
        `driver_gender` longtext CHARACTER SET utf8mb4 NULL,
        `driver_race` longtext CHARACTER SET utf8mb4 NULL,
        `driver_image` longtext CHARACTER SET utf8mb4 NULL,
        `driver_emailaddress` longtext CHARACTER SET utf8mb4 NULL,
        `driver_pysicaladdress` longtext CHARACTER SET utf8mb4 NULL,
        `driver_town` longtext CHARACTER SET utf8mb4 NULL,
        `driver_postaladdress` longtext CHARACTER SET utf8mb4 NULL,
        `driver_contact` longtext CHARACTER SET utf8mb4 NULL,
        `driver_idno` longtext CHARACTER SET utf8mb4 NULL,
        `driver_nationality` longtext CHARACTER SET utf8mb4 NULL,
        `driver_age` int NOT NULL,
        `driver_physcaldescription` longtext CHARACTER SET utf8mb4 NULL,
        `driver_deletedstatus` tinyint(1) NOT NULL,
        `driver_regdate` datetime(6) NOT NULL,
        CONSTRAINT `PK_Driver` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `EACAct` (
        `id` int NOT NULL AUTO_INCREMENT,
        `caseid` longtext CHARACTER SET utf8mb4 NULL,
        `datetime` datetime(6) NOT NULL,
        `casedocid` longtext CHARACTER SET utf8mb4 NULL,
        `gvwfee` double NOT NULL,
        `axlefee` double NOT NULL,
        `demeritspoints` int NOT NULL,
        `cdemeritspoints` int NOT NULL,
        `penalties` longtext CHARACTER SET utf8mb4 NULL,
        `dollarrate` double NOT NULL,
        `act` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_EACAct` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `Events` (
        `id` int NOT NULL AUTO_INCREMENT,
        `events` longtext CHARACTER SET utf8mb4 NULL,
        `userid` longtext CHARACTER SET utf8mb4 NULL,
        `datetime` datetime(6) NOT NULL,
        `confirmed` int NOT NULL,
        `confirmedby` longtext CHARACTER SET utf8mb4 NULL,
        `desc` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_Events` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `GVWdemeritPoints` (
        `id` int NOT NULL AUTO_INCREMENT,
        `category` int NOT NULL,
        `point` int NOT NULL,
        CONSTRAINT `PK_GVWdemeritPoints` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `GVWoverloadCharges` (
        `id` int NOT NULL AUTO_INCREMENT,
        `overloadkg` int NOT NULL,
        `fee` double NOT NULL,
        `period` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_GVWoverloadCharges` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `HealthCheck` (
        `id` int NOT NULL AUTO_INCREMENT,
        `station` longtext CHARACTER SET utf8mb4 NULL,
        `ipaddress` longtext CHARACTER SET utf8mb4 NULL,
        `sysname` longtext CHARACTER SET utf8mb4 NULL,
        `status` int NOT NULL,
        `lastchecked` datetime(6) NOT NULL,
        `deletestatus` int NOT NULL,
        `uptime` datetime(6) NOT NULL,
        `message` longtext CHARACTER SET utf8mb4 NULL,
        `userid` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_HealthCheck` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `Hswim` (
        `id` int NOT NULL AUTO_INCREMENT,
        `hswim_recordserialno` int NOT NULL,
        `station` longtext CHARACTER SET utf8mb4 NULL,
        `axles` longtext CHARACTER SET utf8mb4 NULL,
        `anpr` longtext CHARACTER SET utf8mb4 NULL,
        `anprassist` longtext CHARACTER SET utf8mb4 NULL,
        `axleconfig` longtext CHARACTER SET utf8mb4 NULL,
        `speed` double NOT NULL,
        `axle1` double NOT NULL,
        `axle2` double NOT NULL,
        `axle3` double NOT NULL,
        `axle4` double NOT NULL,
        `axle5` double NOT NULL,
        `axle6` double NOT NULL,
        `axle7` double NOT NULL,
        `axle8` double NOT NULL,
        `perm1` double NOT NULL,
        `perm2` double NOT NULL,
        `perm3` double NOT NULL,
        `perm4` double NOT NULL,
        `perm5` double NOT NULL,
        `perm6` double NOT NULL,
        `perm7` double NOT NULL,
        `perm8` double NOT NULL,
        `lngth1` double NOT NULL,
        `lngth2` double NOT NULL,
        `lngth3` double NOT NULL,
        `lngth4` double NOT NULL,
        `lngth5` double NOT NULL,
        `lngth6` double NOT NULL,
        `lngth7` double NOT NULL,
        `lngth8` double NOT NULL,
        `avw` double NOT NULL,
        `gvw` double NOT NULL,
        `pernmissible` double NOT NULL,
        `timestamp` datetime(6) NOT NULL,
        `calledin` tinyint(1) NOT NULL,
        `anprchecked` tinyint(1) NOT NULL,
        `wbrg_ticket_no` longtext CHARACTER SET utf8mb4 NULL,
        `wimtagged` tinyint(1) NOT NULL,
        `wintagreason` longtext CHARACTER SET utf8mb4 NULL,
        `anprassistreason` longtext CHARACTER SET utf8mb4 NULL,
        `lane` int NOT NULL,
        `type` int NOT NULL,
        `wimtagedcheck` tinyint(1) NOT NULL,
        `gap` int NOT NULL,
        `calledinreason` longtext CHARACTER SET utf8mb4 NULL,
        `MyProperty` int NOT NULL,
        `wbrg_ticket_exported` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_Hswim` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `IOSettings` (
        `id` int NOT NULL AUTO_INCREMENT,
        `Name` longtext CHARACTER SET utf8mb4 NULL,
        `IPAddress` longtext CHARACTER SET utf8mb4 NULL,
        `deckEntry` tinyint(1) NOT NULL,
        `deckExit` tinyint(1) NOT NULL,
        `WIM` tinyint(1) NOT NULL,
        `Status` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_IOSettings` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `ManualWeighing` (
        `id` int NOT NULL AUTO_INCREMENT,
        `date` datetime(6) NOT NULL,
        `hour` longtext CHARACTER SET utf8mb4 NULL,
        `tickets` int NOT NULL,
        `userid` longtext CHARACTER SET utf8mb4 NULL,
        `station` longtext CHARACTER SET utf8mb4 NULL,
        `deletestatus` int NOT NULL,
        CONSTRAINT `PK_ManualWeighing` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `Origin` (
        `id` int NOT NULL AUTO_INCREMENT,
        `origin_name` longtext CHARACTER SET utf8mb4 NULL,
        `deletedstatus` tinyint(1) NOT NULL,
        CONSTRAINT `PK_Origin` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `Penalties` (
        `id` int NOT NULL AUTO_INCREMENT,
        `points` int NOT NULL,
        `penalties` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_Penalties` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `PermitURL` (
        `id` int NOT NULL AUTO_INCREMENT,
        `permiturl` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_PermitURL` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `ProsecutionSettings` (
        `id` int NOT NULL AUTO_INCREMENT,
        `alongroad` longtext CHARACTER SET utf8mb4 NULL,
        `district` longtext CHARACTER SET utf8mb4 NULL,
        `weighbridge` longtext CHARACTER SET utf8mb4 NULL,
        `county` longtext CHARACTER SET utf8mb4 NULL,
        `court` longtext CHARACTER SET utf8mb4 NULL,
        `complainant` longtext CHARACTER SET utf8mb4 NULL,
        `police` longtext CHARACTER SET utf8mb4 NULL,
        `inspector` longtext CHARACTER SET utf8mb4 NULL,
        `movevehicle` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_ProsecutionSettings` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `RoadAuthority` (
        `id` int NOT NULL AUTO_INCREMENT,
        `authority_code` longtext CHARACTER SET utf8mb4 NULL,
        `authority_name` longtext CHARACTER SET utf8mb4 NULL,
        `prefix` longtext CHARACTER SET utf8mb4 NULL,
        `deletedstatus` tinyint(1) NOT NULL,
        CONSTRAINT `PK_RoadAuthority` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `Routes` (
        `id` int NOT NULL AUTO_INCREMENT,
        `route_name` longtext CHARACTER SET utf8mb4 NULL,
        `deletedstatus` tinyint(1) NOT NULL,
        CONSTRAINT `PK_Routes` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `ScaleTest` (
        `id` int NOT NULL AUTO_INCREMENT,
        `scaleid` longtext CHARACTER SET utf8mb4 NULL,
        `station` varchar(50) CHARACTER SET utf8mb4 NULL,
        `scalename` varchar(1) CHARACTER SET utf8mb4 NULL,
        `scalevehreg` longtext CHARACTER SET utf8mb4 NULL,
        `scaledeck1` double NOT NULL,
        `scaledeck2` double NOT NULL,
        `scaledeck3` double NOT NULL,
        `scaledeck4` double NOT NULL,
        `scaledatetime` datetime(6) NOT NULL,
        `scaleweighedby` longtext CHARACTER SET utf8mb4 NULL,
        `result` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_ScaleTest` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `Screens` (
        `id` int NOT NULL AUTO_INCREMENT,
        `screen_name` longtext CHARACTER SET utf8mb4 NULL,
        `active` int NOT NULL,
        CONSTRAINT `PK_Screens` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `Shifts` (
        `id` int NOT NULL AUTO_INCREMENT,
        `Email` longtext CHARACTER SET utf8mb4 NULL,
        `shift_type` longtext CHARACTER SET utf8mb4 NULL,
        `From` datetime(6) NOT NULL,
        `To` datetime(6) NOT NULL,
        `position` longtext CHARACTER SET utf8mb4 NULL,
        `deletedstatus` tinyint(1) NOT NULL,
        `repeat` tinyint(1) NOT NULL,
        CONSTRAINT `PK_Shifts` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `Source` (
        `id` int NOT NULL AUTO_INCREMENT,
        `source_code` longtext CHARACTER SET utf8mb4 NULL,
        `source_name` longtext CHARACTER SET utf8mb4 NULL,
        `source_deletedstatus` longtext CHARACTER SET utf8mb4 NULL,
        `source_regdate` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_Source` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `SpecialRelease` (
        `id` int NOT NULL AUTO_INCREMENT,
        `specialid` longtext CHARACTER SET utf8mb4 NULL,
        `vehiclereg` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_ticket_no` longtext CHARACTER SET utf8mb4 NULL,
        `reason` longtext CHARACTER SET utf8mb4 NULL,
        `otherreason` longtext CHARACTER SET utf8mb4 NULL,
        `realeasedate` datetime(6) NOT NULL,
        `realeasedby` longtext CHARACTER SET utf8mb4 NULL,
        `excessload` double NOT NULL,
        `case_document_id` longtext CHARACTER SET utf8mb4 NULL,
        `specialrelease_status` int NOT NULL,
        `station` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_SpecialRelease` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `SystemSettings` (
        `id` int NOT NULL AUTO_INCREMENT,
        `stationcode` longtext CHARACTER SET utf8mb4 NULL,
        `origin` longtext CHARACTER SET utf8mb4 NULL,
        `destination` longtext CHARACTER SET utf8mb4 NULL,
        `hswim` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_SystemSettings` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `Tag` (
        `id` int NOT NULL AUTO_INCREMENT,
        `taguid` longtext CHARACTER SET utf8mb4 NULL,
        `vehreg` longtext CHARACTER SET utf8mb4 NULL,
        `reason` longtext CHARACTER SET utf8mb4 NULL,
        `datetime` datetime(6) NOT NULL,
        `userid` longtext CHARACTER SET utf8mb4 NULL,
        `station` longtext CHARACTER SET utf8mb4 NULL,
        `type` longtext CHARACTER SET utf8mb4 NULL,
        `status` int NOT NULL,
        `sms` longtext CHARACTER SET utf8mb4 NULL,
        `email` longtext CHARACTER SET utf8mb4 NULL,
        `removedby` longtext CHARACTER SET utf8mb4 NULL,
        `removeddatetime` datetime(6) NOT NULL,
        `removedreason` longtext CHARACTER SET utf8mb4 NULL,
        `exported` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_Tag` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `TagsExchange` (
        `id` int NOT NULL AUTO_INCREMENT,
        `tagid` varchar(50) CHARACTER SET utf8mb4 NULL,
        `station` varchar(50) CHARACTER SET utf8mb4 NULL,
        `status` int NOT NULL,
        CONSTRAINT `PK_TagsExchange` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `Tollerance` (
        `id` int NOT NULL AUTO_INCREMENT,
        `singleaxle` double NOT NULL,
        `groupaxle` double NOT NULL,
        `gvw` double NOT NULL,
        `username` longtext CHARACTER SET utf8mb4 NULL,
        `datetime` datetime(6) NOT NULL,
        CONSTRAINT `PK_Tollerance` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `Trailer` (
        `id` int NOT NULL AUTO_INCREMENT,
        `trailer_code` longtext CHARACTER SET utf8mb4 NULL,
        `trailer_name` longtext CHARACTER SET utf8mb4 NULL,
        `trailer_deletedstatus` tinyint(1) NOT NULL,
        `trailer_regdate` datetime(6) NOT NULL,
        CONSTRAINT `PK_Trailer` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `Transporters` (
        `id` int NOT NULL AUTO_INCREMENT,
        `transporter_name` longtext CHARACTER SET utf8mb4 NULL,
        `transporter_address` longtext CHARACTER SET utf8mb4 NULL,
        `transporter_contact` longtext CHARACTER SET utf8mb4 NULL,
        `transporter_deletedstatus` longtext CHARACTER SET utf8mb4 NULL,
        `transporter_regdate` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_Transporters` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `UserPass` (
        `id` int NOT NULL AUTO_INCREMENT,
        `userpassword` longtext CHARACTER SET utf8mb4 NULL,
        `expirationstatus` tinyint(1) NOT NULL,
        CONSTRAINT `PK_UserPass` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `UserRoles` (
        `id` int NOT NULL AUTO_INCREMENT,
        `userrole_name` longtext CHARACTER SET utf8mb4 NULL,
        `userrole_desc` longtext CHARACTER SET utf8mb4 NULL,
        `userrole_deletedstatus` tinyint(1) NOT NULL,
        `screens` longtext CHARACTER SET utf8mb4 NULL,
        `userrole_timestamp` datetime(6) NOT NULL,
        CONSTRAINT `PK_UserRoles` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `Users` (
        `id` int NOT NULL AUTO_INCREMENT,
        `username` varchar(50) CHARACTER SET utf8mb4 NULL,
        `fullnames` varchar(150) CHARACTER SET utf8mb4 NULL,
        `organization` varchar(150) CHARACTER SET utf8mb4 NULL,
        `UserRole` longtext CHARACTER SET utf8mb4 NULL,
        `deletedstatus` tinyint(1) NOT NULL,
        `timestamp` datetime(6) NULL,
        `email` varchar(50) CHARACTER SET utf8mb4 NULL,
        `phone` varchar(50) CHARACTER SET utf8mb4 NULL,
        `sendmail` varchar(50) CHARACTER SET utf8mb4 NULL,
        `sendsms` varchar(50) CHARACTER SET utf8mb4 NULL,
        `resettime` datetime(6) NULL,
        CONSTRAINT `PK_Users` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `VehicleMake` (
        `id` int NOT NULL AUTO_INCREMENT,
        `vehiclemake` longtext CHARACTER SET utf8mb4 NULL,
        `vehiclemake_desc` longtext CHARACTER SET utf8mb4 NULL,
        `vehiclemake_regdate` datetime(6) NOT NULL,
        CONSTRAINT `PK_VehicleMake` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `Vehicles` (
        `id` int NOT NULL AUTO_INCREMENT,
        `vehicle_reg` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
        `vehicle_trailer` varchar(50) CHARACTER SET utf8mb4 NULL,
        `vehicle_make` longtext CHARACTER SET utf8mb4 NULL,
        `vehicle_model` longtext CHARACTER SET utf8mb4 NULL,
        `vehicle_year` longtext CHARACTER SET utf8mb4 NULL,
        `vehicle_axle` longtext CHARACTER SET utf8mb4 NULL,
        `vehicle_driver` longtext CHARACTER SET utf8mb4 NULL,
        `vehicle_tx_code` longtext CHARACTER SET utf8mb4 NULL,
        `vehicle_product` longtext CHARACTER SET utf8mb4 NULL,
        `vehicle_source` longtext CHARACTER SET utf8mb4 NULL,
        `vehicle_destination` longtext CHARACTER SET utf8mb4 NULL,
        `vehicle_desc` longtext CHARACTER SET utf8mb4 NULL,
        `vehicle_regdate` datetime(6) NULL,
        `sendsms` tinyint(1) NOT NULL,
        `sendmail` tinyint(1) NOT NULL,
        `vehicle_permit_no` longtext CHARACTER SET utf8mb4 NULL,
        `vehicle_permitissued_date` datetime(6) NULL,
        `vehicle_permitexpiration_date` datetime(6) NULL,
        `vehicle_height` longtext CHARACTER SET utf8mb4 NULL,
        `vehicle_width` longtext CHARACTER SET utf8mb4 NULL,
        `vehicle_length` longtext CHARACTER SET utf8mb4 NULL,
        `vehicle_maxweight` longtext CHARACTER SET utf8mb4 NULL,
        `vehicle_ownercontact` longtext CHARACTER SET utf8mb4 NULL,
        `vehicle_owneremail` longtext CHARACTER SET utf8mb4 NULL,
        `vehicle_deletedstatus` tinyint(1) NOT NULL,
        `vehicle_demeritpoints` int NOT NULL,
        CONSTRAINT `PK_Vehicles` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `VehiclesInYard` (
        `id` int NOT NULL AUTO_INCREMENT,
        `yard_reg` longtext CHARACTER SET utf8mb4 NULL,
        `yard_status` longtext CHARACTER SET utf8mb4 NULL,
        `released` tinyint(1) NOT NULL,
        `request` longtext CHARACTER SET utf8mb4 NULL,
        `user_id` int NOT NULL,
        `entry_date` datetime(6) NOT NULL,
        `specialrelease` longtext CHARACTER SET utf8mb4 NULL,
        `specialrelease_no` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_name` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_yard` longtext CHARACTER SET utf8mb4 NULL,
        `case_id` longtext CHARACTER SET utf8mb4 NULL,
        `yard_timeout` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_VehiclesInYard` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `VirtualTicket` (
        `id` int NOT NULL AUTO_INCREMENT,
        `cid` int NOT NULL,
        `date_time_local` datetime(6) NOT NULL,
        `wim` longtext CHARACTER SET utf8mb4 NULL,
        `vehicle_class` int NOT NULL,
        `velocity` int NOT NULL,
        `length` int NOT NULL,
        `lp` longtext CHARACTER SET utf8mb4 NULL,
        `anprassist` longtext CHARACTER SET utf8mb4 NULL,
        `lp_back` longtext CHARACTER SET utf8mb4 NULL,
        `insert_timestamp` datetime(6) NOT NULL,
        `total_weight` int NOT NULL,
        `axles_count` int NOT NULL,
        `axle_1_load` int NOT NULL,
        `axle_2_load` int NOT NULL,
        `axle_3_load` int NOT NULL,
        `axle_4_load` int NOT NULL,
        `axle_5_load` int NOT NULL,
        `axle_6_load` int NOT NULL,
        `axle_7_load` int NOT NULL,
        `axle_8_load` int NOT NULL,
        `axle_9_load` int NOT NULL,
        `axle_1_leftside_load` int NOT NULL,
        `axle_2_leftside_load` int NOT NULL,
        `axle_3_leftside_load` int NOT NULL,
        `axle_4_leftside_load` int NOT NULL,
        `axle_5_leftside_load` int NOT NULL,
        `axle_6_leftside_load` int NOT NULL,
        `axle_7_leftside_load` int NOT NULL,
        `axle_8_leftside_load` int NOT NULL,
        `axle_9_leftside_load` int NOT NULL,
        `axle_1_rightside_load` int NOT NULL,
        `axle_2_rightside_load` int NOT NULL,
        `axle_3_rightside_load` int NOT NULL,
        `axle_4_rightside_load` int NOT NULL,
        `axle_5_rightside_load` int NOT NULL,
        `axle_6_rightside_load` int NOT NULL,
        `axle_7_rightside_load` int NOT NULL,
        `axle_8_rightside_load` int NOT NULL,
        `axle_9_rightside_load` int NOT NULL,
        `axle_conf` longtext CHARACTER SET utf8mb4 NULL,
        `Axl_Legal_Weight_1` int NOT NULL,
        `Axl_Legal_Weight_2` int NOT NULL,
        `Axl_Legal_Weight_3` int NOT NULL,
        `Axl_Legal_Weight_4` int NOT NULL,
        `Axl_Legal_Weight_5` int NOT NULL,
        `Axl_Legal_Weight_6` int NOT NULL,
        `Axl_Legal_Weight_7` int NOT NULL,
        `Axl_Legal_Weight_8` int NOT NULL,
        `Axl_Legal_Weight_9` int NOT NULL,
        `permissible` int NOT NULL,
        `AVW` int NOT NULL,
        `GVW` int NOT NULL,
        `overweighting` longtext CHARACTER SET utf8mb4 NULL,
        `dual_tire` longtext CHARACTER SET utf8mb4 NULL,
        `car_layout` longtext CHARACTER SET utf8mb4 NULL,
        `axle_overload_no` int NOT NULL,
        `tagged` int NOT NULL,
        `exported` int NOT NULL,
        `calledin` int NOT NULL,
        `calledinreason` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_ticket_no` longtext CHARACTER SET utf8mb4 NULL,
        `gap` int NOT NULL,
        `lp_image_front` longtext CHARACTER SET utf8mb4 NULL,
        `lp_image_back` longtext CHARACTER SET utf8mb4 NULL,
        `overview` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_VirtualTicket` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `WideLoad` (
        `id` int NOT NULL AUTO_INCREMENT,
        `permitid` longtext CHARACTER SET utf8mb4 NULL,
        `permitno` longtext CHARACTER SET utf8mb4 NULL,
        `registration` longtext CHARACTER SET utf8mb4 NULL,
        `traillerreg` longtext CHARACTER SET utf8mb4 NULL,
        `transp` longtext CHARACTER SET utf8mb4 NULL,
        `transptel` longtext CHARACTER SET utf8mb4 NULL,
        `transpadd` longtext CHARACTER SET utf8mb4 NULL,
        `model` longtext CHARACTER SET utf8mb4 NULL,
        `origin` longtext CHARACTER SET utf8mb4 NULL,
        `destination` longtext CHARACTER SET utf8mb4 NULL,
        `Axleconf` longtext CHARACTER SET utf8mb4 NULL,
        `FirstAkit` longtext CHARACTER SET utf8mb4 NULL,
        `Fireext` longtext CHARACTER SET utf8mb4 NULL,
        `DrunkDriver` longtext CHARACTER SET utf8mb4 NULL,
        `Dlicensestate` longtext CHARACTER SET utf8mb4 NULL,
        `Inspstick` longtext CHARACTER SET utf8mb4 NULL,
        `InsuaranceStic` longtext CHARACTER SET utf8mb4 NULL,
        `Loaddisc` longtext CHARACTER SET utf8mb4 NULL,
        `Insecureload` longtext CHARACTER SET utf8mb4 NULL,
        `Lightstatus` longtext CHARACTER SET utf8mb4 NULL,
        `Sterringdef` longtext CHARACTER SET utf8mb4 NULL,
        `FHBreak` longtext CHARACTER SET utf8mb4 NULL,
        `AODefects` longtext CHARACTER SET utf8mb4 NULL,
        `AbnormalLPermit` longtext CHARACTER SET utf8mb4 NULL,
        `remarks` longtext CHARACTER SET utf8mb4 NULL,
        `Dpermitissu` datetime(6) NOT NULL,
        `PStartD` datetime(6) NOT NULL,
        `PEndD` datetime(6) NOT NULL,
        `DateofTravel` datetime(6) NOT NULL,
        `height_` double NOT NULL,
        `length_` double NOT NULL,
        `width_` double NOT NULL,
        `weighofload` double NOT NULL,
        `Authweight` double NOT NULL,
        `deletestatus` int NOT NULL,
        `AmountPaid` double NOT NULL,
        `permitdatetime` datetime(6) NOT NULL,
        `status` longtext CHARACTER SET utf8mb4 NULL,
        `Totaltyres` int NOT NULL,
        `perconf` int NOT NULL,
        `nonperconf` int NOT NULL,
        `station` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_WideLoad` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `Wim` (
        `id` int NOT NULL AUTO_INCREMENT,
        `cid` int NOT NULL,
        `datetime` datetime(6) NOT NULL,
        `wim` longtext CHARACTER SET utf8mb4 NULL,
        `vehicle_class` int NOT NULL,
        `velocity` int NOT NULL,
        `length` int NOT NULL,
        `lp` longtext CHARACTER SET utf8mb4 NULL,
        `lp_back` longtext CHARACTER SET utf8mb4 NULL,
        `anprassist` longtext CHARACTER SET utf8mb4 NULL,
        `axles_count` int NOT NULL,
        `axle_conf` longtext CHARACTER SET utf8mb4 NULL,
        `total_weight` int NOT NULL,
        `axle_1_load` int NOT NULL,
        `axle_2_load` int NOT NULL,
        `axle_3_load` int NOT NULL,
        `axle_4_load` int NOT NULL,
        `axle_5_load` int NOT NULL,
        `axle_6_load` int NOT NULL,
        `axle_7_load` int NOT NULL,
        `axle_8_load` int NOT NULL,
        `axle_9_load` int NOT NULL,
        `Axl_Legal_Weight_1` int NOT NULL,
        `Axl_Legal_Weight_2` int NOT NULL,
        `Axl_Legal_Weight_3` int NOT NULL,
        `Axl_Legal_Weight_4` int NOT NULL,
        `Axl_Legal_Weight_5` int NOT NULL,
        `Axl_Legal_Weight_6` int NOT NULL,
        `Axl_Legal_Weight_7` int NOT NULL,
        `Axl_Legal_Weight_8` int NOT NULL,
        `Axl_Legal_Weight_9` int NOT NULL,
        `permissible` int NOT NULL,
        `AVW` int NOT NULL,
        `GVW` int NOT NULL,
        `overweighting` longtext CHARACTER SET utf8mb4 NULL,
        `dual_tire` longtext CHARACTER SET utf8mb4 NULL,
        `car_layout` longtext CHARACTER SET utf8mb4 NULL,
        `calledin` int NOT NULL,
        `tagged` int NOT NULL,
        `gap` int NOT NULL,
        `calledinreason` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_Wim` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `YardInspection` (
        `id` int NOT NULL AUTO_INCREMENT,
        `inspectionid` longtext CHARACTER SET utf8mb4 NULL,
        `registration` longtext CHARACTER SET utf8mb4 NULL,
        `traillerreg` longtext CHARACTER SET utf8mb4 NULL,
        `transp` longtext CHARACTER SET utf8mb4 NULL,
        `transptel` longtext CHARACTER SET utf8mb4 NULL,
        `transpadd` longtext CHARACTER SET utf8mb4 NULL,
        `model` longtext CHARACTER SET utf8mb4 NULL,
        `origin` longtext CHARACTER SET utf8mb4 NULL,
        `destination` longtext CHARACTER SET utf8mb4 NULL,
        `Axleconf` longtext CHARACTER SET utf8mb4 NULL,
        `FirstAkit` longtext CHARACTER SET utf8mb4 NULL,
        `Fireext` longtext CHARACTER SET utf8mb4 NULL,
        `DrunkDriver` longtext CHARACTER SET utf8mb4 NULL,
        `Dlicensestate` longtext CHARACTER SET utf8mb4 NULL,
        `Inspstick` longtext CHARACTER SET utf8mb4 NULL,
        `InsuaranceStic` longtext CHARACTER SET utf8mb4 NULL,
        `Loaddisc` longtext CHARACTER SET utf8mb4 NULL,
        `Insecureload` longtext CHARACTER SET utf8mb4 NULL,
        `Lightstatus` longtext CHARACTER SET utf8mb4 NULL,
        `Sterringdef` longtext CHARACTER SET utf8mb4 NULL,
        `FHBreak` longtext CHARACTER SET utf8mb4 NULL,
        `AODefects` longtext CHARACTER SET utf8mb4 NULL,
        `AbnormalLPermit` longtext CHARACTER SET utf8mb4 NULL,
        `remarks` longtext CHARACTER SET utf8mb4 NULL,
        `Dpermitissu` datetime(6) NOT NULL,
        `PStartD` datetime(6) NOT NULL,
        `PEndD` datetime(6) NOT NULL,
        `DateofTravel` datetime(6) NOT NULL,
        `height_` double NOT NULL,
        `length_` double NOT NULL,
        `width_` double NOT NULL,
        `weighofload` double NOT NULL,
        `Authweight` double NOT NULL,
        `deletestatus` int NOT NULL,
        `datetime` datetime(6) NOT NULL,
        `status` longtext CHARACTER SET utf8mb4 NULL,
        `Totaltyres` int NOT NULL,
        `perconf` int NOT NULL,
        `nonperconf` int NOT NULL,
        `station` longtext CHARACTER SET utf8mb4 NULL,
        `SteeringOL` longtext CHARACTER SET utf8mb4 NULL,
        `SteeringBP` longtext CHARACTER SET utf8mb4 NULL,
        `rodend` longtext CHARACTER SET utf8mb4 NULL,
        `balljoints` longtext CHARACTER SET utf8mb4 NULL,
        `wheelallignment` longtext CHARACTER SET utf8mb4 NULL,
        `ticketno` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_YardInspection` PRIMARY KEY (`id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `AspNetRoleClaims` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `RoleId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `ClaimType` longtext CHARACTER SET utf8mb4 NULL,
        `ClaimValue` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_AspNetRoleClaims` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `AspNetUserClaims` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `ClaimType` longtext CHARACTER SET utf8mb4 NULL,
        `ClaimValue` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_AspNetUserClaims` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `AspNetUserLogins` (
        `LoginProvider` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
        `ProviderKey` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
        `ProviderDisplayName` longtext CHARACTER SET utf8mb4 NULL,
        `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_AspNetUserLogins` PRIMARY KEY (`LoginProvider`, `ProviderKey`),
        CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `AspNetUserRoles` (
        `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `RoleId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_AspNetUserRoles` PRIMARY KEY (`UserId`, `RoleId`),
        CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `AspNetUserTokens` (
        `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `LoginProvider` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
        `Name` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
        `Value` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_AspNetUserTokens` PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
        CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `BillingInstruction` (
        `id` int NOT NULL AUTO_INCREMENT,
        `billingid` longtext CHARACTER SET utf8mb4 NULL,
        `caseid` longtext CHARACTER SET utf8mb4 NULL,
        `billingfrom` longtext CHARACTER SET utf8mb4 NULL,
        `billingto` longtext CHARACTER SET utf8mb4 NULL,
        `datetime` datetime(6) NULL,
        `absconding` int NOT NULL,
        `abscondingdatetime` datetime(6) NULL,
        `parking` int NOT NULL,
        `parkingfromdatetime` datetime(6) NULL,
        `parkingtodatetime` datetime(6) NULL,
        `overload` int NOT NULL,
        `others` int NOT NULL,
        `otherdetails` longtext CHARACTER SET utf8mb4 NULL,
        `payableamount` double NULL,
        `timestamp` datetime(6) NULL,
        `createdby` longtext CHARACTER SET utf8mb4 NULL,
        `deleted` int NOT NULL,
        `deletedby` longtext CHARACTER SET utf8mb4 NULL,
        `deleteddatetime` datetime(6) NULL,
        `verifiedby` longtext CHARACTER SET utf8mb4 NULL,
        `verifieddatetime` datetime(6) NULL,
        `casedetailsid` int NOT NULL,
        CONSTRAINT `PK_BillingInstruction` PRIMARY KEY (`id`),
        CONSTRAINT `FK_BillingInstruction_CaseDetails_casedetailsid` FOREIGN KEY (`casedetailsid`) REFERENCES `CaseDetails` (`id`) ON DELETE CASCADE
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `CaseDocs` (
        `id` int NOT NULL AUTO_INCREMENT,
        `casedocid` longtext CHARACTER SET utf8mb4 NULL,
        `caseid` longtext CHARACTER SET utf8mb4 NULL,
        `docname` longtext CHARACTER SET utf8mb4 NULL,
        `docdate` datetime(6) NOT NULL,
        `createdby` longtext CHARACTER SET utf8mb4 NULL,
        `editedy` longtext CHARACTER SET utf8mb4 NULL,
        `cancelled` longtext CHARACTER SET utf8mb4 NULL,
        `cancelledby` longtext CHARACTER SET utf8mb4 NULL,
        `printed` longtext CHARACTER SET utf8mb4 NULL,
        `poorder` longtext CHARACTER SET utf8mb4 NULL,
        `casedetailsid` int NOT NULL,
        CONSTRAINT `PK_CaseDocs` PRIMARY KEY (`id`),
        CONSTRAINT `FK_CaseDocs_CaseDetails_casedetailsid` FOREIGN KEY (`casedetailsid`) REFERENCES `CaseDetails` (`id`) ON DELETE CASCADE
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `CaseResults` (
        `id` int NOT NULL AUTO_INCREMENT,
        `casereid` longtext CHARACTER SET utf8mb4 NULL,
        `caseid` longtext CHARACTER SET utf8mb4 NULL,
        `drivercharge` double NULL,
        `ownercharge` double NULL,
        `transportercharge` double NULL,
        `loadercharge` double NULL,
        `parkingfee` double NULL,
        `casebasis` longtext CHARACTER SET utf8mb4 NULL,
        `receiptno` longtext CHARACTER SET utf8mb4 NULL,
        `reason` longtext CHARACTER SET utf8mb4 NULL,
        `datetime` datetime(6) NULL,
        `timestamp` datetime(6) NULL,
        `createdby` longtext CHARACTER SET utf8mb4 NULL,
        `casereticketno` longtext CHARACTER SET utf8mb4 NULL,
        `casereregno` longtext CHARACTER SET utf8mb4 NULL,
        `deleted` int NOT NULL,
        `deletedby` longtext CHARACTER SET utf8mb4 NULL,
        `deleteddatetime` datetime(6) NULL,
        `casedetailsid` int NOT NULL,
        CONSTRAINT `PK_CaseResults` PRIMARY KEY (`id`),
        CONSTRAINT `FK_CaseResults_CaseDetails_casedetailsid` FOREIGN KEY (`casedetailsid`) REFERENCES `CaseDetails` (`id`) ON DELETE CASCADE
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `UserRoleCombination` (
        `id` int NOT NULL AUTO_INCREMENT,
        `userrolesid` int NULL,
        `deletedstatus` tinyint(1) NOT NULL,
        CONSTRAINT `PK_UserRoleCombination` PRIMARY KEY (`id`),
        CONSTRAINT `FK_UserRoleCombination_UserRoles_userrolesid` FOREIGN KEY (`userrolesid`) REFERENCES `UserRoles` (`id`) ON DELETE RESTRICT
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `WeighbridgeTransactions` (
        `id` int NOT NULL AUTO_INCREMENT,
        `wbrg_ticket_no` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_ticket_type` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_station` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_roadauthority` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_ticket_weighbridge` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_ticket_source` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_ticket_destination` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_ticket_date` datetime(6) NOT NULL,
        `wbrg_ticket_dateout` datetime(6) NOT NULL,
        `wbrg_product` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_tx_code` longtext CHARACTER SET utf8mb4 NULL,
        `veh_reg` longtext CHARACTER SET utf8mb4 NULL,
        `anpr_veh_reg` longtext CHARACTER SET utf8mb4 NULL,
        `axle1_weight` double NOT NULL,
        `axle1_limit` double NOT NULL,
        `axle1_time` datetime(6) NULL,
        `axle1_group_type1` longtext CHARACTER SET utf8mb4 NULL,
        `axle1_group_type2` longtext CHARACTER SET utf8mb4 NULL,
        `axle2_weight` double NOT NULL,
        `axle2_limit` double NOT NULL,
        `axle2_time` datetime(6) NULL,
        `axle2_group_type1` longtext CHARACTER SET utf8mb4 NULL,
        `axle2_group_type2` longtext CHARACTER SET utf8mb4 NULL,
        `axle3_weight` double NOT NULL,
        `axle3_limit` double NOT NULL,
        `axle3_time` datetime(6) NULL,
        `axle3_group_type1` longtext CHARACTER SET utf8mb4 NULL,
        `axle3_group_type2` longtext CHARACTER SET utf8mb4 NULL,
        `axle4_weight` double NOT NULL,
        `axle4_limit` double NOT NULL,
        `axle4_time` datetime(6) NULL,
        `axle4_group_type1` longtext CHARACTER SET utf8mb4 NULL,
        `axle4_group_type2` longtext CHARACTER SET utf8mb4 NULL,
        `axle5_weight` double NOT NULL,
        `axle5_limit` double NOT NULL,
        `axle5_time` datetime(6) NULL,
        `axle5_group_type1` longtext CHARACTER SET utf8mb4 NULL,
        `axle5_group_type2` longtext CHARACTER SET utf8mb4 NULL,
        `axle6_weight` double NOT NULL,
        `axle6_limit` double NOT NULL,
        `axle6_time` datetime(6) NULL,
        `axle6_group_type1` longtext CHARACTER SET utf8mb4 NULL,
        `axle6_group_type2` longtext CHARACTER SET utf8mb4 NULL,
        `axle7_weight` double NOT NULL,
        `axle7_limit` double NOT NULL,
        `axle7_time` datetime(6) NULL,
        `axle7_group_type1` longtext CHARACTER SET utf8mb4 NULL,
        `axle7_group_type2` longtext CHARACTER SET utf8mb4 NULL,
        `axle8_weight` double NOT NULL,
        `axle8_limit` double NOT NULL,
        `axle8_time` datetime(6) NULL,
        `axle8_group_type1` longtext CHARACTER SET utf8mb4 NULL,
        `axle8_group_type2` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_ticket_grossweight` double NOT NULL,
        `wbrg_ticket_tareweight` double NOT NULL,
        `wbrg_ticket_stamp` double NOT NULL,
        `wbrg_ticket_operator` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_ticket_status` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_ticket_state` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_ticket_printed` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_ticket_direction` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_ticket_directionarea` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_ticket_axel` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_ticket_ref` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_ticket_excessweight` double NOT NULL,
        `wbrg_ticket_driver` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_ticket_shift` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_ticket_wim` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_ticket_notes` longtext CHARACTER SET utf8mb4 NULL,
        `wbgr_ticket_trailer_no` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_ticket_excessload` longtext CHARACTER SET utf8mb4 NULL,
        `yardlistid` int NOT NULL,
        `wbrg_ticket_case_id` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_ticket_reweigh_no` int NOT NULL,
        `wbrg_ticket_tagged` int NOT NULL,
        `wbrg_ticket_charge_no` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_ticket_axleload` double NOT NULL,
        `wbrg_ticket_gvwload` double NOT NULL,
        `permit_no` longtext CHARACTER SET utf8mb4 NULL,
        `wbrg_ticket_make` longtext CHARACTER SET utf8mb4 NULL,
        `reliefvehreg` longtext CHARACTER SET utf8mb4 NULL,
        `exported` int NOT NULL,
        `VehiclesInYardid` int NULL,
        CONSTRAINT `PK_WeighbridgeTransactions` PRIMARY KEY (`id`),
        CONSTRAINT `FK_WeighbridgeTransactions_VehiclesInYard_VehiclesInYardid` FOREIGN KEY (`VehiclesInYardid`) REFERENCES `VehiclesInYard` (`id`) ON DELETE RESTRICT
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE TABLE `YardList` (
        `id` int NOT NULL AUTO_INCREMENT,
        `vehreg` longtext CHARACTER SET utf8mb4 NULL,
        `status` longtext CHARACTER SET utf8mb4 NULL,
        `userid` longtext CHARACTER SET utf8mb4 NULL,
        `datetime` datetime(6) NOT NULL,
        `wbtno` longtext CHARACTER SET utf8mb4 NULL,
        `specialrelease` longtext CHARACTER SET utf8mb4 NULL,
        `spreleaseno` longtext CHARACTER SET utf8mb4 NULL,
        `station` longtext CHARACTER SET utf8mb4 NULL,
        `caseid` longtext CHARACTER SET utf8mb4 NULL,
        `yardtimeout` datetime(6) NOT NULL,
        `inspected` int NOT NULL,
        `WeighbridgeTransactionsid` int NOT NULL,
        CONSTRAINT `PK_YardList` PRIMARY KEY (`id`),
        CONSTRAINT `FK_YardList_WeighbridgeTransactions_WeighbridgeTransactionsid` FOREIGN KEY (`WeighbridgeTransactionsid`) REFERENCES `WeighbridgeTransactions` (`id`) ON DELETE CASCADE
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE INDEX `IX_AspNetRoleClaims_RoleId` ON `AspNetRoleClaims` (`RoleId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE UNIQUE INDEX `RoleNameIndex` ON `AspNetRoles` (`NormalizedName`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE INDEX `IX_AspNetUserClaims_UserId` ON `AspNetUserClaims` (`UserId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE INDEX `IX_AspNetUserLogins_UserId` ON `AspNetUserLogins` (`UserId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE INDEX `IX_AspNetUserRoles_RoleId` ON `AspNetUserRoles` (`RoleId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE INDEX `EmailIndex` ON `AspNetUsers` (`NormalizedEmail`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE UNIQUE INDEX `UserNameIndex` ON `AspNetUsers` (`NormalizedUserName`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE INDEX `IX_BillingInstruction_casedetailsid` ON `BillingInstruction` (`casedetailsid`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE INDEX `IX_CaseDocs_casedetailsid` ON `CaseDocs` (`casedetailsid`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE INDEX `IX_CaseResults_casedetailsid` ON `CaseResults` (`casedetailsid`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE INDEX `IX_UserRoleCombination_userrolesid` ON `UserRoleCombination` (`userrolesid`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE INDEX `IX_WeighbridgeTransactions_VehiclesInYardid` ON `WeighbridgeTransactions` (`VehiclesInYardid`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    CREATE INDEX `IX_YardList_WeighbridgeTransactionsid` ON `YardList` (`WeighbridgeTransactionsid`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20220406115254_updates001') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20220406115254_updates001', '5.0.17');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

