--СТУДЕНТ    СТУДЕНТ    СТУДЕНТ--
--Просмотр учебных материалов
CREATE VIEW V_STUDENT_MATERIALS AS
SELECT u.user_id, c.course_name, l.lesson_name, l.lesson_materials
FROM [USER] u
JOIN USER_COURSE uc ON u.user_id = uc.user_id
JOIN COURSE c ON uc.course_id = c.course_id
JOIN LESSON l ON c.course_id = l.course_id
WHERE u.role_id = (SELECT role_id FROM [ROLE] WHERE role_name = 'Студент');

--Просмотр записей уроков
CREATE VIEW V_LESSON_RECORDS AS
SELECT u.user_id, l.lesson_name, l.lesson_materials
FROM [USER] u
JOIN USER_COURSE uc ON u.user_id = uc.user_id
JOIN COURSE c ON uc.course_id = c.course_id
JOIN LESSON l ON c.course_id = l.course_id
WHERE u.role_id = (SELECT role_id FROM [ROLE] WHERE role_name = 'Студент');

--МЕНЕДЖЕР    МЕНЕДЖЕР    МЕНЕДЖЕР--
--Отслеживание активности--
CREATE VIEW V_USER_ACTIVITY_SUM AS
SELECT u.user_id, u.first_name, u.last_name, COUNT(ua.activity_id) AS activity_count
FROM [USER] u
LEFT JOIN USER_ACTIVITY ua ON u.user_id = ua.user_id
GROUP BY u.user_id, u.first_name, u.last_name
GO

--Топ 5 преподавателей по среднему рейтингу--
CREATE VIEW V_TOP_TEACHERS_BY_RATING AS
SELECT TOP 5
u.user_id, u.first_name, u.last_name,
AVG(tr.rating) AS average_rating
from TEACHER_RATING tr
JOIN [USER] u ON tr.teacher_id = u.user_id
GROUP BY u.user_id, u.first_name, u.last_name
ORDER BY average_rating desc;
go

--Cтуденты с просроченными заданиями--
CREATE VIEW V_STUD_OVERDUE_ASSIGNMENT AS
SELECT 
    u.user_id,
    u.first_name,
    u.last_name,
    a.assignment_id,
    a.due_date,
    ua.submission_date
FROM USER_ASSIGNMENT ua
JOIN ASSIGNMENT a ON ua.assignment_id = a.assignment_id
JOIN [USER] u ON ua.user_id = u.user_id
WHERE ua.submission_date > a.due_date;
GO

--Pасписание преподавателей на текущую неделю--
CREATE VIEW V_TEACHER_SCHEDULE_WEEK AS
SELECT 
    u.user_id,
    u.first_name,
    u.last_name,
    c.course_name,
    l.lesson_name,
    s.schedule_date,
    s.start_time,
    s.end_time
FROM SCHEDULE s
JOIN LESSON l ON s.lesson_id = l.lesson_id
JOIN COURSE c ON l.course_id = c.course_id
JOIN [USER] u ON c.teacher_id = u.user_id
WHERE s.schedule_date BETWEEN GETDATE() AND DATEADD(DAY, 7, GETDATE());
GO

--детализированный прогресс студентов по курсам--
CREATE VIEW V_STUD_PROGRESS_DETAILS AS
SELECT 
    u.user_id,
    u.first_name,
    u.last_name,
    c.course_name,
    cp.status,
    cp.completed_at
FROM COURSE_PROGRESS cp
JOIN [USER] u ON cp.user_id = u.user_id
JOIN COURSE c ON cp.course_id = c.course_id;
GO