using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ADO.NET.Data
{
    public static class DatabaseHelper
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["ADONETConnection"].ConnectionString;

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static void EnsureDatabaseExists()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                }
            }
            catch
            {
                // Если база не существует, создадим ее
                var masterConnectionString = connectionString.Replace("Initial Catalog=ADONET", "Initial Catalog=master");
                using (var connection = new SqlConnection(masterConnectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(
                        "IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = 'ADONET') " +
                        "CREATE DATABASE ADONET", connection);
                    command.ExecuteNonQuery();
                }

                // Создаем таблицы
                using (var connection = GetConnection())
                {
                    connection.Open();

                    // Создание таблицы ROLES
                    var createRolesTable = new SqlCommand(
                        "IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ROLES' and xtype='U') " +
                        "CREATE TABLE ROLES(ROLEID INT PRIMARY KEY, ROLENAME NVARCHAR(50))", connection);
                    createRolesTable.ExecuteNonQuery();

                    // Вставка ролей, если их нет
                    var insertRoles = new SqlCommand(
                        "IF NOT EXISTS (SELECT * FROM ROLES WHERE ROLEID=1) " +
                        "INSERT INTO ROLES (ROLEID, ROLENAME) VALUES (1, 'admin'), (2, 'user')", connection);
                    insertRoles.ExecuteNonQuery();

                    // Создание таблицы USERS
                    var createUsersTable = new SqlCommand(
                        "IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='USERS' and xtype='U') " +
                        "CREATE TABLE USERS(" +
                        "ID INT PRIMARY KEY, " +
                        "NAME NVARCHAR(50), " +
                        "PASSWORD NVARCHAR(50), " +
                        "ROLEID INT FOREIGN KEY REFERENCES ROLES(ROLEID))", connection);
                    createUsersTable.ExecuteNonQuery();

                    // Вставка пользователей, если их нет
                    var insertUsers = new SqlCommand(
                        "IF NOT EXISTS (SELECT * FROM USERS WHERE ID=1) " +
                        "INSERT INTO USERS(ID, NAME, PASSWORD, ROLEID) " +
                        "VALUES(1, 'Admin', '1', 1), (2,'David', '2', 2)", connection);
                    insertUsers.ExecuteNonQuery();

                    // Создание таблицы COURSES
                    var createCoursesTable = new SqlCommand(
                        "IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='COURSES' and xtype='U') " +
                        "CREATE TABLE COURSES (" +
                        "COURSE_NAME NVARCHAR(100) PRIMARY KEY, " +
                        "CATEGORY NVARCHAR(50), " +
                        "LESSONS INT, " +
                        "DESCRIPTION NVARCHAR(200), " +
                        "PRICE INT)", connection);
                    createCoursesTable.ExecuteNonQuery();

                    // Вставка курсов, если их нет
                    var insertCourses = new SqlCommand(
                        "IF NOT EXISTS (SELECT * FROM COURSES WHERE COURSE_NAME='Английский А1') " +
                        "INSERT INTO COURSES (COURSE_NAME, CATEGORY, LESSONS, DESCRIPTION, PRICE) " +
                        "VALUES " +
                        "('Английский А1', 'Базовый', 60 ,'Базовый курс английского для начинающих', 1200), " +
                        "('Испанский А1', 'Базовый', 40 ,'Базовый курс испанского для начинающих', 1000), " +
                        "('Немецкий А1', 'Базовый', 40 ,'Базовый курс немецкого для начинающих', 1150), " +
                        "('Китайский', 'Продвинутый', 90,'Курс китайского, с которым вы сможете общаться и понимать других!', 3200), " +
                        "('Английский В2', 'Продвинутый', 60,'Продвинутый уровень английского для общения с носителем', 1300), " +
                        "('Турецкий В2', 'Продвинутый', 90,'Курс турецкого, с которым вы сможете понимать сериалы в оригинале!', 3150), " +
                        "('Корейский TOPIK1', 'Базовый', 50 ,'Курс корейского, для общения и понимания носителя', 2980), " +
                        "('Французкий В2', 'Продвинутый', 60 ,'Продвинутый курс французкого для хорошего общения и понимания', 1400), " +
                        "('Итальянский А2', 'Средний', 65 ,'Курс итальянского для легкой беседы с носителем', 1600), " +
                        "('Немецкий А2', 'Средний', 40 ,'Курс немецкого для легкой беседы с носителем', 1200), " +
                        "('Испанский В1', 'Средний', 50 ,'Курс испанского для легкой беседы с носителем', 1440), " +
                        "('Арабский В1', 'Средний', 70,'Курс арабского для общения с носителем', 2655)", connection);
                    insertCourses.ExecuteNonQuery();
                }
            }
        }
    }

}

