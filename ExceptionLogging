using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using context = System.Web.HttpContext; 


/// <summary>
/// Summary description for ExceptionLogging
/// </summary>
public class ExceptionLogging
{
    private static String exepurl;
    static MySqlConnection con;
    private static void connecttion()
    {
         con = new MySqlConnection(ConfigurationManager.ConnectionStrings["SqlCom"].ConnectionString);
        
        con.Open();
    }
    public static void SendExcepToDB(Exception exdb)
    { 
        connecttion();
        exepurl = context.Current.Request.Url.ToString();
        MySqlCommand cmd = new MySqlCommand("ExceptionLoggingToDataBase", con);
        cmd.CommandType = CommandType.StoredProcedure;
        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        cmd.Parameters.AddWithValue("@P_ExceptionMsg", exdb.Message.ToString());
        cmd.Parameters.AddWithValue("@P_ExceptionType", exdb.GetType().Name.ToString());
        cmd.Parameters.AddWithValue("@P_ExceptionURL", exepurl);
        cmd.Parameters.AddWithValue("@P_ExceptionSource", exdb.StackTrace.ToString());
        int j = cmd.ExecuteNonQuery();
        con.Close();
        cmd.Parameters.Clear();
	}
}
