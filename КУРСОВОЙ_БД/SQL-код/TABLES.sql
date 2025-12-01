use master 
create database LINGUABENDER_DB;

use LINGUABENDER_DB
CREATE TABLE [USER] (
  [user_id] int IDENTITY(1,1) PRIMARY KEY,
  [first_name] nvarchar(50),
  [last_name] nvarchar(50),
  [email] nvarchar(50) UNIQUE,
  [password_hash] nvarchar(50),
  [role_id] int,
  [created_at] datetime
)
GO


CREATE TABLE [ROLE] (
  [role_id] int IDENTITY(1,1) PRIMARY KEY,
  [role_name] nvarchar(50),
  [description] nvarchar(200)
)
GO

CREATE TABLE [COURSE] (
  [course_id] int IDENTITY(1,1) PRIMARY KEY,
  [course_name] nvarchar(50),
  [description] nvarchar(200),
  [start_date] date,
  [end_date] date,
  [teacher_id] int
)
GO

CREATE TABLE [LESSON] (
  [lesson_id] int IDENTITY(1,1) PRIMARY KEY,
  [course_id] int,
  [lesson_name] nvarchar(50),
  [lesson_date] date,
  [lesson_duration] time,
  [lesson_materials] nvarchar(200)
)
GO

CREATE TABLE [USER_COURSE] (
  [user_course_id] int IDENTITY(1,1) PRIMARY KEY,
  [user_id] int,
  [course_id] int,
  [enrollment_date] date
)
GO

CREATE TABLE [CERTIFICATES] (
  [sertificate_id] int IDENTITY(1,1) PRIMARY KEY,
  [user_id] int,
  [course_id] int,
  [course_name] nvarchar(50),
  [issue_date] date
)
GO


CREATE TABLE [COURSE_PROGRESS] (
  [progress_id] int IDENTITY(1,1) PRIMARY KEY,
  [user_id] int,
  [course_id] int,
  [status] nvarchar(50),
  [completed_at] datetime
)
GO

CREATE TABLE [ASSIGNMENT] (
  [assignment_id] int IDENTITY(1,1) PRIMARY KEY,
  [lesson_id] int,
  [description] nvarchar(200),
  [due_date] date
)
GO

CREATE TABLE [USER_ASSIGNMENT] (
  [user_assignment_id] int IDENTITY(1,1) PRIMARY KEY,
  [user_id] int,
  [assignment_id] int,
  [submission_text] nvarchar(200),
  [attachment] nvarchar(max),
  [submission_date] datetime,
  [grade] int,
  [feedback] nvarchar(200)
)
GO

CREATE TABLE [USER_ACTIVITY] (
  [activity_id] int IDENTITY(1,1) PRIMARY KEY,
  [user_id] int,
  [activity_description] nvarchar(50),
  [activity_datetime] datetime
)
GO

CREATE TABLE [COURSE_REVIEW] (
  [review_id] int IDENTITY(1,1) PRIMARY KEY,
  [course_id] int,
  [user_id] int,
  [rating] int,
  [review_text] nvarchar(200),
  [review_date] datetime
)
GO


CREATE TABLE [SCHEDULE] (
  [schedule_id] int IDENTITY(1,1) PRIMARY KEY,
  [course_id] int,
  [lesson_id] int,
  [start_time] time,
  [end_time] time,
  [day_of_week] int,
  [schedule_date] date
)
GO

CREATE TABLE [EXCEPTION_DATE] (
  [exception_id] int IDENTITY(1,1) PRIMARY KEY,
  [schedule_id] int,
  [exception_date] date,
  [reason] nvarchar(100),
  [rescheduling_for] date
)
GO

CREATE TABLE [TEACHER_QUALIFICATION] (
  [qualification_id] int IDENTITY(1,1) PRIMARY KEY,
  [teacher_id] int,
  [qualification_type] nvarchar(50),
  [institution] nvarchar(50),
  [description] nvarchar(200),
  [obtained_date] date,
  [file_url] nvarchar(500)
)
GO

CREATE TABLE [TEACHER_RATING] (
  [rating_id] int IDENTITY(1,1) PRIMARY KEY,
  [teacher_id] int,
  [rating] int,
  [review] nvarchar(200),
  [reviewer_id] int,
  [review_date] datetime
)
GO

CREATE TABLE [FORUM] (
  [forum_id] int IDENTITY(1,1) PRIMARY KEY,
  [course_id] int,
  [forum_name] nvarchar(50),
  [created_by] int,
  [created_at] datetime,
  [description] nvarchar(200)
)
GO

