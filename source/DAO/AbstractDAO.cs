using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace DAO
{
    public class AbstractDAO
    {
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=BeerStore.accdb";
        public OleDbConnection Connect()
        {

            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
