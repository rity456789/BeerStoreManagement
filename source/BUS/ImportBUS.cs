using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;

namespace BUS
{
    public class ImportBUS
    {

        public ImportDTO LoadOneImport(int id)
        {
            ImportDAO dao = new ImportDAO();
            ImportDTO result = dao.loadImportByID(id);

            return result;
        }

        public int AddNewImport(ImportDTO import)
        {
            ImportDAO dao = new ImportDAO();
            return dao.AddNewImport(import);
        }

        public List<ImportDTO> LoadAllImport()
        {
            ImportDAO dao = new ImportDAO();
            List<ImportDTO> result = dao.loadAllImport();

            return result;
        }

        public void DeleteImport(int id)
        {
            ImportDAO dao = new ImportDAO();
            dao.deleteImportByID(id);
        }

        public void UpdateImport(ImportDTO import)
        {
            ImportDAO dao = new ImportDAO();
            dao.updateImport(import);
        }

    }
}
