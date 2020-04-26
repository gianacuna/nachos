using System;
using System.Data.Sql;
using System.Data.SqlClient;
using nachos.io.model;
using nachos.model;

namespace nachos.io.manager
{
    public static class ContactIO
    {
        public static Boolean Upsert(Client cli, Contact contact)
        {
            return cli.Upsert("UpsertContact", new System.Collections.Generic.List<SqlParameter>
            {
                new SqlParameter("@Id", contact.Id),
                new SqlParameter("@Name", contact.Name),
                new SqlParameter("@MobileNumber", contact.MobileNumber),
                new SqlParameter("@ContactGroup", contact.ContactGroup),
                new SqlParameter("@AccessToken", contact.AccessToken),
                new SqlParameter("@DateRegistered", contact.DateRegistered),
                new SqlParameter("@IsActive", contact.IsActive),
                new SqlParameter("@DateCreated", contact.DateCreated),
                new SqlParameter("@CreatedBy", contact.CreatedBy),
                new SqlParameter("@DateUpdated", contact.DateUpdated),
                new SqlParameter("@UpdatedBy", contact.UpdatedBy)
            });
        }

        public static Contact Find(Client cli, Guid? id = null, String name = null, String mobileNumber = null)
        {
            return cli.FindOne<Contact>("LookUpContact", new System.Collections.Generic.List<SqlParameter>
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@Name", name),
                new SqlParameter("@MobileNumber", mobileNumber)
            },
            (reader) =>
            {
                return ParseContact(reader);
            });
        }

        public static Boolean Delete(Client cli, Guid id)
        {
            return cli.Upsert("UpsertContact", new System.Collections.Generic.List<SqlParameter>
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@IsActive", false)
            });
        }

        public static Boolean Unregister(Client cli, Guid id)
        {
            return cli.Upsert("UnregisterContact", new System.Collections.Generic.List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            });
        }

        public static Contact ParseContact(SqlDataReader reader)
        {
            Contact contact = new Contact();
            contact.Id = Guid.Parse(reader["Id"].ToString());
            contact.Name = Convert.ToString(reader["Name"]);
            contact.MobileNumber = Convert.ToString(reader["MobileNumber"]);
            if (reader["ContactGroup"] != DBNull.Value)
            {
                contact.ContactGroup = Guid.Parse(reader["ContactGroup"].ToString());
            }
            contact.AccessToken = Convert.ToString(reader["AccessToken"]);
            if (reader["DateRegistered"] != DBNull.Value)
            {
                contact.DateRegistered = DateTime.Parse(reader["DateRegistered"].ToString());
            }
            contact.IsActive = Convert.ToBoolean(reader["IsActive"]);
            if (reader["DateCreated"] != DBNull.Value)
            {
                contact.DateCreated = DateTime.Parse(reader["DateCreated"].ToString());
            }
            if (reader["CreatedBy"] != DBNull.Value)
            {
                contact.CreatedBy = Guid.Parse(reader["CreatedBy"].ToString());
            }
            if (reader["DateUpdated"] != DBNull.Value)
            {
                contact.DateUpdated = DateTime.Parse(reader["DateUpdated"].ToString());
            }
            if (reader["UpdatedBy"] != DBNull.Value)
            {
                contact.UpdatedBy = Guid.Parse(reader["UpdatedBy"].ToString());
            }
            return contact;
        }
    }
}
