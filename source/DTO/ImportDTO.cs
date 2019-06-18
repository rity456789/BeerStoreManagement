using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ImportDTO
    {
        public int ID { get; set; }
        public int Product { get; set; }
        public int Suplier { get; set; }
        public int Amount { get; set; }
        public string DateInput { get; set; }
        public int Total { get; set; }
    }
}
