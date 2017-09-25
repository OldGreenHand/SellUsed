using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using SellUsed.Hubs;
using SellUsed.Models;

namespace SellUsed.Utility
{
    public class ConversationRepository
    {
        public IEnumerable<ConversationDetail> GetData()
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [ConversationId],[FromUserId],[ToUserId],[ToUserName],[UnreadMessageNo],[Updatetime] 
                                                            FROM [dbo].[ConversationDetails] 
                                                            WHERE [FromUserId] = @myUserId AND [UnreadMessageNo] > 0 
                                                            ORDER BY [Updatetime] ", connection))
                {
                    command.Parameters.AddWithValue("@myUserId", HttpContext.Current.User.Identity.GetUserId());

                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += dependency_OnChange;

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                        return reader.Cast<IDataRecord>()
                            .Select(x => new ConversationDetail
                            {
                                ConversationId = x.GetGuid(0),
                                FromUserId = x.GetString(1),
                                ToUserId = x.GetString(2),
                                ToUserName = x.GetString(3),
                                UnreadMessageNo = x.GetInt32(4),
                                Updatetime = x.GetDateTime(5)
                            }).ToList();
                }
            }
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            NotificationHub.Show();
        }
    }
}