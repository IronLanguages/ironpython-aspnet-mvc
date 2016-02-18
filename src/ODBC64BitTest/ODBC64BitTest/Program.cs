using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODBC64BitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ODBC 64it Test");

            using (OdbcConnection connection = new OdbcConnection("DSN=Test"))
            {
                connection.Open();

                OdbcCommand cmd = new OdbcCommand("SELECT * FROM ADDR", connection);

                var reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    Console.WriteLine(reader.GetValue(1).ToString());
                }
                

                connection.Close();
            }

            Console.ReadLine();
        }
    }
}
