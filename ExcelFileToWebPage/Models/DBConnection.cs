using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelFileToWebPage.Models
{
	public class DBConnection
	{
        //static string DbConnnectionString = @"workstation id=Portail.mssql.somee.com;packet size=4096;user id=KhalilMensi_SQLLogin_1;pwd=3234ys9719;data source=Portail.mssql.somee.com;persist security info=False;initial catalog=Portail";
        static string DbConnnectionString = @"Data Source=DESKTOP-A5NHULB\SQLEXPRESS;Initial Catalog=FSM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";        //Create a database

        public static void CreateDatabase()
        {
            SqlConnection myConn = new SqlConnection(@"Data Source=DESKTOP-A5NHULB\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            string str = "IF(NOT EXISTS (SELECT 'FSM' FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = 'FSM' OR name = 'FSM'))) Create DATABASE FSM";

            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (Exception e) { }
        }
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(DbConnnectionString);
        }
    }
}
