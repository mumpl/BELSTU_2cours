--СТУДЕНТ    СТУДЕНТ    СТУДЕНТ--
--Участие в уроках
USE LINGUABENDER_DB
GO
CREATE PROCEDURE SP_JOIN_LESSON
@user_id INT, @lesson_id INT
AS 
BEGIN
IF EXISTS(
SELECT 1 FROM USER_COURSE uc
JOIN LESSON l ON uc.course_id = l.course_id
WHERE uc.user_id = @user_id AND l.lesson_id = @lesson_id
)
BEGIN
INSERT INTO USER_ACTIVITY (user_id, activity_description, activity_datetime)
VALUES(@user_id, 'Присоединился к уроку', CURRENT_TIMESTAMP);
END
ELSE
BEGIN
RAISERROR('Пользователь не зачислен на курс этого урока.', 16, 1);
END
END;

--Выполнение д/з
USE LINGUABENDER_DB
GO
CREATE PROCEDURE SP_SUBMIT_ASSIGNMENT
@user_id int, @assignment_id int, @submission_text nvarchar(200), @submission_attachment nvarchar(200)
AS 
BEGIN
INSERT INTO USER_ASSIGNMENT(user_id, assignment_id, submission_text, attachment, submission_date)
VALUES (@user_id, @assignment_id, @submission_text, @submission_attachment, CURRENT_TIMESTAMP);
END;

--Участие в форумах
USE LINGUABENDER_DB
GO
CREATE PROCEDURE SP_POST_FORUM_MESSAGE
@user_id int, @thread_id int, @post_text nvarchar(200)
AS
BEGIN
INSERT INTO FORUM_POST (thread_id, author_id, post_text, posted_at)
VALUES (@thread_id, @user_id, @post_text, CURRENT_TIMESTAMP);
END;

--Упрвление личной информацией и настройками аккаунта
USE LINGUABENDER_DB
GO
CREATE PROCEDURE SP_UPDATE_USER_INFO
@user_id int, @first_name nvarchar(50), @last_name nvarchar(50), @email nvarchar(50)
AS 
BEGIN
UPDATE [USER]
SET first_name = @first_name,
	last_name = @last_name,
	email = @email
WHERE user_id = @user_id;
end;

--Получение сертификатов
CREATE PROCEDURE SP_ISSUE_CERTIFICATE
@user_id int, @course_id int
AS
BEGIN
DECLARE @course_name nvarchar(50);
SELECT @course_name = course_name FROM COURSE WHERE course_id = @course_id;

INSERT INTO CERTIFICATES(user_id, course_id, course_name, issue_date)
VALUES(@user_id, @course_id, @course_name, CURRENT_TIMESTAMP);
END;

--ПРЕПОДАВАТЕЛЬ    ПРЕПОДАВАТЕЛЬ     ПРЕПОДАВАТЕЛЬ--
--Подтверждение навыков
CREATE PROCEDURE SP_TEACHER_QUALIFICATION
@teacher_id int, @qualification_type nvarchar(50), 
@institution nvarchar(50), @description nvarchar(200), 
@obtained_date date, @file_url nvarchar(500)
AS 
BEGIN
INSERT INTO TEACHER_QUALIFICATION (teacher_id, qualification_type, institution, description, obtained_date, file_url)
VALUES (@teacher_id, @qualification_type, @institution, @description, @obtained_date, @file_url);
END;

--Создание курсов
CREATE PROCEDURE SP_CREATE_COURSE
@course_name nvarchar(50), @description nvarchar(200),
@start_date date, @end_date date, @teacher_id int
AS
BEGIN
INSERT INTO COURSE(course_name, description, start_date, end_date, teacher_id)
VALUES(@course_name, @description, @start_date, @end_date, @teacher_id);
END;

--Проведение уроков
CREATE PROCEDURE SP_SCHEDULE_LESSON
@course_id int, @lesson_name nvarchar(50), 
@lesson_date date, @lesson_duration time,
@lesson_materials nvarchar(200)
AS
BEGIN
INSERT INTO LESSON(course_id, lesson_name, lesson_date, lesson_duration, lesson_materials)
VALUES(@course_id, @lesson_name, @lesson_date, @lesson_duration, @lesson_materials);
END;

