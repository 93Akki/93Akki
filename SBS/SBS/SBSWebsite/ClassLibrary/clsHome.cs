using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class clsHome
    {
        DataAccessService DAS = new DataAccessService();

        public DataTable GetProductCategory(string Criteria)
        {
            DataTable DT = DAS.SelectDataTable("EXEC SBS_GetProductCategory @Criteria='" + Criteria + "'");
            return DT;
        }

        public DataSet GetProducts(string Criteria, string CallBy)
        {
            DataSet DS = DAS.SelectDataSet("EXEC SBS_GetProducts @Criteria='" + Criteria + "',@CallBy='" + CallBy + "'");
            return DS;
        }

        public DataTable SaveAddToCartDetails(string PID, string quantity)
        {
            DataTable DT = DAS.SelectDataTable("EXEC SBS_SaveAddToCartDetails @PID=" + PID + ",@quantity=" + quantity + "");
            return DT;
        }
    }
}
