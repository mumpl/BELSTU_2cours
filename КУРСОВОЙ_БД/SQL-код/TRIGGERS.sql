--автоматическое добавление прогресса при создании курса
CREATE TRIGGER TR_INIT_COURSE_PROGRESS
ON USER_COURSE
AFTER INSERT
AS
BEGIN
INSERT INTO COURSE_PROGRESS (user_id, course_id, status)
SELECT user_id, course_id, 'Не начат'
FROM inserted;
END;
GO

--проверка даты начала и окончания курса
CREATE TRIGGER TR_VALIDATE_COURSE_DATES
ON COURSE
INSTEAD OF INSERT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM inserted WHERE start_date >= end_date)
    BEGIN
        RAISERROR('Дата начала курса должна быть раньше, чем дата завершения.', 16, 1);
    END
    ELSE
    BEGIN
        INSERT INTO COURSE (course_name, description, start_date, end_date, teacher_id)
        SELECT course_name, description, start_date, end_date, teacher_id
        FROM inserted;
    END
END;
GO


--Обеспечение безопасности
CREATE TRIGGER TR_CHECK_EMAIL_UNIQ
ON [USER]
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN [USER] u ON i.email = u.email
    )
    BEGIN
        THROW 50002, 'Этот email уже существует.', 1;
        RETURN;
    END

    INSERT INTO [USER] (first_name, last_name, email, password_hash, role_id)
    SELECT first_name, last_name, email, password_hash, role_id
    FROM inserted;
END;
GO

