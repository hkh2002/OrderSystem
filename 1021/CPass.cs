using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;

namespace _1021
{
    public class CPass
    {
        private String account, uac;
        private String password, upass;
        private SHA256 sha256 = SHA256.Create();
        string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\ireen\\Desktop\\TCU\\大四上\\視窗程式\\1021\\1021\\Database1.mdf;Integrated Security=True";
        SqlConnection conn;
        public CPass(String u, String p)
        {
            uac = u;
            upass = p;
            byte[] b = Encoding.ASCII.GetBytes(upass);

            try
            {
                b = sha256.ComputeHash(b);
                upass = Encoding.ASCII.GetString(b);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        public bool compare()
        {
            try
            {
                using (conn = new SqlConnection(conString))
                {
                    conn.Open();
                    String query = "Select * from [Account] where account = '" + uac + "' and password = '" + upass + "';";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteReader();
                    conn.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return false;
        }
        public bool writePass()
        {
            try
            {
                using (conn = new SqlConnection(conString))
                {
                    conn.Open();

                    String checkQuery = "SELECT COUNT(*) FROM [Account] WHERE account = @uac";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@uac", uac);

                    int count = (int)checkCmd.ExecuteScalar();


                    if (count == 0)
                    {
                        String query = "INSERT INTO [Account] (account, password) VALUES (@uac, @upass)";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@uac", uac);
                        cmd.Parameters.AddWithValue("@upass", upass);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
                    else
                    {
                        conn.Close();
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
