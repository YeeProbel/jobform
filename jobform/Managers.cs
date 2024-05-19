using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace jobform
{
    internal class Manager
    {
        public string Name { get; set; }
        public string NameCli { get; set; }
        public string Price { get; set; }
        public string Auto { get; set; }
    }
    public class Sale
    {
        public string cManager { get; set; }
        public string cAuto { get; set; }
        public string cPrice { get; set; }
        public string cReward { get; set; }
        
    }
    public class ManagerSales
    {
        public string ManagerName { get; set; }
        public decimal TotalSales { get; set; }
    }
}
