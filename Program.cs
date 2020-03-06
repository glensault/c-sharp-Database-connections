using System;
using System.Data;
using CnClass;
using GetCredentials;


namespace ClassTester
{
    class Program
    {
        static void Main(string[] args)
        {
            GetCreds gc = new GetCreds(@"C:\Users\gsaul\source\repos\CnClass\GetCredentials\Credentials.txt");
            Console.WriteLine("Prop1: " + gc.Uname);

           
            string SQL = "SELECT AddressLine1 FROM Person.Address";
            DataSet ds = new DataSet();
            DBConn db = new DBConn("AdventureWorks2017","GLENWIN10");
            ds = db.GetData(0,SQL);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Console.WriteLine(row["AddressLine1"]);
            }

        }
    }
}
