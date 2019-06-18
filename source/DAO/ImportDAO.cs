using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using DTO;

namespace DAO
{
    public class ImportDAO : AbstractDAO
    {

        public ImportDTO loadImportByID(int id)
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: get information by ID
            string sql = "SELECT * FROM Import WHERE ID=@ID";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbParameter para = command.Parameters.Add("@ID", OleDbType.Integer);
            para.Value = id;
            OleDbDataReader reader = command.ExecuteReader();

            ImportDTO result = new ImportDTO();

            while (reader.Read())
            {
                result.ID = reader.GetInt32(0);
                result.Product = reader.GetInt32(1);
                result.Suplier = reader.GetInt32(2);
                result.Amount = reader.GetInt32(3);
                result.DateInput = reader.GetString(4);
                result.Total = reader.GetInt32(5);
            }
            reader.Close();

            // B3: Close connection
            connection.Close();
            return result;
        }

        // 0: failed
        // 1: success
        public int AddNewImport(ImportDTO Import)
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: add to the database
            string sql = "INSERT INTO Import (Product, Suplier, Amount, DateInput, Total) VALUES(@product, @suplier, @amount, @dateinput, @total)";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbParameter para = command.Parameters.Add("@product", OleDbType.Integer);
            para.Value = Import.Product;
            para = command.Parameters.Add("@suplier", OleDbType.Integer);
            para.Value = Import.Suplier;
            para = command.Parameters.Add("@amount", OleDbType.Integer);
            para.Value = Import.Amount;
            para = command.Parameters.Add("@dateinput", OleDbType.VarChar);
            para.Value = Import.DateInput;
            para = command.Parameters.Add("@total", OleDbType.Integer);
            para.Value = Import.Total;


            int count = command.ExecuteNonQuery();

            if (count < 1)
                count = 0;
            else
                count = 1;

            // Step 3: Close connection
            connection.Close();
            return count;
        }


        public List<ImportDTO> loadAllImport()
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: get information
            string sql = "SELECT * FROM Import";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbDataReader reader = command.ExecuteReader();

            List<ImportDTO> result = new List<ImportDTO>();

            while (reader.Read())
            {
                ImportDTO import = new ImportDTO();
                import.ID = reader.GetInt32(0);
                import.Product = reader.GetInt32(1);
                import.Suplier = reader.GetInt32(2);
                import.Amount = reader.GetInt32(3);
                import.DateInput = reader.GetString(4);
                import.Total = reader.GetInt32(5);
                result.Add(import);
            }
            reader.Close();

            // B3: Close connection
            connection.Close();
            return result;
        }


        public void deleteImportByID(int id)
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: delete match by ID
            string sql = "DELETE FROM Import WHERE ID=@ID";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbParameter para = command.Parameters.Add("@ID", OleDbType.Integer);
            para.Value = id;
            OleDbDataReader reader = command.ExecuteReader();

            reader.Close();

            // B3: Close connection
            connection.Close();
            return;
        }

        public void updateImport(ImportDTO import)
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: update by ID
            string sql = "UPDATE Import SET Product = @product, Suplier = @suplier, Amount = @amount, DateInput = @dateinput, Total = @total WHERE ID=@ID";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbParameter para = command.Parameters.Add("@product", OleDbType.Integer);
            para.Value = import.Product;
            para = command.Parameters.Add("@suplier", OleDbType.Integer);
            para.Value = import.Suplier;
            para = command.Parameters.Add("@amount", OleDbType.Integer);
            para.Value = import.Amount;
            para = command.Parameters.Add("@dateinput", OleDbType.VarChar);
            para.Value = import.DateInput;
            para = command.Parameters.Add("@total", OleDbType.Integer);
            para.Value = import.Total;
            para = command.Parameters.Add("@ID", OleDbType.Integer);
            para.Value = import.ID;

            OleDbDataReader reader = command.ExecuteReader();

            reader.Close();

            //Step 3: Close connection
            connection.Close();
            return;
        }

    }
}
