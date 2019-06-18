using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using DTO;

namespace DAO
{
    public class ExportDAO : AbstractDAO
    {

        public ExportDTO loadExportByID(int id)
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: get information by ID
            string sql = "SELECT * FROM Export WHERE ID=@ID";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbParameter para = command.Parameters.Add("@ID", OleDbType.Integer);
            para.Value = id;
            OleDbDataReader reader = command.ExecuteReader();

            ExportDTO result = new ExportDTO();

            while (reader.Read())
            {
                result.ID = reader.GetInt32(0);
                result.Product = reader.GetInt32(1);
                result.Customer = reader.GetInt32(2);
                result.Amount = reader.GetInt32(3);
                result.DateOutput = reader.GetString(4);
                result.Total = reader.GetInt32(5);
            }
            reader.Close();

            // B3: Close connection
            connection.Close();
            return result;
        }

        // 0: failed
        // 1: success
        public int AddNewExport(ExportDTO export)
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: add to the database
            string sql = "INSERT INTO Export (Product, Customer, Amount, DateOutput, Total) VALUES(@product, @customer, @amount, @dateoutput, @total)";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbParameter para = command.Parameters.Add("@product", OleDbType.Integer);
            para.Value = export.Product;
            para = command.Parameters.Add("@customer", OleDbType.Integer);
            para.Value = export.Customer;
            para = command.Parameters.Add("@amount", OleDbType.Integer);
            para.Value = export.Amount;
            para = command.Parameters.Add("@dateoutput", OleDbType.VarChar);
            para.Value = export.DateOutput;
            para = command.Parameters.Add("@total", OleDbType.Integer);
            para.Value = export.Total;


            int count = command.ExecuteNonQuery();

            if (count < 1)
                count = 0;
            else
                count = 1;

            // Step 3: Close connection
            connection.Close();
            return count;
        }


        public List<ExportDTO> loadAllExport()
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: get information
            string sql = "SELECT * FROM Export";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbDataReader reader = command.ExecuteReader();

            List<ExportDTO> result = new List<ExportDTO>();

            while (reader.Read())
            {
                ExportDTO export = new ExportDTO();
                export.ID = reader.GetInt32(0);
                export.Product = reader.GetInt32(1);
                export.Customer = reader.GetInt32(2);
                export.Amount = reader.GetInt32(3);
                export.DateOutput = reader.GetString(4);
                export.Total = reader.GetInt32(5);
                result.Add(export);
            }
            reader.Close();

            // B3: Close connection
            connection.Close();
            return result;
        }


        public void deleteExportByID(int id)
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: delete match by ID
            string sql = "DELETE FROM Export WHERE ID=@ID";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbParameter para = command.Parameters.Add("@ID", OleDbType.Integer);
            para.Value = id;
            OleDbDataReader reader = command.ExecuteReader();

            reader.Close();

            // B3: Close connection
            connection.Close();
            return;
        }

        public void updateExport(ExportDTO export)
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: update by ID
            string sql = "UPDATE Export SET Product = @product, Customer = @customer, Amount = @amount, DateOutput = @dateoutput, Total = @total WHERE ID=@ID";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbParameter para = command.Parameters.Add("@product", OleDbType.Integer);
            para.Value = export.Product;
            para = command.Parameters.Add("@customer", OleDbType.Integer);
            para.Value = export.Customer;
            para = command.Parameters.Add("@amount", OleDbType.Integer);
            para.Value = export.Amount;
            para = command.Parameters.Add("@dateoutput", OleDbType.VarChar);
            para.Value = export.DateOutput;
            para = command.Parameters.Add("@total", OleDbType.Integer);
            para.Value = export.Total;
            para = command.Parameters.Add("@ID", OleDbType.Integer);
            para.Value = export.ID;

            OleDbDataReader reader = command.ExecuteReader();

            reader.Close();

            //Step 3: Close connection
            connection.Close();
            return;
        }

    }
}
