USE LINGUABENDER_DB
GO
--Получение среднего рейтинга преподавателя
CREATE FUNCTION FN_TEACHER_AV_RATING (@teacher_id int)
RETURNS FLOAT
AS
BEGIN
DECLARE @avg_rating FLOAT;
SELECT @avg_rating = AVG(CAST(rating AS FLOAT))
FROM TEACHER_RATING
WHERE teacher_id = @teacher_id;

RETURN ISNULL(@avg_rating, 0);
END;
GO

--Получение всех домашних заданий студента по курсу
CREATE FUNCTION FN_STUD_ASSIGNMNET_BY_COURSE (@user_id int, @course_id int)
RETURNS TABLE
AS
RETURN (
SELECT
a.assignment_id,
a.description,
a.due_date,
ua. submission_date,
ua.grade
FROM USER_ASSIGNMENT ua
JOIN ASSIGNMENT a ON ua.assignment_id = a.assignment_id
JOIN LESSON l ON a.lesson_id = l.lesson_id
WHERE ua.user_id = @user_id AND l.course_id = @course_id
);
GO

--Получение всех форумов, где пользователь участвовал
CREATE FUNCTION FN_USER_FORUMS (@user_id int)
RETURNS TABLE
AS
RETURN (
SELECT DISTINCT f.forum_id, f.forum_name, f.description
FROM FORUM f
JOIN FORUM_THREAD ft ON f.forum_id = ft.forum_id
JOIN FORUM_POST fp ON ft.thread_id = fp.thread_id
WHERE fp.author_id = @user_id
);
GO

--Проверка, завершил ли пользователь курс
CREATE FUNCTION FN_COURSE_COMPLETED (@user_id int, @course_id int)
RETURNS BIT
AS
BEGIN
DECLARE @status nvarchar(50);
SELECT @status = status FROM COURSE_PROGRESS
WHERE user_id = @user_id AND course_id = @course_id;

RETURN CASE WHEN @status = 'Завершен' THEN 1 ELSE 0 END;
END;
GO

--Подсчет общего количества курсов, завершенных студентом
CREATE FUNCTION FN_COMPLETED_COURSES_COUNT (@user_id int)
RETURNS INT
AS
BEGIN
DECLARE @count int;
SELECT @count = COUNT(*) 
FROM COURSE_PROGRESS
WHERE user_id = @user_id AND status = 'Завершен';
return @count;
END;
GO

--Функция для подсчета загруженности преподавателя (кол-во активных курсов)
CREATE FUNCTION FN_TEACHER_COURSE_LOAD (@teacher_id int)
RETURNS INT
AS
BEGIN
DECLARE @count INT;
SELECT @count = COUNT(*) FROM COURSE
WHERE teacher_id = @teacher_id AND end_date > GETDATE();

RETURN @count;
END;
GO


--Функция для вывода среднего балла студента по всем заданиям
CREATE FUNCTION FN_STUDENT_AVG_GRADE (@user_id int)
RETURNS FLOAT
AS
BEGIN
DECLARE @avg FLOAT;
SELECT @avg = AVG(CAST(grade AS FLOAT)) FROM USER_ASSIGNMENT
WHERE user_id = @user_id and grade IS NOT NULL;

RETURN ISNULL(@avg, 0);
END;
GO

--Популярные курсы по количеству студентов
CREATE FUNCTION FN_TOP_COURSES_BY_STUD_COUNT()
RETURNS TABLE
AS
RETURN (
SELECT TOP 10
c.course_id,
c.course_name,
COUNT (uc.user_id) AS enrollment_count
FROM COURSE c
JOIN USER_COURSE uc ON c.course_id = uc.course_id
GROUP BY c.course_id, c.course_name
ORDER BY enrollment_count DESC
);
GO

--Функция получения активности по дате
CREATE FUNCTION FN_USER_ACTIVITY_BY_DATE (@user_id int, @activity_date date)
RETURNS TABLE
AS
RETURN (
SELECT * FROM USER_ACTIVITY
WHERE user_id = @user_id AND CAST(activity_datetime AS DATE) = @activity_date
);
GO