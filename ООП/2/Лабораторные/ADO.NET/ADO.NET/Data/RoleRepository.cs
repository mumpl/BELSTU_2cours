using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADO.NET.Models;

namespace ADO.NET.Data
{
    public class RoleRepository
    {
        private string connectionString;

        public RoleRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ADONETConnection"].ConnectionString;
        }

        public List<Role> GetAllRoles()
        {
            var roles = new List<Role>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM ROLES", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(new Role
                        {
                            RoleId = Convert.ToInt32(reader["ROLEID"]),
                            RoleName = reader["ROLENAME"].ToString()
                        });
                    }
                }
            }
            return roles;
        }
    }
}