--Проверка д/з
CREATE PROCEDURE SP_GRADE_ASSIGNMENT
@user_assignment_id int, @grade int, 
@feedback nvarchar(200)
AS
BEGIN
UPDATE USER_ASSIGNMENT
SET grade = @grade,
	feedback = @feedback
WHERE user_assignment_id = @user_assignment_id;
END;

--Управление расписанием
CREATE PROCEDURE SP_MANAGE_SCHEDULE
@course_id int, @lesson_id int,
@start_time time, @end_time time,
@day_of_week int, @schedule_date date
AS
BEGIN
INSERT INTO SCHEDULE(course_id, lesson_id, start_time, end_time, day_of_week, schedule_date)
VALUES(@course_id, @lesson_id, @start_time, @end_time, @day_of_week, @schedule_date);
END;

--Повышение квалификации
CREATE PROCEDURE SP_LOG_TEACHER_TRAINING
@teacher_id int, 
@event_description nvarchar(100)
AS
BEGIN
INSERT INTO USER_ACTIVITY (user_id, activity_description, activity_datetime)
VALUES (@teacher_id, @event_description, CURRENT_TIMESTAMP)
END
GO

--МЕНЕДЖЕР    МЕНЕДЖЕР    МЕНЕДЖЕР--
--Управление профилями пользователей
USE LINGUABENDER_DB
GO
CREATE PROCEDURE SP_MANAGE_USER_PROFILE
@action nvarchar(50), --INSERT, UPDATE, DELETE
@user_id int = null, 
@first_name nvarchar(50) = null,
@last_name nvarchar(50) = null,
@email nvarchar(50) = null,
@password_hash nvarchar(50) = null,
@role_id int = null
AS
BEGIN
IF @action = 'INSERT'
BEGIN
INSERT INTO [USER] (user_id, first_name, last_name, email, password_hash, role_id)
VALUES (@user_id, @first_name, @last_name, @email, @password_hash, @role_id)
END
ELSE IF @action = 'UPDATE'
BEGIN
UPDATE [USER]
SET first_name = @first_name, last_name = @last_name, email = @email,
	password_hash = @password_hash, role_id = @role_id
WHERE user_id = @user_id
END
ELSE IF @action = 'DELETE'
BEGIN
DELETE FROM [USER] WHERE user_id = @user_id
END
END
GO

--Управление учебными материалами
CREATE PROCEDURE SP_UPDATE_LESSON_MATERIAL
@lesson_id int, @new_material nvarchar(200)
AS 
BEGIN
UPDATE LESSON SET lesson_materials = @new_material WHERE lesson_id = @lesson_id
END
GO

--Организация мероприятий
CREATE PROCEDURE SP_CREATE_EVENT
@forum_name nvarchar(50), 
@course_id int, @created_by int,
@description nvarchar(200)
AS
BEGIN
INSERT INTO FORUM (forum_name, course_id, created_by, created_at, description)
VALUES (@forum_name, @course_id, @created_by, CURRENT_TIMESTAMP, @description)
END
GO


--АДМИНИСРАТОР    АДМИНИСТРАТОР    АДМИНИСТРАТОР--
--Проверка документов
CREATE PROCEDURE SP_VERIFY_TEACHER_DOCUMENT
@qualification_id int, @verified bit
AS
BEGIN
-- Для демонстрации: логирование в activity
DECLARE @teacher_id int
SELECT @teacher_id = teacher_id FROM TEACHER_QUALIFICATION WHERE qualification_id = @qualification_id

INSERT INTO USER_ACTIVITY (user_id, activity_description, activity_datetime)
VALUES (@teacher_id, 'ДОКУМЕНТ ПРОВЕРЕН' + CAST(@qualification_id AS NVARCHAR), CURRENT_TIMESTAMP)
END
GO

--Управление правами доступа
CREATE PROCEDURE SP_CHANGE_USER_ROLE
@user_id int, @new_role int
AS
BEGIN
UPDATE [USER]
SET role_id = @new_role
WHERE user_id = @user_id
END
GO
