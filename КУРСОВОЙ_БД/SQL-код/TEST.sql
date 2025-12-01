--1
BEGIN TRANSACTION;
DECLARE @i INT = 1;
WHILE @i <= 100000
BEGIN
    INSERT INTO [ROLE] ([role_name], [description]) 
    VALUES ('Роль ' + CAST(@i AS NVARCHAR), 'Описание роли ' + CAST(@i AS NVARCHAR));
    SET @i = @i + 1;
END;
COMMIT TRANSACTION;
GO


--2 время выполнения
SET STATISTICS TIME ON;

--3  фильтрация без индекса
DECLARE @start_time DATETIME2 = SYSDATETIME();

SELECT * FROM [ROLE] WHERE role_name LIKE 'Роль 999%';

DECLARE @end_time DATETIME2 = SYSDATETIME();

SELECT DATEDIFF(MILLISECOND, @start_time, @end_time) AS ExecutionTimeWithoutIndex;


--4 создаем индекс
CREATE INDEX IDX_ROLE_NAME ON [ROLE] (role_name);

--5  фильтрация с индексом
DECLARE @start_time DATETIME2 = SYSDATETIME();

SELECT * FROM [ROLE] WHERE role_name LIKE 'Роль 999%';

DECLARE @end_time DATETIME2 = SYSDATETIME();

SELECT DATEDIFF(MILLISECOND, @start_time, @end_time) AS ExecutionTimeWithIndex;



