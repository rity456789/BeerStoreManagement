using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;

namespace BUS
{
    public class ProductBUS
    {

        public ProductDTO LoadOneProduct(int id)
        {
            ProductDAO dao = new ProductDAO();
            ProductDTO result = dao.loadProductByID(id);

            return result;
        }

        public int AddNewProduct(ProductDTO product)
        {
            ProductDAO dao = new ProductDAO();
            return dao.AddNewProduct(product);
        }

        public List<ProductDTO> LoadAllProduct()
        {
            ProductDAO dao = new ProductDAO();
            List<ProductDTO> result = dao.loadAllProduct();

            return result;
        }

        public void DeleteProduct(int id)
        {
            ProductDAO dao = new ProductDAO();
            dao.deleteProductByID(id);
        }

        public void UpdateProduct(ProductDTO product)
        {
            ProductDAO dao = new ProductDAO();
            dao.updateProduct(product);
        }

    }
}
