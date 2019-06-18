using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;

namespace BUS
{
    public class SuplierBUS
    {

        public SuplierDTO LoadOneSuplier(int id)
        {
            SuplierDAO dao = new SuplierDAO();
            SuplierDTO result = dao.loadSuplierByID(id);

            return result;
        }

        public int AddNewSuplier(SuplierDTO suplier)
        {
            SuplierDAO dao = new SuplierDAO();
            return dao.AddNewSuplier(suplier);
        }

        public List<SuplierDTO> LoadAllSuplier()
        {
            SuplierDAO dao = new SuplierDAO();
            List<SuplierDTO> result = dao.loadAllSuplier();

            return result;
        }

        public void DeleteSuplier(int id)
        {
            SuplierDAO dao = new SuplierDAO();
            dao.deleteSuplierByID(id);
        }

        public void UpdateSuplier(SuplierDTO suplier)
        {
            SuplierDAO dao = new SuplierDAO();
            dao.updateSuplier(suplier);
        }

    }
}
