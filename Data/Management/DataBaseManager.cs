using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;

namespace DeviceManager.Api.Data.Management
{
    /// <summary>
    /// Contains all tenants database mappings and options
    /// </summary>
    public class DataBaseManager : IDataBaseManager
    {
        /// <summary>
        /// Gets the name of the data base.
        /// </summary>
        /// <param name="masterDbConnection"></param>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns>db name</returns>
        public string GetDataBaseName(string masterDbConnection, int tenantId)
        {
            var dataBaseName = string.Empty;
            using (var connection = new MySqlConnection(masterDbConnection))
            {
                string query = " Select * from tenantmaster WHERE id = " + tenantId;
                MySqlCommand cmd = new MySqlCommand(query, connection);
                connection.Open();
                var reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    dataBaseName = reader.GetValue(4).ToString();
                }
            }         

            if(string.IsNullOrEmpty(dataBaseName))
            {
                throw new Exception(string.Format("Invalid Tenant Id : {0}", tenantId));
            }

            return dataBaseName;
        }
    }
}