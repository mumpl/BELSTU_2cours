USE master;
CREATE DATABASE ADONET;

USE ADONET
CREATE TABLE COURSES (
COURSE_NAME NVARCHAR(100) PRIMARY KEY,
CATEGORY NVARCHAR(50),
LESSONS INT,
DESCRIPTION NVARCHAR(200),
PRICE INT);

USE ADONET

INSERT INTO COURSES (COURSE_NAME, CATEGORY, LESSONS, DESCRIPTION, PRICE) 
VALUES 
('Английский А1', 'Базовый', 60 ,'Базовый курс английского для начинающих', 1200 ),
('Испанский А1', 'Базовый', 40 ,'Базовый курс испанского для начинающих', 1000),
('Немецкий А1', 'Базовый', 40 ,'Базовый курс немецкого для начинающих', 1150),
('Китайский', 'Продвинутый', 90,'Курс китайского, с которым вы сможете общаться и понимать других!', 3200),
('Английский В2', 'Продвинутый', 60,'Продвинутый уровень английского для общения с носителем', 1300),
('Турецкий В2', 'Продвинутый', 90,'Курс турецкого, с которым вы сможете понимать сериалы в оригинале!', 3150),
('Корейский TOPIK1', 'Базовый', 50 ,'Курс корейского, для общения и понимания носителя', 2980),
('Французкий В2', 'Продвинутый', 60 ,'Продвинутый курс французкого для хорошего общения и понимания', 1400),
('Итальянский А2', 'Средний', 65 ,'Курс итальянского для легкой беседы с носителем', 1600),
('Немецкий А2', 'Средний', 40 ,'Курс немецкого для легкой беседы с носителем', 1200),
('Испанский В1', 'Средний', 50 ,'Курс испанского для легкой беседы с носителем', 1440),
('Арабский В1', 'Средний', 70,'Курс арабского для общения с носителем', 2655);

USE ADONET
CREATE TABLE USERS(
ID INT PRIMARY KEY,
NAME NVARCHAR(50),
PASSWORD NVARCHAR(50),
ROLEID INT foreign key references ROLES(ROLEID)
);

USE ADONET
CREATE TABLE ROLES(
ROLEID INT PRIMARY KEY,
ROLENAME NVARCHAR(50));
	
INSERT INTO ROLES (ROLEID, ROLENAME) 
VALUES( 1, 'admin'),
(2, 'user');

INSERT INTO USERS(ID, NAME, PASSWORD, ROLEID)
VALUES(1, 'Admin', '1', 1),
(2,'David', '2', 2);