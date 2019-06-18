using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;


namespace BUS
{
    public class ExportBUS
    {

        public ExportDTO LoadOneExport(int id)
        {
            ExportDAO dao = new ExportDAO();
            ExportDTO result = dao.loadExportByID(id);

            return result;
        }

        public int AddNewExport(ExportDTO export)
        {
            ExportDAO dao = new ExportDAO();
            return dao.AddNewExport(export);
        }

        public List<ExportDTO> LoadAllExport()
        {
            ExportDAO dao = new ExportDAO();
            List<ExportDTO> result = dao.loadAllExport();

            return result;
        }

        public void DeleteExport(int id)
        {
            ExportDAO dao = new ExportDAO();
            dao.deleteExportByID(id);
        }

        public void UpdateExport(ExportDTO export)
        {
            ExportDAO dao = new ExportDAO();
            dao.updateExport(export);
        }

    }
}
