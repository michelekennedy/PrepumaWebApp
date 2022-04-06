using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepumaWebApp.App_Data.DAL
{
    public class AccountImport
    {

        public string Account { get; set; }
        public string Customer { get; set; }
        public string Branch { get; set; }
        public int CostCenter { get; set; }
        public string BusinessType { get; set; }
        public string StrategicSRID { get; set; }
        public string LocalSRID { get; set; }
        public DateTime SSMATimeStamp { get; set; }


        
    }
}
