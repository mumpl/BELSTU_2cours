using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ADO.NET.Models;

namespace ADO.NET.Data
{
    public class UserRepository
    {
        private string connectionString;

        public UserRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ADONETConnection"].ConnectionString;
        }

        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    "SELECT u.ID, u.NAME, u.PASSWORD, u.ROLEID, r.ROLENAME " +
                    "FROM USERS u JOIN ROLES r ON u.ROLEID = r.ROLEID", connection);

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

        public User Authenticate(string username, string password)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    "SELECT u.ID, u.NAME, u.PASSWORD, u.ROLEID, r.ROLENAME " +
                    "FROM USERS u JOIN ROLES r ON u.ROLEID = r.ROLEID " +
                    "WHERE u.NAME = @Username AND u.PASSWORD = @Password", connection);

                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Id = Convert.ToInt32(reader["ID"]),
                            Name = reader["NAME"].ToString(),
                            Password = reader["PASSWORD"].ToString(),
                            RoleId = Convert.ToInt32(reader["ROLEID"]),
                            RoleName = reader["ROLENAME"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        public void AddUser(User user)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var command = new SqlCommand(
                            "INSERT INTO USERS (ID, NAME, PASSWORD, ROLEID) " +
                            "VALUES (@Id, @Name, @Password, @RoleId)",
                            connection, transaction);

                        command.Parameters.AddWithValue("@Id", user.Id);
                        command.Parameters.AddWithValue("@Name", user.Name);
                        command.Parameters.AddWithValue("@Password", user.Password);
                        command.Parameters.AddWithValue("@RoleId", user.RoleId);

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

        public void UpdateUser(User user)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var command = new SqlCommand(
                            "UPDATE USERS SET NAME = @Name, PASSWORD = @Password, ROLEID = @RoleId " +
                            "WHERE ID = @Id",
                            connection, transaction);

                        command.Parameters.AddWithValue("@Id", user.Id);
                        command.Parameters.AddWithValue("@Name", user.Name);
                        command.Parameters.AddWithValue("@Password", user.Password);
                        command.Parameters.AddWithValue("@RoleId", user.RoleId);

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

        public void DeleteUser(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var command = new SqlCommand(
                            "DELETE FROM USERS WHERE ID = @Id",
                            connection, transaction);

                        command.Parameters.AddWithValue("@Id", id);
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
    }
}
