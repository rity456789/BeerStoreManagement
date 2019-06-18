using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;

namespace BUS
{
    public class CustomerBUS
    {
        public CustomerDTO LoadOneCustomer(int id)
        {
            CustomerDAO dao = new CustomerDAO();
            CustomerDTO result = dao.loadCustomerByID(id);

            return result;
        }

        public int AddNewCustomer(CustomerDTO customer)
        {
            CustomerDAO dao = new CustomerDAO();
            return dao.AddNewCustomer(customer);
        }

        public List<CustomerDTO> LoadAllCustomer()
        {
            CustomerDAO dao = new CustomerDAO();
            List<CustomerDTO> result = dao.loadAllCustomer();

            return result;
        }

        public void DeleteCustomer(int id)
        {
            CustomerDAO dao = new CustomerDAO();
            dao.deleteCustomerByID(id);
        }

        public void UpdateCustomer(CustomerDTO customer)
        {
            CustomerDAO dao = new CustomerDAO();
            dao.updateCustomer(customer);
        }
    }
}
