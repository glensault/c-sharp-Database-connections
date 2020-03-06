using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace GetCredentials
{
    public class GetCreds
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
        //constructor
        public GetCreds(string filePath)
        {
            List<string> creds = readCredentials(path: filePath);
            this.Uname = creds[0];
            this.Pword = creds[1];
            this.DBname = creds[2];
            this.Server = creds[3];
            
        }

        public GetCreds()
        { }

        private List<string> readCredentials(string path) 
        {
            try
            {   
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    var list = new List<string>();
                    while ((line = sr.ReadLine()) != null)
                    {
                        list.Add(line);
                    }
                    return list;
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
