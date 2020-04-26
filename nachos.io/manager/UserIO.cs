using System;
using System.Data.Sql;
using System.Data.SqlClient;
using nachos.io.model;
using nachos.model;

namespace nachos.io.manager
{
    public static class UserIO
    {
        public static Boolean Upsert(Client cli, User user)
        {
            return cli.Upsert("UpsertUser", new System.Collections.Generic.List<SqlParameter>
            {
                new SqlParameter("@Id", user.Id),
                new SqlParameter("@UserName", user.UserName),
                new SqlParameter("@PasswordSalt", user.PasswordSalt),
                new SqlParameter("@PasswordHash", user.PasswordHash),
                new SqlParameter("@FirstName", user.FirstName),
                new SqlParameter("@LastName", user.LastName),
                new SqlParameter("@EmailAddress", user.EmailAddress),
                new SqlParameter("@MobileNumber", user.MobileNumber),
                new SqlParameter("@UserType", user.UserType),
                new SqlParameter("@IsActive", user.IsActive),
                new SqlParameter("@DateCreated", user.DateCreated),
                new SqlParameter("@CreatedBy", user.CreatedBy),
                new SqlParameter("@DateUpdated", user.DateUpdated),
                new SqlParameter("@UpdatedBy", user.UpdatedBy)
            });
        }

        public static User Find(Client cli, Guid? id = null, String userName = null, String emailAddress = null)
        {
            return cli.FindOne<User>("LookupUser", new System.Collections.Generic.List<SqlParameter>
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@UserName", userName),
                new SqlParameter("@EmailAddress", emailAddress)
            },
            (reader) =>
            {
                User user = new User();
                user.Id = Guid.Parse(reader["Id"].ToString());
                user.UserName = reader["UserName"].ToString();
                user.PasswordSalt = reader["PasswordSalt"].ToString();
                user.PasswordHash = reader["PasswordHash"].ToString();
                user.FirstName = reader["FirstName"].ToString();
                user.LastName = reader["LastName"].ToString();
                user.EmailAddress = reader["EmailAddress"].ToString();
                user.MobileNumber = reader["MobileNumber"].ToString();
                if (reader["UserType"] != null)
                {
                    user.UserType = Guid.Parse(reader["UserType"].ToString());
                }
                user.IsActive = Convert.ToBoolean(reader["IsActive"]);
                if (reader["DateCreated"] != DBNull.Value)
                {
                    user.DateCreated = DateTime.Parse(reader["DateCreated"].ToString());
                }
                if (reader["CreatedBy"] != DBNull.Value)
                {
                    user.CreatedBy = Guid.Parse(reader["CreatedBy"].ToString());
                }
                if (reader["DateUpdated"] != DBNull.Value)
                {
                    user.DateUpdated = DateTime.Parse(reader["DateUpdated"].ToString());
                }
                if (reader["UpdatedBy"] != DBNull.Value)
                {
                    user.UpdatedBy = Guid.Parse(reader["UpdatedBy"].ToString());
                }
                return user;
            });
        }
    }
}