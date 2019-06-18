using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using DTO;

namespace DAO
{
    public class SuplierDAO : AbstractDAO
    {
        public SuplierDTO loadSuplierByID(int id)
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: get information by ID
            string sql = "SELECT * FROM Suplier WHERE ID=@ID";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbParameter para = command.Parameters.Add("@ID", OleDbType.Integer);
            para.Value = id;
            OleDbDataReader reader = command.ExecuteReader();

            SuplierDTO result = new SuplierDTO();

            while (reader.Read())
            {
                result.ID = reader.GetInt32(0);
                result.Name = reader.GetString(1);
                result.Email = reader.GetString(2);
                result.Phone = reader.GetString(3);
                result.Address = reader.GetString(4);
            }
            reader.Close();

            // B3: Close connection
            connection.Close();
            return result;
        }

        // 0: failed
        // 1: success
        public int AddNewSuplier(SuplierDTO suplier)
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: add to the database
            string sql = "INSERT INTO Suplier (Name, Email, Phone, Address) VALUES(@name, @email, @phone, @address)";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbParameter para = command.Parameters.Add("@name", OleDbType.VarChar);
            para.Value = suplier.Name;
            para = command.Parameters.Add("@email", OleDbType.VarChar);
            para.Value = suplier.Email;
            para = command.Parameters.Add("@phone", OleDbType.VarChar);
            para.Value = suplier.Phone;
            para = command.Parameters.Add("@address", OleDbType.VarChar);
            para.Value = suplier.Address;


            int count = command.ExecuteNonQuery();

            if (count < 1)
                count = 0;
            else
                count = 1;

            // Step 3: Close connection
            connection.Close();
            return count;
        }


        public List<SuplierDTO> loadAllSuplier()
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: get information
            string sql = "SELECT * FROM Suplier";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbDataReader reader = command.ExecuteReader();

            List<SuplierDTO> result = new List<SuplierDTO>();

            while (reader.Read())
            {
                SuplierDTO suplier = new SuplierDTO();
                suplier.ID = reader.GetInt32(0);
                suplier.Name = reader.GetString(1);
                suplier.Email = reader.GetString(2);
                suplier.Phone = reader.GetString(3);
                suplier.Address = reader.GetString(4);
                result.Add(suplier);
            }
            reader.Close();

            // B3: Close connection
            connection.Close();
            return result;
        }


        public void deleteSuplierByID(int id)
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: delete match by ID
            string sql = "DELETE FROM Suplier WHERE ID=@ID";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbParameter para = command.Parameters.Add("@ID", OleDbType.Integer);
            para.Value = id;
            OleDbDataReader reader = command.ExecuteReader();

            reader.Close();

            // B3: Close connection
            connection.Close();
            return;
        }

        public void updateSuplier(SuplierDTO suplier)
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: update by ID
            string sql = "UPDATE Suplier SET Name = @name, Email = @email, Phone = @phone, Address = @address WHERE ID=@ID";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbParameter para = command.Parameters.Add("@name", OleDbType.VarChar);
            para.Value = suplier.Name;
            para = command.Parameters.Add("@email", OleDbType.VarChar);
            para.Value = suplier.Email;
            para = command.Parameters.Add("@phone", OleDbType.VarChar);
            para.Value = suplier.Phone;
            para = command.Parameters.Add("@address", OleDbType.VarChar);
            para.Value = suplier.Address;
            para = command.Parameters.Add("@ID", OleDbType.Integer);
            para.Value = suplier.ID;

            OleDbDataReader reader = command.ExecuteReader();

            reader.Close();

            //Step 3: Close connection
            connection.Close();
            return;
        }
    }
}
