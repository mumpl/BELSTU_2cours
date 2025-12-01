USE LINGUABENDER_DB;
GO

CREATE LOGIN student_login WITH PASSWORD = 'student';
CREATE LOGIN teacher_login WITH PASSWORD = 'teacher';
CREATE LOGIN manager_login WITH PASSWORD = 'manager';
CREATE LOGIN admin_login WITH PASSWORD = 'admin';

--создание пользователей
USE LINGUABENDER_DB;

CREATE USER student_user FOR LOGIN student_login;
CREATE USER teacher_user FOR LOGIN teacher_login;
CREATE USER manager_user FOR LOGIN manager_login;
CREATE USER admin_user FOR LOGIN admin_login;

--создание ролей
CREATE ROLE student_role;
CREATE ROLE teacher_role;
CREATE ROLE manager_role;
CREATE ROLE admin_role;

--назначение пользователей на роли
ALTER ROLE student_role ADD MEMBER student_user;
ALTER ROLE teacher_role ADD MEMBER teacher_user;
ALTER ROLE manager_role ADD MEMBER manager_user;
ALTER ROLE admin_role ADD MEMBER admin_user;

--выдаем гранты
GRANT CONTROL ON DATABASE::LINGUABENDER_DB TO admin_role;


ALTER TABLE USER_ASSIGNMENT ADD attachment nvarchar(max);
ALTER TABLE USER_ASSIGNMENT DROP submission_attachment;