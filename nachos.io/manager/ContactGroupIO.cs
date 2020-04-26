using System;
using System.Data.Sql;
using System.Data.SqlClient;
using nachos.io.model;
using nachos.model;

namespace nachos.io.manager
{
    public static class ContactGroupIO
    {
        public static Boolean Upsert(Client cli, ContactGroup group)
        {
            return cli.Upsert("UpsertContactGroup", new System.Collections.Generic.List<SqlParameter>
            {
                new SqlParameter("@Id", group.Id),
                new SqlParameter("@Name", group.Name),
                new SqlParameter("@Description", group.Description),
                new SqlParameter("@IsActive", group.IsActive),
                new SqlParameter("@DateCreated", group.DateCreated),
                new SqlParameter("@CreatedBy", group.CreatedBy),
                new SqlParameter("@DateUpdated", group.DateUpdated),
                new SqlParameter("@UpdatedBy", group.UpdatedBy)
            });
        }

        public static Boolean UpdateGrouping(Client cli, Guid group, Guid contact, Boolean isAddToGroup = true)
        {
            return cli.Upsert("UpsertContactGrouping", new System.Collections.Generic.List<SqlParameter>
            {
                new SqlParameter("@Group", group),
                new SqlParameter("@Contact", contact),
                new SqlParameter("@IsActive", isAddToGroup)
            });
        }
    }
}
