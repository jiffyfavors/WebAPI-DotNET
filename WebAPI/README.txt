To Make the REST API save data in the database, please execute the following code in MySQL Script.
Please modify the connection string found in RESTAPI.cs under SaveToDb method accordingly.

1. Create Database;
CREATE DATABASE `propelrr`;

2. Create Table name "rest".

CREATE TABLE  `propelrr`.`rest` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `fullname` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `mobile` varchar(14) NOT NULL,
  `birthdate` varchar(10) NOT NULL,
  `age` int unsigned NOT NULL,
  `gender` varchar(6) NOT NULL,
  PRIMARY KEY (`id`)
);

3. Create Stored Procedule name 'SaveData'.
Execute this as script;

DELIMITER $$
CREATE PROCEDURE `propelrr`.`SaveData` (IN fullname varchar(100), IN email varchar(100), IN mobile varchar(11), IN birthdate varchar(10), IN age int, IN gender varchar(6))
BEGIN
INSERT INTO rest (fullname, email, mobile, birthdate, age, gender) values(fullname, email, mobile, birthdate, age, gender);
END $$
DELIMITER ;
