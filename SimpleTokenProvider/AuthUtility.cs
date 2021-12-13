using BookRight.Api.SimpleTokenProvider;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.SimpleTokenProvider
{
    public class AuthUtility
    {

        public static bool ValidateSigninUser(string uname, string pword, string connection)
        {
            AuthUser susr = new AuthUser
            {
                isAuth = false
            };
            return true;
            SqlConnection scon = new SqlConnection(connection);
            SqlCommand scmd = new SqlCommand();
            scon.Open();
            scmd.Connection = scon;
            scmd.CommandText = "SELECT uid,cmpid,usr_level,usr_f_name,usr_l_name,usr_email,usr_approve_status,usr_level,RoleId,mu.usr_status,usr_pwd,cm.CountryName,cm.DomainName,cm.DbServerName,cm.DbName,cm.DbPort,cm.DbUserName,cm.DbPassword FROM mstr_usr mu INNER JOIN company_master cm ON mu.cmpid = cm.id WHERE usr_name=@usr_name";
            scmd.Parameters.AddWithValue("usr_name", uname);
            scmd.Parameters.AddWithValue("usr_pwd", pword);
            scmd.Prepare();
            //using (SqlDataReader reader = scmd.ExecuteReader())
            //{
            //    if (reader.HasRows)
            //    {
            //        _logger.Info(string.Format("user found:", uname));
            //        reader.Read();
            //        var hashedPassword = reader.GetString("usr_pwd");
            //        if (CustomHasher.ValidatePassword(pword, hashedPassword))
            //        {
            //            susr.uid = Convert.ToInt32(reader["uid"]);
            //            susr.roles = GetRolesForUser(susr.uid);
            //            susr.cmpid = Convert.ToInt32(reader["cmpid"]);
            //            susr.ulevel = Convert.ToInt32(reader["usr_level"]);
            //            susr.firstName = reader["usr_f_name"].ToString();
            //            susr.lastName = reader["usr_l_name"].ToString();
            //            susr.email = reader["usr_email"].ToString();
            //            susr.ref1 = "";
            //            susr.usr_level = reader.GetInt16("usr_level");
            //            susr.roleId = reader.GetInt16("RoleId");
            //            susr.status = reader.GetBoolean("usr_status");
            //            susr.isapprover = reader.GetBoolean("usr_approve_status");
            //            susr.countryName = reader["CountryName"].ToString();
            //            susr.DbServerName = reader.GetString("DbServerName");
            //            susr.DbName = reader.GetString("DbName");
            //            susr.DbPort = reader.GetString("DbPort");
            //            susr.DbUserName = reader.GetString("DbUserName");
            //            susr.DbPassword = reader.GetString("DbPassword");
            //            susr.domainName = reader.GetString("DomainName");
            //            if (susr.status == true)
            //            {
            //                susr.isAuth = true;
            //            }
            //            else
            //            {
            //                susr.isAuth = false;
            //            }
            //        }
            //        else
            //        {
            //            _logger.Warn(string.Format("Invalid password:", pword));
            //            susr.isAuth = false;
            //            susr.status = true;
            //        }
            //    }
            //    else
            //    {
            //        _logger.Warn(string.Format("User not available:", uname));
            //        susr.isAuth = false;
            //        susr.status = true;
            //    }

            //}
            //return susr;
        }
        //public static List<string> GetRolesForUser(int uid)
        //{
        //    MySqlConnection scon = new MySqlConnection(MyApp.GetSettings().constr);
        //    MySqlCommand scmd = new MySqlCommand();
        //    scon.Open();
        //    scmd.Connection = scon;
        //    List<string> roleList = new List<string>();
        //    scmd.CommandText = "SELECT a.rname FROM mstr_roles a INNER JOIN user_in_role b ON a.rid=b.rid WHERE b.uid=@uid";
        //    scmd.Parameters.AddWithValue("uid", uid);
        //    scmd.Prepare();
        //    MySqlDataReader sdr = scmd.ExecuteReader();
        //    if (sdr.HasRows)
        //    {
        //        while (sdr.Read())
        //        {
        //            int index;
        //            index = sdr.GetOrdinal("rname");
        //            string rol = sdr.GetString(index);
        //            roleList.Add(rol);
        //        }
        //    }
        //    sdr.Close();
        //    sdr.Dispose();
        //    return roleList;
        //}
    }
}
