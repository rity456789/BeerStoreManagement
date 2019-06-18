using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using DTO;

namespace DAO
{
    public class ProductDAO : AbstractDAO
    {
        public ProductDTO loadProductByID(int id)
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: get information by ID
            string sql = "SELECT * FROM Product WHERE ID=@ID";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbParameter para = command.Parameters.Add("@ID", OleDbType.Integer);
            para.Value = id;
            OleDbDataReader reader = command.ExecuteReader();

            ProductDTO result = new ProductDTO();

            while (reader.Read())
            {
                result.ID = reader.GetInt32(0);
                result.Name = reader.GetString(1);
                result.IvenNum = reader.GetInt32(2);
                result.InputPrice = reader.GetInt32(3);
                result.OutputPrice = reader.GetInt32(4);
            }
            reader.Close();

            // B3: Close connection
            connection.Close();
            return result;
        }


        // 0: failed
        // 1: success
        public int AddNewProduct(ProductDTO product)
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: add to the database
            string sql = "INSERT INTO Product (Name, IvenNum, InputPrice, OutputPrice) VALUES(@name, @ivennum, @inputprice, @outputprice)";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbParameter para = command.Parameters.Add("@name", OleDbType.VarChar);
            para.Value = product.Name;
            para = command.Parameters.Add("@ivennum", OleDbType.Integer);
            para.Value = product.IvenNum;
            para = command.Parameters.Add("@inputprice", OleDbType.Integer);
            para.Value = product.InputPrice;
            para = command.Parameters.Add("@outputprice", OleDbType.Integer);
            para.Value = product.OutputPrice;

            int count = command.ExecuteNonQuery();

            if (count < 1)
                count = 0;
            else
                count = 1;

            // Step 3: Close connection
            connection.Close();
            return count;
        }

        public List<ProductDTO> loadAllProduct()
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: get information
            string sql = "SELECT * FROM Product";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbDataReader reader = command.ExecuteReader();

            List<ProductDTO> result = new List<ProductDTO>();

            while (reader.Read())
            {
                ProductDTO product = new ProductDTO();
                product.ID = reader.GetInt32(0);
                product.Name = reader.GetString(1);
                product.IvenNum = reader.GetInt32(2);
                product.InputPrice = reader.GetInt32(3);
                product.OutputPrice = reader.GetInt32(4);
                result.Add(product);
            }
            reader.Close();

            // B3: Close connection
            connection.Close();
            return result;
        }


        public void deleteProductByID(int id)
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: delete match by ID
            string sql = "DELETE FROM Product WHERE ID=@ID";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbParameter para = command.Parameters.Add("@ID", OleDbType.Integer);
            para.Value = id;
            OleDbDataReader reader = command.ExecuteReader();

            reader.Close();

            // B3: Close connection
            connection.Close();
            return;
        }

        public void updateProduct(ProductDTO product)
        {
            //Step 1: connect to database
            OleDbConnection connection = Connect();

            //Step 2: update by ID
            string sql = "UPDATE Product SET Name = @name, IvenNum = @ivennum, InputPrice = @inputprice, OutputPrice = @outputprice WHERE ID=@ID";
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbParameter para = command.Parameters.Add("@name", OleDbType.VarChar);
            para.Value = product.Name;
            para = command.Parameters.Add("@ivennum", OleDbType.Integer);
            para.Value = product.IvenNum;
            para = command.Parameters.Add("@inputprice", OleDbType.Integer);
            para.Value = product.InputPrice;
            para = command.Parameters.Add("@outputprice", OleDbType.Integer);
            para.Value = product.OutputPrice;
            para = command.Parameters.Add("@ID", OleDbType.Integer);
            para.Value = product.ID;


            OleDbDataReader reader = command.ExecuteReader();

            reader.Close();

            //Step 3: Close connection
            connection.Close();
            return;
        }
    }
}
