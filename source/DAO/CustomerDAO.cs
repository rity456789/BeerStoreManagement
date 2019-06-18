using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using DTO;

namespace DAO
{
    public class CustomerDAO : AbstractDAO
    {

        public CustomerDTO loadCustomerByID(int id)
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: get information by ID
            string sql = "SELECT * FROM Customer WHERE ID=@ID";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbParameter para = command.Parameters.Add("@ID", OleDbType.Integer);
            para.Value = id;
            OleDbDataReader reader = command.ExecuteReader();

            CustomerDTO result = new CustomerDTO();

            while (reader.Read())
            {
                result.ID = reader.GetInt32(0);
                result.Name = reader.GetString(1);
                result.Email = reader.GetString(2);
                result.Phone = reader.GetString(3);
                result.CoopDay = reader.GetString(4);
                result.Address = reader.GetString(5);
                result.Paid = reader.GetInt32(6);
            }
            reader.Close();

            // B3: Close connection
            connection.Close();
            return result;
        }

        // 0: failed
        // 1: success
        public int AddNewCustomer(CustomerDTO customer)
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: add to the database
            string sql = "INSERT INTO Customer (Name, Email, Phone, CoopDay, Address, Paid) VALUES(@name, @email, @phone, @coopday, @address, @paid)";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbParameter para = command.Parameters.Add("@name", OleDbType.VarChar);
            para.Value = customer.Name;
            para = command.Parameters.Add("@email", OleDbType.VarChar);
            para.Value = customer.Email;
            para = command.Parameters.Add("@phone", OleDbType.VarChar);
            para.Value = customer.Phone;
            para = command.Parameters.Add("@coopday", OleDbType.VarChar);
            para.Value = customer.CoopDay;
            para = command.Parameters.Add("@address", OleDbType.VarChar);
            para.Value = customer.Address;
            para = command.Parameters.Add("@paid", OleDbType.Integer);
            para.Value = customer.Paid;

            int count = command.ExecuteNonQuery();

            if (count < 1)
                count = 0;
            else
                count = 1;

            // Step 3: Close connection
            connection.Close();
            return count;
        }


        public List<CustomerDTO> loadAllCustomer()
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: get information
            string sql = "SELECT * FROM Customer";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbDataReader reader = command.ExecuteReader();

            List<CustomerDTO> result = new List<CustomerDTO>();

            while (reader.Read())
            {
                CustomerDTO customer = new CustomerDTO();
                customer.ID = reader.GetInt32(0);
                customer.Name = reader.GetString(1);
                customer.Email = reader.GetString(2);
                customer.Phone = reader.GetString(3);
                customer.CoopDay = reader.GetString(4);
                customer.Address = reader.GetString(5);
                customer.Paid = reader.GetInt32(6);
                result.Add(customer);
            }
            reader.Close();

            // B3: Close connection
            connection.Close();
            return result;
        }


        public void deleteCustomerByID(int id)
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: delete match by ID
            string sql = "DELETE FROM Customer WHERE ID=@ID";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbParameter para = command.Parameters.Add("@ID", OleDbType.Integer);
            para.Value = id;
            OleDbDataReader reader = command.ExecuteReader();

            reader.Close();

            // B3: Close connection
            connection.Close();
            return;
        }

        public void updateCustomer(CustomerDTO customer)
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: update by ID
            string sql = "UPDATE Customer SET Name = @name, Email = @email, Phone = @phone, CoopDay = @coopday, Address = @address, Paid = @paid WHERE ID=@ID";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbParameter para = command.Parameters.Add("@name", OleDbType.VarChar);
            para.Value = customer.Name;
            para = command.Parameters.Add("@email", OleDbType.VarChar);
            para.Value = customer.Email;
            para = command.Parameters.Add("@phone", OleDbType.VarChar);
            para.Value = customer.Phone;
            para = command.Parameters.Add("@coopday", OleDbType.VarChar);
            para.Value = customer.CoopDay;
            para = command.Parameters.Add("@address", OleDbType.VarChar);
            para.Value = customer.Address;
            para = command.Parameters.Add("@paid", OleDbType.VarChar);
            para.Value = customer.Paid;
            para = command.Parameters.Add("@ID", OleDbType.Integer);
            para.Value = customer.ID;

            OleDbDataReader reader = command.ExecuteReader();

            reader.Close();

            //Step 3: Close connection
            connection.Close();
            return;
        }
    }
}
