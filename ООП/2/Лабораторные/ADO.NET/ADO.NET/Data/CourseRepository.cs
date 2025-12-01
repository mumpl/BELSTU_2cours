using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ADO.NET.Models;

namespace ADO.NET.Data
{
    public class CourseRepository
    {
        private string connectionString;

        public CourseRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ADONETConnection"].ConnectionString;
        }

        public List<Course> GetAllCourses()
        {
            var courses = new List<Course>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM COURSES", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courses.Add(new Course
                        {
                            CourseName = reader["COURSE_NAME"].ToString(),
                            Category = reader["CATEGORY"].ToString(),
                            Lessons = Convert.ToInt32(reader["LESSONS"]),
                            Description = reader["DESCRIPTION"].ToString(),
                            Price = Convert.ToInt32(reader["PRICE"])
                        });
                    }
                }
            }
            return courses;
        }

        /*public void AddCourse(Course course)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var command = new SqlCommand(
                            "INSERT INTO COURSES (COURSE_NAME, CATEGORY, LESSONS, DESCRIPTION, PRICE) " +
                            "VALUES (@CourseName, @Category, @Lessons, @Description, @Price)",
                            connection, transaction);

                        command.Parameters.AddWithValue("@CourseName", course.CourseName);
                        command.Parameters.AddWithValue("@Category", course.Category);
                        command.Parameters.AddWithValue("@Lessons", course.Lessons);
                        command.Parameters.AddWithValue("@Description", course.Description);
                        command.Parameters.AddWithValue("@Price", course.Price);

                        command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }*/

        public void UpdateCourse(Course course, string originalName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    "UPDATE COURSES SET COURSE_NAME = @CourseName, CATEGORY = @Category, LESSONS = @Lessons, DESCRIPTION = @Description, PRICE = @Price " +
                    "WHERE COURSE_NAME = @OriginalName", connection);

                command.Parameters.AddWithValue("@CourseName", course.CourseName);
                command.Parameters.AddWithValue("@Category", course.Category);
                command.Parameters.AddWithValue("@Lessons", course.Lessons);
                command.Parameters.AddWithValue("@Description", course.Description);
                command.Parameters.AddWithValue("@Price", course.Price);
                command.Parameters.AddWithValue("@OriginalName", originalName);

                command.ExecuteNonQuery();
            }
        }


        // ДОДЕЛАТЬ
        public void DeleteCourse(string courseName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var command = new SqlCommand(
                            "DELETE FROM COURSES WHERE COURSE_NAME = @CourseName",
                            connection, transaction);

                        command.Parameters.AddWithValue("@CourseName", courseName);
                        command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<User> GetUsersByRole(int roleId)
        {
            var users = new List<User>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = new SqlCommand(
                    "SELECT u.ID, u.NAME, u.PASSWORD, u.ROLEID, r.ROLENAME " +
                    "FROM USERS u JOIN ROLES r ON u.ROLEID = r.ROLEID " +
                    "WHERE u.ROLEID = @RoleId", connection);

                command.Parameters.AddWithValue("@RoleId", roleId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            Id = Convert.ToInt32(reader["ID"]),
                            Name = reader["NAME"].ToString(),
                            Password = reader["PASSWORD"].ToString(),
                            RoleId = Convert.ToInt32(reader["ROLEID"]),
                            RoleName = reader["ROLENAME"].ToString()
                        });
                    }
                }
            }

            return users;
        }

        public List<User> GetUsersByRoleUsingProcedure(int roleId)
        {
            var users = new List<User>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = new SqlCommand("GetUsersByRole", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@RoleId", roleId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            Id = Convert.ToInt32(reader["ID"]),
                            Name = reader["NAME"].ToString(),
                            Password = reader["PASSWORD"].ToString(),
                            RoleId = Convert.ToInt32(reader["ROLEID"]),
                            RoleName = reader["ROLENAME"].ToString()
                        });
                    }
                }
            }

            return users;
        }

        //СТОП СТОП

        public List<Course> GetCoursesByCategory(string category)
        {
            var courses = new List<Course>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("GetCoursesByCategory", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Category", category);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courses.Add(new Course
                        {
                            CourseName = reader["COURSE_NAME"].ToString(),
                            Category = reader["CATEGORY"].ToString(),
                            Lessons = Convert.ToInt32(reader["LESSONS"]),
                            Description = reader["DESCRIPTION"].ToString(),
                            Price = Convert.ToInt32(reader["PRICE"])
                        });
                    }
                }
            }
            return courses;
        }

        public void AddCourse(Course course)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    "INSERT INTO COURSES (COURSE_NAME, CATEGORY, LESSONS, DESCRIPTION, PRICE) " +
                    "VALUES (@CourseName, @Category, @Lessons, @Description, @Price)", connection);

                command.Parameters.AddWithValue("@CourseName", course.CourseName);
                command.Parameters.AddWithValue("@Category", course.Category);
                command.Parameters.AddWithValue("@Lessons", course.Lessons);
                command.Parameters.AddWithValue("@Description", course.Description);
                command.Parameters.AddWithValue("@Price", course.Price);

                command.ExecuteNonQuery();
            }
        }



    }
}