CREATE TABLE [FORUM_THREAD] (
  [thread_id] int IDENTITY(1,1) PRIMARY KEY,
  [forum_id] int,
  [thread_title] nvarchar(50),
  [created_by] int,
  [created_at] datetime
)
GO

CREATE TABLE [FORUM_POST] (
  [post_id] int IDENTITY(1,1) PRIMARY KEY,
  [thread_id] int,
  [author_id] int,
  [post_text] nvarchar(200),
  [posted_at] datetime
)
GO


ALTER TABLE [USER_ACTIVITY] ADD FOREIGN KEY ([user_id]) REFERENCES [USER] ([user_id])
GO

ALTER TABLE [COURSE] ADD FOREIGN KEY ([teacher_id]) REFERENCES [USER] ([user_id])
GO

ALTER TABLE [USER_COURSE] ADD FOREIGN KEY ([user_id]) REFERENCES [USER] ([user_id])
GO

ALTER TABLE [USER_COURSE] ADD FOREIGN KEY ([course_id]) REFERENCES [COURSE] ([course_id])
GO

ALTER TABLE [COURSE_REVIEW] ADD FOREIGN KEY ([course_id]) REFERENCES [COURSE] ([course_id])
GO

ALTER TABLE [COURSE_REVIEW] ADD FOREIGN KEY ([user_id]) REFERENCES [USER] ([user_id])
GO

ALTER TABLE [COURSE_PROGRESS] ADD FOREIGN KEY ([user_id]) REFERENCES [USER] ([user_id])
GO

ALTER TABLE [COURSE_PROGRESS] ADD FOREIGN KEY ([course_id]) REFERENCES [COURSE] ([course_id])
GO

ALTER TABLE [CERTIFICATES] ADD FOREIGN KEY ([user_id]) REFERENCES [USER] ([user_id])
GO

ALTER TABLE [CERTIFICATES] ADD FOREIGN KEY ([course_id]) REFERENCES [COURSE] ([course_id])
GO

ALTER TABLE [LESSON] ADD FOREIGN KEY ([course_id]) REFERENCES [COURSE] ([course_id])
GO

ALTER TABLE [ASSIGNMENT] ADD FOREIGN KEY ([lesson_id]) REFERENCES [LESSON] ([lesson_id])
GO

ALTER TABLE [USER_ASSIGNMENT] ADD FOREIGN KEY ([assignment_id]) REFERENCES [ASSIGNMENT] ([assignment_id])
GO

ALTER TABLE [USER_ASSIGNMENT] ADD FOREIGN KEY ([user_id]) REFERENCES [USER] ([user_id])
GO

ALTER TABLE [SCHEDULE] ADD FOREIGN KEY ([course_id]) REFERENCES [COURSE] ([course_id])
GO

ALTER TABLE [SCHEDULE] ADD FOREIGN KEY ([lesson_id]) REFERENCES [LESSON] ([lesson_id])
GO

ALTER TABLE [EXCEPTION_DATE] ADD FOREIGN KEY ([schedule_id]) REFERENCES [SCHEDULE] ([schedule_id])
GO

ALTER TABLE [TEACHER_QUALIFICATION] ADD FOREIGN KEY ([teacher_id]) REFERENCES [USER] ([user_id])
GO

ALTER TABLE [TEACHER_RATING] ADD FOREIGN KEY ([teacher_id]) REFERENCES [USER] ([user_id])
GO

ALTER TABLE [FORUM] ADD FOREIGN KEY ([course_id]) REFERENCES [COURSE] ([course_id])
GO

ALTER TABLE [FORUM] ADD FOREIGN KEY ([created_by]) REFERENCES [USER] ([user_id])
GO

ALTER TABLE [FORUM_THREAD] ADD FOREIGN KEY ([forum_id]) REFERENCES [FORUM] ([forum_id])
GO

ALTER TABLE [FORUM_THREAD] ADD FOREIGN KEY ([created_by]) REFERENCES [USER] ([user_id])
GO

ALTER TABLE [FORUM_POST] ADD FOREIGN KEY ([thread_id]) REFERENCES [FORUM_THREAD] ([thread_id])
GO

ALTER TABLE [FORUM_POST] ADD FOREIGN KEY ([author_id]) REFERENCES [USER] ([user_id])
GO

ALTER TABLE [USER] ADD FOREIGN KEY ([role_id]) REFERENCES [ROLE] ([role_id])
GO
