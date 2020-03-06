using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System;
using GetCredentials;


namespace CnClass
{
    public enum TrustLevel { 
        Credentials = 0, 
        Integrated = 1 
    }

    public class DBConn
    {
        private string _uname;
        private string _pwd;
        private string _dbname;
        private string _server;

        public string Uname
        {
            get { return _uname; }
            set { _uname = value; }
        }

        public string Pword
        {
            get { return _pwd; }
            set { _pwd = value; }
        }

        public string DBname
        {
            get { return _dbname; }
            set { _dbname = value; }
        }

        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }

        //constructor for explicit passing of parameters
        public DBConn(string uname, string pword, string dbname, string server)
        {
            this.Uname = uname;
            this.Pword = pword;
            this.DBname = dbname;
            this.Server = server;
        }

        //contructor for passing an objects containing the parameters
        public DBConn(GetCreds cr)
        {
            Uname = cr.Uname;
            Pword = cr.Pword;
            DBname = cr.DBname;
            Server = cr.Server;
        }

        // for a trusted connection
        public DBConn(string dbname, string server)
        {
            this.DBname = dbname;
            this.Server = server;
        }

        public DataSet GetData(TrustLevel trustLevel, string SQL)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    if (trustLevel.Equals(TrustLevel.Integrated))
                    { 
                        conn.ConnectionString = "Server=" + Server + "; Database=" + DBname + ";Trusted_Connection=true";
                    }
                        else
                    {
                        conn.ConnectionString = "Server=" + Server + ";Database=" + DBname + ";Trusted_Connection=true";
                    }
                     conn.Open();
                    DataSet ds = new DataSet();
                    ds = DoQuery(SQL, conn);
                    return ds;
                }
            }
            catch(SqlException e)
            {
                Console.WriteLine("DBConn Error: " + e.Message.ToString());
                return null;


            }
        }

        private DataSet DoQuery(string SQL, SqlConnection conn)
        {
            try
            {
                Console.WriteLine("connect state: " + conn.State);

                using (SqlCommand command = new SqlCommand(SQL, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds;
                }

            }
            catch (SqlException  se)
            {
                Console.WriteLine("SQL Exception: " + se.Message.ToString());
                return null;
            }
        }
    }
}
